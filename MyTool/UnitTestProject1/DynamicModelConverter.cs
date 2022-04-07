/**************************************************************** 
 * 作    者：Livia.Liu 
 * CLR 版本：4.0.30319.42000 
 * 创建时间：2018/6/22 19:55:06 
***************************************************************** 
 * Copyright @ Livia.Liu  2018 All rights reserved 
*****************************************************************/

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GaiaWorks.HumanResource.Utilities
{
    public class DynamicPropertyStrategy<TModel>
    {
        public Action<TModel, object> Setter { get; set; }

        public Func<TModel, object> Getter { get; set; }

        public Type PropertyType { get; set; }

        public Type ConverterType { get; set; }
    }

    public class PropertyValueComparer : IEqualityComparer<object>
    {
        private readonly IEqualityComparer _comparer;

        public PropertyValueComparer(IEqualityComparer comparer)
        {
            _comparer = comparer;
        }

        public new bool Equals(object x, object y)
        {
            return _comparer.Equals(x, y);
        }

        public int GetHashCode(object obj)
        {
            return _comparer.GetHashCode(obj);
        }
    }

    public static class DynamicModelConverterHelper
    {
        private static readonly IEqualityComparer<object> _propertyValueComparer = new PropertyValueComparer(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel">Impl T</typeparam>
        /// <param name="childTypes">Imp C</param>
        /// <param name="allowMultipleProperties"></param>
        /// <param name="ignoreProperties"></param>
        /// <returns></returns>
        public static (ConcurrentDictionary<string, List<DynamicPropertyStrategy<TModel>>> intrinsicProperties,
                                                 Func<TModel, IDictionary<string, object>> dynamicProperties)
            CompileProperties<TModel>(Type[] childTypes, string[] allowMultipleProperties = null, string[] ignoreProperties = null)
        {
            var intrinsicProperties = new ConcurrentDictionary<string, List<DynamicPropertyStrategy<TModel>>>(StringComparer.OrdinalIgnoreCase);
            Func<TModel, IDictionary<string, object>> dynamicProperties = null;
            var childProperties = new List<PropertyInfo>();
            var staticProperties = new List<PropertyInfo>();
            PropertyInfo extendedPropertyInfo = null;
            foreach (var property in typeof(TModel).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (childTypes.Contains(property.PropertyType))
                {
                    childProperties.Add(property);
                }
                else if (property.PropertyType == typeof(IDictionary<string, object>))
                {
                    extendedPropertyInfo = property;
                }
                else if (ignoreProperties == null || !ignoreProperties.Contains(property.Name))
                {
                    staticProperties.Add(property);
                    // 编译顶层普通属性的解析策略
                    var entityParameter = Expression.Parameter(typeof(TModel));
                    var propertyParameter = Expression.Parameter(typeof(object));
                    var propertyExpr = Expression.Property(entityParameter, property);
                    var assignExpr = Expression.Assign(propertyExpr,
                        Expression.Convert(propertyParameter, property.PropertyType));

                    intrinsicProperties.GetOrAdd(property.Name,
                            name => new List<DynamicPropertyStrategy<TModel>>())
                        .Add(new DynamicPropertyStrategy<TModel>
                        {
                            PropertyType = property.PropertyType,
                            Getter = Expression
                                .Lambda<Func<TModel, object>>(Expression.Convert(propertyExpr, typeof(object)),
                                    entityParameter).Compile(),
                            Setter = Expression
                                .Lambda<Action<TModel, object>>(assignExpr, entityParameter, propertyParameter)
                                .Compile(),
                            ConverterType = property.GetCustomAttribute<JsonConverterAttribute>()?.ConverterType
                        });
                }
            }

            var properties = childProperties.SelectMany(childProperty => childProperty.PropertyType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => ignoreProperties == null || !ignoreProperties.Contains(p.Name))
                .Select(p => new
                {
                    ParentProperty = childProperty,
                    ChildProperty = p
                })).ToArray();

            // 验证重复的属性，以免在运行时无法确定策略
            var ambiguousProperties = properties.Union(staticProperties.Select(p => new
            {
                ParentProperty = (PropertyInfo)null,
                ChildProperty = p
            })).GroupBy(x => x.ChildProperty.Name)
                .Where(x => x.Count() > 1 &&
                            !(allowMultipleProperties == null || allowMultipleProperties.Contains(x.Key)))
                .Select(x => x.Key).ToArray();
            if (ambiguousProperties.Any())
            {
                throw new InvalidOperationException(
                    $"There are ambiguous properties for json deserialization: {string.Join(", ", ambiguousProperties)}.");
            }

            // 编译特殊实体内的属性，并将其展开到顶层。
            foreach (var property in properties)
            {
                var tParameterExpr = Expression.Parameter(typeof(TModel));
                var cParameterExpr = Expression.Parameter(typeof(object));
                // set ChildProperty for T
                var childMemberExpr = Expression.Property(tParameterExpr, property.ParentProperty); 
                // set property for Child
                var cMemberExpr = Expression.Property(childMemberExpr, property.ChildProperty);     
                // if c == null then c = new Child()
                var instanceTExpr = Expression.IfThen(Expression.Equal(childMemberExpr, Expression.Constant(null)),
                    Expression.Assign(childMemberExpr, Expression.New(property.ParentProperty.PropertyType)));
                // cMember = cParameterExpr as property.ChildProperty.PropertyType
                var InstanceCExpr = Expression.Assign(cMemberExpr,
                        Expression.Convert(cParameterExpr, property.ChildProperty.PropertyType));

                var variableExpr = Expression.Variable(typeof(object));
                var fetchValueExpr = Expression.IfThen(
                    Expression.NotEqual(childMemberExpr, Expression.Constant(null)),
                    Expression.Assign(variableExpr,
                        Expression.Convert(cMemberExpr, typeof(object))));


                var dynamicPropertyStrategies = intrinsicProperties.GetOrAdd(property.ChildProperty.Name, name => new List<DynamicPropertyStrategy<TModel>>());
                dynamicPropertyStrategies.Add(new DynamicPropertyStrategy<TModel>
                {
                    PropertyType = property.ChildProperty.PropertyType,
                    Getter = Expression.Lambda<Func<TModel, object>>(
                            Expression.Block(new[] { variableExpr }, fetchValueExpr, variableExpr),
                            tParameterExpr)
                        .Compile(),
                    Setter = Expression.Lambda<Action<TModel, object>>(
                            Expression.Block(instanceTExpr, InstanceCExpr), tParameterExpr, cParameterExpr)
                        .Compile(),
                    ConverterType = property.ChildProperty.GetCustomAttribute<JsonConverterAttribute>()?.ConverterType
                });
            }

            // 编译动态属性
            if (extendedPropertyInfo != null)
            {
                var tParameterExpr = Expression.Parameter(typeof(TModel));
                var personInfoProperty = Expression.Property(tParameterExpr, extendedPropertyInfo);
                var instanceExpr = Expression.IfThen(Expression.Equal(personInfoProperty, Expression.Constant(null)),
                    Expression.Assign(personInfoProperty, Expression.New(typeof(Dictionary<string, object>))));
                dynamicProperties = Expression.Lambda<Func<TModel, IDictionary<string, object>>>(
                                        Expression.Block(instanceExpr, personInfoProperty), tParameterExpr)
                                    .Compile();
            }

            return (intrinsicProperties, dynamicProperties);
        }

        public static TModel ReadJson<TModel>(JsonReader reader, JsonSerializer serializer,
            IDictionary<string, List<DynamicPropertyStrategy<TModel>>> intrinsicProperties,
            Func<TModel, IDictionary<string, object>> dynamicProperties)
            where TModel : class, new()
        {
            var entity = new TModel();
            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.PropertyName:
                        var propertyName = reader.Value.ToString();

                        if (!reader.Read())
                        {
                            throw new JsonSerializationException($"Unexpected end when reading {typeof(TModel).Name}.");
                        }

                        if (intrinsicProperties.TryGetValue(propertyName, out var strategies))
                        {
                            foreach (var strategy in strategies)
                            {
                                object propertyValue;
                                if (strategy.ConverterType != null)
                                {
                                    propertyValue =
                                        ((JsonConverter)Activator.CreateInstance(strategy.ConverterType)).ReadJson(
                                            reader, strategy.PropertyType, null, serializer);
                                }
                                else
                                {
                                    propertyValue = serializer.Deserialize(reader, strategy.PropertyType);
                                }
                                strategy.Setter(entity, propertyValue);
                            }
                        }
                        else if (dynamicProperties != null)
                        {
                            dynamicProperties(entity)[propertyName] = serializer.Deserialize(reader);
                        }

                        break;
                    case JsonToken.Comment:
                        break;
                    case JsonToken.EndObject:
                        return entity;
                }
            }

            throw new JsonSerializationException($"Unexpected end when reading {typeof(TModel).Name}.");
        }

        private static IDictionary<string, object> Flatten<TModel>(TModel value,
            IDictionary<string, List<DynamicPropertyStrategy<TModel>>> intrinsicProperties,
            Func<TModel, IDictionary<string, object>> dynamicProperties)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var property in intrinsicProperties)
            {
                var propertyValues = new List<object>();
                foreach (var strategy in property.Value)
                {
                    var propertyValue = strategy.Getter(value);
                    if (!ReferenceEquals(propertyValue, null))
                    {
                        propertyValues.Add(propertyValue);
                    }
                }

                propertyValues = propertyValues.Distinct(_propertyValueComparer).ToList();
                if (propertyValues.Count > 1)
                {
                    throw new InvalidOperationException($"There are multiple different values for the property '{property.Key}'.");
                }

                if (propertyValues.Count == 1)
                {
                    var key = char.ToLower(property.Key[0]) + property.Key.Substring(1, property.Key.Length - 1);
                    dictionary.Add(key, propertyValues[0]);
                }
            }

            var dictionaryDynamicProperties = dynamicProperties?.Invoke(value);
            if (dictionaryDynamicProperties != null)
            {
                foreach (var dynamicProperty in dictionaryDynamicProperties)
                {
                    var key = char.ToLower(dynamicProperty.Key[0]) + dynamicProperty.Key.Substring(1, dynamicProperty.Key.Length - 1);
                    if (dictionary.TryGetValue(key, out var dynamicPropertyValue) &&
                        !Equals(dynamicPropertyValue, dynamicProperty.Value))
                    {
                        throw new InvalidOperationException($"There are multiple different values for the property '{dynamicProperty.Key}'.");
                    }

                    dictionary[key] = dynamicProperty.Value;
                }
            }

            return dictionary;
        }

        public static void WriteJson<TModel>(JsonWriter writer, TModel value, JsonSerializer serializer,
            IDictionary<string, List<DynamicPropertyStrategy<TModel>>> intrinsicProperties,
            Func<TModel, IDictionary<string, object>> dynamicProperties)
        {
            if (ReferenceEquals(value, null)) return;
            var dictionary = Flatten(value, intrinsicProperties, dynamicProperties);
            var dictionaryContract = (JsonDictionaryContract)serializer.ContractResolver.ResolveContract(dictionary.GetType());
            writer.WriteStartObject();
            foreach (var property in dictionary)
            {
                var propertyName = dictionaryContract.DictionaryKeyResolver != null
                    ? dictionaryContract.DictionaryKeyResolver(property.Key)
                    : property.Key;
                writer.WritePropertyName(propertyName, true);
                if (intrinsicProperties.TryGetValue(property.Key, out var strategies))
                {
                    var converters = (from strategy in strategies
                                      where strategy.ConverterType != null
                                      select strategy.ConverterType).Distinct().ToArray();
                    if (converters.Length > 1)
                    {
                        throw new InvalidOperationException(
                            $"There are multiple converters for the property '{property.Key}'.");
                    }

                    if (converters.Length == 1)
                    {
                        ((JsonConverter)Activator.CreateInstance(converters[0])).WriteJson(writer, property.Value, serializer);
                        continue;
                    }
                }

                serializer.Serialize(writer, property.Value);
            }
            writer.WriteEndObject();
        }
    }

    public class DynamicModelConverter<TModel, TChild> : JsonConverter<TModel>
        where TModel : class, new()
        where TChild : class, new()
    {
        private static readonly IDictionary<string, List<DynamicPropertyStrategy<TModel>>>
            _intrinsicProperties;

        private static readonly Func<TModel, IDictionary<string, object>> _dynamicProperties;

        static DynamicModelConverter()
        {
            (_intrinsicProperties, _dynamicProperties) =
                DynamicModelConverterHelper.CompileProperties<TModel>(new[] { typeof(TChild) });
        }

        public override void WriteJson(JsonWriter writer, TModel value, JsonSerializer serializer)
        {
            DynamicModelConverterHelper.WriteJson(writer, value, serializer, _intrinsicProperties, _dynamicProperties);
        }

        public override TModel ReadJson(JsonReader reader, Type objectType, TModel existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            return DynamicModelConverterHelper.ReadJson(reader, serializer, _intrinsicProperties, _dynamicProperties);
        }
    }

    public class DynamicModelConverter<TModel, TChild1, TChild2> : JsonConverter<TModel>
        where TModel : class, new()
        where TChild1 : class, new()
        where TChild2 : class, new()
    {
        private static readonly IDictionary<string, List<DynamicPropertyStrategy<TModel>>>
            _intrinsicProperties;

        private static readonly Func<TModel, IDictionary<string, object>> _dynamicProperties;

        static DynamicModelConverter()
        {
            (_intrinsicProperties, _dynamicProperties) =
                DynamicModelConverterHelper.CompileProperties<TModel>(new[] { typeof(TChild1), typeof(TChild2) });
        }

        public override void WriteJson(JsonWriter writer, TModel value, JsonSerializer serializer)
        {
            DynamicModelConverterHelper.WriteJson(writer, value, serializer, _intrinsicProperties, _dynamicProperties);
        }

        public override TModel ReadJson(JsonReader reader, Type objectType, TModel existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            return DynamicModelConverterHelper.ReadJson(reader, serializer, _intrinsicProperties, _dynamicProperties);
        }
    }

    public class DynamicModelConverter<TModel, TChild1, TChild2, TChild3> : JsonConverter<TModel>
    where TModel : class, new()
    where TChild1 : class, new()
    where TChild2 : class, new()
    {
        private static readonly IDictionary<string, List<DynamicPropertyStrategy<TModel>>>
            _intrinsicProperties;

        private static readonly Func<TModel, IDictionary<string, object>> _dynamicProperties;

        static DynamicModelConverter()
        {
            (_intrinsicProperties, _dynamicProperties) =
                DynamicModelConverterHelper.CompileProperties<TModel>(new[] { typeof(TChild1), typeof(TChild2), typeof(TChild3) });
        }

        public override void WriteJson(JsonWriter writer, TModel value, JsonSerializer serializer)
        {
            DynamicModelConverterHelper.WriteJson(writer, value, serializer, _intrinsicProperties, _dynamicProperties);
        }

        public override TModel ReadJson(JsonReader reader, Type objectType, TModel existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            return DynamicModelConverterHelper.ReadJson(reader, serializer, _intrinsicProperties, _dynamicProperties);
        }
    }

    public class DynamicModelConverter<TModel, TChild1, TChild2, TChild3, TChild4> : JsonConverter<TModel>
        where TModel : class, new()
        where TChild1 : class, new()
        where TChild2 : class, new()
        where TChild4 : class, new()
    {
        private static readonly IDictionary<string, List<DynamicPropertyStrategy<TModel>>>
            _intrinsicProperties;

        private static readonly Func<TModel, IDictionary<string, object>> _dynamicProperties;

        static DynamicModelConverter()
        {
            (_intrinsicProperties, _dynamicProperties) =
                DynamicModelConverterHelper.CompileProperties<TModel>(new[] { typeof(TChild1), typeof(TChild2), typeof(TChild3), typeof(TChild4) });
        }

        public override void WriteJson(JsonWriter writer, TModel value, JsonSerializer serializer)
        {
            DynamicModelConverterHelper.WriteJson(writer, value, serializer, _intrinsicProperties, _dynamicProperties);
        }

        public override TModel ReadJson(JsonReader reader, Type objectType, TModel existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            return DynamicModelConverterHelper.ReadJson(reader, serializer, _intrinsicProperties, _dynamicProperties);
        }
    }
}
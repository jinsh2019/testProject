using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GaiaWorks.HumanResource.Utilities
{
    /// <summary>
    /// Defines interface for base entity type. All entities in the system must implement this interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
    public interface IAuditable
    {
        string CreateUser { get; set; }

        DateTime CreateTime { get; set; }

        string LastUpdateUser { get; set; }

        DateTime LastUpdateTime { get; set; }
    }

    public static class AuditableExtensions
    {
        public static void AssignCreateAudit(this IAuditable entity, string userName)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.CreateTime = DateTime.Now;
            entity.CreateUser = string.IsNullOrEmpty(userName) ? "admin" : userName;
            AssignUpdateAudit(entity, userName);
        }

        public static void AssignUpdateAudit(this IAuditable entity, string userName)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.LastUpdateTime = DateTime.Now;
            entity.LastUpdateUser = string.IsNullOrEmpty(userName) ? "admin" : userName;
        }
    }
    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, Expression> _map;
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterRebinder"/> class.
        /// </summary>
        /// <param name="map">The map.</param>
        private ParameterRebinder(Dictionary<ParameterExpression, Expression> map)
        {
            _map = map ?? new Dictionary<ParameterExpression, Expression>();
        }
        /// <summary>
        /// Replaces the parameters.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <param name="exp">The exp.</param>
        /// <returns>Expression</returns>
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, Expression> map, Expression exp)
        {
            if (exp == null) return null;
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_map.TryGetValue(node, out var replacement))
            {
                return Visit(replacement);
            }
            return base.VisitParameter(node);
        }
    }

    public interface IHistorical
    {
        DateTime EffectDate { get; set; }

        DateTime? ExpireDate { get; set; }
    }

    public interface ISequenceHistorical : IHistorical
    {
        int Sequence { get; set; }
    }

    public static class HistoricalExtensions
    {
        private class EntityVersion
        {
            public Guid? Id { get; set; }

            public DateTime? EffectDate { get; set; }

            public int? Sequence { get; set; }
        }

        private class StringEntityVersion
        {
            public string Id { get; set; }

            public DateTime? EffectDate { get; set; }

            public int Sequence { get; set; }
        }

        private class EntityWithMaxVersion<T>
        {
            public T Entity { get; set; }

            public EntityVersion MaxVersion { get; set; }
        }

        private class EntityWithMaxStringVersion<T>
        {
            public T Entity { get; set; }

            public StringEntityVersion MaxVersion { get; set; }
        }

        private class EntityWithVersion<T>
        {
            public T Entity { get; set; }

            public EntityVersion MaxVersion { get; set; }

            public EntityVersion MinVersion { get; set; }
        }

        private class EntityWithStringVersion<T>
        {
            public T Entity { get; set; }

            public StringEntityVersion MaxVersion { get; set; }

            public StringEntityVersion MinVersion { get; set; }
        }

        private static readonly PropertyInfo _propertyEntityId = typeof(EntityVersion).GetProperty("Id");
        private static readonly PropertyInfo _propertyEffectDate = typeof(EntityVersion).GetProperty("EffectDate");
        private static readonly PropertyInfo _propertySequence = typeof(EntityVersion).GetProperty("Sequence");
        private static readonly PropertyInfo _propertyStringEntityId = typeof(StringEntityVersion).GetProperty("Id");
        private static readonly PropertyInfo _propertyStringEntityEffectDate = typeof(StringEntityVersion).GetProperty("EffectDate");
        private static readonly PropertyInfo _propertyStringEntitySequence = typeof(StringEntityVersion).GetProperty("Sequence");
        /// <summary>
        /// 处理时间轴问题(包含重复日期)
        /// </summary>
        /// <param name="historyPeriods"></param>
        /// <param name="userName"></param>
        public static void CorrectHistory<T>(this IEnumerable<T> historyPeriods, string userName)
        where T : class, IHistorical
        {
            if (historyPeriods == null)
            {
                throw new ArgumentNullException(nameof(historyPeriods));
            }
            var historyList = historyPeriods.OrderBy(x => x.EffectDate).ToList();
            var effectDates = historyList.Select(x => x.EffectDate).Distinct().ToList();

            DateTime? nextEffectDate = null;
            var dics = new Dictionary<DateTime, DateTime?>();
            foreach (var effectDate in effectDates)
            {
                var currentEffectDate = nextEffectDate;
                nextEffectDate = effectDate;
                if (!currentEffectDate.HasValue)
                {
                    continue;
                }

                var expireDate = nextEffectDate.Value.AddDays(-1);
                if (dics.ContainsKey(currentEffectDate.Value))
                {
                    dics[currentEffectDate.Value] = expireDate;
                }
                else
                {
                    dics.Add(currentEffectDate.Value, expireDate);
                }
            }

            //设置最后一根版本为开区间
            if (nextEffectDate != null)
            {
                // DateTime.Parse(System.Data.SqlTypes.SqlDateTime.MaxValue.ToString()) 
                //unix 系统时间生成是两位数的年的时间格式，比如：12/31/99 11:59:59 PM
                dics[nextEffectDate.Value] = null;
            }

            foreach (var historyPeriod in historyList)
            {
                if (!dics.ContainsKey(historyPeriod.EffectDate))
                {
                    continue;
                }
                //时间区间不发生变动的，不需要重新赋值
                if (historyPeriod.ExpireDate == dics[historyPeriod.EffectDate])
                {
                    continue;
                }
                historyPeriod.ExpireDate = dics[historyPeriod.EffectDate];

                (historyPeriod as IAuditable)?.AssignUpdateAudit(userName);
            }
        }

        /// <summary>
        /// 验证生效日期，在上一生效日期与下一生效日期之间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="historyPeriods"></param>
        /// <param name="oldEffectDate"></param>
        /// <param name="newEffectDate"></param>
        /// <returns></returns>
        public static async Task<bool> VerifyEffectDate<T>(this IQueryable<T> historyPeriods, DateTime oldEffectDate, DateTime newEffectDate)
            where T : class, IHistorical
        {
            var effectDates = await historyPeriods.Select(x => (DateTime?)x.EffectDate).Distinct().ToListAsync();
            return newEffectDate < (effectDates.Where(x => x > oldEffectDate).OrderBy(x => x).FirstOrDefault() ?? DateTime.MaxValue) &&
                    newEffectDate > (effectDates.Where(x => x < oldEffectDate).OrderByDescending(x => x).FirstOrDefault() ?? DateTime.MinValue);
        }
        /// <summary>
        /// 根据日期获取最近生效的版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="versionDate"></param>
        /// <returns></returns>
        public static IQueryable<T> GetNearestVersions<T>(this IQueryable<T> query, DateTime versionDate)
            where T : IHistorical, IEntity<Guid>
        {
            return query.GetNearestVersions(versionDate, x => x.Id, x => x.EffectDate,
                typeof(ISequenceHistorical).IsAssignableFrom(typeof(T))
                    ? x => ((ISequenceHistorical)x).Sequence
                    : (Expression<Func<T, int>>)null);
        }

        /// <summary>
        /// 查询与指定日期最接近的版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="versionDate"></param>
        /// <param name="identity"></param>
        /// <param name="effectDate"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static IQueryable<T> GetNearestVersions<T>(this IQueryable<T> query, DateTime versionDate,
            Expression<Func<T, Guid>> identity, Expression<Func<T, DateTime?>> effectDate, Expression<Func<T, int>> sequence = null)
        {
            var entityParameter = Expression.Parameter(typeof(T));
            var identityExpression = Expression.Convert(ReplaceParameter(identity, entityParameter), typeof(Guid?));
            // 将未知类型的实体映射为特定类型的实体，简化后续使用linq表达式的方式。
            var entityVersions = GetEntityVersions(query,
                identityExpression, ReplaceParameter(effectDate, entityParameter),
                sequence == null ? null : Expression.Convert(ReplaceParameter(sequence, entityParameter), typeof(int?)),
                entityParameter);

            // 已经消费的版本：生效日期小于等于版本日期
            var queryConsumedVersions = entityVersions.Where(x => x.EffectDate <= versionDate);

            //生效日期小于版本日期的列表中每个实体的最高版本
            var queryMaxVersion = GetLatestVersion(queryConsumedVersions, sequence != null);

            // 生效日期大于版本日期中每个实体的最低版本
            var queryMinVersion = GetEarliestVersion(entityVersions.Where(version =>
                    version.EffectDate > versionDate && !queryConsumedVersions.Select(x => x.Id).Contains(version.Id)),
                sequence != null);

            // 第一次左连接：连接存在已消费版本的实体列表。
            var firstJoin = query.GroupJoin(queryMaxVersion,
                    Expression.Lambda<Func<T, Guid?>>(Expression.Convert(identityExpression, typeof(Guid?)), entityParameter),
                    x => x.Id, (entity, versions) => new { Entity = entity, MaxVersions = versions })
                .SelectMany(x => x.MaxVersions.DefaultIfEmpty(), (entity, version) =>
                    new EntityWithMaxVersion<T>
                    {
                        Entity = entity.Entity,
                        MaxVersion = version
                    });

            // 第二次左连接：连接存在未消费版本的实体列表
            var parameterChaindEntity = Expression.Parameter(typeof(EntityWithMaxVersion<T>));
            var entityIdentitySelector = Expression.Lambda<Func<EntityWithMaxVersion<T>, Guid?>>(
                Expression.Convert(ReplaceParameter(identity, Expression.Property(parameterChaindEntity, "Entity")), typeof(Guid?)),
                parameterChaindEntity);
            var lastJoin = firstJoin.GroupJoin(queryMinVersion, entityIdentitySelector, x => x.Id, (entity, versions) =>
                    new { entity.Entity, entity.MaxVersion, MinVersions = versions })
                .SelectMany(x => x.MinVersions.DefaultIfEmpty(), (entity, version) =>
                    new EntityWithVersion<T>
                    {
                        Entity = entity.Entity,
                        MaxVersion = entity.MaxVersion,
                        MinVersion = version
                    });

            // 对连接进行条件过滤
            Expression<Func<EntityWithVersion<T>, bool>> filterExpression;
            var filterParameter = Expression.Parameter(typeof(EntityWithVersion<T>));
            var entityExpression = Expression.Property(filterParameter, "Entity");
            var entityEffectDateExpression = ReplaceParameter(effectDate, entityExpression);
            var maxVersionExpression = Expression.Property(filterParameter, "MaxVersion");
            var minVersionExpression = Expression.Property(filterParameter, "MinVersion");
            var maxDateCompare = Expression.Equal(entityEffectDateExpression, Expression.Property(maxVersionExpression, "EffectDate"));
            var minDateCompare = Expression.Equal(entityEffectDateExpression, Expression.Property(minVersionExpression, "EffectDate"));
            if (sequence != null)
            {
                // (u.EffectDate == u1.EffectDate && u.Sequence.u1.Sequence) || (u.EffectDate == u2.EffectDate && u.Sequence == u2.Sequence)
                var entitySequenceExpression = Expression.Convert(ReplaceParameter(sequence, entityExpression), typeof(int?));
                filterExpression = Expression.Lambda<Func<EntityWithVersion<T>, bool>>(
                    Expression.Or(
                        Expression.And(maxDateCompare, Expression.Equal(entitySequenceExpression, Expression.Property(maxVersionExpression, "Sequence"))),
                        Expression.And(minDateCompare, Expression.Equal(entitySequenceExpression, Expression.Property(minVersionExpression, "Sequence")))),
                    filterParameter);
            }
            else
            {
                // u.EffectDate == u1.EffectDate || u.EffectDate == u2.EffectDate
                filterExpression = Expression.Lambda<Func<EntityWithVersion<T>, bool>>(
                    Expression.Or(maxDateCompare, minDateCompare), filterParameter);
            }

            return lastJoin.Where(filterExpression).Select(x => x.Entity);
        }

        private static IQueryable<EntityVersion> GetLatestVersion(IQueryable<EntityVersion> query, bool includeSequence)
        {
            if (!includeSequence)
            {
                return from version in query
                       group version.EffectDate by version.Id
                    into g
                       select new EntityVersion
                       {
                           Id = g.Key,
                           EffectDate = g.Max()
                       };
            }

            return from version in query
                   group version by version.Id
                into g
                   let maxEffectDate = g.Max(x => x.EffectDate)
                   select new EntityVersion
                   {
                       Id = g.Key,
                       EffectDate = maxEffectDate,
                       Sequence = g.Where(x => x.EffectDate == maxEffectDate).Max(x => x.Sequence)
                   };
        }

        private static IQueryable<EntityVersion> GetEarliestVersion(IQueryable<EntityVersion> query, bool includeSequence)
        {
            if (!includeSequence)
            {
                return from version in query
                       group version.EffectDate by version.Id
                    into g
                       select new EntityVersion
                       {
                           Id = g.Key,
                           EffectDate = g.Min()
                       };
            }

            return from version in query
                   group version by version.Id
                into g
                   let minEffectDate = g.Min(x => x.EffectDate)
                   select new EntityVersion
                   {
                       Id = g.Key,
                       EffectDate = minEffectDate,
                       Sequence = g.Where(x => x.EffectDate == minEffectDate).Min(x => x.Sequence)
                   };
        }

        private static Expression ReplaceParameter<TSource, TResult>(Expression<Func<TSource, TResult>> lambda, Expression replacement)
        {
            if (lambda == null)
            {
                return null;
            }

            if (replacement == null)
            {
                return lambda;
            }

            return ParameterRebinder.ReplaceParameters(lambda.Parameters.ToDictionary(
                parameter => parameter,
                parameter => parameter.Type == typeof(TSource) ? replacement : parameter), lambda.Body);
        }

        private static IQueryable<EntityVersion> GetEntityVersions<T>(IQueryable<T> query, Expression identity,
            Expression effectDate, Expression sequence, ParameterExpression parameter)
        {
            var entityBindings = new List<MemberBinding>
            {
                Expression.Bind(_propertyEntityId, identity),
                Expression.Bind(_propertyEffectDate, effectDate)
            };

            if (sequence != null)
            {
                entityBindings.Add(Expression.Bind(_propertySequence, sequence));
            }

            return query.Select(Expression.Lambda<Func<T, EntityVersion>>(
                Expression.MemberInit(Expression.New(typeof(EntityVersion)), entityBindings),
                parameter));
        }

        /// <summary>
        /// 查询与指定日期最接近的版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="versionDate"></param>
        /// <param name="identity"></param>
        /// <param name="effectDate"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static IQueryable<T> GetNearestVersions<T>(this IQueryable<T> query, DateTime versionDate,
            Expression<Func<T, string>> identity, Expression<Func<T, DateTime?>> effectDate, Expression<Func<T, int>> sequence = null)
        {
            var entityParameter = Expression.Parameter(typeof(T));
            var identityExpression = ReplaceParameter(identity, entityParameter);
            // 将未知类型的实体映射为特定类型的实体，简化后续使用linq表达式的方式。
            var entityVersions = GetStringEntityVersions(query,
                identityExpression, ReplaceParameter(effectDate, entityParameter),
                sequence == null ? null : ReplaceParameter(sequence, entityParameter),
                entityParameter);

            // 已经消费的版本：生效日期小于等于版本日期
            var queryConsumedVersions = entityVersions.Where(x => x.EffectDate <= versionDate);

            //生效日期小于版本日期的列表中每个实体的最高版本
            var queryMaxVersion = GetLatestStringVersion(queryConsumedVersions, sequence != null);

            // 生效日期大于版本日期中每个实体的最低版本
            var queryMinVersion = GetEarliestStringVersion(entityVersions.Where(version =>
                    version.EffectDate > versionDate && !queryConsumedVersions.Select(x => x.Id).Contains(version.Id)),
                sequence != null);

            // 第一次左连接：连接存在已消费版本的实体列表。
            var firstJoin = query.GroupJoin(queryMaxVersion,
                    Expression.Lambda<Func<T, string>>(identityExpression, entityParameter),
                    x => x.Id, (entity, versions) => new { Entity = entity, MaxVersions = versions })
                .SelectMany(x => x.MaxVersions.DefaultIfEmpty(), (entity, version) =>
                    new EntityWithMaxStringVersion<T>
                    {
                        Entity = entity.Entity,
                        MaxVersion = version
                    });

            // 第二次左连接：连接存在未消费版本的实体列表
            var parameterChaindEntity = Expression.Parameter(typeof(EntityWithMaxStringVersion<T>));
            var entityIdentitySelector = Expression.Lambda<Func<EntityWithMaxStringVersion<T>, string>>(
                ReplaceParameter(identity, Expression.Property(parameterChaindEntity, "Entity")),
                parameterChaindEntity);
            var lastJoin = firstJoin.GroupJoin(queryMinVersion, entityIdentitySelector, x => x.Id, (entity, versions) =>
                    new { entity.Entity, entity.MaxVersion, MinVersions = versions })
                .SelectMany(x => x.MinVersions.DefaultIfEmpty(), (entity, version) =>
                    new EntityWithStringVersion<T>
                    {
                        Entity = entity.Entity,
                        MaxVersion = entity.MaxVersion,
                        MinVersion = version
                    });

            // 对连接进行条件过滤
            Expression<Func<EntityWithStringVersion<T>, bool>> filterExpression;
            var filterParameter = Expression.Parameter(typeof(EntityWithStringVersion<T>));
            var entityExpression = Expression.Property(filterParameter, "Entity");
            var entityEffectDateExpression = ReplaceParameter(effectDate, entityExpression);
            var maxVersionExpression = Expression.Property(filterParameter, "MaxVersion");
            var minVersionExpression = Expression.Property(filterParameter, "MinVersion");
            var maxDateCompare = Expression.Equal(entityEffectDateExpression, Expression.Property(maxVersionExpression, "EffectDate"));
            var minDateCompare = Expression.Equal(entityEffectDateExpression, Expression.Property(minVersionExpression, "EffectDate"));
            if (sequence != null)
            {
                // (u.EffectDate == u1.EffectDate && u.Sequence.u1.Sequence) || (u.EffectDate == u2.EffectDate && u.Sequence == u2.Sequence)
                var entitySequenceExpression = ReplaceParameter(sequence, entityExpression);
                filterExpression = Expression.Lambda<Func<EntityWithStringVersion<T>, bool>>(
                    Expression.Or(
                        Expression.And(maxDateCompare, Expression.Equal(entitySequenceExpression, Expression.Property(maxVersionExpression, "Sequence"))),
                        Expression.And(minDateCompare, Expression.Equal(entitySequenceExpression, Expression.Property(minVersionExpression, "Sequence")))),
                    filterParameter);
            }
            else
            {
                // u.EffectDate == u1.EffectDate || u.EffectDate == u2.EffectDate
                filterExpression = Expression.Lambda<Func<EntityWithStringVersion<T>, bool>>(
                    Expression.Or(maxDateCompare, minDateCompare), filterParameter);
            }

            return lastJoin.Where(filterExpression).Select(x => x.Entity);
        }

        private static IQueryable<StringEntityVersion> GetLatestStringVersion(IQueryable<StringEntityVersion> query, bool includeSequence)
        {
            if (!includeSequence)
            {
                var versions = from version in query
                               group version by version.Id
                    into g
                               select new StringEntityVersion
                               {
                                   Id = g.Key,
                                   EffectDate = g.Max(p => p.EffectDate)
                               };
                return versions;
            }

            return from version in query
                   group version by version.Id
                into g
                   let maxEffectDate = g.Max(x => x.EffectDate)
                   select new StringEntityVersion
                   {
                       Id = g.Key,
                       EffectDate = maxEffectDate,
                       Sequence = g.Where(x => x.EffectDate == maxEffectDate).Max(x => x.Sequence)
                   };
        }

        private static IQueryable<StringEntityVersion> GetEarliestStringVersion(IQueryable<StringEntityVersion> query, bool includeSequence)
        {
            if (!includeSequence)
            {
                return from version in query
                       group version by version.Id
                    into g
                       select new StringEntityVersion
                       {
                           Id = g.Key,
                           EffectDate = g.Min(p => p.EffectDate)
                       };
            }

            return from version in query
                   group version by version.Id
                into g
                   let minEffectDate = g.Min(x => x.EffectDate)
                   select new StringEntityVersion
                   {
                       Id = g.Key,
                       EffectDate = minEffectDate,
                       Sequence = g.Where(x => x.EffectDate == minEffectDate).Min(x => x.Sequence)
                   };
        }

        private static IQueryable<StringEntityVersion> GetStringEntityVersions<T>(IQueryable<T> query, Expression identity,
            Expression effectDate, Expression sequence, ParameterExpression parameter)
        {
            var entityBindings = new List<MemberBinding>
            {
                Expression.Bind(_propertyStringEntityId, identity),
                Expression.Bind(_propertyStringEntityEffectDate, effectDate)
            };

            if (sequence != null)
            {
                entityBindings.Add(Expression.Bind(_propertyStringEntitySequence, sequence));
            }

            return query.Select(Expression.Lambda<Func<T, StringEntityVersion>>(
                Expression.MemberInit(Expression.New(typeof(StringEntityVersion)), entityBindings),
                parameter));
        }
    }
}

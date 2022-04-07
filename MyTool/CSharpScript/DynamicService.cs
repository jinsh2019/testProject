//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using GaiaWorks.HumanResource.BusinessContracts;
//using GaiaWorks.HumanResource.BusinessModels.Metadata;
//using GaiaWorks.HumanResource.DataAccess;
//using GaiaWorks.HumanResource.Utilities;
//using GaiaWorks.Localization;
//using GaiaWorks.PowerMapper;
//using GaiaWorks.Storage;
//using Microsoft.CodeAnalysis.CSharp.Scripting;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;

//namespace GaiaWorks.HumanResource.BusinessServices
//{
//    public class DynamicService : IDynamicService
//    {
//        private readonly ApplicationContext _context;
//        private readonly ITenantContext _tenant;
//        private readonly IStorageFactory _storageFactory;
//        //private readonly IResourceReader _resourceReader;
//        private readonly IResourceFactory _resourceFactory;
//        private readonly IAccessContext _accessContext;
//        private readonly IConfiguration _configuration;

//        public DynamicService(ApplicationContext context, ITenantContext tenant, IStorageFactory storageFactory,
//             IAccessContext accessContext, IResourceFactory resourceFactory, IConfiguration configuration)
//        {
//            _context = context;
//            _tenant = tenant;
//            _storageFactory = storageFactory;
//            _resourceFactory = resourceFactory;
//            _configuration = configuration;
//            _accessContext = accessContext;
//            //_resourceReader = resourceFactory.GetReader(LanguageConsts.Job);
//        }

//        public async Task SavePropertyValues(string typeName, string identity,
//            IDictionary<string, object> propertyValues, bool isSave = true)
//        {
//            var existingPropertyValues = await (from propertyValue in _context.DynamicValues
//                                                where propertyValue.Tenant == _tenant.Tenant && propertyValue.Identity == identity
//                                                select propertyValue).ToListAsync();
//            var metadataProperties = await (from metadateProperty in _context.MetadataProperties
//                                            where metadateProperty.Tenant == _tenant.Tenant
//                                            select metadateProperty).ToListAsync();
//            foreach (var propertyValue in propertyValues?.Where(x => metadataProperties.Select(p => p.PropertyName).ToList().Contains(x.Key)) ?? new Dictionary<string, object>())
//            {
//                if (!string.IsNullOrEmpty(Convert.ToString(propertyValue.Value)))
//                {
//                    if (metadataProperties.Any(x => x.PropertyName == propertyValue.Key && x.PropertyKind == (int)PropertyKind.Extended))
//                    {
//                        var entity = existingPropertyValues.SingleOrDefault(x => string.Equals(x.PropertyName, propertyValue.Key, StringComparison.OrdinalIgnoreCase));
//                        if (entity == null)
//                        {
//                            entity = new DynamicValueEntity {
//                                Tenant = _tenant.Tenant,
//                                TypeName = typeName,
//                                Identity = identity,
//                                PropertyName = propertyValue.Key
//                            };
//                           CustomizeProperty(entity);
//                            await _context.DynamicValues.AddAsync(entity);
//                        }
//                        else
//                        {
//                            existingPropertyValues.Remove(entity);
//                        }
//                        entity.PropertyValue = metadataProperties.Any(x => x.PropertyType == (int)PropertyType.File && x.PropertyName == propertyValue.Key) ? Files(propertyValue.Value.ToString()) : propertyValue.Value.ToString();
//                        CustomizeProperty(entity);

//                    }
//                }
//            }
//            _context.DynamicValues.RemoveRange(existingPropertyValues);
//            if (isSave)
//            {
//                await _context.SaveChangesAsync();
//            }
//        }

//        private void CustomizeProperty(DynamicValueEntity entity)
//        {
//            if (!string.IsNullOrWhiteSpace(_configuration[Consts.HrBlacklistLevel]))
//            {
//                var myGlobals = entity;
//                var myScript = _configuration[Consts.HrBlacklistLevel];
//                var result = CSharpScript.EvaluateAsync(myScript, globals: myGlobals).Result;
//                entity.PropertyValue = result?.ToString();
//            }
//            return;
//        }
//        /// <summary>
//        /// 删除自定义字段
//        /// </summary>
//        /// <param name="deleteModel"></param>
//        /// <returns></returns>
//        public async Task DeleteWorkforcePropertyValues(DynamicValueGetModel deleteModel)
//        {
//            var propertyValues = await (from propertyValue in _context.WorkforceDynamicValue
//                                        where propertyValue.Tenant == _tenant.Tenant && propertyValue.TypeName == deleteModel.TypeName &&
//                                              propertyValue.Identity == deleteModel.Identity
//                                        select propertyValue).ToListAsync();
//            _context.WorkforceDynamicValue.RemoveRange(propertyValues);
//        }

//        public async Task SavePropertyValues(DynamicValueSaveModel saveModel)
//        {
//            var existingPropertyValues = await (from propertyValue in _context.WorkforceDynamicValue
//                                                where propertyValue.Tenant == _tenant.Tenant && propertyValue.Identity == saveModel.Identity
//                                                select propertyValue).ToListAsync();
//            var metadataProperties = await (from metadateProperty in _context.MetadataProperties
//                                            where metadateProperty.Tenant == _tenant.Tenant
//                                            select metadateProperty).ToListAsync();
//            foreach (var propertyValue in saveModel.PropertyValues?.Where(x => metadataProperties.Select(p => p.PropertyName).ToList().Contains(x.Key)) ?? new Dictionary<string, object>())
//            {
//                if (!string.IsNullOrEmpty(Convert.ToString(propertyValue.Value)))
//                {
//                    if (metadataProperties.Any(x => x.PropertyName == propertyValue.Key && x.PropertyKind == (int)PropertyKind.Extended))
//                    {
//                        var entity = existingPropertyValues.SingleOrDefault(x => string.Equals(x.PropertyName, propertyValue.Key, StringComparison.OrdinalIgnoreCase));
//                        if (entity == null)
//                        {
//                            entity = new WorkforceDynamicValueEntity {
//                                Tenant = _tenant.Tenant,
//                                TypeName = saveModel.TypeName,
//                                Identity = saveModel.Identity,
//                                PropertyName = propertyValue.Key
//                            };
//                            await _context.WorkforceDynamicValue.AddAsync(entity);
//                        }
//                        else
//                        {
//                            existingPropertyValues.Remove(entity);
//                        }
//                        entity.PropertyValue = metadataProperties.Any(x => x.PropertyType == (int)PropertyType.File && x.PropertyName == propertyValue.Key) ? Files(propertyValue.Value.ToString()) : propertyValue.Value.ToString();
//                    }
//                }
//            }
//            _context.WorkforceDynamicValue.RemoveRange(existingPropertyValues);
//        }

//        public async Task<PropertyDynamicModel[]> GetPersonPropertyAsync(params string[] identities)
//        {
//            var property = await _context.DynamicValues.Where(x => identities.Contains(x.Identity)).ToArrayAsync();
//            var dynamic = _context.MetadataProperties.ToArray();

//            var models = Mapper.Map<DynamicValueEntity, PropertyDynamicModel>(property);
//            foreach (var model in models)
//            {
//                var pro = dynamic.FirstOrDefault(x => x.PropertyName == model.PropertyName);
//                model.DataSource = pro?.DataSource;
//                model.Code = pro?.Code;
//            }

//            return models;
//        }

//        public async Task SavePropertyAsync(List<DynamicValueModel> models)
//        {
//            var metadataProperties = await (from dynamicValue in _context.DynamicValues
//                                            where dynamicValue.Tenant == _tenant.Tenant
//                                            select dynamicValue).ToListAsync();
//            foreach (var model in models)
//            {
//                if (string.IsNullOrEmpty(model.PropertyValue)) continue;
//                var entity = new DynamicValueEntity {
//                    Tenant = _tenant.Tenant,
//                    TypeName = model.TypeName,
//                    Identity = model.Identity,
//                    PropertyName = model.PropertyName,
//                    PropertyValue = model.PropertyValue
//                };
//                var metadata = metadataProperties.FirstOrDefault(x => x.PropertyName == model.PropertyName && x.TypeName == model.TypeName && x.Identity == model.Identity);
//                if (metadata != null)
//                {
//                    metadata.PropertyValue = entity.PropertyValue;
//                }
//                else
//                {
//                    await _context.DynamicValues.AddAsync(entity);
//                }
//            }
//            await _context.SaveChangesAsync();
//        }

//        public async Task<MetadataProperty[]> GetPropertyAsync()
//        {
//            var metadataProperties = await (from metadateProperty in _context.MetadataProperties
//                                            where metadateProperty.Tenant == _tenant.Tenant
//                                            select metadateProperty).ToArrayAsync();
//            return Mapper.Map<MetadataPropertyEntity, MetadataProperty>(metadataProperties);
//        }

//        public async Task<IDictionary<string, object>> GetPropertyValuesAsync(string typeName, string identity)
//        {
//            var propertyValues = from propertyValue in _context.DynamicValues
//                                 where propertyValue.Tenant == _tenant.Tenant && propertyValue.TypeName == typeName &&
//                                       propertyValue.Identity == identity
//                                 select propertyValue;
//            var metadataProperties = await (from metadateProperty in _context.MetadataProperties
//                                            where metadateProperty.Tenant == _tenant.Tenant
//                                            select metadateProperty).ToListAsync();

//            foreach (var propertyValue in propertyValues)
//            {
//                if (metadataProperties.Any(x =>
//                    x.PropertyName == propertyValue.PropertyName && x.PropertyKind == (int)PropertyKind.Extended && x.PropertyType == (int)PropertyType.File))
//                {
//                    propertyValue.PropertyValue = FileUrls(propertyValue.PropertyValue.ToString());
//                }
//            }

//            return await propertyValues.ToDictionaryAsync(x => x.PropertyName, x => (object)x.PropertyValue);
//        }

//        public async Task<IDictionary<string, object>> GetPropertyValuesAsync(DynamicValueGetModel getModel)
//        {
//            var propertyValues = from propertyValue in _context.WorkforceDynamicValue
//                                 where propertyValue.Tenant == _tenant.Tenant && propertyValue.TypeName == getModel.TypeName &&
//                                       propertyValue.Identity == getModel.Identity
//                                 select propertyValue;
//            var metadataProperties = await (from metadateProperty in _context.MetadataProperties
//                                            where metadateProperty.Tenant == _tenant.Tenant
//                                            select metadateProperty).ToListAsync();

//            foreach (var propertyValue in propertyValues)
//            {
//                if (metadataProperties.Any(x =>
//                    x.PropertyName == propertyValue.PropertyName && x.PropertyKind == (int)PropertyKind.Extended && x.PropertyType == (int)PropertyType.File))
//                {
//                    propertyValue.PropertyValue = FileUrls(propertyValue.PropertyValue.ToString());
//                }
//            }

//            return await propertyValues.ToDictionaryAsync(x => x.PropertyName, x => (object)x.PropertyValue);
//        }

//        /// <summary>
//        /// 批量取值
//        /// </summary>
//        /// <param name="typeName"></param>
//        /// <param name="companyVersionId"></param>
//        /// <returns></returns>
//        public async Task<List<DynamicValueBatchModel>> GetPropertyValuesAsync(string typeName, params string[] companyVersionId)
//        {
//            var result = new List<DynamicValueBatchModel>();
//            var propertyValues = (from propertyValue in _context.WorkforceDynamicValue
//                                  where propertyValue.Tenant == _tenant.Tenant && propertyValue.TypeName == typeName
//                                  select propertyValue).WhereIf(x => companyVersionId.Contains(x.Identity), companyVersionId.Any());
//            var metadataProperties = await (from metadateProperty in _context.MetadataProperties
//                                            where metadateProperty.Tenant == _tenant.Tenant
//                                            select metadateProperty).ToListAsync();

//            foreach (var propertyValue in propertyValues)
//            {
//                if (metadataProperties.Any(x =>
//                    x.PropertyName == propertyValue.PropertyName && x.PropertyKind == (int)PropertyKind.Extended && x.PropertyType == (int)PropertyType.File))
//                {
//                    propertyValue.PropertyValue = FileUrls(propertyValue.PropertyValue.ToString());
//                }
//            }
//            var rawData = propertyValues.ToList();
//            var groupedData = propertyValues.Select(t => t.Identity).Distinct().ToList();
//            foreach (var item in groupedData)
//            {
//                var newItem = new DynamicValueBatchModel();
//                newItem.Identity = item;
//                var filteredData = rawData.Where(r => r.Identity == item).ToList();
//                newItem.PropertyValues = filteredData.ToDictionary(x => x.PropertyName, x => (object)x.PropertyValue);
//                result.Add(newItem);
//            }

//            return result;
//        }

//        public IDictionary<string, object> GetPropertyValues(string typeName, string identity)
//        {
//            var propertyValues = from propertyValue in _context.DynamicValues
//                                 where propertyValue.Tenant == _tenant.Tenant && propertyValue.TypeName == typeName &&
//                                       propertyValue.Identity == identity
//                                 select propertyValue;
//            var metadataProperties = (from metadateProperty in _context.MetadataProperties
//                                      where metadateProperty.Tenant == _tenant.Tenant
//                                      select metadateProperty).ToList();

//            foreach (var propertyValue in propertyValues)
//            {
//                if (metadataProperties.Any(x =>
//                    x.PropertyName == propertyValue.PropertyName && x.PropertyKind == (int)PropertyKind.Extended && x.PropertyType == (int)PropertyType.File))
//                {
//                    propertyValue.PropertyValue = FileUrls(propertyValue.PropertyValue.ToString());
//                }
//            }

//            return propertyValues.ToDictionary(x => x.PropertyName, x => (object)x.PropertyValue);
//        }

//        private string Files(string josnObjec)
//        {
//            if (string.IsNullOrEmpty(josnObjec))
//            {
//                return null;
//            }

//            var fileModel = JsonConvert.DeserializeObject<List<FilesModel>>(josnObjec);
//            if (fileModel.Count() > 5)
//            {
//                throw new ApplicationValidationException(string.Format(_resourceFactory.GetReader(LanguageConsts.Job).GetString(LanguageConsts.MaxCount, _accessContext.CultureCode)?.Value, 5));
//            }
//            foreach (var model in fileModel)
//            {
//                model.Url = "";
//                if (model.Name.Length > 30)
//                {
//                    throw new ApplicationValidationException(string.Format(_resourceFactory.GetReader(LanguageConsts.Job).GetString(LanguageConsts.FileNameCount, _accessContext.CultureCode)?.Value, 30));
//                }

//                if (model.Status == "error")
//                {
//                    throw new ApplicationValidationException(_resourceFactory.GetReader("").GetString(LanguageConsts.FileStatuc, _accessContext.CultureCode)?.Value);
//                }
//                if (string.IsNullOrEmpty(model.FileName))
//                {
//                    throw new ApplicationValidationException(_resourceFactory.GetReader(LanguageConsts.Job).GetString(LanguageConsts.FileName, _accessContext.CultureCode)?.Value);
//                }
//            }

//            return JsonConvert.SerializeObject(fileModel);
//        }
//        private string FileUrls(string josnObjec)
//        {
//            if (string.IsNullOrEmpty(josnObjec))
//            {
//                return null;
//            }

//            var fileModel = JsonConvert.DeserializeObject<List<FilesModel>>(josnObjec);

//            foreach (var model in fileModel)
//            {
//                model.Url = FileString.GetPersonFile(_tenant.Tenant, model.FileName, _storageFactory);
//            }

//            return JsonConvert.SerializeObject(fileModel);
//        }
//    }


//    public class FilesModel
//    {

//        [JsonProperty("name")]
//        public string Name { get; set; }

//        [JsonProperty("size")]
//        public int Size { get; set; }

//        [JsonProperty("type")]
//        public string Type { get; set; }

//        [JsonProperty("url")]
//        public string Url { get; set; }

//        [JsonProperty("status")]
//        public string Status { get; set; }

//        [JsonProperty("fileName")]
//        public string FileName { get; set; }
//    }
//    public class Globals
//    {
//        public string myKey;
//        public object myValue;
//    }
//}

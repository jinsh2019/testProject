using System;
using Newtonsoft.Json;

namespace DateConverter
{

    public class cls
    {
        public string tableName { get; set; }
        public col[] columns { get; set; }
    }

    public class col
    {
        public string resourceKey { get; set; }
    }

    public class row
    {
        public string resourceKey { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string str1 = @"[
	{
		""tableName"": ""identities"",
		""columns"": [
			{
				""key"": ""isPrimary"",
				""resourceKey"": ""common.label.primary"",
				""i18n"": ""主要""
			},
			{
				""key"": ""country"",
				""resourceKey"": ""m.conf.pers.customField.common.country"",
				""i18n"": ""国家／地区""
			},
			{
				""key"": ""identitieType"",
				""resourceKey"": ""m.conf.pers.customField.idCard.idType"",
				""i18n"": ""证件类型""
			},
			{
				""key"": ""identitieNumber"",
				""resourceKey"": ""m.conf.pers.customField.idCard.idNumber"",
				""i18n"": ""证件号码""
			},
			{
				""key"": ""issuingAuthority"",
				""resourceKey"": ""HRCC.setting.customField.idCard.issuingAuthority"",
				""i18n"": ""签发机关""
			},
			{
				""key"": ""effectDate"",
				""resourceKey"": ""m.conf.pers.customField.idCard.beginDate"",
				""i18n"": ""签发日期""
			},
			{
				""key"": ""expireDate"",
				""resourceKey"": ""m.conf.pers.customField.common.expireDate"",
				""i18n"": ""过期日期""
			}
		]
	},
	{
		""tableName"": ""addresses"",
		""columns"": [
			{
				""key"": ""isPrimary"",
				""resourceKey"": ""common.label.primary"",
				""i18n"": ""主要""
			},
			{
				""key"": ""typeId"",
				""resourceKey"": ""m.conf.pers.customField.address.type"",
				""i18n"": ""地址类型""
			},
			{
				""key"": ""districtName"",
				""resourceKey"": ""m.org.company.address.addressLines"",
				""i18n"": ""详细地址""
				""isPinjie"": ""是 省市区 + 详细地址"",
			},
			{
				""key"": ""postalCode"",
				""resourceKey"": ""m.org.company.address.postalCode"",
				""i18n"": ""邮政编码""
			}
		]
	},
	{
		""tableName"": ""telephones"",
		""columns"": [
			{
				""key"": ""isPrimary"",
				""resourceKey"": ""common.label.primary"",
				""i18n"": ""主要""
			},
			{
				""key"": ""type"",
				""resourceKey"": ""m.org.company.tel.type"",
				""i18n"": ""电话类型""
			},
			{
				""key"": ""countryCode"",
				""resourceKey"": ""m.org.company.tel.countryCode"",
				""i18n"": ""国家/地区号""
			},
			{
				""key"": ""telephoneNumber"",
				""resourceKey"": ""m.org.company.tel.telephoneNumber"",
				""i18n"": ""电话号码""
			},
			{
				""key"": ""extensionNumber"",
				""resourceKey"": ""m.org.company.tel.extensionNumber"",
				""i18n"": ""分机""
			}
		]
	},
	{
		""tableName"": ""educations"",
		""columns"": [
			{
				""key"": ""startDate"",
				""resourceKey"": ""m.conf.pers.customField.common.startDate"",
				""i18n"": ""开始日期""
			},
			{
				""key"": ""endDate"",
				""resourceKey"": ""m.conf.pers.customField.common.endDate"",
				""i18n"": ""结束日期""
			},
			{
				""key"": ""school"",
				""resourceKey"": ""m.conf.pers.customField.eExperience.school"",
				""i18n"": ""学校""
			},
			{
				""key"": ""major"",
				""resourceKey"": ""m.conf.pers.customField.eExperience.major"",
				""i18n"": ""专业""
			},
			{
				""key"": ""address"",
				""resourceKey"": ""m.org.company.address.city"",
				""i18n"": ""城市""
			},
			{
				""key"": ""education"",
				""resourceKey"": ""m.conf.pers.customField.eExperience.education"",
				""i18n"": ""学历""
			},
			{
				""key"": ""degree"",
				""resourceKey"": ""m.conf.pers.customField.eExperience.academicDegree"",
				""i18n"": ""学位""
			},
			{
				""key"": ""educationMode"",
				""resourceKey"": ""m.conf.pers.customField.eExperience.educationMethod"",
				""i18n"": ""教育方式""
			},
			{
				""key"": ""graduationType"",
				""resourceKey"": ""HRCC.conf.custom.eExperience.graduationType"",
				""i18n"": ""毕业类型""
			},
			{
				""key"": ""degreeName"",
				""resourceKey"": ""HRCC.conf.custom.eExperience.degreeName"",
				""i18n"": ""学位名称""
			},
			{
				""key"": ""certificateNo"",
				""resourceKey"": ""HRCC.conf.custom.eExperience.certificateNo"",
				""i18n"": ""学历证书编号""
			},
			{
				""key"": ""educationGetDate"",
				""resourceKey"": ""HRCC.conf.custom.eExperience.educationGetDate"",
				""i18n"": ""学历获得时间""
			},
			{
				""key"": ""degreeCertificateNo"",
				""resourceKey"": ""HRCC.conf.custom.eExperience.degreeCertificateNo"",
				""i18n"": ""学位证书编号""
			},
			{
				""key"": ""degreeGetDate"",
				""resourceKey"": ""HRCC.conf.custom.eExperience.degreeGetDate"",
				""i18n"": ""学位获得时间""
			},
			{
				""key"": ""schoolAddress"",
				""resourceKey"": ""HRCC.conf.custom.eExperience.schoolAddress"",
				""i18n"": ""学校地址""
			},
			{
				""key"": ""witness"",
				""resourceKey"": ""HRCC.conf.custom.eExperience.witness"",
				""i18n"": ""证明人""
			},
			{
				""key"": ""contact"",
				""resourceKey"": ""HRCC.conf.custom.eExperience.contact"",
				""i18n"": ""联系方式""
			},
			{
				""key"": ""overseasEducation"",
				""resourceKey"": ""HRCC.conf.custom.eExperience.overseasEducation"",
				""i18n"": ""是否海外教育""
			},
			{
				""key"": ""freshGraduates"",
				""resourceKey"": ""HRCC.conf.custom.eExperience.freshGraduates"",
				""i18n"": ""是否应届毕业生""
			},
			{
				""key"": ""isHighest"",
				""resourceKey"": ""m.conf.pers.customField.eExperience.maxEducation"",
				""i18n"": ""最高学历""
			},
			{
				""key"": ""value"",
				""resourceKey"": ""m.conf.pers.customField.eExperience.maxEducation"",
				""i18n"": ""最高学历""
			},
			{
				""key"": ""label"",
				""resourceKey"": ""m.conf.pers.customField.eExperience.maxEducation"",
				""i18n"": ""最高学历""
			},
			{
				""key"": ""remark"",
				""resourceKey"": ""common.label.remark"",
				""i18n"": ""备注""
			},
			{
				""key"": ""files"",
				""resourceKey"": ""m.conf.pers.fieldList.file"",
				""i18n"": ""附件""
			}
		]
	},
	{
		""tableName"": ""manager"",
		""columns"": [
			{
				""key"": ""managerType"",
				""resourceKey"": ""m.conf.pers.customField.manager.type"",
				""i18n"": ""经理类型""
			},
			{
				""key"": ""managerPersonId"",
				""resourceKey"": ""m.conf.pers.customField.manager.reportTo"",
				""i18n"": ""汇报给""
			},
			{
				""key"": ""source"",
				""resourceKey"": ""m.conf.pers.customField.manager.source"",
				""i18n"": ""来源""
			}
		]
	},
	{
		""tableName"": ""emails"",
		""columns"": [
			{
				""key"": ""isPrimary"",
				""resourceKey"": ""common.label.primary"",
				""i18n"": ""主要""
			},
			{
				""key"": ""type"",
				""resourceKey"": ""m.org.company.email.type"",
				""i18n"": ""邮件类型""
			},
			{
				""key"": ""address"",
				""resourceKey"": ""m.org.company.email.address"",
				""i18n"": ""邮件地址""
			}
		]
	},
	{
		""tableName"": ""contacts"",
		""columns"": [
			{
				""key"": ""isPrimary"",
				""resourceKey"": ""common.label.primary"",
				""i18n"": ""主要""
			},
			{
				""key"": ""type"",
				""resourceKey"": ""m.conf.pers.customField.IM.type"",
				""i18n"": ""即时通讯类型""
			},
			{
				""key"": ""number"",
				""resourceKey"": ""m.conf.pers.customField.common.account"",
				""i18n"": ""账号""
			}
		]
	},
	{
		""tableName"": ""socials"",
		""columns"": [
			{
				""key"": ""isPrimary"",
				""resourceKey"": ""common.label.primary"",
				""i18n"": ""主要""
			},
			{
				""key"": ""type"",
				""resourceKey"": ""m.conf.pers.customField.SNS.type"",
				""i18n"": ""社交网络类型""
			},
			{
				""key"": ""number"",
				""resourceKey"": ""m.conf.pers.customField.common.account"",
				""i18n"": ""账号""
			}
		]
	},
	{
		""tableName"": ""families"",
		""columns"": [
			{
				""key"": ""familyName"",
				""resourceKey"": ""m.pers.person.name"",
				""i18n"": ""姓名""
			},
			{
				""key"": ""relation"",
				""resourceKey"": ""m.conf.pers.customField.common.relationShipEmployee"",
				""i18n"": ""与员工关系""
			},
			{
				""key"": ""gender"",
				""resourceKey"": ""common.label.sex.title"",
				""i18n"": ""性别""
			},
			{
				""key"": ""birthDate"",
				""resourceKey"": ""m.pers.person.family.birthday"",
				""i18n"": ""出生日期""
			},
			{
				""key"": ""company"",
				""resourceKey"": ""m.conf.pers.customField.family.workPlace"",
				""i18n"": ""工作单位""
			},
			{
				""key"": ""job"",
				""resourceKey"": ""m.conf.pers.customField.common.job"",
				""i18n"": ""职务""
			},
			{
				""key"": ""politicsStatus"",
				""resourceKey"": ""m.pers.person.family.politicalStatus"",
				""i18n"": ""政治面貌""
			},
			{
				""key"": ""telephone"",
				""resourceKey"": ""m.conf.pers.customField.common.contactNumber"",
				""i18n"": ""联系电话""
			},
			{
				""key"": ""email"",
				""resourceKey"": ""m.pers.person.family.email"",
				""i18n"": ""电子邮件""
			},
			{
				""key"": ""address"",
				""resourceKey"": ""m.conf.pers.customField.contact.address"",
				""i18n"": ""联系地址""
			},
			{
				""key"": ""isEmployee"",
				""resourceKey"": ""m.pers.person.family.companyEmployee"",
				""i18n"": ""是否公司员工""
			},
			{
				""key"": ""employeeId"",
				""resourceKey"": ""m.pers.person.employeeId"",
				""i18n"": ""工号""
			},
			{
				""key"": ""country"",
				""resourceKey"": ""m.org.company.address.country"",
				""i18n"": ""国家/地区""
			},
			{
				""key"": ""regionName"",
				""resourceKey"": ""m.org.company.address.country"",
				""i18n"": ""国家/地区""
			},
			{
				""key"": ""id"",
				""resourceKey"": ""m.org.company.address.country"",
				""i18n"": ""国家/地区""
			},
			{
				""key"": ""identitieType"",
				""resourceKey"": ""m.conf.pers.customField.idCard.idType"",
				""i18n"": ""证件类型""
			},
			{
				""key"": ""identitieNumber"",
				""resourceKey"": ""m.conf.pers.customField.idCard.idNumber"",
				""i18n"": ""证件号码""
			},
			{
				""key"": ""familyEmployeeStatus"",
				""resourceKey"": ""HRCC.conf.custom.family.familyEmployeeStatus"",
				""i18n"": ""员工状态""
			},
			{
				""key"": ""medicalInsuranceStartDate"",
				""resourceKey"": ""HRCC.conf.custom.family.medicalInsuranceStartDate"",
				""i18n"": ""医疗险开始日期""
			}
		]
	},
	{
		""tableName"": ""links"",
		""columns"": [
			{
				""key"": ""isPrimary"",
				""resourceKey"": ""common.label.primary"",
				""i18n"": ""主要""
			},
			{
				""key"": ""linkName"",
				""resourceKey"": ""m.conf.pers.customField.common.name"",
				""i18n"": ""姓名""
			},
			{
				""key"": ""relation"",
				""resourceKey"": ""m.conf.pers.customField.common.relationShipEmployee"",
				""i18n"": ""与员工关系""
			},
			{
				""key"": ""gender"",
				""resourceKey"": ""common.label.sex.title"",
				""i18n"": ""性别""
			},
			{
				""key"": ""telePhone"",
				""resourceKey"": ""m.conf.pers.customField.common.contactNumber"",
				""i18n"": ""联系电话""
			},
			{
				""key"": ""address"",
				""resourceKey"": ""m.conf.pers.customField.contact.address"",
				""i18n"": ""联系地址""
			}
		]
	},
	{
		""tableName"": ""experiences"",
		""columns"": [
			{
				""key"": ""startDate"",
				""resourceKey"": ""m.conf.pers.customField.common.startDate"",
				""i18n"": ""开始日期""
			},
			{
				""key"": ""endDate"",
				""resourceKey"": ""m.conf.pers.customField.common.endDate"",
				""i18n"": ""结束日期""
			},
			{
				""key"": ""employer"",
				""resourceKey"": ""m.conf.pers.customField.wExperience.employer"",
				""i18n"": ""雇主""
			},
			{
				""key"": ""industry"",
				""resourceKey"": ""m.pers.person.wExperience.industry"",
				""i18n"": ""行业""
			},
			{
				""key"": ""address"",
				""resourceKey"": ""m.org.company.address.city"",
				""i18n"": ""城市""
			},
			{
				""key"": ""job"",
				""resourceKey"": ""m.conf.pers.customField.common.job"",
				""i18n"": ""职务""
			},
			{
				""key"": ""isRelated"",
				""resourceKey"": ""m.conf.pers.customField.wExperience.relatedExperience"",
				""i18n"": ""相关工作经历""
			},
			{
				""key"": ""salary"",
				""resourceKey"": ""m.pers.person.wExperience.salary"",
				""i18n"": ""薪酬""
			},
			{
				""key"": ""salaryUnit"",
				""resourceKey"": ""m.pers.person.wExperience.salary"",
				""i18n"": ""薪酬""
			},
			{
				""key"": ""payCycle"",
				""resourceKey"": ""m.pers.person.wExperience.salary"",
				""i18n"": ""薪酬""
			},
			{
				""key"": ""witness"",
				""resourceKey"": ""HRCC.conf.custom.eExperience.witness"",
				""i18n"": ""证明人""
			},
			{
				""key"": ""contact"",
				""resourceKey"": ""HRCC.conf.custom.eExperience.contact"",
				""i18n"": ""联系方式""
			},
			{
				""key"": ""overseasJob"",
				""resourceKey"": ""HRCC.conf.custom.wExperience.overseasJob"",
				""i18n"": ""是否海外工作""
			},
			{
				""key"": ""serviceAge"",
				""resourceKey"": ""HRCC.conf.custom.wExperience.serviceAge"",
				""i18n"": ""工龄""
			},
			{
				""key"": ""approbateServiceAge"",
				""resourceKey"": ""HRCC.conf.custom.wExperience.approbateServiceAge"",
				""i18n"": ""是否认可工龄""
			},
			{
				""key"": ""remark"",
				""resourceKey"": ""common.label.remark"",
				""i18n"": ""备注""
			},
			{
				""key"": ""files"",
				""resourceKey"": ""m.conf.pers.fieldList.file"",
				""i18n"": ""附件""
			}
		]
	},
	{
		""tableName"": ""trainings"",
		""columns"": [
			{
				""key"": ""training"",
				""resourceKey"": ""m.conf.pers.customField.tExperience.training"",
				""i18n"": ""培训""
			},
			{
				""key"": ""startDate"",
				""resourceKey"": ""m.conf.pers.customField.common.startDate"",
				""i18n"": ""开始日期""
			},
			{
				""key"": ""endDate"",
				""resourceKey"": ""m.conf.pers.customField.common.endDate"",
				""i18n"": ""结束日期""
			},
			{
				""key"": ""beforeHire"",
				""resourceKey"": ""HRCC.conf.custom.tExperience.beforeHire"",
				""i18n"": ""入职前后""
			},
			{
				""key"": ""trainingType"",
				""resourceKey"": ""HRCC.conf.custom.tExperience.trainingType"",
				""i18n"": ""培训类型""
			},
			{
				""key"": ""trainingForm"",
				""resourceKey"": ""HRCC.conf.custom.tExperience.trainingForm"",
				""i18n"": ""培训形式""
			},
			{
				""key"": ""trainingTheme"",
				""resourceKey"": ""HRCC.conf.custom.tExperience.trainingTheme"",
				""i18n"": ""培训主题""
			},
			{
				""key"": ""trainingInstitution"",
				""resourceKey"": ""HRCC.conf.custom.tExperience.trainingInstitution"",
				""i18n"": ""培训机构""
			},
			{
				""key"": ""trainingAddress"",
				""resourceKey"": ""HRCC.conf.custom.tExperience.trainingAddress"",
				""i18n"": ""培训地点""
			},
			{
				""key"": ""classHour"",
				""resourceKey"": ""HRCC.conf.custom.tExperience.classHour"",
				""i18n"": ""学时""
			},
			{
				""key"": ""certificateName"",
				""resourceKey"": ""HRCC.conf.custom.tExperience.certificateName"",
				""i18n"": ""证件名""
			},
			{
				""key"": ""certificateNo"",
				""resourceKey"": ""HRCC.conf.custom.tExperience.certificateNo"",
				""i18n"": ""证件编号""
			},
			{
				""key"": ""agreementNo"",
				""resourceKey"": ""HRCC.conf.custom.tExperience.agreementNo"",
				""i18n"": ""协议编号""
			},
			{
				""key"": ""agreementStartDate"",
				""resourceKey"": ""HRCC.conf.custom.tExperience.agreementStartDate"",
				""i18n"": ""协议开始日期""
			},
			{
				""key"": ""agreementPeriod"",
				""resourceKey"": ""HRCC.conf.custom.tExperience.agreementPeriod"",
				""i18n"": ""协议期""
			},
			{
				""key"": ""agreementEndDate"",
				""resourceKey"": ""HRCC.conf.custom.tExperience.agreementEndDate"",
				""i18n"": ""协议结束日期""
			},
			{
				""key"": ""trainingAmount"",
				""resourceKey"": ""HRCC.conf.custom.tExperience.trainingAmount"",
				""i18n"": ""培训费用""
			},
			{
				""key"": ""remark"",
				""resourceKey"": ""common.label.remark"",
				""i18n"": ""备注""
			},
			{
				""key"": ""files"",
				""resourceKey"": ""m.conf.pers.fieldList.file"",
				""i18n"": ""附件""
			}
		]
	},
	{
		""tableName"": ""skills"",
		""columns"": [
			{
				""key"": ""skill"",
				""resourceKey"": ""m.conf.pers.customField.skills.ability"",
				""i18n"": ""技能""
			},
			{
				""key"": ""skillLevel"",
				""resourceKey"": ""m.conf.pers.customField.skills.skilled"",
				""i18n"": ""熟练程度""
			},
			{
				""key"": ""degreeNecessary"",
				""resourceKey"": ""m.conf.pers.customField.skills.necessary"",
				""i18n"": ""必要程度""
			},
			{
				""key"": ""files"",
				""resourceKey"": ""m.conf.pers.fieldList.file"",
				""i18n"": ""附件""
			}
		]
	},
	{
		""tableName"": ""languages"",
		""columns"": [
			{
				""key"": ""language"",
				""resourceKey"": ""m.conf.pers.customField.language.language"",
				""i18n"": ""语种""
			},
			{
				""key"": ""readingProficiency"",
				""resourceKey"": ""m.conf.pers.customField.language.readingLevel"",
				""i18n"": ""阅读水平""
			},
			{
				""key"": ""oralProficiency"",
				""resourceKey"": ""m.conf.pers.customField.language.spokenLevel"",
				""i18n"": ""口语水平""
			},
			{
				""key"": ""writingProficiency"",
				""resourceKey"": ""m.conf.pers.customField.language.writingLevel"",
				""i18n"": ""写作水平""
			},
			{
				""key"": ""motherTongue"",
				""resourceKey"": ""m.conf.pers.customField.language.motherTongue"",
				""i18n"": ""母语""
			},
			{
				""key"": ""proficiency"",
				""resourceKey"": ""m.conf.pers.customField.skills.skilled"",
				""i18n"": ""熟练程度""
			},
			{
				""key"": ""certificate"",
				""resourceKey"": ""HRCC.conf.custom.language.certificate"",
				""i18n"": ""证书""
			},
			{
				""key"": ""certificateDate"",
				""resourceKey"": ""HRCC.conf.custom.language.certificateDate"",
				""i18n"": ""证书获得日期""
			},
			{
				""key"": ""files"",
				""resourceKey"": ""m.conf.pers.fieldList.file"",
				""i18n"": ""附件""
			}
		]
	},
	{
		""tableName"": ""licenses"",
		""columns"": [
			{
				""key"": ""license"",
				""resourceKey"": ""m.conf.pers.customField.certificate.certificate"",
				""i18n"": ""执照／证书""
			},
			{
				""key"": ""licenseName"",
				""resourceKey"": ""HRCC.person.customField.certificateName"",
				""i18n"": ""证书名称""
			},
			{
				""key"": ""licenseLevel"",
				""resourceKey"": ""HRCC.conf.custom.certificate.licenseLevel"",
				""i18n"": ""证书级别""
			},
			{
				""key"": ""nation"",
				""resourceKey"": ""m.conf.pers.customField.common.country"",
				""i18n"": ""国家／地区""
			},
			{
				""key"": ""certificateNo"",
				""resourceKey"": ""m.conf.pers.customField.certificate.certificateNo"",
				""i18n"": ""证书编号""
			},
			{
				""key"": ""authorizationDate"",
				""resourceKey"": ""m.conf.pers.customField.common.awardedDate"",
				""i18n"": ""获得日期""
			},
			{
				""key"": ""expiredDate"",
				""resourceKey"": ""m.conf.pers.customField.common.expireDate"",
				""i18n"": ""过期日期""
			},
			{
				""key"": ""isValid"",
				""resourceKey"": ""common.label.state"",
				""i18n"": ""状态""
			},
			{
				""key"": ""remark"",
				""resourceKey"": ""common.label.remark"",
				""i18n"": ""备注""
			},
			{
				""key"": ""files"",
				""resourceKey"": ""m.conf.pers.fieldList.file"",
				""i18n"": ""附件""
			}
		]
	},
	{
		""tableName"": ""honors"",
		""columns"": [
			{
				""key"": ""honor"",
				""resourceKey"": ""m.conf.pers.customField.honor.name"",
				""i18n"": ""奖惩经历""
			},
			{
				""key"": ""awardedDate"",
				""resourceKey"": ""m.conf.pers.customField.common.awardedDate"",
				""i18n"": ""获得日期""
			},
			{
				""key"": ""companyInOut"",
				""resourceKey"": ""HRCC.conf.custom.honor.companyInOut"",
				""i18n"": ""公司内外""
			},
			{
				""key"": ""honorType"",
				""resourceKey"": ""HRCC.bonusPenalty.type"",
				""i18n"": ""奖惩类型""
			},
			{
				""key"": ""institution"",
				""resourceKey"": ""HRCC.conf.custom.honor.institution"",
				""i18n"": ""机构""
			},
			{
				""key"": ""reason"",
				""resourceKey"": ""m.conf.pers.change.reason"",
				""i18n"": ""原因""
			},
			{
				""key"": ""remark"",
				""resourceKey"": ""common.label.remark"",
				""i18n"": ""备注""
			},
			{
				""key"": ""files"",
				""resourceKey"": ""m.conf.pers.fieldList.file"",
				""i18n"": ""附件""
			}
		]
	},
	{
		""tableName"": ""manager"",
		""columns"": [
			{
				""key"": ""managerType"",
				""resourceKey"": ""m.conf.pers.customField.manager.type"",
				""i18n"": ""经理类型""
			},
			{
				""key"": ""oldManagerPersonId"",
				""resourceKey"": ""m.pers.person.manager.oldManager"",
				""i18n"": ""原经理""
			},
			{
				""key"": ""newManagerPersonId"",
				""resourceKey"": ""m.conf.pers.customField.manager.reportTo"",
				""i18n"": ""汇报给""
			},
			{
				""key"": ""source"",
				""resourceKey"": ""m.conf.pers.customField.manager.source"",
				""i18n"": ""来源""
			}
		]
	},
	{
		""tableName"": ""subordinateManager"",
		""columns"": [
			{
				""key"": ""checkbox"",
				""resourceKey"": ""m.pers.person.manager.subordinate"",
				""i18n"": """"
			},
			{
				""key"": ""personId"",
				""resourceKey"": ""m.conf.pers.customField.manager.type"",
				""i18n"": ""下属""
			},
			{
				""key"": ""managerType"",
				""resourceKey"": ""m.pers.person.manager.newManager"",
				""i18n"": ""经理类型""
			},
			{
				""key"": ""newManagerPersonId"",
				""resourceKey"": ""m.conf.pers.customField.manager.source"",
				""i18n"": ""新的经理""
			},
			{
				""key"": ""source"",
				""resourceKey"": ""m.pers.person.manager.subordinate"",
				""i18n"": ""来源""
			}
		]
	},
	{
		""tableName"": ""newSubordinates"",
		""columns"": [
			{
				""key"": ""personId"",
				""resourceKey"": ""m.conf.pers.customField.manager.type"",
				""i18n"": ""下属""
			},
			{
				""key"": ""managerType"",
				""resourceKey"": ""m.pers.person.manager.oldManager"",
				""i18n"": ""经理类型""
			},
			{
				""key"": ""oldManagerPersonId"",
				""resourceKey"": ""m.conf.pers.change.title"",
				""i18n"": ""原经理""
			}
		]
	},
	{
		""tableName"": ""change"",
		""columns"": [
			{
				""key"": ""changeTypeName"",
				""resourceKey"": ""common.label.effectDate"",
				""i18n"": ""异动类型""
			},
			{
				""key"": ""effectDate"",
				""resourceKey"": ""common.label.expireDate"",
				""i18n"": ""生效日期""
			},
			{
				""key"": ""expireDate"",
				""resourceKey"": ""m.conf.pers.change.changeReason"",
				""i18n"": ""失效日期""
			},
			{
				""key"": ""changeReasonName"",
				""resourceKey"": ""HRCC.setting.customField.change.company"",
				""i18n"": ""异动原因""
			},
			{
				""key"": ""companyName"",
				""resourceKey"": ""m.conf.pers.customField.manager.unit"",
				""i18n"": ""公司""
			},
			{
				""key"": ""unitName"",
				""resourceKey"": ""HRCC.setting.customField.change.jobTitle"",
				""i18n"": ""组织""
			},
			{
				""key"": ""jobTitle"",
				""resourceKey"": ""m.conf.pers.customField.common.job"",
				""i18n"": ""岗位""
			},
			{
				""key"": ""jobName"",
				""resourceKey"": ""m.conf.pers.job.jobLevel"",
				""i18n"": ""职务""
			},
			{
				""key"": ""jobLevelName"",
				""resourceKey"": ""person.contract.code"",
				""i18n"": ""职务级别""
			}
		]
	},
	{
		""tableName"": ""contract"",
		""columns"": [
			{
				""key"": ""code"",
				""resourceKey"": ""person.contract.name"",
				""i18n"": ""合约编号""
			},
			{
				""key"": ""name"",
				""resourceKey"": ""person.contract.startDate"",
				""i18n"": ""合约名称""
			},
			{
				""key"": ""startDate"",
				""resourceKey"": ""person.contract.endDate"",
				""i18n"": ""合约开始时间""
			},
			{
				""key"": ""endDate"",
				""resourceKey"": ""contract.template.type"",
				""i18n"": ""合约结束时间""
			},
			{
				""key"": ""type"",
				""resourceKey"": ""common.label.effectDate"",
				""i18n"": ""合约类型""
			}
		]
	},
	{
		""tableName"": ""salary"",
		""columns"": [
			{
				""key"": ""effectiveStartDate"",
				""resourceKey"": ""HRCC.setting.customField.salary.sumAmount"",
				""i18n"": ""生效日期""
			},
			{
				""key"": ""sumAmount"",
				""resourceKey"": ""HRCC.bonusPenalty.result"",
				""i18n"": ""固定金额合计""
			}
		]
	},
	{
		""tableName"": ""bonusPenalty"",
		""columns"": [
			{
				""key"": ""rewardsPunishmentsSettingName"",
				""resourceKey"": ""HRCC.bonusPenalty.times"",
				""i18n"": ""奖惩结果""
			},
			{
				""key"": ""times"",
				""resourceKey"": ""HRCC.bonusPenalty.fixedAmount"",
				""i18n"": ""次数""
			},
			{
				""key"": ""fixedAmount"",
				""resourceKey"": ""HRCC.bonusPenalty.salaryPercent"",
				""i18n"": ""固定金额""
			},
			{
				""key"": ""applyDate"",
				""resourceKey"": ""HRCC.bonusPenalty.applyDate"",
				""i18n"": ""申请日期""
			},
			{
				""key"": ""beginDate"",
				""resourceKey"": ""common.label.effectDate"",
				""i18n"": ""生效日期""
			},
			{
				""key"": ""endDate"",
				""resourceKey"": ""m.conf.pers.item.expireDate"",
				""i18n"": ""结束日期""
			},
			{
				""key"": ""reason"",
				""resourceKey"": ""HRCC.bonusPenalty.reason"",
				""i18n"": ""奖惩理由""
			}
		]
	},
	{
		""tableName"": ""agreements"",
		""columns"": [
			{
				""key"": ""startDate"",
				""resourceKey"": ""m.conf.pers.customField.common.startDate"",
				""i18n"": ""开始日期""
			},
			{
				""key"": ""endDate"",
				""resourceKey"": ""m.conf.pers.customField.common.endDate"",
				""i18n"": ""结束日期""
			},
			{
				""key"": ""trainingContent"",
				""resourceKey"": ""HRCC.conf.custom.agreement.trainingContent"",
				""i18n"": ""培训内容""
			},
			{
				""key"": ""amount"",
				""resourceKey"": ""HRCC.conf.custom.agreement.amount"",
				""i18n"": ""培训金额""
			},
			{
				""key"": ""remark"",
				""resourceKey"": ""common.label.remark"",
				""i18n"": ""备注""
			}
		]
	},
	{
		""tableName"": ""performances"",
		""columns"": [
			{
				""key"": ""year"",
				""resourceKey"": ""HRCC.conf.custom.performance.year"",
				""i18n"": ""绩效年度""
			},
			{
				""key"": ""evaluateDate"",
				""resourceKey"": ""HRCC.conf.custom.performance.evaluateDate"",
				""i18n"": ""评估日期""
			},
			{
				""key"": ""score"",
				""resourceKey"": ""HRCC.conf.custom.performance.score"",
				""i18n"": ""评分""
			},
			{
				""key"": ""rating"",
				""resourceKey"": ""HRCC.conf.custom.performance.rating"",
				""i18n"": ""评级""
			},
			{
				""key"": ""remark"",
				""resourceKey"": ""common.label.remark"",
				""i18n"": ""备注""
			},
			{
				""key"": ""files"",
				""resourceKey"": ""m.conf.pers.fieldList.file"",
				""i18n"": ""附件""
			}
		]
	},
	{
		""tableName"": ""professionalTitles"",
		""columns"": [
			{
				""key"": ""titleName"",
				""resourceKey"": ""HRCC.conf.custom.professional.titleName"",
				""i18n"": ""职称名""
			},
			{
				""key"": ""titleType"",
				""resourceKey"": ""HRCC.conf.custom.professional.titleType"",
				""i18n"": ""职称类型""
			},
			{
				""key"": ""titleLevel"",
				""resourceKey"": ""HRCC.conf.custom.professional.titleLevel"",
				""i18n"": ""职称级别""
			},
			{
				""key"": ""remark"",
				""resourceKey"": ""HRCC.conf.custom.professional.remark"",
				""i18n"": ""职称描述""
			},
			{
				""key"": ""authorizationDate"",
				""resourceKey"": ""m.conf.pers.customField.common.awardedDate"",
				""i18n"": ""获得日期""
			},
			{
				""key"": ""certificateNo"",
				""resourceKey"": ""m.conf.pers.customField.certificate.certificateNo"",
				""i18n"": ""证书编号""
			}
		]
	},
	{
		""tableName"": ""projects"",
		""columns"": [
			{
				""key"": ""startDate"",
				""resourceKey"": ""m.conf.pers.customField.common.startDate"",
				""i18n"": ""开始日期""
			},
			{
				""key"": ""endDate"",
				""resourceKey"": ""m.conf.pers.customField.common.endDate"",
				""i18n"": ""结束日期""
			},
			{
				""key"": ""projectName"",
				""resourceKey"": ""HRCC.conf.custom.project.name"",
				""i18n"": ""项目名称""
			},
			{
				""key"": ""remark"",
				""resourceKey"": ""HRCC.conf.custom.project.remark"",
				""i18n"": ""项目描述""
			},
			{
				""key"": ""duty"",
				""resourceKey"": ""HRCC.conf.custom.project.duty"",
				""i18n"": ""担任职务""
			},
			{
				""key"": ""url"",
				""resourceKey"": ""HRCC.conf.custom.project.url"",
				""i18n"": ""项目地址""
			},
			{
				""key"": ""manager"",
				""resourceKey"": ""HRCC.conf.custom.project.manager"",
				""i18n"": ""项目负责人""
			}
		]
	}
]";
            var rsCls = JsonConvert.DeserializeObject<cls[]>(str1);
            for (int i = 0; i < rsCls.Length; i++)
            {
                var columns = rsCls[i].columns;
                for (int j = 0; j < columns.Length; j++)
                {
                    Console.WriteLine(columns[j].resourceKey); 
                }
            }
        }
        ///// <summary>
        /////          long[] nums ={ 121151, 100000, 122270 };
        ///// </summary>
        ///// <param name="args"></param>
        //static void Main(string[] args)
        //{
        //    if (args.Length < 2)
        //        return;
        //    if (args[0] == "1")
        //    {
        //        long[] nums = new long[args.Length - 1];
        //        for (int i = 1; i < args.Length; i++)
        //        {
        //            if (long.TryParse(args[i], out long rsLong))
        //            {
        //                nums[i - 1] = rsLong;
        //            }
        //        }
        //        GetConvertOracleNumToDateTime(nums);
        //    }
        //    else if (args[0] == "2")
        //    {
        //        for (int i = 1; i < args.Length; i++)
        //        {
        //            if (DateTime.TryParse(args[i], out DateTime rsDt))
        //            {
        //                Console.WriteLine(args[i] + " " + (rsDt.Year * 1000 + rsDt.DayOfYear - 1900000));
        //            }
        //        }
        //    }
        //}

        public static void GetConvertOracleNumToDateTime(long[] nums)
        {
            int thisYear = DateTime.Now.Year;
            for (int i = 0; i < nums.Length; i++)
            {
                int k = 0;
                while (nums[i] + 1900000 - (thisYear - k) * 1000 < 0)
                {
                    k++;
                }
                while (nums[i] + 1900000 - (thisYear - k) * 1000 > 1000)
                {
                    k--;
                }
                Console.WriteLine(nums[i].ToString() + " " + new DateTime(thisYear - k, 1, 1).AddDays(nums[i] + 1900000 - (thisYear - k) * 1000).ToString("yyyy-MM-dd"));
            }
        }
    }
}

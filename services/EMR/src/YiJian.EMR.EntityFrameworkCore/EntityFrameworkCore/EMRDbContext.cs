using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.Libs.Entities;
using YiJian.EMR.Props.Entities;
using YiJian.EMR.Templates;
using YiJian.EMR.Templates.Entities;
using YiJian.EMR.Writes.Entities;
using YiJian.EMR.XmlHistories.Entities;
using YiJian.EMR.DataElements.Entities;
using YiJian.EMR.DataBinds.Entities;
using YiJian.EMR.ApplicationSettings.Entities;
using YiJian.EMR.DailyExpressions.Entities;
using YiJian.EMR.Characters.Entities;
using YiJian.EMR.EmrPermissions.Entities;
using YiJian.EMR.CloudSign.Entities;

namespace YiJian.EMR.EntityFrameworkCore
{
    [ConnectionStringName(EMRDbProperties.ConnectionStringName)]
    public class EMRDbContext : AbpDbContext<EMRDbContext>, IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        #region 电子病历库模板

        /// <summary>
        /// 电子病历目录树结构[电子病历库]
        /// </summary>
        public DbSet<Catalogue> Catalogues { get; set; }
        /// <summary>
        /// xml模板内容
        /// </summary>
        public DbSet<XmlTemplate> XmlTemplates { get; set; }

        /// <summary>
        /// 合并电子病历模板白名单
        /// </summary>
        public DbSet<MergeTemplateWhiteList> MergeTemplateWhiteLists { get;set;}

        #endregion

        #region 电子病例模板

        /// <summary>
        /// 我的电子病历模板
        /// </summary>
        public DbSet<TemplateCatalogue> TemplateCatalogues { get; set; }
        /// <summary>
        /// 我的电子病历模板
        /// </summary>
        public DbSet<MyXmlTemplate> MyXmlTemplates { get; set; }
        /// <summary>
        /// 病区管理
        /// </summary>
        public DbSet<InpatientWard> InpatientWards { get; set; }
        /// <summary>
        /// 科室历史记录
        /// </summary>
        public DbSet<Department> Departments { get; set; }

        #endregion

        #region 患者电子病历
        /// <summary>
        /// 患者电子病历
        /// </summary>
        public DbSet<PatientEmr> PatientEmrs { get; set; }
        /// <summary>
        /// 患者的电子病历xml文档
        /// </summary>
        public DbSet<PatientEmrXml> PatientEmrXmls { get; set; }
        /// <summary>
        /// 患者的电子病历xml文档
        /// </summary>
        public DbSet<XmlHistory> XmlHistorys { get; set; }

        /*
        /// <summary>
        /// 患者的电子病历的数据存档
        /// </summary>
        public DbSet<PatientEmrData> PatientEmrDatas { get;set; }
        */
        #endregion

        #region 电子病历属性

        /// <summary>
        /// 电子病历属性
        /// </summary>
        public DbSet<CategoryProperty> EmrPropertys { get; set; }

        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定的上下文
        /// </summary>
        public DbSet<DataBindContext> DataBindContexts { get; set; }
        /// <summary>
        /// 数据绑定的字典
        /// </summary>
        public DbSet<DataBindMap> DataBindMaps { get; set; }

        /// <summary>
        /// 电子病例采集的基础信息
        /// </summary>
        public DbSet<EmrBaseInfo> EmrBaseInfos { get; set; }

        #endregion

        #region 数据元

        public DbSet<DataElement> DataElements { get; set; }
        public DbSet<DataElementItem> DataElementItems { get; set; }
        public DbSet<DataElementDropdown> DataElementDropdowns { get; set; }
        public DbSet<DataElementDropdownItem> DataElementDropdownItems { get; set; }

        #endregion

        #region 常用语

        public DbSet<PhraseCatalogue> PhraseCatalogues { get; set; }
        public DbSet<Phrase> Phrases { get; set; }

        #endregion

        #region 通用字符

        public DbSet<UniversalCharacter> UniversalCharacters { get; set; }
        public DbSet<UniversalCharacterNode> UniversalCharacterNodes { get; set; }

        #endregion

        #region Minio数据采集

        /// <summary>
        /// Minio对象存储采集表
        /// </summary>
        public DbSet<MinioEmrInfo> MinioEmrInfos { get; set; } 

        #endregion

        #region 权限配置

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<OperatingAccount> OperatingAccounts { get; set; }

        #endregion
        
        public DbSet<AppSetting> AppSettings { get; set; }

        public DbSet<CloudSignInfo> CloudSignInfos { get; set; }

        public DbSet<ViewVisitSerialEmr> ViewVisitSerialEmrs { get; set; }

        public EMRDbContext(DbContextOptions<EMRDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureEMR();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }
}
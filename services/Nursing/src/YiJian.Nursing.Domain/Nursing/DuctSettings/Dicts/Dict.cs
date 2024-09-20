using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using YiJian.ECIS;

namespace YiJian.Nursing
{
    /// <summary>
    /// 表:导管字典-通用业务（基础配置表）
    /// </summary>
    [Comment("表:导管字典-通用业务（基础配置表）")]
    public class Dict : Entity<Guid>
    {
        /// <summary>
        /// 参数代码
        /// </summary>
        [StringLength(20)]
        [Required]
        [Comment("参数代码")]
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [StringLength(40)]
        [Required]
        [Comment("参数名称")]
        public string ParaName { get; set; }

        /// <summary>
        /// 字典代码
        /// </summary>
        [StringLength(20)]
        [Required]
        [Comment("字典代码")]
        public string DictCode { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        [StringLength(80)]
        [Required]
        [Comment("字典值")]
        public string DictValue { get; set; }

        /// <summary>
        /// 字典值说明
        /// </summary>
        [StringLength(200)]
        [Comment("字典值说明")]
        public string DictDesc { get; set; }

        /// <summary>
        /// 上级代码
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("上级代码")]
        public string ParentId { get; set; }

        /// <summary>
        /// 字典标准（国标、自定义）
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("字典标准（国标、自定义）")]
        public string DictStandard { get; set; }

        /// <summary>
        /// HIS对照代码
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("HIS对照代码")]
        public string HisCode { get; set; }

        /// <summary>
        /// HIS对照
        /// </summary>
        [CanBeNull]
        [StringLength(40)]
        [Comment("HIS对照")]
        public string HisName { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("科室代码")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 模块代码
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("模块代码")]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        [Comment("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        [Comment("是否默认")]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Comment("是否启用")]
        public bool IsEnable { get; set; }

        #region constructor
        /// <summary>
        /// 表:导管字典-通用业务构造器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paraCode">参数代码</param>
        /// <param name="paraName">参数名称</param>
        /// <param name="dictCode">字典代码</param>
        /// <param name="dictValue">字典值</param>
        /// <param name="dictDesc">字典值说明</param>
        /// <param name="parentId">上级代码</param>
        /// <param name="dictStandard">字典标准（国标、自定义）</param>
        /// <param name="hisCode">HIS对照代码</param>
        /// <param name="hisName">HIS对照</param>
        /// <param name="deptCode">科室代码</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="sort">排序</param>
        /// <param name="isDefault">是否默认</param>
        /// <param name="isEnable">是否启用</param>
        public Dict(Guid id,
            [NotNull] string paraCode,    // 参数代码
            [NotNull] string paraName,    // 参数名称
            [NotNull] string dictCode,    // 字典代码
            [NotNull] string dictValue,   // 字典值
            string dictDesc,              // 字典值说明
            string parentId,              // 上级代码
            string dictStandard,          // 字典标准（国标、自定义）
            string hisCode,               // HIS对照代码
            string hisName,               // HIS对照
            string deptCode,              // 科室代码
            string moduleCode,            // 模块代码
            [NotNull] int sort,           // 排序
            bool isDefault,               // 是否默认
            bool isEnable                // 是否启用
            ) : base(id)
        {
            //参数代码
            ParaCode = Check.NotNull(paraCode, "参数代码", DictConsts.MaxParaCodeLength);

            Modify(paraName,// 参数名称
                dictCode,       // 字典代码
                dictValue,      // 字典值
                dictDesc,       // 字典值说明
                parentId,       // 上级代码
                dictStandard,   // 字典标准（国标、自定义）
                hisCode,        // HIS对照代码
                hisName,        // HIS对照
                deptCode,       // 科室代码
                moduleCode,     // 模块代码
                sort,           // 排序
                isDefault,      // 是否默认
                isEnable       // 是否启用
                );
        }
        #endregion

        #region Modify
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <param name="dictCode">字典代码</param>
        /// <param name="dictValue">字典值</param>
        /// <param name="dictDesc">字典值说明</param>
        /// <param name="parentId">上级代码</param>
        /// <param name="dictStandard">字典标准（国标、自定义）</param>
        /// <param name="hisCode">HIS对照代码</param>
        /// <param name="hisName">HIS对照</param>
        /// <param name="deptCode">科室代码</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="sort">排序</param>
        /// <param name="isDefault">是否默认</param>
        /// <param name="isEnable">是否启用</param>
        public void Modify([NotNull] string paraName,// 参数名称
            [NotNull] string dictCode,    // 字典代码
            [NotNull] string dictValue,   // 字典值
            string dictDesc,              // 字典值说明
            string parentId,              // 上级代码
            string dictStandard,          // 字典标准（国标、自定义）
            string hisCode,               // HIS对照代码
            string hisName,               // HIS对照
            string deptCode,              // 科室代码
            string moduleCode,            // 模块代码
            [NotNull] int sort,           // 排序
            bool isDefault,               // 是否默认
            bool isEnable                // 是否启用
            )
        {

            //参数名称
            ParaName = Check.NotNull(paraName, "参数名称", DictConsts.MaxParaNameLength);

            //字典代码
            DictCode = Check.NotNull(dictCode, "字典代码", DictConsts.MaxDictCodeLength);

            //字典值
            DictValue = Check.NotNull(dictValue, "字典值", DictConsts.MaxDictValueLength);

            //字典值说明
            DictDesc = Check.Length(dictDesc, "字典值说明", DictConsts.MaxDictDescLength);

            //上级代码
            ParentId = Check.Length(parentId, "上级代码", DictConsts.MaxParentIdLength);

            //字典标准（国标、自定义）
            DictStandard = Check.Length(dictStandard, "字典标准（国标、自定义）", DictConsts.MaxDictStandardLength);

            //HIS对照代码
            HisCode = Check.Length(hisCode, "HIS对照代码", DictConsts.MaxHisCodeLength);

            //HIS对照
            HisName = Check.Length(hisName, "HIS对照", DictConsts.MaxHisNameLength);

            //科室代码
            DeptCode = Check.Length(deptCode, "科室代码", DictConsts.MaxDeptCodeLength);

            //模块代码
            ModuleCode = Check.Length(moduleCode, "模块代码", DictConsts.MaxModuleCodeLength);

            //排序
            Sort = Check.NotNull(sort, "排序");

            //是否默认
            IsDefault = isDefault;

            //是否启用
            IsEnable = isEnable;

        }
        #endregion

        #region constructor
        private Dict()
        {
            // for EFCore
        }

        #endregion
    }
}

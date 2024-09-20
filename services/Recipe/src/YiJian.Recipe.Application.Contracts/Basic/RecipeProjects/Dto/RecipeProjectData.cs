using System;

namespace YiJian.Basic.RecipeProjects.Dto
{
    public class RecipeProjectData
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 类别编码
        /// </summary>
        /// (Medicine-药品，Examine-检查，Laboratory-检验，CZ-处置，HL-护理，SW-膳食，MZ-麻醉，SS-手术，HZ-会诊，HC-耗材，QT-其他，ZT-嘱托)
        public string CategoryCode { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 源ID
        /// </summary>
        public virtual int SourceId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 学名
        /// </summary>
        public string ScientificName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 类别拼音码
        /// </summary>
        public string CategoryPyCode { get; set; }

        /// <summary>
        /// 类别五笔码
        /// </summary>
        public string CategoryWbCode { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        public string PyCode { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>
        public string WbCode { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 别名拼音码
        /// </summary>
        public string AliasPyCode { get; set; }

        /// <summary>
        /// 别名五笔码
        /// </summary>
        public string AliasWbCode { get; set; }

        /// <summary>
        /// 执行科室编码
        /// </summary>
        public string ExecDeptCode { get; set; }

        /// <summary>
        /// 执行科室
        /// </summary>
        public string ExecDeptName { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 单位价格
        /// </summary>
        public virtual decimal Price { get; set; }

        /// <summary>
        /// 其他价格
        /// </summary>
        public virtual decimal OtherPrice { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// 备注/说明
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 是否可用于院前急救急救
        /// </summary>
        public bool CanUseInFirstAid { get; set; }

        /// <summary>
        /// 药物属性：西药、中药、西药制剂、中药制剂
        /// </summary>
        public string MedicineProperty { get; set; }

        #region 药品属性

        #region 基本(单位)价格

        /// <summary>
        /// 基本单位（药品属性）
        /// </summary>
        public string BasicUnit { get; set; }

        /// <summary>
        /// 基本单位价格（药品属性）
        /// </summary>
        public decimal BasicUnitPrice { get; set; }

        #endregion 基本(单位)价格

        #region 大包装价格

        /// <summary>
        /// 包装价格（药品属性）
        /// </summary>
        public decimal BigPackPrice { get; set; }

        /// <summary>
        /// 大包装量（药品属性）
        /// </summary>
        public int BigPackFactor { get; set; }

        /// <summary>
        /// 包装单位（药品属性）
        /// </summary>
        public string BigPackUnit { get; set; }

        #endregion 大包装价格

        #region 小包装价格

        /// <summary>
        /// 小包装单价（药品属性）
        /// </summary>
        public decimal SmallPackPrice { get; set; }

        /// <summary>
        /// 小包装单位（药品属性）
        /// </summary>
        public string SmallPackUnit { get; set; }

        /// <summary>
        /// 小包装量（药品属性）
        /// </summary>
        public int SmallPackFactor { get; set; }

        #endregion 小包装价格

        /// <summary>
        /// 剂量（药品特有属性）
        /// </summary>
        public virtual decimal DosageQty { get; set; }

        /// <summary>
        /// 剂量单位（药品特有属性）
        /// </summary>
        public string DosageUnit { get; set; }

        /// <summary>
        /// 药房代码（药品属性）
        /// </summary>
        public string PharmacyCode { get; set; }

        /// <summary>
        /// 药房（药品属性）
        /// </summary>
        public string PharmacyName { get; set; }

        /// <summary>
        /// 厂家代码
        /// </summary> 
        public string FactoryCode { get; set; }

        /// <summary>
        /// 厂家
        /// </summary> 
        public string FactoryName { get; set; }

        /// <summary>
        /// 皮试药（药品特有属性）
        /// </summary>
        public virtual bool IsSkinTest { get; set; }

        /// <summary>
        /// 复方药（药品特有属性）
        /// </summary>
        public virtual bool IsCompound { get; set; }

        /// <summary>
        /// 麻醉药（药品特有属性）
        /// </summary>
        public virtual bool IsDrunk { get; set; }

        /// <summary>
        /// 用途/途径编码（药品属性）
        /// </summary>
        public string UsageCode { get; set; }

        /// <summary>
        /// 用途/途径名称（药品属性）
        /// </summary>
        public string UsageName { get; set; }

        /// <summary>
        /// 频次编码（药品属性）
        /// </summary>
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 频次名称（药品属性）
        /// </summary>
        public string FrequencyName { get; set; }

        #endregion

        /// <summary>
        /// 检验目录编码（检查检验属性）
        /// </summary>
        public string CatalogCode { get; set; }

        /// <summary>
        /// 目录分类名称（检查检验属性）
        /// </summary>
        public string CatalogName { get; set; }

        /// <summary>
        /// 位置编码（检查检验属性）
        /// </summary>
        public string PositionCode { get; set; }

        /// <summary>
        /// 位置（检查检验属性）
        /// </summary>
        public string PositionName { get; set; }


        #region 检查属性

        /// <summary>
        /// 检查部位（检查属性）
        /// </summary>
        public virtual string PartName { get; set; }

        /// <summary>
        /// 检查部位编码（检查属性）
        /// </summary>
        public virtual string PartCode { get; set; }

        /// <summary>
        /// 执行机房编码（检查属性）
        /// </summary>
        public virtual string RoomCode { get; set; }

        /// <summary>
        /// 执行机房描述（检查属性）
        /// </summary>
        public virtual string RoomName { get; set; }

        #endregion



        #region 检验属性

        /// <summary>
        /// 标本编码（检验属性）
        /// </summary>
        public string SpecimenCode { get; set; }

        /// <summary>
        /// 标本（检验属性）
        /// </summary>
        public string SpecimenName { get; set; }

        /// <summary>
        /// 容器编码（检验属性）
        /// </summary>
        public string ContainerCode { get; set; }

        /// <summary>
        /// 容器名称（检验属性）
        /// </summary>
        public string ContainerName { get; set; }
        #endregion

        /// <summary>
        /// 附加卡片类型
        /// </summary> 
        public virtual string AddCard { get; set; }
    }
}

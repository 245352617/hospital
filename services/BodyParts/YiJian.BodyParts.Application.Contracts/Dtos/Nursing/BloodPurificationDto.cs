using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 血液净化单查询
    /// </summary>
    public class BloodPurificationDto
    {
        /// <summary>
        /// 置管位置
        /// </summary>
        /// <example></example>
        public string CanulaLocation { get; set; }

        /// <summary>
        /// 血管通路
        /// </summary>
        public string VascularAccess { get; set; }

        /// <summary>
        /// 置管时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 通畅
        /// </summary>
        public string Unobstructed { get; set; }

        /// <summary>
        /// 治疗模式
        /// </summary>
        public string TreatmentMode { get; set; }

        /// <summary>
        /// 抗凝方式
        /// </summary>
        public string Anticoagulation { get; set; }

        /// <summary>
        /// 钙的种类
        /// </summary>
        public string Calcium { get; set; }

        /// <summary>
        /// 净化次数
        /// </summary>
        public string PurificationNumBer { get; set; } = string.Empty;

        /// <summary>
        /// 净化小时数
        /// </summary>
        public string PurificationHours { get; set; } = string.Empty;

        public List<BloodPurificationItem> bpItem { get; set; }
    }


    /// <summary>
    /// PICCO单数据
    /// </summary>
    public class PiccoDto
    {
        /// <summary>
        /// 身高/体重
        /// </summary>
        /// <example></example>
        public string HeightWeight { get; set; }

        /// <summary>
        /// PICCO导管穿刺部位
        /// </summary>
        public string PiccoPart { get; set; }

        /// <summary>
        /// 深静脉穿刺部位
        /// </summary>
        public string DeepveinPart { get; set; }

        public List<PiccoGroup> PiccoGroup { get; set; }
    }

    /// <summary>
    /// Picco分组
    /// </summary>
    public class PiccoGroup
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        public List<BloodPurificationItem> bpItem { get; set; }
    }

    /// <summary>
    /// 参数列表
    /// </summary>
    public class BloodPurificationItem
    {
        /// <summary>
        /// 参数编号
        /// </summary>
        /// <example></example>
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        /// <example></example>
        public string ParaName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string UnitName { get; set; }

        public List<BloodPurificationItems> bpItems { get; set; }
    }

    /// <summary>
    /// 明细值
    /// </summary>
    public class BloodPurificationItems
    {
        /// <summary>
        /// 护理时间
        /// </summary>
        /// <example></example>
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 参数编号
        /// </summary>
        /// <example></example>
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        /// <example></example>
        public string ParaValue { get; set; }

        /// <summary>
        /// 参数值串
        /// </summary>
        /// <example></example>
        public string ParaValueString { get; set; }
    }
}

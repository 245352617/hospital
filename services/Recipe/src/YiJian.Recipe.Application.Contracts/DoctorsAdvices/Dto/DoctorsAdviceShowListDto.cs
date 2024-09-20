using System;

namespace YiJian.DoctorsAdvices.Dto
{
    public class DoctorsAdviceShowListDto
    {
        /// <summary>
        /// 医嘱名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 开嘱时间
        /// </summary>
        public DateTime? ApplyTime { get; set; }

        /// <summary>
        /// 包装规格
        /// </summary> 
        public string Specification { get; set; }

        /// <summary>
        /// 每次剂量
        /// </summary> 
        public string DosageQty { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary> 
        public string DosageUnit { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary> 
        public string UsageName { get; set; }

        /// <summary>
        /// 频次
        /// </summary> 
        public string FrequencyName { get; set; }

        /// <summary>
        /// 医嘱常临类型
        /// </summary>
        public string PrescribeTypeName { get; set; }

        /// <summary>
        /// 每次用量
        /// </summary>
        public decimal? QtyPerTimes { get; set; }

        /// <summary>
        /// 申请医生（开嘱医生）
        /// </summary>
        public string ApplyDoctorName { get; set; }

        /// <summary>
        /// 停嘱时间
        /// </summary> 
        public DateTime? StopDateTime { get; set; }

        /// <summary>
        /// 停嘱医生名称
        /// </summary> 
        public string StopDoctorName { get; set; }

        /// <summary>
        /// 医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行,18=已缴费,19=已退费
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 开药天数
        /// </summary>
        public int LongDays { get; set; }

        /// <summary>
        /// 医嘱号
        /// </summary> 
        public string RecipeNo { get; set; }

        /// <summary>
        /// 医嘱子号（同组下参数修改）
        /// </summary>
        public int RecipeGroupNo { get; set; }
        /// <summary>
        /// 单价
        /// </summary> 
        public decimal Price { get; set; }

        /// <summary>
        /// 总费用
        /// </summary>
        public decimal Amount { get; set; }
    }
}
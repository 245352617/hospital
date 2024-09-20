using System;
using System.Collections.Generic;
using YiJian.Hospitals.Enums;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 医嘱套餐开嘱实体
    /// </summary>
    public class SetMealAdviceV2Dto
    {
        /// <summary>
        /// 患者信息（用来处理儿童价问题的）
        /// </summary>
        public PatientInfoDto PatientInfo { get; set; }

        /// <summary>
        /// 医嘱基础信息
        /// </summary>
        public DoctorsAdviceRequestDto DoctorsAdvice { get; set; }

        /// <summary>
        /// 套餐Id
        /// </summary>
        public Guid PackageId { get; set; }

        /// <summary>
        /// 需要筛选的项目Id集合
        /// </summary>
        public List<SetMealAdviceEntry> Entries { get; set; } = new List<SetMealAdviceEntry>();
    }

    /// <summary>
    /// 医嘱套餐开嘱实体明细
    /// </summary>
    public class SetMealAdviceEntry
    {
        /// <summary>
        /// 套餐 EntryId
        /// </summary>
        /// <example>1</example>
        public int EntryId { get; set; }

        /// <summary>
        /// 皮试选择结果
        /// </summary>
        /// <example>1</example>
        public ESkinTestSignChoseResult? SkinTestSignChoseResult { get; set; }

        /// <summary>
        /// 限制用药标记 “1.限制用药 2.非限制用药” 出现1.限制用药需要弹出 限制费用提示消息
        /// </summary>
        /// <example>1</example>
        public int LimitType { get; set; }

        /// <summary>
        /// 收费类型
        /// </summary>  
        public ERestrictedDrugs? RestrictedDrugs { get; set; }

        /// <summary>
        /// 执行科室编码
        /// </summary>
        /// <example>1001</example>
        public string ExecDeptCode { get; set; }

        /// <summary>
        /// 执行科室
        /// </summary>
        /// <example>耳鼻喉科</example>
        public string ExecDeptName { get; set; }

        /// <summary>
        /// 疫苗接种记录信息，只有当 toxicLevel=7时，传过来
        /// </summary>
        public ImmunizationRecordDto ImmunizationRecord { get; set; }

    }
}

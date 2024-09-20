using System;
using System.ComponentModel.DataAnnotations;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 医嘱列表查询
    /// </summary>
    public class QueryDoctorsAdviceDto
    {
        /// <summary>
        /// 患者年龄参数，用来检查是否是儿童
        /// </summary>
        public PatientInfoDto PatientInfo { get; set; }

        /// <summary>
        /// 患者唯一标识
        /// </summary> 
        public Guid PIID { get; set; }

        /// <summary>
        /// 系统标识:0=急诊，1=院前 
        /// </summary> 
        [Required(ErrorMessage = "系统标识必传，0=急诊，1=院前")]
        public EPlatformType PlatformType { get; set; } = EPlatformType.EmergencyTreatment;

        /// <summary>
        /// 医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行,8=已缴费,9=已退费
        /// </summary> 
        public ERecipeStatus? Status { get; set; }

        /// <summary>
        /// 医嘱类型：临嘱、长嘱
        /// </summary> 
        public string PrescribeTypeCode { get; set; }

        /// <summary>
        /// 查询时间
        /// </summary>
        public EApplyType? ApplyType { get; set; }

        /// <summary>
        ///  医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)
        /// </summary> 
        public string CategoryCode { get; set; }

        /// <summary>
        /// 医嘱各项分类: 0=药方项,1=检查项,2=检验项,3=诊疗项
        /// </summary>
        public EDoctorsAdviceItemType? ItemType { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 打开附加项展示
        /// </summary>
        public bool? CloseAdditionalTreat { get; set; } = true;

        /// <summary>
        /// 获取开嘱时间查询的开始时间
        /// </summary> 
        /// <returns></returns>
        public DateTime? GetApplyBeginTime()
        {
            if (!ApplyType.HasValue) return null;

            switch (ApplyType.Value)
            {
                case EApplyType.H24:
                    return DateTime.Now.AddDays(-1);
                case EApplyType.H48:
                    return DateTime.Now.AddDays(-2);
                case EApplyType.H72:
                    return DateTime.Now.AddDays(-3);
                default:
                    break;
            }
            return null;
        }

    }
}

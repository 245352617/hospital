using YiJian.DoctorsAdvices.Enums;

namespace YiJian.ChargeBills.Dto
{
    /// <summary>
    /// 医嘱记录单
    /// </summary>
    public class AdviceBillsDto
    {
        /// <summary>
        /// 医嘱名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 医嘱各项分类: 0=药方项,1=检查项,2=检验项,3=诊疗项
        /// </summary>
        public EDoctorsAdviceItemType ItemType { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行
        /// </summary>
        public ERecipeStatus Status { get; set; }

        /// <summary>
        /// 是否已支付
        /// </summary> 
        public bool IsPaid { get; set; }

    }
}

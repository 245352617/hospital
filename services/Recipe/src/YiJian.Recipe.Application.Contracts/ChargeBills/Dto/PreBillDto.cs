using System.Collections.Generic;
using System.Linq;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.ChargeBills.Dto
{
    /// <summary>
    /// 院前收款单
    /// </summary>
    public class PreBillDto
    {
        /// <summary>
        /// 收费单分类
        /// </summary>
        public List<PreBillCategoryDto> BillCategory { get; set; } = new List<PreBillCategoryDto>();

        /// <summary>
        /// 应收
        /// </summary>
        public decimal Receivable
        {
            get
            {
                return SubmitTotal;
            }
        }

        /// <summary>
        /// 已收
        /// </summary>
        public decimal Received
        {
            get
            {
                return BillCategory.Sum(s => s.PaidAmount);
            }
        }

        /// <summary>
        /// 支付类型 , 0=待支付,1=已支付,2=部分支付  
        /// </summary>
        public EPayStatus PayStatus
        {
            get
            {
                if (SubmitTotal == Received) return EPayStatus.HavePaid;
                if (Received > 0 && SubmitTotal > Received) return EPayStatus.PartialPayment;
                return EPayStatus.NoPayment;
            }
        }

        /// <summary>
        /// 合计（已提交）
        /// </summary>
        public decimal SubmitTotal { get; set; }

        /// <summary>
        /// 未提交的合计
        /// </summary>
        public decimal NoSubmitTotal { get; set; }

    }
}

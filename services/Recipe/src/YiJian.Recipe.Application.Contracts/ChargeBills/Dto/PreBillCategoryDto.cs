using System.Collections.Generic;
using System.Linq;

namespace YiJian.ChargeBills.Dto
{
    /// <summary>
    /// 院前收费单分类
    /// </summary>
    public class PreBillCategoryDto
    {
        /// <summary>
        /// 收费类型编码
        /// </summary> 
        public string ChargeCode { get; set; }

        /// <summary>
        /// 收费类型名称
        /// </summary> 
        public string ChargeName { get; set; }

        /// <summary>
        /// 收费单明细
        /// </summary>
        public List<AdviceBillsDto> AdviceBill { get; set; } = new List<AdviceBillsDto>();

        /// <summary>
        /// 分类小计
        /// </summary>
        public decimal Amount
        {
            get
            {
                if (AdviceBill.Count > 0)
                {
                    return AdviceBill.Sum(s => s.Price);
                }
                return 0;
            }
        }

        /// <summary>
        /// 已支付的金额
        /// </summary>
        public decimal PaidAmount
        {
            get
            {
                return AdviceBill.Where(w => w.IsPaid).Sum(s => s.Price);
            }
        }

        /// <summary>
        /// 未支付的金额
        /// </summary>
        public decimal NoPaymentAmount
        {
            get
            {
                return Amount - PaidAmount;
            }
        }

    }
}

using System;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Recipes.DoctorsAdvices.Entities
{
    public class DoctorsAdvicePartial
    {
        private DoctorsAdvicePartial()
        {
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public string InsuranceCode { get; set; }
        public EInsuranceCatalog InsuranceType { get; set; }
        public string PayTypeCode { get; set; }
        public ERecipePayType PayType { get; set; }
        public string PrescriptionNo { get; set; }
        public string RecipeNo { get; set; }
        public int RecipeGroupNo { get; set; }
        public DateTime? ApplyTime { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public bool IsBackTracking { get; set; }
        public bool IsRecipePrinted { get; set; }
        public string HisOrderNo { get; set; }
        public string ExecDeptCode { get; set; }
        public string ExecDeptName { get; set; }
        public string Remarks { get; set; }

        /// <summary>
        /// 收费类型编码
        /// </summary> 
        public string ChargeCode { get; set; }

        /// <summary>
        /// 收费类型名称
        /// </summary> 
        public string ChargeName { get; set; }

        /// <summary>
        /// 医嘱类型编码
        /// </summary> 
        public string PrescribeTypeCode { get; set; }

        /// <summary>
        /// 医嘱类型：临嘱、长嘱、出院带药等
        /// </summary> 
        public string PrescribeTypeName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary> 
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary> 
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 领量(数量)
        /// </summary> 
        public decimal RecieveQty { get; set; }

        /// <summary>
        /// 领量单位
        /// </summary> 
        public string RecieveUnit { get; set; }

    }
}

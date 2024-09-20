using System.ComponentModel.DataAnnotations;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 更新医嘱单记录状态参数
    /// </summary>
    public class UpdateRecordStatusDto
    {
        /// <summary>
        /// 流水号
        /// </summary>
        [Required(ErrorMessage = "就诊流水号必填")]
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 渠道订单序号
        /// </summary>
        [Required(ErrorMessage = "渠道订单序号必填")]
        public string ChannelBillId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [Required(ErrorMessage = "就诊患者姓名必填")]
        public string PatientName { get; set; }

        /// <summary>
        /// His订单序号
        /// </summary>
        [Required(ErrorMessage = "His订单序号必填")]
        public string HisBillId { get; set; }

        /// <summary>
        /// 就诊科室
        /// </summary>
        [Required(ErrorMessage = "就诊科室必填")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 就诊医生编号
        /// </summary>
        [Required(ErrorMessage = "就诊医生编号必填")]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 订单状态 0.未缴费 1.已缴费  -1.已作废  2.已执行
        /// </summary>
        [Required(ErrorMessage = "返回状态必填")]
        public int BillState { get; set; }


    }


}

namespace YiJian.Recipes.GroupConsultation
{
    using System;
    using YiJian.ECIS.ShareModel.Models;

    /// <summary>
    /// 会诊管理 分页排序输入
    /// </summary>
    [Serializable]
    public class GetGroupConsultationPagedInput : PageBase
    {
        /// <summary>
        /// 分诊患者id
        /// </summary>
        public Guid PI_ID { get; set; }


        /// <summary>
        /// 患者id
        /// </summary>
        public string PatientId { get; set; }


        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }


        /// <summary>
        ///结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 申请科室编码
        /// </summary>
        public string ApplyDeptCode { get; set; }

        /// <summary>
        /// 申请人编码
        /// </summary>
        public string ApplyCode { get; set; }


        /// <summary>
        /// 受邀医生编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 受邀科室编码
        /// </summary>
        public string DeptCode { get; set; }


        /// <summary>
        /// 会诊状态
        /// </summary>
        public GroupConsultationStatus Status { get; set; }
    }
}
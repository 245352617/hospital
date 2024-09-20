using YiJian.Recipes.GroupConsultation;

namespace YiJian.Recipes.InviteDoctor
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 会诊邀请医生 修改输入
    /// </summary>
    [Serializable]
    public class InviteDoctorUpdate
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 会诊id
        /// </summary>
        public Guid GroupConsultationId { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "医生编码不能为空！")]
        public string Code { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        [StringLength(100)]
        [Required(ErrorMessage = "医生名称不能为空！")]
        public string Name { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "科室编码不能为空！")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        [StringLength(100)]
        [Required(ErrorMessage = "科室名称不能为空！")]
        public string DeptName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [StringLength(20)]
        public string Mobile { get; set; }
        /// <summary>
        /// 状态，0：已邀请，1：已报到
        /// </summary>
        public CheckInStatus CheckInStatus { get; set; }
        /// <summary>
        /// 医生职称
        /// </summary>
        public string DoctorTitle { get; set; }

        /// <summary>
        /// 报道时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }

        /// <summary>
        /// 意见
        /// </summary>
        public string Opinion { get; set; }
    }
}
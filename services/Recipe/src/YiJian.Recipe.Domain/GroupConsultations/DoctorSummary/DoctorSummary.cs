using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace YiJian.Recipe
{
    /// <summary>
    /// 急诊医生会议纪要
    /// </summary>
    [Comment("急诊医生会议纪要")]
    public class DoctorSummary : Entity<Guid>
    {
        /// <summary>
        /// 会诊id
        /// </summary>
        [Comment("会诊id")]
        public Guid GroupConsultationId { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary>
        [StringLength(50)]
        [Required]
        [Comment("医生编码")]
        public string Code { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        [StringLength(100)]
        [Required]
        [Comment("医生名称")]
        public string Name { get; set; }

        /// <summary>
        /// 医生职称
        /// </summary>
        [Comment("医生职称")]
        public string DoctorTitle { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [StringLength(20)]
        [Comment("联系电话")]
        public string Mobile { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        [StringLength(50)]
        [Required]
        [Comment("科室编码")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>

        [StringLength(100)]
        [Required]
        [Comment("科室名称")]
        public string DeptName { get; set; }

        /// <summary>
        /// 报道时间
        /// </summary>
        [Comment("报道时间")]
        public DateTime? CheckInTime { get; set; }

        /// <summary>
        /// 意见
        /// </summary>
        [StringLength(500)]
        [Comment("意见")]
        public string Opinion { get; set; }
        #region constructor

        /// <summary>
        /// 会诊纪要医生构造器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="groupConsultationId">会诊id</param>
        /// <param name="code">医生编码</param>
        /// <param name="name">医生名称</param>
        /// <param name="deptCode">科室编码</param>
        /// <param name="deptName">科室名称</param>
        /// <param name="checkInTime">报道时间</param>
        /// <param name="opinion">意见</param>
        /// <param name="doctorTitle"></param>
        /// <param name="mobile"></param>
        public DoctorSummary(Guid id,
            Guid groupConsultationId, // 会诊id
            [NotNull] string code, // 医生编码
            [NotNull] string name, // 医生名称
            [NotNull] string deptCode, // 科室编码
            [NotNull] string deptName, // 科室名称
            DateTime? checkInTime, // 报道时间
            string opinion, // 意见
            string doctorTitle // 医生职称
            , string mobile
        ) : base(id)
        {
            //会诊id
            GroupConsultationId = groupConsultationId;

            Modify(code, name, deptCode, deptName, doctorTitle,
                checkInTime, // 报道时间
                opinion// 意见
                , mobile
            );
        }

        #endregion

        #region ModifyDoctor

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="code">医生编码</param>
        /// <param name="name">医生名称</param>
        /// <param name="deptCode">科室编码</param>
        /// <param name="deptName">科室名称</param> 
        /// <param name="checkInTime">报道时间</param>
        /// <param name="opinion">意见</param>
        /// <param name="mobile"></param>
        /// <param name="doctorTitle"></param>  
        public void Modify(
             [NotNull] string code, // 医生编码
            [NotNull] string name, // 医生名称
            [NotNull] string deptCode, // 科室编码
            [NotNull] string deptName, // 科室名称

             string doctorTitle, // 医生职称
            DateTime? checkInTime, // 报道时间
            string opinion, string mobile// 意见
        )
        {
            //医生编码
            Code = Check.NotNull(code, nameof(code), maxLength: 50);

            //医生名称
            Name = Check.NotNull(name, nameof(name), maxLength: 100);

            //科室编码
            DeptCode = Check.NotNull(deptCode, nameof(deptCode), maxLength: 50);

            //科室名称
            DeptName = Check.NotNull(deptName, nameof(deptName), maxLength: 100);

            DoctorTitle = doctorTitle;
            //报道时间
            CheckInTime = checkInTime;
            Mobile = mobile;
            //意见
            Opinion = Check.Length(opinion, "意见", maxLength: 500);
        }

        #endregion
    }
}

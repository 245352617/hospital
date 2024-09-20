using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using YiJian.ECIS;
using YiJian.Recipes.GroupConsultation;

namespace YiJian.Recipes.InviteDoctor
{
    /// <summary>
    /// 会诊邀请医生
    /// </summary>
    [Comment("会诊邀请医生")]
    public class InviteDoctor : Entity<Guid>
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
        /// 状态，0：已邀请，1：已报到
        /// </summary>
        [Comment("状态，0：已邀请，1：已报到")]
        public CheckInStatus CheckInStatus { get; set; }

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
        /// 会诊邀请医生构造器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="groupConsultationId">会诊id</param>
        /// <param name="code">医生编码</param>
        /// <param name="name">医生名称</param>
        /// <param name="deptCode">科室编码</param>
        /// <param name="deptName">科室名称</param>
        /// <param name="checkInStatus">状态，0：已邀请，1：已报到</param>
        /// <param name="checkInTime">报道时间</param>
        /// <param name="opinion">意见</param>
        /// <param name="doctorTitle"></param>
        /// <param name="mobile"></param>
        public InviteDoctor(Guid id,
            Guid groupConsultationId, // 会诊id
            [NotNull] string code, // 医生编码
            [NotNull] string name, // 医生名称
            [NotNull] string deptCode, // 科室编码
            [NotNull] string deptName, // 科室名称
            CheckInStatus checkInStatus, // 状态，0：已邀请，1：已报到
            DateTime? checkInTime, // 报道时间
            string opinion, // 意见
            string doctorTitle, // 医生职称
            string mobile
        ) : base(id)
        {
            //会诊id
            GroupConsultationId = groupConsultationId;

            ModifyDoctor(code, name, deptCode, deptName, doctorTitle,
                checkInStatus, // 状态，0：已邀请，1：已报到
                checkInTime, // 报道时间
                opinion, // 意见
                mobile
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
        /// <param name="checkInStatus">状态，0：已邀请，1：已报到</param>
        /// <param name="checkInTime">报道时间</param>
        /// <param name="opinion">意见</param>
        /// <param name="doctorTitle"></param>  
        /// <param name="mobile"></param>
        public void ModifyDoctor(
             [NotNull] string code, // 医生编码
            [NotNull] string name, // 医生名称
            [NotNull] string deptCode, // 科室编码
            [NotNull] string deptName, // 科室名称
             string doctorTitle, // 医生职称
            CheckInStatus checkInStatus, // 状态，0：已邀请，1：已报到
            DateTime? checkInTime, // 报道时间
            string opinion, // 意见
           string mobile
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

            Mobile = mobile;
            Modify(
               checkInStatus, // 状态，0：已邀请，1：已报到
               checkInTime, // 报道时间
               opinion// 意见
           );
        }

        #endregion

        #region Modify

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="checkInStatus">状态，0：已邀请，1：已报到</param>
        /// <param name="checkInTime">报道时间</param>
        /// <param name="opinion">意见</param>
        public void Modify(
            CheckInStatus checkInStatus, // 状态，0：已邀请，1：已报到
            DateTime? checkInTime, // 报道时间
            string opinion// 意见
        )
        {
            //状态，0：已邀请，1：已报到
            CheckInStatus = checkInStatus;

            //报道时间
            CheckInTime = checkInTime;

            //意见
            Opinion = Check.Length(opinion, "意见", maxLength: 500);

        }

        #endregion

        #region constructor

        InviteDoctor()
        {
            // for EFCore
        }

        #endregion
    }
}
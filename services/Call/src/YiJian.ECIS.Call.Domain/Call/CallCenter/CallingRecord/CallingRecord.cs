using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using YiJian.ECIS.ShareModel.Utils;

namespace YiJian.ECIS.Call.Domain.CallCenter
{
    /// <summary>
    /// 叫号记录
    /// </summary>
    [Comment("叫号记录")]
    public class CallingRecord : Entity<Guid>, ICreationAuditedObject, IHasCreationTime, IMayHaveCreator
    {
        /// <summary>
        /// 叫号信息id
        /// </summary>
        [Comment("叫号信息id")]
        public virtual Guid CallInfoId { get; private set; }

        /// <summary>
        /// 创建时间（叫号时间）
        /// </summary>
        [Comment("创建时间（叫号时间）")]
        public DateTime CreationTime { get; private set; }

        /// <summary>
        /// 创建人id（叫号医生/护士）
        /// </summary>
        [Comment("创建人id（叫号医生/护士）")]
        public Guid? CreatorId { get; private set; }

        /// <summary>
        /// 急诊医生id
        /// </summary>
        [Comment("急诊医生id")]
        [StringLength(60)]
        public virtual string DoctorId { get; internal set; }

        /// <summary>
        /// 急诊医生姓名
        /// </summary>
        [Comment("急诊医生姓名")]
        [StringLength(60)]
        public virtual string DoctorName { get; internal set; }

        /// <summary>
        /// 就诊诊室编码
        /// </summary>
        [Comment("就诊诊室编码")]
        public virtual string ConsultingRoomCode { get; internal set; }

        /// <summary>
        /// 就诊诊室名称
        /// </summary>
        [Comment("就诊诊室名称")]
        [StringLength(60)]
        public virtual string ConsultingRoomName { get; internal set; }

        /// <summary>
        /// 就诊状态（0: 未就诊; 1: 已就诊）
        /// </summary>
        [Comment("就诊状态")]
        [DefaultValue(ETreatStatus.UnTreated)]
        public virtual ETreatStatus TreatStatus { get; private set; }

        /// <summary>
        /// 叫号患者信息
        /// </summary>
        public virtual CallInfo CallInfo { get; private set; }

        /// <summary>
        /// 创建叫号记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="callInfoId">叫号信息id</param>
        /// <param name="doctorId">急诊医生id</param>
        /// <param name="doctorName">急诊医生姓名</param>
        /// <param name="consultingRoomCode">就诊诊室编码</param>
        /// <param name="consultingRoomName">就诊诊室名称</param>
        public CallingRecord([NotNull] Guid id, [NotNull] Guid callInfoId, string doctorId, string doctorName, [NotNull] string consultingRoomCode, [NotNull] string consultingRoomName)
        {
            this.Id = EcisCheck.NotNullOrEmpty(id, nameof(Id));
            this.CallInfoId = EcisCheck.NotNullOrEmpty(callInfoId, nameof(CallInfoId));
            // 诊室不能为空，某些特殊情况下医生有可能为空
            this.ConsultingRoomCode = Check.NotNullOrEmpty(consultingRoomCode, nameof(ConsultingRoomCode));
            this.ConsultingRoomName = Check.NotNullOrEmpty(consultingRoomName, nameof(ConsultingRoomName));

            this.CreationTime = DateTime.Now;
            this.DoctorId = doctorId;
            this.DoctorName = doctorName;
        }

        /// <summary>
        /// 接诊已经就诊
        /// </summary>
        /// <returns></returns>
        public CallingRecord Treated()
        {
            this.TreatStatus = ETreatStatus.Treated;
            return this;
        }
    }
}

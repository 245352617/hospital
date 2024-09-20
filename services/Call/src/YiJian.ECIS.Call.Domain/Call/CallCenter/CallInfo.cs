using System;
using System.ComponentModel;
using JetBrains.Annotations;
using System.Collections.Generic;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Utils;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace YiJian.ECIS.Call.Domain.CallCenter
{
    /// <summary>
    /// 【叫号信息】领域实体
    /// </summary>
    [Comment("叫号信息")]
    public class CallInfo : FullAuditedAggregateRoot<Guid>
    {

        #region 排队信息

        /// <summary>
        /// 登记时间
        /// </summary>
        [Comment("登记时间")]
        public virtual DateTime? LogTime { get; private set; }

        /// <summary>
        /// 排队日期（非自然日，按工作日时间计算，当天起始时间按叫号设置的时间计算）
        /// 按取号（分配排队号）的时间计算
        /// </summary>
        [Comment("排队日期（工作日计算）")]
        public virtual DateTime? LogDate { get; private set; }

        /// <summary>
        /// 排队号
        /// </summary>
        [Comment("排队号")]
        [StringLength(20)]
        public virtual string CallingSn { get; private set; }

        /// <summary>
        /// 开始排队时间（取号时间）
        /// </summary>
        [Comment("开始排队时间")]
        public virtual DateTime? InCallQueueTime { get; private set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        [Comment("是否置顶")]
        [DefaultValue(false)]
        [Required]
        public virtual bool IsTop { get; private set; }

        /// <summary>
        /// 置顶时间
        /// </summary>
        [Comment("置顶时间")]
        public virtual DateTime? TopTime { get; private set; }

        /// <summary>
        /// 叫号状态（0: 未叫号; 1: 叫号中; 2: 暂停中  3: 已叫号 已就诊 4: 已经过号 5：作废，失效 ）
        /// </summary>
        [Comment("叫号状态")]
        [DefaultValue(CallStatus.NotYet)]
        public virtual CallStatus CallStatus { get; private set; }

        /// <summary>
        /// 叫号时间
        /// </summary>
        [Comment("叫号时间")]
        public virtual DateTime? LastCalledTime { get; private set; }

        #endregion

        /// <summary>
        /// 指定急诊医生Code
        /// </summary>
        [Comment("急诊医生Code")]
        [StringLength(60)]
        public virtual string DoctorCode { get; private set; }

        /// <summary>
        /// 指定急诊医生姓名
        /// </summary>
        [Comment("急诊医生姓名")]
        [StringLength(60)]
        public virtual string DoctorName { get; private set; }

        /// <summary>
        /// 叫号急诊医生Code
        /// </summary>
        [Comment("急诊医生Code")]
        [StringLength(60)]
        public virtual string CallingDoctorCode { get; private set; }

        /// <summary>
        /// 叫号急诊医生姓名
        /// </summary>
        [Comment("急诊医生姓名")]
        [StringLength(60)]
        public virtual string CallingDoctorName { get; private set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        [Comment("患者唯一标识(HIS)")]
        [StringLength(50)]
        [Required]
        public virtual string PatientID { get; private set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [Comment("患者姓名")]
        [StringLength(50)]
        [Required]
        public virtual string PatientName { get; private set; }

        /// <summary>
        /// 实际分诊级别
        /// </summary>
        [Comment("患者实际分诊级别")]
        [StringLength(60)]
        public virtual string ActTriageLevel { get; private set; }

        /// <summary>
        /// 实际分诊级别名称
        /// </summary>
        [Comment("患者实际分诊级别名称")]
        [StringLength(60)]
        public virtual string ActTriageLevelName { get; private set; }

        /// <summary>
        /// 分诊科室编码
        /// </summary>
        [Comment("患者分诊科室编码")]
        [StringLength(60)]
        public virtual string TriageDept { get; private set; }

        /// <summary>
        /// 分诊科室名称
        /// </summary>
        [Comment("患者分诊科室名称")]
        [StringLength(60)]
        public virtual string TriageDeptName { get; private set; }

        /// <summary>
        /// 就诊号
        /// 挂号之后生成就诊号
        /// </summary>
        [Comment("就诊号")]
        [StringLength(50)]
        public virtual string RegisterNo { get; private set; }

        /// <summary>
        /// 是否显示 (叫号大屏幕) IsActive  是否激活
        /// </summary>
        [Comment("是否显示")]
        public bool IsShow { get; set; }

        /// <summary>
        /// 是否是查看报告
        /// </summary>
        [Comment("是否是查看报告")]
        [DefaultValue(false)]
        public bool IsReport { get; set; }

        /// <summary>
        /// 分诊去向Code
        /// </summary>
        [Comment("分诊去向Code")]
        [StringLength(60)]
        public string TriageTarget { get; set; }

        /// <summary>
        /// 分诊去向Name
        /// </summary>
        [Comment("分诊去向Name")]
        [StringLength(60)]
        public string TriageTargetName { get; set; }

        /// <summary>
        /// 查看报告入队的时间
        /// </summary>
        //[Comment("查看报告入队的时间")]
        //public virtual DateTime? ReportTime { get; private set; }

        /// <summary>
        /// 分诊科室名称 （顺呼之后产生）
        /// </summary>
        public string ConsultingRoomName { get; private set; }

        /// <summary>
        /// 分诊科室编码 （顺呼之后产生）
        /// </summary>
        public string ConsultingRoomCode { get; private set; }

        /// <summary>
        /// 叫号记录
        /// </summary>
        public virtual ICollection<CallingRecord> CallingRecords { get; private set; }

        private CallInfo()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patientID">患者 ID</param>
        /// <param name="patientName">患者名称</param>
        public CallInfo(
            Guid id,
            [CanBeNull] string patientID = null,
            [CanBeNull] string patientName = null)
        {
            this.Id = EcisCheck.NotNullOrEmpty(id, nameof(Id));
            this.PatientID = Check.NotNullOrEmpty(patientID, nameof(PatientID));
            this.PatientName = Check.NotNullOrEmpty(patientName, nameof(PatientName), maxLength: 50);

            // 设置叫号登记时间为当前时间
            this.LogTime = DateTime.Now;
        }

        /// <summary>
        /// 挂号
        /// </summary>
        /// <param name="registerNo">挂号注册号</param>
        /// <returns></returns>
        public CallInfo Register([CanBeNull] string registerNo = null)
        {
            this.RegisterNo = registerNo;
            return this;
        }

        public CallInfo UpdatePatientInfo(
            [CanBeNull] string patientID = null,
            [CanBeNull] string patientName = null)
        {
            this.PatientID = patientID;
            this.PatientName = patientName;
            return this;
        }

        /// <summary>
        /// 关联分诊信息
        /// </summary>
        /// <param name="actTriageLevel">患者实际分诊级别</param>
        /// <param name="actTriageLevelName">患者实际分诊级别名称</param>
        /// <returns></returns>
        public CallInfo LinkConsequenceInfo(
            [CanBeNull] string actTriageLevel = null,
            [CanBeNull] string actTriageLevelName = null)
        {
            this.ActTriageLevel = actTriageLevel;
            this.ActTriageLevelName = actTriageLevelName;
            return this;
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        /// <param name="isShow">是否显示</param>
        /// <returns></returns>
        public CallInfo SetVisible(bool isShow)
        {
            this.IsShow = isShow;
            return this;
        }

        /// <summary>
        /// 分配排队号
        /// </summary>
        /// <param name="callingSn">排队号</param>
        /// <param name="logDate">登记日期</param>
        /// <returns></returns>
        public CallInfo SetCallingSn([NotNull] string callingSn, [NotNull] DateTime logDate)
        {
            this.CallingSn = Check.NotNullOrEmpty(callingSn, nameof(CallingSn));
            this.LogDate = Check.NotNull(logDate, nameof(LogDate));
            this.InCallQueueTime = DateTime.Now;
            this.LogTime = DateTime.Now;
            return this;
        }

        /// <summary>
        /// 设置入队列时间
        /// </summary>
        /// <param name="queueTime"></param>
        /// <param name="logTime"></param>
        /// <returns></returns>
        public CallInfo SetQueueTime(DateTime? queueTime, DateTime? logTime)
        {
            this.InCallQueueTime = queueTime;
            this.LogTime = logTime;
            return this;
        }
        /// <summary>
        /// 置顶
        /// </summary>
        /// <returns></returns>
        public CallInfo MoveToTop()
        {
            this.IsTop = true;
            this.TopTime = DateTime.Now;

            return this;
        }


        /// <summary>
        /// 给当前用户设置暂停
        /// </summary>
        /// <returns></returns>
        public CallInfo SetPause()
        {
            CallStatus = CallStatus.Pause;
            return this;
        }


        /// <summary>
        /// 取消暂停
        /// </summary>
        /// <returns></returns>
        public CallInfo CancelPause()
        {
            CallStatus = CallStatus.NotYet;
            return this;
        }

        /// <summary>
        /// 设置看报告
        /// </summary>
        /// <returns></returns>
        public CallInfo SetLookReport()
        {
            IsReport = true;
            //ReportTime = DateTime.Now;
            return this;
        }

        /// <summary>
        /// 取消查看报告
        /// </summary>
        /// <returns></returns>
        public CallInfo CancelLookReport()
        {
            IsReport = false;
            //ReportTime = null;
            return this;
        }

        /// <summary>
        /// 取消置顶
        /// </summary>
        /// <returns></returns>
        public CallInfo CancelMoveToTop()
        {
            this.IsTop = false;
            this.TopTime = null;

            return this;
        }

        /// <summary>
        /// 正在叫号
        /// </summary>
        /// <param name="doctorId">医生 ID</param>
        /// <param name="doctorName">医生名称</param>
        /// <param name="consultingRoomCode">诊室编码</param>
        /// <param name="consultingRoomName">诊室名称</param>
        /// <param name="justReCall">是否重呼</param>
        /// <returns></returns>
        public CallInfo NowCalling([NotNull] string doctorId, [NotNull] string doctorName, [NotNull] string consultingRoomCode, [NotNull] string consultingRoomName,bool justReCall=false)
        {
            if (!justReCall)
            {
                CallStatus = CallStatus.Calling;  // 正在叫号
                CallingDoctorCode = doctorId;
                CallingDoctorName = doctorName;
                ConsultingRoomCode = consultingRoomCode;
                ConsultingRoomName = consultingRoomName;
            }
            LastCalledTime = DateTime.Now;
        

            // 发布正在叫号的领域事件
            AddLocalEvent(new CallingEvent
            {
                CallInfoId = this.Id,
                DoctorName = Check.NotNull(doctorName, nameof(this.CallingDoctorName)),
                DoctorId = doctorId,
                ConsultingRoomCode = consultingRoomCode,
                ConsultingRoomName = consultingRoomName
            });

            return this;
        }

        /// <summary>
        /// 设置医生
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="doctorName"></param>
        /// <returns></returns>
        public CallInfo SetDoctor( string doctorId, string doctorName)
        {
            this.DoctorCode = doctorId; // 更新当前顺呼的医生
            this.DoctorName = doctorName;
            return this;
        }

        /// <summary>
        /// 设置叫号医生
        /// </summary>
        /// <param name="callingDoctorId"></param>
        /// <param name="callingDoctorName"></param>
        /// <returns></returns>
        public CallInfo SetCallingDoctor(string callingDoctorId, string callingDoctorName)
        {
            this.CallingDoctorCode = callingDoctorId; 
            this.CallingDoctorName = callingDoctorName;
            return this;
        }

        /// <summary>
        /// 取消叫号
        /// </summary>
        /// <returns></returns>
        public CallInfo CancelCalling()
        {
            this.CallStatus = CallStatus.NotYet;  // 取消
            this.LastCalledTime = null;
            this.CallingDoctorCode = null;
            this.CallingDoctorName = null;

            return this;
        }

        /// <summary>
        /// 接诊
        /// </summary>
        /// <param name="doctorId">医生 ID</param>
        /// <param name="doctorName">医生名称</param>
        /// <param name="consultingRoomCode">诊室编码</param>
        /// <param name="consultingRoomName">诊室名称</param>
        /// <returns></returns>
        public CallInfo Treat([NotNull] string doctorId, [NotNull] string doctorName, [NotNull] string consultingRoomCode, [NotNull] string consultingRoomName)
        {
            this.CallStatus = CallStatus.Over;
            this.DoctorName = Check.NotNull(doctorName, nameof(this.DoctorName));



            return this;
        }

        public CallInfo Treat()
        {
            this.CallStatus = CallStatus.Over;
            return this;
        }

        /// <summary>
        /// 就诊中患者退回候诊
        /// </summary>
        public CallInfo SendBackWaiting()
        {
            // 设置叫号状态为“未叫号”
            this.CallStatus = CallStatus.NotYet;
            this.LastCalledTime = null;
            CallingDoctorCode = null;
            CallingDoctorName = null;
            ConsultingRoomCode = null;
            ConsultingRoomName = null;
            return this;
        }

        /// <summary>
        /// 结束就诊
        /// </summary>
        public CallInfo TreatFinish([NotNull] string doctorId, [NotNull] string doctorName, [NotNull] string consultingRoomCode, [NotNull] string consultingRoomName)
        {
            CallStatus = CallStatus.Over;

            // 发布就诊中的领域事件
            this.AddLocalEvent(new TreatedEvent
            {
                CallInfoId = this.Id,
                ConsultingRoomCode = consultingRoomCode,
                ConsultingRoomName = consultingRoomName,
                DoctorId = doctorId,
                DoctorName = this.DoctorName

            });
            return this;
        }

        /// <summary>
        /// 过号
        /// </summary>
        public CallInfo Exceed()
        {
            // 设置叫号状态为“过号” 
            CallStatus = CallStatus.Exceed;
            return this;
        }

        /// <summary>
        /// 过号患者恢复候诊
        /// </summary>
        public CallInfo ReturnToQueue()
        {
            // 设置叫号状态为“未叫号”
            this.InCallQueueTime = DateTime.Now;
            this.CallStatus = CallStatus.NotYet;
            this.LastCalledTime = null;
            return this;
        }

        /// <summary>
        /// 过号患者
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public CallInfo ReturnToQueue(DateTime dateTime)
        {
            // 设置叫号状态为“未叫号”
            this.InCallQueueTime = dateTime.AddSeconds(1);
            this.CallStatus = CallStatus.NotYet;
            this.LastCalledTime = null;
            return this;
        }

        /// <summary>
        /// 退号
        /// </summary>
        /// <returns></returns>
        public CallInfo RegisterRefund()
        {
            this.CallStatus = CallStatus.Refund;

            return this;
        }

        /// <summary>
        /// 出科
        /// </summary>
        public CallInfo OutDepartment()
        {
            this.InCallQueueTime = null;
            this.CallStatus = CallStatus.Over;
            this.LastCalledTime = null;
            this.DoctorName = null;
            return this;
        }

        /// <summary>
        /// 患者流转
        /// </summary>
        /// <param name="transferType"></param>
        /// <param name="deptCode"></param>
        /// <param name="deptName"></param>
        public void Transfer(TransferType transferType, string deptCode, string deptName)
        {
            this.IsShow = transferType == TransferType.OutpatientArea;
        }
    }
}

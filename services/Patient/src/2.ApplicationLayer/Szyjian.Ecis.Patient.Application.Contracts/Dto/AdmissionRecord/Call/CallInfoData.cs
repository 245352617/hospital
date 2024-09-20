using System;
using System.ComponentModel;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 【叫号患者】查询结果 DTO
    /// direction：output
    /// </summary>
    [Serializable]
    public class CallInfoData
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 登记时间
        /// 等候时长从登记时间开始，当有开始就诊时间时，到开始就诊时间结束; 否则到当前时间
        /// </summary>
        [Description("登记时间")]
        public DateTime? LogTime { get; set; }

        /// <summary>
        /// 等候时长
        /// </summary>
        [Description("等候时长")]
        public string WaitingDuration
        {
            get
            {
                return this.LogTime?.GetWaitingTimeString();
            }
        }

        /// <summary>
        /// 排队日期（非自然日，按工作日时间计算，当天起始时间按叫号设置的时间计算）
        /// 按取号（分配排队号）的时间计算
        /// </summary>
        [Description("排队日期（工作日计算）")]
        public virtual DateTime? LogDate { get; set; }

        /// <summary>
        /// 排队号
        /// </summary>
        [Description("排队号")]
        public string CallingSn { get; set; }

        /// <summary>
        /// 开始排队时间
        /// </summary>
        [Description("开始排队时间")]
        public DateTime? InCallQueueTime { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        [Description("是否置顶")]
        public bool IsTop { get; set; }

        /// <summary>
        /// 置顶时间
        /// </summary>
        [Description("置顶时间")]
        public DateTime? TopTime { get; set; }

        /// <summary>
        /// 叫号状态（0: 未叫号; 1: 叫号中; 2: 已叫号（已就诊、已过号)）
        /// </summary>
        [Description("叫号状态")]
        public CallStatus CallStatus { get; set; }

        /// <summary>
        /// 叫号时间
        /// </summary>
        [Description("叫号时间")]
        public DateTime? LastCalledTime { get; set; }

        /// <summary>
        /// 急诊医生id
        /// </summary>
        [Description("急诊医生id")]
        public string DoctorId { get; set; }

        /// <summary>
        /// 急诊医生姓名
        /// </summary>
        [Description("急诊医生姓名")]
        public string DoctorName { get; set; }

        /// <summary>
        /// 就诊诊室编码
        /// </summary>
        [Description("就诊诊室编码")]
        public string ConsultingRoomCode { get; set; }

        /// <summary>
        /// 就诊诊室名称
        /// </summary>
        [Description("就诊诊室名称")]
        public string ConsultingRoomName { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        [Description("患者唯一标识(HIS)")]
        public string PatientID { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [Description("患者姓名")]
        public string PatientName { get; set; }

        /// <summary>
        /// 就诊号
        /// 挂号之后生成就诊号
        /// </summary>
        [Description("就诊号")]
        public string RegisterNo { get; set; }

        /// <summary>
        /// 分诊科室编码
        /// </summary>
        [Description("患者分诊科室id")]
        public string TriageDept { get; set; }

        /// <summary>
        /// 分诊科室名称
        /// </summary>
        [Description("分诊科室名称")]
        public string TriageDeptName { get; set; }

        /// <summary>
        /// 实际分诊级别
        /// </summary>
        [Description("实际分诊级别")]
        public string ActTriageLevel { get; set; }

        /// <summary>
        /// 实际分诊级别
        /// </summary>
        [Description("实际分诊级别")]
        public string ActTriageLevelName { get; set; }

    }

    /// <summary>
    /// 【叫号患者】查询结果 DTO
    /// direction：output
    /// </summary>
    [Serializable]
    public class CallInfoData2 : CallInfoData
    {
        /// <summary>
        /// 当前队列的序号
        /// </summary>
        public int DeptSort { get; set; }
    }
}

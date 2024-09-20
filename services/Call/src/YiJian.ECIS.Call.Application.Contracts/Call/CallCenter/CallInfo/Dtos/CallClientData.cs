using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YiJian.ECIS.ShareModel.Extensions;

namespace YiJian.ECIS.Call.CallCenter.Dtos
{
    /// <summary>
    /// 【叫号大屏终端】查询结果 DTO
    /// direction：output
    /// </summary>
    [Serializable]
    public class CallClientData
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 就诊号
        /// 挂号之后生成就诊号
        /// </summary>
        public string RegisterNo { get; set; }

        /// <summary>
        /// 排队号
        /// </summary>
        [Description("排队号")]
        public string CallingSn { get; set; }

        /// <summary>
        /// 叫号状态（0: 未叫号; 1: 叫号中;）
        /// </summary>
        [Description("叫号状态")]
        public CallStatus CallStatus { get; set; }

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
        /// 就诊状态
        /// 1 = 待就诊
        /// 4 = 正在就诊
        /// </summary>
        [Description("就诊状态")]
        public EVisitStatus VisitStatus { get; set; }

        /// <summary>
        /// 综合状态（合并就诊状态、叫号状态）
        /// 1 = 待就诊
        /// 4 = 正在就诊
        /// 7 = 叫号中
        /// </summary>
        [Description("就诊状态")]
        public Status MultiStatus
        {
            get
            {
                if (this.CallStatus == CallStatus.Calling) return Status.Calling;
                if (this.VisitStatus == EVisitStatus.WaittingTreat) return Status.WaittingTreat;
                if (this.VisitStatus == EVisitStatus.Treating) return Status.Treating;
                return Status.None;
            }
        }

        /// <summary>
        /// 状态名称
        /// </summary>
        [Description("就诊状态")]
        public string MultiStatusName
        {
            get
            {
                return MultiStatus switch
                {
                    Status.WaittingTreat => "待就诊",
                    Status.Treating => "就诊中",
                    Status.Calling => "叫号中",
                    _ => "",
                };
            }
        }

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
        /// 患者性别
        /// </summary>
        [Description("患者性别")]
        public string Sex { get; set; }

        /// <summary>
        /// 患者姓名拼音码
        /// </summary>
        [Description("患者姓名拼音首字母")]
        public string Py { get; set; }

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



        /// <summary>
        /// 1 = 待就诊
        /// 4 = 正在就诊
        /// 7 = 叫号中
        /// </summary>
        public enum Status : int
        {
            None = -1,

            /// <summary>
            /// 待就诊
            /// </summary>
            [Description("待就诊")]
            WaittingTreat = 1,

            /// <summary>
            /// 正在就诊
            /// </summary>
            [Description("正在就诊")]
            Treating = 4,

            /// <summary>
            /// 叫号中
            /// </summary>
            [Description("叫号中")]
            Calling = 7,
        }
    }
}

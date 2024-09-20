using System;
using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 患者快速通道登记信息查询Dto
    /// </summary>
    public class FastTrackRegisterInfoInput
    {
        /// <summary>
        /// 当前页数
        /// </summary>
        /// <example>1</example>
        [Required]
        public int SkipCount { get; set; }
        
        /// <summary>
        /// 最大结果数
        /// </summary>
        /// <example>50</example>
        [Required]
        public int MaxResultCount { get; set; }
        
        /// <summary>
        /// 查询开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        
        /// <summary>
        /// 查询结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 接诊护士
        /// </summary>
        public string ReceptionNurse { get; set; }
        /// <summary>
        /// 接诊护士名称
        /// </summary>
        public string ReceptionNurseName { get; set; }
        /// <summary>
        /// 所属派出所
        /// </summary>
        public string PoliceStationCode { get; set; }

        /// <summary>
        /// 警务人员警号/姓名
        /// </summary>
        public string PoliceInfo { get; set; }
    }
}
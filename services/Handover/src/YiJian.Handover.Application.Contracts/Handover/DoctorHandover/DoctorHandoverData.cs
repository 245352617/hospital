using System.Collections.Generic;

namespace YiJian.Handover
{
    using System;

    [Serializable]
    public class DoctorHandoverData
    {        
        public Guid Id { get; set; }
        
        /// <summary>
        /// 交班日期
        /// </summary>
        public string  HandoverDate { get; set; }
        
        /// <summary>
        /// 交班时间
        /// </summary>
        public string  HandoverTime { get; set; }
        
        /// <summary>
        /// 交班医生编码
        /// </summary>
        public string  HandoverDoctorCode { get; set; }
        
        /// <summary>
        /// 交班医生名称
        /// </summary>
        public string  HandoverDoctorName { get; set; }
        
        /// <summary>
        /// 班次id
        /// </summary>
        public Guid  ShiftSettingId { get; set; }
        
        /// <summary>
        /// 班次名称
        /// </summary>
        public string  ShiftSettingName { get; set; }
        
        /// <summary>
        /// 其他事项
        /// </summary>
        public string  OtherMatters { get; set; }
        /// <summary>
        /// 交班状态，0：未提交，1：已提交
        /// </summary>
        public int Status { get; set; } = 0;
        /// <summary>
        /// 统计
        /// </summary>
        public DoctorPatientStatisticData  PatientStatistics { get; set; }
        
        /// <summary>
        /// 交班患者
        /// </summary>
        public List<DoctorPatientsData>  DoctorPatients { get; set; }
        
    }
}
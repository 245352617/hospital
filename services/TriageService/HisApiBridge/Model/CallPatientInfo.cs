using System;

namespace TriageService.HisApiBridge.Model
{
    /// <summary>
    /// 叫号服务患者信息
    /// </summary>
    public class CallPatientInfo
    {
        public Guid Id { get; set; }
        public DateTime? LogTime { get; set; }
        public string WaitingDuration { get; set; }
        public DateTime? logDate { get; set; }
        public string CallingSn { get; set; }
        public DateTime? InCallQueueTime { get; set; }
        public bool IsTop { get; set; }
        public DateTime? TopTime { get; set; }
        public int CallStatus { get; set; }
        public DateTime? LastCalledTime { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string ConsultingRoomCode { get; set; }
        public string ConsultingRoomName { get; set; }
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public string RegisterNo { get; set; }
        public string TriageDept { get; set; }
        public string TriageDeptName { get; set; }
        public string ActTriageLevel { get; set; }
        public string ActTriageLevelName { get; set; }
        public int DeptSort { get; set; }
    }
}

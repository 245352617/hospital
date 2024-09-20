namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// His患者状态同步信息
    /// </summary>
    public class PatientInfoFromHisStatusDto
    {
        public string visitNo { get; set; }
        public string visSerialNo { get; set; }
        public string regSerialNo { get; set; }
        public string deptCode { get; set; }
        public string deptName { get; set; }
        public string regdeptCode { get; set; }
        public string regdeptName { get; set; }
        public string doctorCode { get; set; }
        public string doctorName { get; set; }
        public string visitStatus { get; set; }
        public string visitStatusName { get; set; }
        public string registrationDate { get; set; }
        public string visitDate { get; set; }
        public string endDate { get; set; }
        public string visitType { get; set; }
        public string preDiagName { get; set; }
        public string greenChannel { get; set; }
        public string triageNurse { get; set; }
        public string patientName { get; set; }
        public string sex { get; set; }
        public string phone { get; set; }
        public string idCard { get; set; }
        public string age { get; set; }
    }
}

using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public class GetDiagnoseRecordBySocketDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string PKNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VisitId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BrId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DiagnoseType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DiagnoseGroupNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DiagnoseDescribed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LapseTo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DiagnoseDoctor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DiagnoseTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AttachIdentifier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CWType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GroupSerialNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CancelMark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SumbitMark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ReportStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ConfirmMark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ConfirmTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DefiniteMark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AdmissionSituation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? VisitDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DiagnoseDoctorName { get; set; }
    }
}

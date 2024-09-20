using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class UpdatePatientGreenRoadDto
    {

        /// <summary>
        /// PatientInfo表Id（注：修改时传入）
        /// </summary>
        public Guid TriagePatientInfoId { get; set; }

        /// <summary>
        /// 绿色通道编码
        /// </summary>
        public string GreenRoadCode { get; set; }

    }
}

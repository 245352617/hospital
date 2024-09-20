using System;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class TraumaPatientsResponseDto
    {
        [Excel("病历名称", 1000)]
        public string EmrTitle { get; set; }

        [Excel("记录时间", 900)]
        public DateTime CreationTime { get; set; }

        [Excel("书写医生", 800)]
        public string DoctorName { get; set; }

        public Guid PI_ID { get; set; }

        [Excel("科室名称", 700)]
        public string DeptName { get; set; }

        [Excel("门诊号码", 600)]
        public string VisitNo { get; set; }

        [Excel("姓名", 500)]
        public string PatientName { get; set; }

        [Excel("性别", 400)]
        public string SexName { get; set; }

        [Excel("年龄", 300)]
        public string Age { get; set; }

        [Excel("诊断名称", 200)]
        public string DiagnoseNames { get; set; }

        [Excel("完成时间", 100)]
        public DateTime? OutDeptTime { get; set; }
    }
}

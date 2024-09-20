using System;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class TraumaProjectResponseDto
    {
        [Excel("姓名", 1000)]
        public string PatientName { get; set; }

        [Excel("年龄", 990)]
        public string Age { get; set; }

        [Excel("性别", 980)]
        public string SexName { get; set; }

        [Excel("门诊号", 970)]
        public string VisitNo { get; set; }

        [Excel("操作日期", 960)]
        public DateTime ApplyTime { get; set; }

        [Excel("操作项目", 950)]
        public string Name { get; set; }

        [Excel("诊断", 940)]
        public string Diagnosis { get; set; }

        [Excel("开嘱医生", 930)]
        public string ApplyDoctorName { get; set; }
    }
}

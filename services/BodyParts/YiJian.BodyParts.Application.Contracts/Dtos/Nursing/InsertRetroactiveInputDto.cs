using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Nursing {
    public class InsertRetroactiveInputDto {
        public string PatientId { get; set; }
        public string PI_ID { get; set; }
        public string PatientName { get; set; }
        public string BedNum { get; set; }
        public DateTime RemindTime { get; set; }
        public string CanulaPart { get; set; }
        public string ModuleCode { get; set; }
        public string DeptCode { get; set; }
    }
}

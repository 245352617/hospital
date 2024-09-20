using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class TimeNursingOrder
    {
        public String paraCode { get; set; }
        public DateTime nurseTime { get; set; }
        public decimal dDosage { get; set; }
    }
}

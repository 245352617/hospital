using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 住院人数
    /// </summary>
    public class SdZyrs
    {
        public string inhosp_index_no { get; set; }

        public DateTime admission_date_time { get; set; }

        public DateTime? discharge_date_time { get; set; }

        public string patient_status { get; set; }

        public decimal costs { get; set; }
    }
}

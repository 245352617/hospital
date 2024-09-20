using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 汕大抗生素视图模型
    /// </summary>
    public class SdKss
    {
        public string patient_id { get; set; }

        public decimal visit_id { get; set; }

        public string use_purpose { get; set; }

        public string inspection { get; set; }

        public string dept { get; set; }

        public string pat_name { get; set; }

        public decimal order_no { get; set; }

        public string antibacterial_drug_name { get; set; }

        public DateTime record_date_time { get; set; }
    }
}

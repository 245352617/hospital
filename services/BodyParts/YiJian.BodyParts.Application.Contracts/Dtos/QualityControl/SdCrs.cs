using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 汕大床日数视图模型
    /// </summary>
    public class SdCrs
    {
        public string dept_code { get; set; }

        public string dept_name { get; set; }

        public DateTime data_date { get; set; }

        public decimal now_num { get; set; }
    }
}

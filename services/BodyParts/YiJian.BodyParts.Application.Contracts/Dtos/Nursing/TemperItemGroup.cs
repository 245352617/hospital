using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 海泰体征数据
    /// </summary>
    public class TemperItemGroup
    {
        public string billSN { set; get; }
        public string effDateTime { set; get; }
        public string inpatientId { set; get; }
        public string patientId { set; get; }
        public string userId { set; get; }
        public string userName { set; get; }
    }
}

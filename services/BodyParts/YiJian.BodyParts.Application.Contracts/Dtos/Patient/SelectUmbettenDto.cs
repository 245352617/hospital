using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 换床记录
    /// </summary>
    public class SelectUmbettenDto
    {
        /// <summary>
        /// 患者id(通过业务构造的流水号，每个患者每次入科号码唯一)
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 换床时间
        /// </summary>
        public string RecordTime { get; set; }

        /// <summary>
        /// 换床记录
        /// </summary>
        public string Record { get; set; }
    }
}

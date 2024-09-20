using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 结束护理措施
    /// </summary>
    public class UpdateNursingMeasureDto
    {
        public List<Guid> Ids { get; set; }

        /// <summary>
        /// 结束人
        /// </summary>
        public string EndCode { get; set; }
        /// <summary>
        /// 结束人名称
        /// </summary>
        public string EndName { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 上一班次Dto
    /// </summary>
    public class LastScheduleDto
    {
        public string LastScheduleCode { get; set; }

        public DateTime LastScheduleTime { get; set; }
    }
}

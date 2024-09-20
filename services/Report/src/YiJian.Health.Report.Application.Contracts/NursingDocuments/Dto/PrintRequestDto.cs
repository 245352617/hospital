using System;
using System.Collections.Generic;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 护理单打印的请求参数
    /// </summary>
    public class PrintRequestDto
    {
        /// <summary>
        /// 全局的患者ID
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 护理单sheet页面Id（GUID）集合，传过来可以指定打印页面，如果不希望指定，传个空集合过来默认全部打印
        /// <see cref="SheetDto.Id"/>
        /// </summary>
        public List<Guid> SheetId { get; set; } = new List<Guid>();

        /// <summary>
        /// 当前入院护理单的开始时间
        /// </summary>
        public DateTime? Begintime { get; set; }

        /// <summary>
        /// 当前入院护理单的结束时间
        /// </summary>
        public DateTime? Endtime { get; set; }

    }
}

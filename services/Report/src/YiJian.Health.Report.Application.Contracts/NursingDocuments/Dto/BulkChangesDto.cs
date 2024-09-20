using System;
using System.Collections.Generic;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 批量更改的护理单数据
    /// </summary>
    public class BulkChangesDto
    {
        /// <summary>
        /// 入院护理单Id
        /// </summary>
        public Guid NursingDocumentId { get; set; }

        /// <summary>
        /// 护理单记录
        /// </summary>
        public List<NursingRecordChangeDto> NursingRecords { get; set; } = new List<NursingRecordChangeDto>();

        /// <summary>
        /// 新建页索引
        /// </summary> 
        public int SheetIndex { get; set; }

    }
}

using System;
using System.Collections.Generic;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 护理单核对人信息
    /// </summary>
    public class NursingRecordCollatorDto
    {
        /// <summary>
        /// 护理单行id
        /// </summary>
        public List<Guid> Ids { get; set; }

        /// <summary>
        /// 核对人名称
        /// </summary> 
        public string Collator { get; set; }

        /// <summary>
        /// 核对人code
        /// </summary>
        public string CollatorCode { get; set; }

        /// <summary>
        /// 核对人图片Base64
        /// </summary>
        public string CollatorImage { get; set; }
    }

}

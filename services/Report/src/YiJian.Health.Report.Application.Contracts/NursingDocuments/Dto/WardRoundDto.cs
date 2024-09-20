using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 查房信息
    /// </summary> 
    public class WardRoundDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 级别
        /// </summary> 
        public string Level { get; set; }

        /// <summary>
        /// 签名
        /// </summary> 
        public List<string> Signature { get; set; } = new List<string> { "", "" };

        /// <summary>
        /// 新建页索引
        /// </summary>
        public int SheetIndex { get; set; }


        /// <summary>
        /// 护理单Id
        /// </summary>
        public Guid NursingDocumentId { get; set; }
    }
}

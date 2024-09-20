using System;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 新建一表新的空护理记录
    /// </summary>
    public class NewNursingRecordDto
    {
        /// <summary>
        /// 护理单Id
        /// </summary>
        public Guid NursingDocumentId { get; set; }

        /// <summary>
        /// 当前页面index
        /// </summary>
        public int SheetIndex { get; set; }

        /// <summary>
        /// 签名数据
        /// </summary>
        public string Signature { get; set; }
    }

}

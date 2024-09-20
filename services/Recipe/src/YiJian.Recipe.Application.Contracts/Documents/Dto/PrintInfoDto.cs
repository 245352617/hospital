using System;

namespace YiJian.Documents.Dto
{
    /// <summary>
    /// 打印反馈数据
    /// </summary>
    public class PrintInfoDto
    {
        /// <summary>
        /// 处方号
        /// </summary> 
        public string PrescriptionNo { get; set; }

        /// <summary>
        /// 打印模板id
        /// </summary>
        public Guid TemplateId { get; set; }
    }
}
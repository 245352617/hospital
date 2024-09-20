using System;
using System.ComponentModel.DataAnnotations;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 添加新的入院护理单
    /// </summary>
    public class AddNursingDocumentDto
    {
        /// <summary>
        /// 全过程唯一ID
        /// </summary> 
        [Required]
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 单据标题
        /// </summary> 
        [Required, StringLength(200, ErrorMessage = "单据标题需在200字内")]
        public string Title { get; set; }

        /// <summary>
        /// 护理单编码(eg: NS-ED-A009)
        /// </summary> 
        [Required, StringLength(200, ErrorMessage = "护理单编码需在32字内")]
        public string NursingCode { get; set; }

        /// <summary>
        /// 登陆token
        /// </summary>
        public string Token { get; set; }

    }
}

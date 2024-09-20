using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace YiJian.EMR.Writes.Dto
{
    /// <summary>
    /// 完整的XML留痕数据
    /// </summary>
    public class XmlHistoryFullDto: XmlHistoryDto
    {

        /// <summary>
        /// 电子病历Xml文档
        /// </summary> 
        [Required(ErrorMessage = "电子病历留痕的Xml文档不能为空")]
        public string EmrXml { get; set; }
    }

}

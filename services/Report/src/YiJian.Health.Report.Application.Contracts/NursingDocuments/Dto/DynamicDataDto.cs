using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 动态数据Dto
    /// </summary>
    public class DynamicDataJsonDto
    {
        /// <summary>
        /// 表头内容
        /// </summary>
        public Guid? HeaderId { get; set; }

        /// <summary>
        /// json 结构的数据(前段根据用户选择填写的内容组装成一个json字符串传给后端) 
        /// </summary> 
        public string JsonData { get; set; }
    }

    /// <summary>
    /// 动态数据字典集合
    /// </summary> 
    public class DynamicDataDto : EntityDto<Guid>
    {
        /// <summary>
        /// 表头[Key]
        /// </summary> 
        [Required]
        public Guid Header { get; set; }

        /// <summary>
        /// 文本数据
        /// </summary> 
        [StringLength(100)]
        public string Text { get; set; }

        /// <summary>
        /// 新建页索引
        /// </summary> 
        public int SheetIndex { get; set; }

        /// <summary>
        /// 护理单记录列Id
        /// </summary> 
        public Guid NursingRecordId { get; set; }

        /// <summary>
        /// 护理单Id
        /// </summary> 
        public Guid NursingDocumentId { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:
    /// </summary>
    public class IcuDocumentConfigDto : EntityDto<Guid>
    {
        /// <summary>
        /// 文书名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模板Url:文书模板地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 文书类别标签
        /// </summary>
        public string Categroy { get; set; }

        /// <summary>
        /// 模板编号,Url参数编码
        /// </summary>
        public string TemplateCode { get; set; }

        /// <summary>
        /// 是否删除，0：否，1：是
        /// </summary>

        public int? IsDeleted { get; set; } = 0;

        /// <summary>
        /// 判断该文书是否启用：0：否，1：是
        /// </summary>

        public int? IsEnable { get; set; }

        /// <summary>
        /// 判断该文书是否默认显示最新时间点：0：否，1：是
        /// </summary>
        public int IsFlashback { get; set; }


        /// <summary>
        /// 排序
        /// </summary>
        /// <example></example>
        public int? SortNum { get; set; }

        /// <summary>
        /// 文书记录类型,状态：0-普通文书，1-多记录文书
        /// </summary>
        public int DocumentRecordType { get; set; } = 0;

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>

        public DateTime? UpdateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 文书类型(0：文书编辑器设计的页面，1：用vue写的单页面)
        /// </summary>

        public int? Type { get; set; }

        /// <summary>
        /// 文书标签，主要用于作为分类检索或者关键字检索
        /// </summary>
        public string Label { get; set; }
    }
}

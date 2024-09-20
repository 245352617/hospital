using System.ComponentModel.DataAnnotations;

namespace YiJian.BodyParts
{
    public class PagedAndSortedMultipleWhereResultRequest:PagedAndSortedWhereResultRequest
    {
        /// <summary>
        /// 搜索关键词
        /// </summary>
        // public List<VueSearchForms> Search { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        /// <example>1</example>
        [Required]
        public override int SkipCount { get; set; }

        /// <summary>
        /// 第页个数
        /// </summary>
        /// <example>15</example>
        [Required]
        public override int MaxResultCount { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        /// <example></example>
        public override string Sorting { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        /// <example>-1</example>
        public virtual StatusEnum Status { get; set; } = StatusEnum.未知;
    }
}
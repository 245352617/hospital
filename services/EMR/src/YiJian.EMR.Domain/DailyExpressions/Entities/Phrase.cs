using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.EMR.DailyExpressions.Entities
{
    /// <summary>
    /// 病历常用语
    /// </summary>
    [Comment("病历常用语")]
    public class Phrase : Entity<int>
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Comment("标题")]
        [StringLength(200,ErrorMessage ="常用语标题最长200个字符")]
        public string Title { get; set; }

        /// <summary>
        /// 显示内容
        /// </summary>
        [Comment("内容")]
        [StringLength(2000, ErrorMessage = "显示内容最长2000个字符")]
        public string Text { get; set; }

        /// <summary>
        /// 排序号码
        /// </summary>
        [Comment("排序号码")]
        public int Sort { get; set; }

        /// <summary>
        /// 目录
        /// </summary>
        [Comment("目录Id")]
        public int CatalogueId { get; set; }

        /// <summary>
        /// 目录
        /// </summary> 
        public virtual PhraseCatalogue Catalogue { get; set; }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="sort"></param>
        public void Update(string title, string text , int sort)
        {
            Title = title;
            Text = text;
            Sort = sort;
        }

    }
}

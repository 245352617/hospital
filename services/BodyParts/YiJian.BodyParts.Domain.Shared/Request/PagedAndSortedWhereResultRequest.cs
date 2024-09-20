using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts
{
    public class PagedAndSortedWhereResultRequest:PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 搜索条件，关键词
        /// </summary>
        /// <example></example>
        public string KeyWord { get; set; }
    }
}
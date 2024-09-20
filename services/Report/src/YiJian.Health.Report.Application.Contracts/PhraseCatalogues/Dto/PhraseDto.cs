using Volo.Abp.Application.Dtos;
using YiJian.Health.Report.Enums;

namespace YiJian.Report.PhraseCatalogues.Dto
{
    /// <summary>
    /// 常用语
    /// </summary>
    public class PhraseDto : EntityDto<int?>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 目录
        /// </summary>
        public int CatalogueId { get; set; }

        /// <summary>
        /// 期望展示的权限 -1=拒绝，没有任何权限  , 0=管理员 , 1=医生 , 2=护士
        /// </summary>
        public EExpectedRole Role { get; set; }

    }

}

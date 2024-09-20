using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.Characters.Dto
{
    /// <summary>
    /// 通用字符分类
    /// </summary>
    public class UniversalCharacterDto:EntityDto<int?>
    {
        /// <summary>
        /// 分类-相当目录
        /// </summary> 
        public string Category { get; set; }

        /// <summary>
        /// 排序
        /// </summary> 
        public int Sort { get; set; }
    }
}

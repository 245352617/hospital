using System.Collections.Generic;
using YiJian.DoctorsAdvices.Enums;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// LisOrPacsItem
    /// </summary>
    public class LisOrPacsItemDto
    {
        /// <summary>
        /// 医嘱各项分类: 0=药方项,1=检查项,2=检验项,3=诊疗项
        /// </summary>
        public EDoctorsAdviceItemType ItemType { get; set; }

        /// <summary>
        /// 检验小项List
        /// </summary>
        public List<LisItemDto> LisItemList { get; set; }

        /// <summary>
        /// 检查小项List
        /// </summary>
        public List<PacsItemDto> PacsItemList { get; set; }
    }
}

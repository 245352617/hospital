using System;
using YiJian.DoctorsAdvices.Enums;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 获取 检验/检查小项 request
    /// </summary>
    public class GetLisOrPacsItemDto
    {
        /// <summary>
        /// PacsItemId or LisId
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 医嘱各项分类: 0=药方项,1=检查项,2=检验项,3=诊疗项
        /// </summary>
        public EDoctorsAdviceItemType ItemType { get; set; }

    }

}

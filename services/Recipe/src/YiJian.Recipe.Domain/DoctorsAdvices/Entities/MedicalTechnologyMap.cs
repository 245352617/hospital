using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Entities;
using YiJian.DoctorsAdvices.Enums;

namespace YiJian.DoctorsAdvices.Entities
{
    /// <summary>
    /// 医技映射字典，包括检查，检验，诊疗
    /// <![CDATA[
    /// 为了处理龙岗中心医院检查检验诊疗projectdetailNo唯一的问题
    /// ]]>
    /// </summary>
    [Comment("医技映射字典，包括检查，检验，诊疗")]
    public class MedicalTechnologyMap : Entity<long>
    {
        public MedicalTechnologyMap(
            Guid lPTId,
            EDoctorsAdviceItemType itemType)
        {
            LPTId = lPTId;
            ItemType = itemType;
        }

        /// <summary>
        /// 医技映射字典，包括检查，检验，诊疗外检
        /// </summary>
        [Comment("医技映射字典，包括检查，检验，诊疗外检")]
        public Guid LPTId { get; set; }

        /// <summary>
        /// 医嘱各项分类: 0=药品,1=检查项,2=检验项,3=诊疗项
        /// <![CDATA[
        ///  只有 1,2,3; 0=药品不需要
        /// ]]>
        /// </summary>
        [Comment("医嘱各项分类: 0=药品,1=检查项,2=检验项,3=诊疗项")]
        public EDoctorsAdviceItemType ItemType { get; set; }
    }
}

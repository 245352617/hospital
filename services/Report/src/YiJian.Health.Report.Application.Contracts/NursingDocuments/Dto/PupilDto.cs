using System;
using Volo.Abp.Application.Dtos;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 瞳孔评估
    /// </summary> 
    public class PupilDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 瞳孔评估(0=左眼，1=右眼)
        /// <see cref="EPupilType"/>
        /// </summary> 
        public EPupilType PupilType { get; set; }

        /// <summary>
        /// 直径（mm）
        /// </summary> 
        public string Diameter { get; set; }

        /// <summary>
        /// 对光反应（灵敏（+）/迟钝（-）/固定（±））
        /// </summary> 
        public string LightReaction { get; set; }

        /// <summary>
        /// 其他特征（眼疾/义眼/缺失/破裂/肿胀/包扎/其他...）
        /// </summary> 
        public string OtherTrait { get; set; }

        /// <summary>
        /// 其他
        /// </summary>
        public string Other { get; set; }

        /// <summary>
        /// 护理记录Id
        /// </summary>
        public Guid NursingRecordId { get; set; }

    }
}

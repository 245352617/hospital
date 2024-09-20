using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingDocuments.Entities
{
    /// <summary>
    /// 瞳孔评估
    /// </summary>
    [Comment("瞳孔评估")]
    public class Pupil : Entity<Guid>
    {
        private Pupil()
        {

        }

        public Pupil(Guid id,
            EPupilType pupilType,
            string diameter,
            string lightReaction,
            string otherTrait,
            string other,
            Guid nursingRecordId)
        {
            Id = id;
            PupilType = pupilType;
            Diameter = diameter;
            LightReaction = lightReaction;
            OtherTrait = otherTrait;
            Other = other;
            NursingRecordId = nursingRecordId;
        }

        /// <summary>
        /// 瞳孔评估(0=左眼，1=右眼)
        /// <see cref="EPupilType"/>
        /// </summary>
        [Comment("瞳孔评估(0=左眼，1=右眼)")]
        public EPupilType PupilType { get; set; }

        /// <summary>
        /// 直径（mm）
        /// </summary>
        [Comment("直径（mm）")]
        [StringLength(10)]
        public string Diameter { get; set; }

        /// <summary>
        /// 对光反应（灵敏（+）/迟钝（-）/固定（±））
        /// </summary>
        [Comment("对光反应（灵敏（+）/迟钝（-）/固定（±））")]
        [StringLength(50)]
        public string LightReaction { get; set; }

        /// <summary>
        /// 其他特征（眼疾/义眼/缺失/破裂/肿胀/包扎/其他...）
        /// </summary>
        [Comment("其他特征（眼疾/义眼/缺失/破裂/肿胀/包扎/其他...）")]
        [StringLength(100)]
        public string OtherTrait { get; set; }

        /// <summary>
        /// 其他
        /// </summary>
        [Comment("其他")]
        [StringLength(50)]
        public string Other { get; set; }

        /// <summary>
        /// 护理记录Id
        /// </summary>
        [Comment("护理记录Id")]
        public Guid NursingRecordId { get; set; }

        public void Update(
           EPupilType pupilType,
           string diameter,
           string lightReaction,
           string otherTrait,
           string other)
        {
            PupilType = pupilType;
            Diameter = diameter;
            LightReaction = lightReaction;
            OtherTrait = otherTrait;
            Other = other;
        }
    }
}

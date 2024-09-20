using System;
using Volo.Abp.Application.Dtos;
using YiJian.Recipe.Enums;

namespace YiJian.Recipe
{
    public class NovelCoronavirusRnaDto : EntityDto<Guid>
    {
        /// <summary>
        /// 患者分诊id
        /// </summary>
        public Guid PIID { get; set; }

        /// <summary>
        /// 医嘱id
        /// </summary>
        public Guid DoctorsAdviceId { get; set; }

        /// <summary>
        /// 标本类型
        /// </summary>
        public ESpecimenType SpecimenType { get; set; }

        /// <summary>
        /// 专家会诊意见
        /// </summary>
        public EConsultationOpinions ConsultationOpinions { get; set; }

        /// <summary>
        /// 流行病学史
        /// </summary>
        public EEpidemicHistory EpidemicHistory { get; set; }

        /// <summary>
        /// 是否发热
        /// </summary>
        public bool IsFever { get; set; }

        /// <summary>
        /// 是否有肺炎影像学特征:
        /// </summary>
        public bool IsPneumonia { get; set; }

        /// <summary>
        /// 淋巴细胞是否降低
        /// </summary>
        public bool IsLymphopenia { get; set; }

        /// <summary>
        /// 人员来源
        /// </summary>
        public string PatientSource { get; set; }

        /// <summary>
        /// 人员身份
        /// </summary>
        public string PatientIdentity { get; set; }

        /// <summary>
        /// 来深地点
        /// </summary>
        public EPlaceToShenzhen PlaceToShenzhen { get; set; }

        /// <summary>
        /// 检验项目名称
        /// </summary>
        public string LisesName { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public string ApplyTime { get; set; }
    }
}
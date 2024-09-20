using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using YiJian.Recipe.Enums;

namespace YiJian.Recipe
{
    /// <summary>
    /// 新冠RNA检测申请
    /// </summary>
    [Comment("新冠RNA检测申请")]
    public class NovelCoronavirusRna : Entity<Guid>
    {
        private NovelCoronavirusRna(string lisesName, DateTime applyTime)
        {
            LisesName = lisesName;
            ApplyTime = applyTime;
        }

        public NovelCoronavirusRna(Guid id, Guid piid, Guid doctorsAdviceId, ESpecimenType specimenType,
            EConsultationOpinions consultationOpinions, EEpidemicHistory epidemicHistory, bool isFever,
            bool isPneumonia, bool isLymphopenia, string patientSource, string patientIdentity,
            EPlaceToShenzhen placeToShenzhen, string lisesName, DateTime applyTime) : base(id)
        {
            Id = id;
            PIID = piid;
            DoctorsAdviceId = doctorsAdviceId;
            Modify(specimenType, consultationOpinions, epidemicHistory, isFever,
                isPneumonia, isLymphopenia, patientSource, patientIdentity, placeToShenzhen, lisesName, applyTime);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="specimenType"></param>
        /// <param name="consultationOpinions"></param>
        /// <param name="epidemicHistory"></param>
        /// <param name="isFever"></param>
        /// <param name="isPneumonia"></param>
        /// <param name="isLymphopenia"></param>
        /// <param name="patientSource"></param>
        /// <param name="patientIdentity"></param>
        /// <param name="placeToShenzhen"></param>
        /// <param name="lisesName"></param>
        /// <param name="applyTime"></param>
        public void Modify(ESpecimenType specimenType, EConsultationOpinions consultationOpinions,
            EEpidemicHistory epidemicHistory, bool isFever, bool isPneumonia, bool isLymphopenia, string patientSource,
            string patientIdentity, EPlaceToShenzhen placeToShenzhen, string lisesName, DateTime applyTime)
        {
            SpecimenType = specimenType;
            ConsultationOpinions = consultationOpinions;
            EpidemicHistory = epidemicHistory;
            IsFever = isFever;
            IsPneumonia = isPneumonia;
            IsLymphopenia = isLymphopenia;
            PatientSource = patientSource;
            PatientIdentity = patientIdentity;
            PlaceToShenzhen = placeToShenzhen;
            LisesName = lisesName;
            ApplyTime = applyTime;
        }

        /// <summary>
        /// 患者分诊id
        /// </summary>
        [Comment("患者分诊id")]
        public Guid PIID { get; private set; }

        /// <summary>
        /// 医嘱id
        /// </summary>
        [Comment("医嘱id")]
        public Guid DoctorsAdviceId { get; private set; }

        /// <summary>
        /// 标本类型
        /// </summary>
        [Comment("标本类型")]
        public ESpecimenType SpecimenType { get; private set; }

        /// <summary>
        /// 专家会诊意见
        /// </summary>
        [Comment("专家会诊意见")]
        public EConsultationOpinions ConsultationOpinions { get; private set; }

        /// <summary>
        /// 流行病学史
        /// </summary>
        [Comment("流行病学史")]
        public EEpidemicHistory EpidemicHistory { get; private set; }

        /// <summary>
        /// 是否发热
        /// </summary>
        [Comment("是否发热")]
        public bool IsFever { get; private set; }

        /// <summary>
        /// 是否有肺炎影像学特征:
        /// </summary>
        [Comment("是否有肺炎影像学特征")]
        public bool IsPneumonia { get; private set; }

        /// <summary>
        /// 淋巴细胞是否降低
        /// </summary>
        [Comment("淋巴细胞是否降低")]
        public bool IsLymphopenia { get; private set; }

        /// <summary>
        /// 人员来源
        /// </summary>
        [Comment("人员来源")]
        [StringLength(50)]
        public string PatientSource { get; private set; }

        /// <summary>
        /// 人员身份
        /// </summary>
        [Comment("人员身份")]
        [StringLength(50)]
        public string PatientIdentity { get; private set; }

        /// <summary>
        /// 来深地点
        /// </summary>
        [Comment("来深地点")]
        public EPlaceToShenzhen PlaceToShenzhen { get; private set; }

        /// <summary>
        /// 检验项目名称
        /// </summary>
        [Comment("检验项目名称")]
        [StringLength(100)]
        public string LisesName { get; private set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        [Comment("申请时间")]
        public DateTime ApplyTime { get; private set; }
    }
}
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    public class IcuDoctorsScoreDto// : EntityDto<Guid>
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 评分名称
        /// </summary>
        [Required]
        public string ScoreName { get; set; }

        /// <summary>
        /// 医生（评估人）ID
        /// </summary>
        //[Required]
        public string DoctorId { get; set; }

        /// <summary>
        /// 医生名称（评估人）
        /// </summary>
        ///
        [Required]
        public string DoctorName { get; set; }

        /// <summary>
        /// 评估时间
        /// </summary>
        public DateTime ScoreTime { get; set; }

        /// <summary>
        /// 慢性健康状况评分值
        /// </summary>
        [Required]
        public string ChronicHealthScoreValue { get; set; }

        /// <summary>
        /// 得分
        /// </summary>
        [Required]
        public int ScoreValue { get; set; }

        /// <summary>
        /// 预计病死率类型(非手术患者=1，手术后患者=2)
        /// </summary>
        public SurgeryEnum IsSurgery { get; set; }

        /// <summary>
        /// 是否为急诊患者
        /// </summary>
        public bool? IsEmergency { get; set; }

        /// <summary>
        /// 诊断分类系数
        /// </summary>
        [Required]
        public string DiagnosisCoefficient { get; set; }

        /// <summary>
        /// 预计死亡率
        /// </summary>
        [Required]
        public string ExpectedDeadRate { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        [Required]
        public string PI_ID { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>
        //public int sortNum { get; set; }

        public List<IcuDoctorsScoreResultDto> icuDoctorsScoreResultDtos { get; set; }

        ///// <summary>
        ///// 评分趋势图
        ///// </summary>
        //public List<HistoricalTrend> trendChart { get; set; }
    }

    public class IcuDoctorsScoreResultDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 参数编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 数值
        /// </summary>
        public string ScoreResult { get; set; }

        /// <summary>
        /// 得分
        /// </summary>
        public int? NumValue { get; set; }

        /// <summary>
        /// 评分主表ID
        /// </summary>
        public Guid? Pid { get; set; }

        /// <summary>
        /// 是否是编辑数值
        /// </summary>
        public bool IsEdit { get; set; }

        /// <summary>
        /// 评估时间
        /// </summary>
        public DateTime ScoreTime { get; set; }

        
    }
}
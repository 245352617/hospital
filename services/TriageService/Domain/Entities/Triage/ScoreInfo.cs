using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SamJan.MicroService.PreHospital.Core.BaseEntities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 院前分诊评分信息
    /// </summary>
    public class ScoreInfo : BaseEntity<Guid>
    {
        public ScoreInfo SetId(Guid id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 院前分诊患者建档表主键Id
        /// </summary>
        [Description("院前分诊患者建档表主键Id")]
        public Guid PatientInfoId { get; set; }
        
        /// <summary>
        /// 等级编码
        /// </summary>
        [Description("等级编码")]
        [StringLength(60)]
        public string LevelCode { get; set; }
         
        /// <summary>
        /// 评分类型
        /// </summary>
        [Description("评分类型")]
        [StringLength(60)]
        public string ScoreType { get; set; }

        /// <summary>
        /// 评分数值
        /// </summary>
        [Description("评分数值")]
        public int ScoreValue { get; set; }

        /// <summary>
        /// 评分等级
        /// </summary>
        [Description("评分等级")]
        [StringLength(60)]
        public string ScoreDescription { get; set; }

        /// <summary>
        /// 评分内容 Json字符串
        /// </summary>
        [Description("评分内容 Json字符串")]
        public string ScoreContent { get; set; }
    }
}
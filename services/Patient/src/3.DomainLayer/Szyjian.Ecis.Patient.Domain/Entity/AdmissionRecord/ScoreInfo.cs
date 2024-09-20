using FreeSql.DataAnnotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 评分表
    /// </summary>
    [Table(Name = "Pat_ScoreInfo")]
    public class ScoreInfo : Entity<Guid>
    {
        /// <summary>
        /// 设置主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ScoreInfo SetId(Guid id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 院前分诊患者建档表主键Id
        /// </summary>
        [Description("院前分诊患者建档表主键Id")]
        public Guid PI_ID { get; set; }

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
        [Column(DbType = "nvarchar(max)")]
        public string ScoreContent { get; set; }
    }
}
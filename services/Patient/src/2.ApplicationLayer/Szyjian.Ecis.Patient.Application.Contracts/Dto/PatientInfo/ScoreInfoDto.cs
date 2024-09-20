using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class ScoreInfoDto
    {
        /// <summary>
        /// 院前分诊患者建档表主键Id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 等级编码
        /// </summary>
        public string LevelCode { get; set; }

        /// <summary>
        /// 评分类型
        /// </summary>
        public string ScoreType { get; set; }

        /// <summary>
        /// 评分数值
        /// </summary>
        public string ScoreValue { get; set; }

        /// <summary>
        /// 评分等级
        /// </summary>
        public string ScoreDescription { get; set; }

        /// <summary>
        /// 评分内容Json
        /// </summary>
        public string ScoreContent { get; set; }
    }
}
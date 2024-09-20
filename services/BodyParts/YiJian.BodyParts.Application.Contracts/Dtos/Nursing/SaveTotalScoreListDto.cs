using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 批量保存总分Dto
    /// </summary>
    public class SaveTotalScoreListDto
    {
        /// <summary>
        /// 文书创建时间
        /// </summary>
        public DateTime CreatDate { get; set; }

        /// <summary>
        /// 评分类型名称
        /// </summary>
        /// <example></example>
        public string ScoreName { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        public string PI_ID { get; set; }

        /// <summary>
        /// 评分时间、总分
        /// </summary>
        public string[,] TotalScore { get; set; }
    }
}


using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class CreateOrUpdateScoreDto
    {
        /// <summary>
        /// ScoreInfo表Id（注：修改时传入）
        /// </summary>
        public Guid Id { get; set; }

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
        public int ScoreValue { get; set; }

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
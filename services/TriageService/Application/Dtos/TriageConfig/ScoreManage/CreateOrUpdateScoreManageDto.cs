using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 创建或修改评分管理实体类
    /// </summary>
    public class CreateOrUpdateScoreManageDto
    {
        /// <summary>
        /// 评分名称
        /// </summary>
        public string ScoreName { get; set; }

        /// <summary>
        /// 评分类型
        /// </summary>
        public string ScoreType { get; set; }

        /// <summary>
        /// 动态库名称
        /// </summary>
        public string DynamicLibraryName { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
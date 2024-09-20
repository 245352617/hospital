using System;
using Volo.Abp.Application.Dtos;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 判定依据项目Dto
    /// </summary>
    public class JudgmentItemDto 
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// 判定依据主诉分类主键Id
        /// </summary>
        public Guid JudgmentMasterId { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int EmergencyLevel { get; set; }
        
        /// <summary>
        /// 分诊级别Code
        /// </summary>
        public string TriageLevelCode { get; set; }

        /// <summary>
        /// 分诊级别名称
        /// </summary>
        public string TriageLevelName { get; set; }

        /// <summary>
        /// 分诊依据
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 是否属于绿色通道 false：不属于 true：属于
        /// </summary>
        public bool IsGreenRoad { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        public string Py { get; set; }
        
        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否启用：false：不启用；true：启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
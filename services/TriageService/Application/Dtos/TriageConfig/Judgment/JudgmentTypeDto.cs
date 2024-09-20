using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 判定依据科室分类Dto
    /// </summary>
    public class JudgmentTypeDto
    {
        /// <summary>
        /// Id 
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// 系统名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 对应科室名称
        /// </summary>
        public string TriageDeptName { get; set; }

        /// <summary>
        /// 对应科室编码
        /// </summary>
        public string TriageDeptCode { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        public string Py { get; set; }
        
        /// <summary>
        /// 是否启用：0：不启用；1：启用
        /// </summary>
        public bool IsEnabled { get; set; }
        
        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 判定依据主诉分类
        /// </summary>
        public List<JudgmentMasterDto> Children { get; set; }
    }
}
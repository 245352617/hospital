using System.Collections.Generic;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分诊服务字典输出项
    /// </summary>
    public class TriageConfigOutput
    {
        /// <summary>
        /// 字典类别
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 字典数据
        /// </summary>
        public ICollection<TriageConfigDto> Items { get; set; }
    }
}
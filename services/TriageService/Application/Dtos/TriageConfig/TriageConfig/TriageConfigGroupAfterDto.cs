using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class TriageConfigGroupAfterDto
    {
        /// <summary>
        /// 类别
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 字典集合
        /// </summary>
        public ICollection<TriageConfigDto> TriageConfigDto { get; set; }
    }
}

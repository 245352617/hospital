using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class QueryTableSettingInput
    {
        /// <summary>
        /// 表格名称（不含中文）
        /// </summary>
        public string TableCode { get; set; }
    }
}

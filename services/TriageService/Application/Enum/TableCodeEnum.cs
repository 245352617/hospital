using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 表格编码类型
    /// </summary>
    public enum TableCodeEnum
    {
        [Description("分诊列表")]
        TriageTable,
    }
}

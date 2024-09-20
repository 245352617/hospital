using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.ECIS.Call.CallCenter.Dtos
{
    public class GetClientCallInfoInput : PageBase
    {
        /// <summary>
        /// 分诊科室编码
        /// </summary>
        public string TriageDept { get; set; }
    }
}

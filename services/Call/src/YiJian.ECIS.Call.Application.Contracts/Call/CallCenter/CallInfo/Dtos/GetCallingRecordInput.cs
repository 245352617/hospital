using System;
using System.Collections.Generic;
using System.Text;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.ECIS.Call.CallCenter.Dtos
{
    public class GetCallingRecordInput : PageBase
    {
        /// <summary>
        /// 分诊科室编码
        /// </summary>
        public string TriageDept { get; set; }
    }
}

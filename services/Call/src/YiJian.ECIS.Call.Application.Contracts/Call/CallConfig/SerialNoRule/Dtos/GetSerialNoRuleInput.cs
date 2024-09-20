using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.ECIS.Call.CallConfig.Dtos
{
    /// <summary>
    /// 【排队号规则】查询条件 DTO
    /// direction：input
    /// </summary>
    [Serializable]
    public class GetSerialNoRuleInput : PageBase
    {
        /// <summary>
        /// 过滤条件.
        /// </summary>
        public string Filter { get; set; }
    }
}

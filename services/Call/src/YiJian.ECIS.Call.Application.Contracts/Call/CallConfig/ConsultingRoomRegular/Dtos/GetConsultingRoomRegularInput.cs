using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.ECIS.Call.CallConfig.Dtos
{
    /// <summary>
    /// 【诊室固定】查询条件 DTO
    /// direction：input
    /// </summary>
    [Serializable]
    public class GetConsultingRoomRegularInput : PageBase
    {
        /// <summary>
        /// 过滤条件.
        /// </summary>
        public string Filter { get; set; }

        // TODO: 在特定情况下，可由前端对数据进行排序，需要传递排序字段
    }
}

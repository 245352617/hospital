namespace YiJian.Handover
{
    using System;
    using Volo.Abp.Application.Dtos;
    using YiJian.ECIS.ShareModel;
    using YiJian.ECIS.ShareModel.Models;

    /// <summary>
    /// 护士交班 分页排序输入
    /// </summary>
    [Serializable]
    public class GetNurseHandoverPagedInput : PageBase
    {
        /// <summary>
        /// 过滤条件.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 排序字段.
        /// </summary>
        public string Sorting { get; set; }
        
        
        public string StartDate { get; set; }

        public string EndDate { get; set; }
        
        
    }
}

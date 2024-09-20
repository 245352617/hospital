namespace YiJian.Handover
{
    using System;
    using Volo.Abp.Application.Dtos;
    using YiJian.ECIS.ShareModel;
    using YiJian.ECIS.ShareModel.Models;

    [Serializable]
    public class GetDoctorHandoverInput : PageBase
    {
        /// <summary>
        /// 过滤条件.
        /// </summary>
        public string Filter { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}
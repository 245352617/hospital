using System.ComponentModel.DataAnnotations;

namespace YiJian.Handover
{
    using System;
    using Volo.Abp.Application.Dtos;
    using YiJian.ECIS.ShareModel;
    using YiJian.ECIS.ShareModel.Models;

    /// <summary>
    /// 护士交班
    /// </summary>
    [Serializable]
    public class GetNurseHandoverInput
    {
        /// <summary>
        /// 班次id
        /// </summary>
        public Guid ShiftId { get; set; }

        /// <summary>
        /// 交班日期
        /// </summary>
        public string HandoverDate { get; set; }

        /// <summary>
        /// 创建人编码
        /// </summary>
        public string CreationCode { get; set; }
        
        /// <summary>
        /// 患者分诊id
        /// </summary>
        [Required]
        public Guid PI_ID { get; set; }
    }
}
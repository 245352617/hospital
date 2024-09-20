using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:床位
    /// </summary>
    public class IcuBedDto : EntityDto<Guid>
    {
        /// <summary>
        /// 床位号码
        /// </summary>
        /// <example></example>
        public string BedNum { get; set; }

        /// <summary>
        /// 病区代码
        /// </summary>
        /// <example></example>
        public string WardCode { get; set; } = "";

        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        public string DeptCode { get; set; }

        /// <summary>
        /// 房间号码
        /// </summary>
        /// <example></example>
        public string RoomCode { get; set; } = "1";

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 有效状态(1-有效，0-无效)
        /// </summary>
        /// <example></example>
        public int ValidState { get; set; } = 1;
    }
}

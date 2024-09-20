using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using YiJian.ECIS;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.Nursing
{
    /// <summary>
    /// 导管护理记录
    /// </summary>
    public class CanulaDto
    {
        /// <summary>
        /// 导管护理记录Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 导管记录Id
        /// </summary>
        public Guid CanulaId { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        [JsonConverter(typeof(DatetimeJsonConvert))]
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 护理时间(去除秒)
        /// </summary>
        /// <example></example>
        public DateTime NurseTime2
        {
            get { return Convert.ToDateTime(NurseTime.ToString("yyyy-MM-dd HH:mm")); }
        }

        // /// <summary>
        // /// 班次代码
        // /// </summary>
        // public string ScheduleCode { get; set; }

        /// <summary>
        /// 护士Id
        /// </summary>
        public string NurseId { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 置入方式
        /// </summary>
        /// <example></example>
        public string CanulaWay { get; set; }

        /// <summary>
        /// 置管长度
        /// </summary>
        /// <example></example>
        public string CanulaLength { get; set; }

        /// <summary>
        /// 导管操作记录
        /// </summary>
        public string CanulaRecord { get; set; }

        /// <summary>
        /// 签名图片
        /// </summary>
        public string Signature { get; set; } = string.Empty;

        /// <summary>
        /// 动态列表
        /// </summary>
        public List<CanulaItemDto> CanulaItemDto { get; set; }
    }
}
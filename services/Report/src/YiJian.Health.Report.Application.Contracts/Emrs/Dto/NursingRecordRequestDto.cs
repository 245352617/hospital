using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.Health.Report.Emrs.Dto
{
    /// <summary>
    /// 请求护理记录参数
    /// </summary>
    public class NursingRecordRequestDto
    {
        /// <summary>
        /// 全过程唯一ID
        /// </summary> 
        [Required(ErrorMessage ="piid是必填项")]
        public Guid PI_ID { get; set; }

        private DateTime? _beginDate;
        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? BeginDate
        {
            get
            {
                if (_beginDate.HasValue) return _beginDate;
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            set { _beginDate = value; }
        }

        private DateTime? _endDate;
        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? EndDate
        {
            get {
                if (_endDate.HasValue) return _endDate;
                return DateTime.Now; 
            }
            set { _endDate = value; }
        } 

    }
}

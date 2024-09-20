using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.Nursing.Recipes
{
    /// <summary>
    /// 描述：查询医嘱Dto
    /// 创建人： yangkai
    /// 创建时间：2023/3/9 10:57:48
    /// </summary>
    public class QueryRecipeDto : PageBase
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? EndTime { get; set; }

        /// <summary>
        ///  医嘱项目分类编码 
        /// </summary> 
        public string CategoryCode { get; set; }

        /// <summary>
        /// 途径编码
        /// </summary>
        [JsonIgnore]
        public List<string> UsageCodes { get; set; } = new List<string>();

        /// <summary>
        /// 执行单状态
        /// </summary>
        public int? ExecuteStatus { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary>
        public Guid PiId { get; set; }

        /// <summary>
        /// 患者PatientId
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        public List<string> AreaCodes { get; set; }
    }

    /// <summary>
    /// 时间日期转换
    /// </summary>
    public class DateTimeJsonConverter : JsonConverter<DateTime?>
    {
        /// <summary>
        /// 读
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.GetString().IsNullOrWhiteSpace()) return null;
            return DateTime.Parse(reader.GetString());
        }

        /// <summary>
        /// 写
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue) writer.WriteStringValue(value.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}

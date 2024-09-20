using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using YiJian.Nursing.Recipes.Dtos;

namespace YiJian.Nursing.RecipeExecutes
{
    /// <summary>
    /// 描述：执行单打印查询Dto
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 10:20:19
    /// </summary>
    public class ExecuteQueryInput
    {
        /// <summary>
        /// 患者Id
        /// </summary>
        public Guid PI_Id { get; set; }

        /// <summary>
        /// 卡片类型
        /// </summary>
        public CardTypeEnum CardType { get; set; }

        /// <summary>
        /// 执行日期
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime ExecuteDate { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
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
        /// 医嘱类型
        /// </summary>
        public string PrescribeTypeCode { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        public List<string> AreaCodes { get; set; }
    }

    /// <summary>
    /// 时间日期转换
    /// </summary>
    public class DateTimeConverter : JsonConverter<DateTime?>
    {
        /// <summary>
        /// 读
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
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
        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue) writer.WriteStringValue(value.Value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }


}

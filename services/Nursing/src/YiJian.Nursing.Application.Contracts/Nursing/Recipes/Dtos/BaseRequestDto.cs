using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YiJian.Nursing.Recipes.Dtos
{
    /// <summary>
    /// 描述：取消操作Dto
    /// 创建人： yangkai
    /// 创建时间：2023/3/9 14:13:19
    /// </summary>
    public class BaseRequestDto
    {
        /// <summary>
        /// 执行单Id
        /// </summary>
        public Guid RecipeExecId { get; set; }

        /// <summary>
        /// 操作人编码
        /// </summary>
        public string OperateCode { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        public string OperateName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime OperateTime { get; set; }
    }

    /// <summary>
    /// 时间日期转换
    /// </summary>
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        /// <summary>
        /// 读
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.GetString().IsNullOrWhiteSpace()) return DateTime.MinValue;
            return DateTime.Parse(reader.GetString());
        }

        /// <summary>
        /// 写
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}

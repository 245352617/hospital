using System.Text.Json;
using System.Text.Json.Serialization;

namespace YiJian.ECIS.ShareModel.DDPs.JsonConverts
{
    /// <summary>
    /// 转换器
    /// </summary>
    public class DdpIntNullableJsonConverter : JsonConverter<int?>
    {
        /// <summary>
        /// Read
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.GetString().IsNullOrWhiteSpace()) return null;
            int result = 0;
            if (int.TryParse(reader.GetString(), out result)) return result;
            return null;
        }
        /// <summary>
        /// Write
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteNumberValue(value.Value);
            }
        }
    }

}

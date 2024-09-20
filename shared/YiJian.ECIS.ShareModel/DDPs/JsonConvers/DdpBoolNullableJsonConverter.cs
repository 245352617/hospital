using System.Text.Json;
using System.Text.Json.Serialization;

namespace YiJian.ECIS.ShareModel.DDPs.JsonConverts
{
    /// <summary>
    /// 转换器
    /// </summary>
    public class DdpBoolNullableJsonConverter : JsonConverter<bool?>
    {
        /// <summary>
        /// Read
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override bool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.GetString().IsNullOrWhiteSpace()) return null;
            var str = reader.GetString();
            if (str == "1")
            {
                return true;
            }
            else if (str == "0")
            {
                return false;
            }
            else
            {
                bool result = false;
                if (bool.TryParse(reader.GetString(), out result)) return result;
            }
            return null;
        }
        /// <summary>
        /// Write
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, bool? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteBooleanValue(value.Value);
            }
        }
    }

}

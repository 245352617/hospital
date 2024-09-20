using System.Text.Json;
using System.Text.Json.Serialization;

namespace YiJian.ECIS.ShareModel.DDPs.JsonConverts
{
    /// <summary>
    /// 转换器
    /// </summary>
    public class DdpIntJsonConverter : JsonConverter<int>
    {
        /// <summary>
        /// Read
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.GetString().IsNullOrWhiteSpace()) return 0;
            int result = 0;
            if (int.TryParse(reader.GetString(), out result)) return result;
            return 0;
        }
        /// <summary>
        /// Write
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }

}

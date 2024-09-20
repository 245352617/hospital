using System.Text.Json;
using System.Text.Json.Serialization;

namespace YiJian.ECIS.ShareModel.DDPs.JsonConverts
{
    /// <summary>
    /// 转换器
    /// </summary>
    public class DdpInsuranceCodeJsonConverter : JsonConverter<int>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.GetString().IsNullOrWhiteSpace()) return 0;
            var insuranceCatalog = reader.GetString();
            int result = 0;
            if (int.TryParse(reader.GetString(), out result))
            {
                if (insuranceCatalog == "自费")
                {
                    result = 0;
                }
                else if (insuranceCatalog == "甲")
                {
                    result = 1;
                }
                else if (insuranceCatalog == "乙")
                {
                    result = 2;
                }
                else if (insuranceCatalog == "丙")
                {
                    result = 4;
                }
                else if (insuranceCatalog == "少儿")
                {
                    result = 5;
                }
                else
                {
                    result = 3;
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }

}

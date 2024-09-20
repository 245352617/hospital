using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using YiJian.ECIS.ShareModel.Extensions;

namespace YiJian.EMR.Props.Dto
{
    public class CategoryPropertyTreeJsonConverter : JsonConverter<CategoryPropertyTreeDto>
    {
        public override CategoryPropertyTreeDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<CategoryPropertyTreeDto>(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, CategoryPropertyTreeDto value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString(nameof(value.Id).ToSmallHump(), value.Id);
            writer.WriteString(nameof(value.Value).ToSmallHump(), value.Value);
            writer.WriteString(nameof(value.Label).ToSmallHump(), value.Label);
            writer.WriteString(nameof(value.ParentId).ToSmallHump(), value.ParentId.HasValue ? value.ParentId.Value.ToString() : "");

            writer.WriteNumber(nameof(value.Lv).ToSmallHump(), value.Lv);


            if (!value.Children.IsNullOrEmpty())
            {
                writer.WritePropertyName(nameof(value.Children).ToSmallHump());
                JsonSerializer.Serialize(writer, value.Children, options);
            }
            writer.WriteEndObject();
        }
    }
}

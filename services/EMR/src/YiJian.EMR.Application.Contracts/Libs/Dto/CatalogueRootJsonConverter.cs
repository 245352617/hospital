using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using YiJian.ECIS.ShareModel.Extensions;

namespace YiJian.EMR.Libs.Dto
{
    public class CatalogueRootJsonConverter : JsonConverter<CatalogueRootDto>
    {
        public CatalogueRootJsonConverter()
        {

        }

        public override CatalogueRootDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<CatalogueRootDto>(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, CatalogueRootDto value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString(nameof(value.Id).ToSmallHump(), value.Id);
            writer.WriteString(nameof(value.Title).ToSmallHump(), value.Title);
            writer.WriteString(nameof(value.ParentId).ToSmallHump(), value.ParentId.HasValue ? value.ParentId.Value.ToString() : "");
            writer.WriteNumber(nameof(value.Sort).ToSmallHump(), value.Sort);
           
            if (!value.Catalogues.IsNullOrEmpty())
            {
                writer.WritePropertyName(nameof(value.Catalogues).ToSmallHump());
                JsonSerializer.Serialize(writer, value.Catalogues, options);
            }
            writer.WriteEndObject();
        }
    }
}

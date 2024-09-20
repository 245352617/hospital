using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using YiJian.ECIS.ShareModel.Extensions;


namespace YiJian.EMR.Libs.Dto
{
    public class CatalogueFileTreeJsonConverter : JsonConverter<CatalogueFileTreeDto>
    {
        public CatalogueFileTreeJsonConverter()
        {

        }

        public override CatalogueFileTreeDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        { 
            return JsonSerializer.Deserialize<CatalogueFileTreeDto>(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, CatalogueFileTreeDto value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString(nameof(value.Id).ToSmallHump(), value.Id);
            writer.WriteString(nameof(value.Title).ToSmallHump(), value.Title);
            writer.WriteString(nameof(value.ParentId).ToSmallHump(), value.ParentId.HasValue ? value.ParentId.Value.ToString() : "");
            writer.WriteNumber(nameof(value.Sort).ToSmallHump(), value.Sort);
            writer.WriteBoolean(nameof(value.IsFile).ToSmallHump(), value.IsFile);
        
            if (!value.Catalogues.IsNullOrEmpty())
            {
                writer.WritePropertyName(nameof(value.Catalogues).ToSmallHump());
                JsonSerializer.Serialize(writer, value.Catalogues, options);
            }
            writer.WriteEndObject();
        }
    }
}

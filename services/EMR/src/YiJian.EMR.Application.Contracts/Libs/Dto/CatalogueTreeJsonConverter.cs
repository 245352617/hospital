using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization; 
using YiJian.ECIS.ShareModel.Extensions;

namespace YiJian.EMR.Libs.Dto
{
    /// <summary>
    /// 目录结构树转换器
    /// </summary>
    public class CatalogueTreeJsonConverter : JsonConverter<CatalogueTreeDto>
    {
        public CatalogueTreeJsonConverter()
        { 
        }

        public override CatalogueTreeDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<CatalogueTreeDto>(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, CatalogueTreeDto value, JsonSerializerOptions options)
        { 
            writer.WriteStartObject();

            writer.WriteString(nameof(value.Id).ToSmallHump(), value.Id.Value);
            writer.WriteString(nameof(value.Title).ToSmallHump(), value.Title);
            writer.WriteString(nameof(value.ParentId).ToSmallHump(), value.ParentId.HasValue ? value.ParentId.Value.ToString() : "");
            writer.WriteNumber(nameof(value.Sort).ToSmallHump(), value.Sort);
            writer.WriteNumber(nameof(value.Lv).ToSmallHump(), value.Lv);
            writer.WriteBoolean(nameof(value.IsFile).ToSmallHump(), value.IsFile);

            if (!value.Catalogues.IsNullOrEmpty())
            {
                writer.WritePropertyName(nameof(value.Catalogues).ToSmallHump());
                JsonSerializer.Serialize(writer,value.Catalogues,options);
            } 
            writer.WriteEndObject(); 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using YiJian.ECIS.ShareModel.Extensions;

namespace YiJian.EMR.Templates.Dto
{
    /// <summary>
    /// 通用模板目录结构树转换器
    /// </summary>
    public class GeneralCatalogueTreeJsonConverter : JsonConverter<GeneralCatalogueTreeDto>
    {
        public override GeneralCatalogueTreeDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<GeneralCatalogueTreeDto>(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, GeneralCatalogueTreeDto value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString(nameof(value.Id).ToSmallHump(), value.Id);
            writer.WriteString(nameof(value.Title).ToSmallHump(), value.Title);
            writer.WriteString(nameof(value.Code).ToSmallHump(), value.Code);
            writer.WriteString(nameof(value.ParentId).ToSmallHump(), value.ParentId.HasValue ? value.ParentId.Value.ToString() : "");
            writer.WriteNumber(nameof(value.Sort).ToSmallHump(), value.Sort);
            writer.WriteNumber(nameof(value.Lv).ToSmallHump(), value.Lv);
            writer.WriteBoolean(nameof(value.IsFile).ToSmallHump(), value.IsFile);
            writer.WriteBoolean(nameof(value.IsEnabled).ToSmallHump(), value.IsEnabled);
            writer.WriteNumber(nameof(value.TemplateType).ToSmallHump(), (int)value.TemplateType);
            writer.WriteString(nameof(value.CatalogueId).ToSmallHump(), value.CatalogueId.HasValue ? value.CatalogueId.Value.ToString() : "");
            writer.WriteString(nameof(value.CatalogueTitle).ToSmallHump(), value.CatalogueTitle);
            writer.WriteString(nameof(value.OriginId).ToSmallHump(), value.OriginId.HasValue? value.OriginId.Value.ToString():"");

            if (!value.Catalogues.IsNullOrEmpty())
            {
                writer.WritePropertyName(nameof(value.Catalogues).ToSmallHump());
                JsonSerializer.Serialize(writer, value.Catalogues, options);
            }
            writer.WriteEndObject();
        }
    }
}

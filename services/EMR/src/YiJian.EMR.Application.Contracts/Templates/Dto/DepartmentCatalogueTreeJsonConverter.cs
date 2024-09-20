using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using YiJian.ECIS.ShareModel.Extensions;


namespace YiJian.EMR.Templates.Dto
{
    internal class DepartmentCatalogueTreeJsonConverter : JsonConverter<DepartmentCatalogueTreeDto>
    {
        public DepartmentCatalogueTreeJsonConverter()
        {

        }

        public override DepartmentCatalogueTreeDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<DepartmentCatalogueTreeDto>(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DepartmentCatalogueTreeDto value, JsonSerializerOptions options)
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
            writer.WriteString(nameof(value.DeptCode).ToSmallHump(), value.DeptCode);
            writer.WriteString(nameof(value.DeptName).ToSmallHump(), value.DeptName);
            writer.WriteString(nameof(value.InpatientWardId).ToSmallHump(), value.InpatientWardId.HasValue ? value.InpatientWardId.Value.ToString():"");
            //writer.WriteString(nameof(value.WardName).ToSmallHump(), value.WardName);
            writer.WriteString(nameof(value.CatalogueId).ToSmallHump(), value.CatalogueId.HasValue? value.CatalogueId.Value.ToString():"");
            writer.WriteString(nameof(value.CatalogueTitle).ToSmallHump(), value.CatalogueTitle);
            writer.WriteString(nameof(value.OriginId).ToSmallHump(), value.OriginId.HasValue ? value.OriginId.Value.ToString() : "");

            if (!value.Catalogues.IsNullOrEmpty())
            {
                writer.WritePropertyName(nameof(value.Catalogues).ToSmallHump());
                JsonSerializer.Serialize(writer, value.Catalogues, options);
            }
            writer.WriteEndObject();
        }
    }
}

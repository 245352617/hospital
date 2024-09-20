using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace YiJian.BodyParts.WebAPI
{
    /// <summary>
    /// 过滤具备SwaggerIgnore特性的api
    /// </summary>
    public class SwaggerIgnoreFilter : IDocumentFilter
    {
        public static List<OpenApiTag> _tagList = new List<OpenApiTag>();

        public static void AddTags(List<OpenApiTag> tagList)
        {
            if (tagList == null || tagList.Count == 0) return;
            _tagList.AddRange(tagList);
        }

        public static void AddTags(OpenApiTag tag)
        {
            if (tag == null) return;
            _tagList.Add(tag);
        }


        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            //	查找 Abp下的API服务名
            // RelativePath	"api/abp/api-definition"
            var apis = context.ApiDescriptions.Where(x => x.GroupName.Contains("Abp"));

            if (apis != null)
            {
                foreach (var ignoreApi in apis)
                {
                    swaggerDoc.Paths.Remove("/" + ignoreApi.RelativePath);
                }
            }

            swaggerDoc.Tags = _tagList.OrderBy(x => x.Name).ToList();
        }

    }
}

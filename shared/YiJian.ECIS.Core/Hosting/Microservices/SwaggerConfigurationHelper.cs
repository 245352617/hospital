using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.IO;
using Volo.Abp.Modularity;
using YiJian.ECIS.Core.Utils;

namespace YiJian.ECIS.Core.Hosting.Microservices;

public static class SwaggerConfigurationHelper
{
    public static void Configure(
        ServiceConfigurationContext context
    )
    {
        var configuration = context.Services.GetConfiguration();

        string name = configuration["Swagger:Name"];
        string ns = configuration["Swagger:ns"];
        string apiTitle = configuration["Swagger:apiTitle"];

        context.Services.AddSwaggerGen(
            options =>
            {
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer {Token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.SwaggerDoc(name, new OpenApiInfo { Title = apiTitle, Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
                var basePath = Path.GetDirectoryName(typeof(SwaggerConfigurationHelper).Assembly.Location);
                var xmlDocs = SwaggerXmlDoc.GetInstance().XmlDocs(ns);
                foreach (var item in xmlDocs)
                {
                    var xmlPath = Path.Combine(basePath, item);
                    if (File.Exists(xmlPath))
                    {
                        options.IncludeXmlComments(xmlPath, true);
                    }
                }

                options.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                    return controllerAction.ControllerName + "-" + controllerAction.ActionName;
                });
            });
    }
}
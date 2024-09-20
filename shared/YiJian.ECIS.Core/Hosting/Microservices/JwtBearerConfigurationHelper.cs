using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using Volo.Abp.Modularity;
using YiJian.ECIS.Core.Middlewares;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.ECIS.Authorization;
using Polly;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace YiJian.ECIS.Core.Hosting.Microservices;

public static class JwtBearerConfigurationHelper
{
    public static void Configure(
        ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        bool skipValidate = bool.TryParse(configuration["AuthServer:SkipValidate"], out var result) && result;
        if (skipValidate)
        {
            context.Services.AddAuthentication(o =>
            {
                o.DefaultScheme = nameof(EcisApiResponseHandler);
                o.DefaultChallengeScheme = nameof(EcisApiResponseHandler);
                o.DefaultForbidScheme = nameof(EcisApiResponseHandler);
            }).AddScheme<AuthenticationSchemeOptions, EcisApiResponseHandler>(nameof(EcisApiResponseHandler), o =>
            {
            });
        }
        else
        {
            context.Services //.AddAuthentication("Bearer")
            .AddAuthentication(o =>
            {
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = nameof(ApiResponseHandler);
                o.DefaultForbidScheme = nameof(ApiResponseHandler);
            })
            .AddJwtBearer(r =>
            {
                //认证地址
                r.Authority = configuration["AuthServer:Authority"];
                //权限标识
                r.Audience = configuration["AuthServer:Audience"];
                //是否必需HTTPS
                r.RequireHttpsMetadata = configuration["AuthServer:RequireHttpsMetadata"]?.ToLower() == "true";
                r.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        // 跳过默认的处理逻辑，返回下面的模型数据
                        context.HandleResponse();
                        context.Response.ContentType = "application/json;charset=utf-8";
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        var ret = new ResponseBase<string>(EStatusCode.C401);
                        ret.Data = "未能获取到登录身份信息，请检查后重试 (×_×)";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(ret));
                    }
                };
            }).AddScheme<AuthenticationSchemeOptions, ApiResponseHandler>(nameof(ApiResponseHandler), o => { });
        }
    }
}

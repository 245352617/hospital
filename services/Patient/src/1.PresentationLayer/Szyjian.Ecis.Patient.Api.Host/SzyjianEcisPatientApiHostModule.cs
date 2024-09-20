using BeetleX.Http.Clients;
using Castle.DynamicProxy;
using FreeSql;
using Hangfire;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using SkyApm.Utilities.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text.Json;
using Szyjian.BasePresentation;
using Szyjian.Consul;
using Szyjian.Ecis.Patient.Application;
using Szyjian.Ecis.Patient.BackgroundJob;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Modularity;
using Volo.Abp.Users;
using YiJian.ECIS.DDP;

namespace Szyjian.Ecis.Patient
{
    [DependsOn(typeof(SzyjianBasePresentationModule),
        typeof(SzyjianEcisPatientApplicationModule),
        typeof(AbpAspNetCoreSignalRModule))]
    public class SzyjianEcisPatientApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            IServiceCollection service = context.Services;
            IConfiguration config = context.Services.GetConfiguration();
            context.Services.AddHttpClient();
            context.Services.Configure<RemoteServices>(config.GetSection("RemoteServices")); //远程服务地址配置

            ConfigureApiService(service, config);

            ConfigureConventionalControllers(config);
            ConfigureAuthentication(service, config);
            ConfigureSwaggerService(service);

            // SkyWalking APM
            context.Services.AddSkyApmExtensions();
        }

        /// <summary>
        /// 根据应用服务层自动生成 Api Controller
        /// </summary>
        private void ConfigureConventionalControllers(IConfiguration config)
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(SzyjianEcisPatientApplicationModule).Assembly,
                    action =>
                    {
                        // 修改成首字母小写
                        string text = config["ServiceName"];
                        string first = text[..1];
                        action.RootPath = first.ToLowerInvariant() + text[1..];
                    });
            });
        }

        /// <summary>
        /// 配置认证 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="config"></param>
        private void ConfigureAuthentication(IServiceCollection service, IConfiguration config)
        {
            // 覆盖 Abp 的当前用户定义，与急危重症一体化平台保持一致，不会因 Abp 版本更新导致问题
            service.AddSingleton<ICurrentUser, EcisCurrentUser>();
            bool skipValidate = bool.TryParse(config["AuthServer:SkipValidate"], out var result) && result;
            if (skipValidate)
            {
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                service.AddAuthentication(o =>
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
                IdentityModelEventSource.ShowPII = true;
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                service.AddAuthentication("Bearer").AddJwtBearer(options =>
                {
                    options.Authority = config["AuthServer:Authority"];
                    options.Audience = config["AuthServer:Audience"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(config["AuthServer:RequireHttpsMetadata"]);
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async challengeContext =>
                        {
                            // 跳过默认的处理逻辑，返回下面的模型数据
                            challengeContext.HandleResponse();
                            challengeContext.Response.ContentType = "application/json;charset=utf-8";
                            challengeContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            var result = new
                            {
                                Code = 401,
                                Msg = "没有权限",
                                Data = "Unauthorized"
                            };

                            await challengeContext.Response.WriteAsync(JsonSerializer.Serialize(result));
                        }
                    };
                });
            }
        }

        /// <summary>
        /// 配置 swagger 文档
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureSwaggerService(IServiceCollection services)
        {
            services.AddApiVersioning(action =>
            {
                action.AssumeDefaultVersionWhenUnspecified = true;
                action.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSwaggerGen(options =>
            {
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);

                options.SwaggerDoc("ecis-patient", new OpenApiInfo
                {
                    Title = "急诊病患微服务",
                    Version = "V1",
                    Description = "急诊系统病患微服务接口文档"
                });

                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header
                });

                // 使用反射获取xml文件。并构造出文件的路径
                var listAssembly = new List<string>
                {
                    "Szyjian.Ecis.Patient.Application",
                    "Szyjian.Ecis.Patient.Application.Contracts",
                    "Szyjian.Ecis.Patient.Domain",
                    "Szyjian.Ecis.Patient.Domain.Shared"
                };

                // 读取xml文件，这些xml文件提供了接口以及参数的注释，能够让人直观看到接口和参数的注释
                // 需要在各Assembly上设置里把“生成包含 API 文档的文件”勾选上
                // listAssembly是需要设置的的Assembly列表
                foreach (string xmlPath in listAssembly
                    .Select(assembly => Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? "", $"{assembly}.xml"))
                    .Where(File.Exists))
                {
                    options.IncludeXmlComments(xmlPath, true);
                }
            });
        }


        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            IApplicationBuilder app = context.GetApplicationBuilder();
            IWebHostEnvironment env = context.GetEnvironment();

            //使用Mapster作为实体映射中间件，替代AutoMapper作用
            MapsterMappingConfig.InitMapsterConfig();

            IConfiguration config = app.ApplicationServices.GetRequiredService<IConfiguration>();
            if (env.IsDevelopment() || Convert.ToBoolean(config["Settings:IsShowSwagger"]))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(a => { a.RouteTemplate = "/api/{documentName}/swagger.json"; });
                app.UseSwaggerUI(options =>
                {
                    options.DocumentTitle = " 急诊病患微服务 - 接口文档 ";
                    options.SwaggerEndpoint("/api/ecis-patient/swagger.json",
                        $"{config["ApplicationName"].ToUpper()} {config["ServiceName"].ToUpper()} v1");

                    options.DocExpansion(DocExpansion.None);
                    options.DefaultModelExpandDepth(-1);

                });
            }

            if (Convert.ToBoolean(config["Settings:IsUseConsul"]))
            {
                app.UseConsulRegister();
            }

            // Hangfire后台工作
            string text = config["ServiceName"];
            string first = text[..1];
            app.UseHangfireServer();
            app.UseHangfireDashboard($"/api/{first.ToLowerInvariant() + text[1..]}/hangfire");
            context.ServiceProvider.UseHangfireJobs(context.GetConfiguration());

            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseConfiguredEndpoints(builder =>
            {
                builder.MapDefaultControllerRoute();
            });
        }

        private void ConfigureApiService(IServiceCollection service, IConfiguration config)
        {
            service.AddTransient(provider =>
            {
                using HttpCluster httpCluster = new HttpCluster();
                httpCluster.TimeOut = 60 * 1000;

                return httpCluster;
            });
            service.AddTransient(typeof(BeetleXApiInterceptor<>));
            service.AddTransient(provider =>
            {
                using HttpCluster httpCluster = provider.GetService<HttpCluster>();
                httpCluster.DefaultNode.Add(config["RemoteServices:Call:BaseUrl"]);

                var beetleXApiInterceptor = provider.GetService<BeetleXApiInterceptor<ICallApi>>();
                Microsoft.Extensions.Logging.ILogger<ICallApi> logger
                    = provider.GetService<Microsoft.Extensions.Logging.ILogger<ICallApi>>();
                ICallApi callService = httpCluster.Create<ICallApi>();
                if (!bool.TryParse(config["DisableApiProxy"], out bool disableApiProxy) || !disableApiProxy)
                {
                    ProxyGenerator proxyGenerator = new ProxyGenerator();
                    ICallApi proxyService = proxyGenerator.CreateInterfaceProxyWithTarget(callService, beetleXApiInterceptor);
                    return proxyService;
                }
                return callService;
            });
            service.AddTransient(provider =>
            {
                using HttpCluster httpCluster = provider.GetService<HttpCluster>();
                httpCluster.DefaultNode.Add(config["RemoteServices:DDP:BaseUrl"]);

                var beetleXApiInterceptor = provider.GetService<BeetleXApiInterceptor<DdpApiClient>>();
                Microsoft.Extensions.Logging.ILogger<DdpApiClient> logger = provider.GetService<Microsoft.Extensions.Logging.ILogger<DdpApiClient>>();
                DdpApiClient ddpApiClient = httpCluster.Create<DdpApiClient>();
                //if (!bool.TryParse(config["DisableApiProxy"], out bool disableApiProxy) || !disableApiProxy)
                //{
                //    ProxyGenerator proxyGenerator = new ProxyGenerator();
                //    DdpApiClient proxyService = proxyGenerator.CreateInterfaceProxyWithTarget(ddpApiClient, beetleXApiInterceptor);
                //    return proxyService;
                //}
                return ddpApiClient;

            });

        }
    }
}
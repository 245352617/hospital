using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using YiJian.BodyParts.Application;
using YiJian.BodyParts.EntityFrameworkCore;
using YiJian.BodyParts.EntityFrameworkCore.Migrations;
using ICIS.Extras.DatabaseAccessor.Minio.Extensions;
using YiJian.BodyParts.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Json;
using Volo.Abp.Modularity;
using DbContext = YiJian.BodyParts.EntityFrameworkCore.DbContext;
using YiJian.BodyParts.EntityFrameworkCore.Extensions;
using YiJian.BodyParts.Domain.Shared.RabbitMq.Producer;
using RabbitMQ.Client;

namespace YiJian.BodyParts.WebAPI
{
    [DependsOn(
          typeof(AbpAspNetCoreMvcModule),
          typeof(ApplicationModule),
          typeof(EntityFrameworkCoreModule),
          typeof(AbpBackgroundWorkersModule),
          typeof(AbpEventBusRabbitMqModule),
          typeof(AbpAutofacModule),
          typeof(EntityFrameworkCoreDbMigrationsModule),
          typeof(AbpAspNetCoreSerilogModule),
          typeof(AbpEntityFrameworkCoreSqlServerModule)
    )]
    public class HttpApiHostModule : AbpModule
    {
        //跨域
        private const string DefaultCorsPolicyName = "Default";
        //系统名
        private const string DefaultSystemName = "ecis-bodyparts";

        private const string DefaultAssemblyName = "YiJian.BodyParts";
        //默认真值字符串
        private const string DefaultTureString = "1trueTRUETrue";

        private static bool isDebugger { get; set; }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            isDebugger = false;
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            context.Services.AddControllers().AddJsonOptions(options =>
            {
                //解决JSON 中文乱码问题
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                options.JsonSerializerOptions.IgnoreNullValues = false;
            });

            // 配置JSON的时间转换格式
            Configure<MvcNewtonsoftJsonOptions>(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            Configure<AbpJsonOptions>(options => options.DefaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss");

            var hasAuthHeader =
                "1trueTRUE".Contains(Convert.ToString(configuration["AuthServer:hasAuthHeader"]));
            if (hasAuthHeader)
            {
                //ToKen验证filter
                context.Services.AddMvc(cfg => { cfg.Filters.Add(new TokenAttribute()); });
            }

            Configure<AbpDbContextOptions>(options => { options.UseSqlServer(); });

            //移除ABP默认拦截器
            Configure<MvcOptions>(options =>
            {
                var filterMetadata = options.Filters.FirstOrDefault(x => x is ServiceFilterAttribute attribute
                                                                         && attribute.ServiceType.Equals(
                                                                             typeof(AbpExceptionFilter)));
                options.Filters.Remove(filterMetadata);
                //加入拦截器
                options.Filters.Add(typeof(GlobalExceptionFilter));
            });


            //自动添加 Controller
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers
                    .Create(typeof(ApplicationModule).Assembly, opts => { opts.RootPath = DefaultSystemName; });
                options.ConventionalControllers.Create(typeof(ApplicationModule).Assembly);
            });

            //if (!isDebugger)
            ConfigureAuthentication(context, configuration);

            // 本地minioadmin 127.0.0.1   192.168.0.173
            context.Services.AddMinio(options =>
            {
                options.Ssl = false;
                if (bool.TryParse(configuration["MinIO:IsHttps"], out var value))
                {
                    options.Ssl = value;
                }

                options.AccessKey = configuration["MinIO:MINIO_ACCESS_KEY"];
                options.SecretKey = configuration["MinIO:MINIO_SECRET_KEY"];
                options.Timeout = 10000;
                options.Endpoint = configuration["MinIO:Url"];
            });

            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc(DefaultSystemName.ToLower(),
                        new OpenApiInfo { Title = $"{DefaultSystemName.ToUpper()}(急诊皮肤微服务系统) API", Version = "1.0" });
                    options.DocumentFilter<SwaggerIgnoreFilter>();
                    options.DocInclusionPredicate((docName, description) => true);

                    //"oauth"
                    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                    {
                        Description = "授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                        Name = "Authorization", //jwt默认的参数名称
                        In = ParameterLocation.Header, //jwt默认存放Authorization信息的位置(请求头中)
                        Type = SecuritySchemeType.ApiKey
                    });

                    //Json Token认证方式，此方式为全局添加
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference()
                                {
                                    Id = JwtBearerDefaults.AuthenticationScheme,
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            Array.Empty<string>()
                        }
                    });


                    // 使用反射获取xml文件。并构造出文件的路径
                    List<string> listAssembly = new List<string>() { $"{DefaultAssemblyName}.WebAPI", $"{DefaultAssemblyName}.Application", $"{DefaultAssemblyName}.Application.Contracts", $"{DefaultAssemblyName}.Domain.Shared" };
                    foreach (var assembly in listAssembly)
                    {
                        var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{assembly}.xml");
                        if (File.Exists(xmlPath))
                            // 启用xml注释. 该方法第二个参数启用控制器的注释，默认为false.
                            options.IncludeXmlComments(xmlPath, true);
                    }
                });

            context.Services.AddCors(options => options.AddPolicy(DefaultCorsPolicyName,
                builder =>
                {
                    builder.AllowAnyMethod()
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyHeader()
                        .AllowCredentials();
                }));


            //#region 开启RabbitMQ_s

            //var preConfigOption = context.Services.ExecutePreConfiguredActions<MQPreConfigOption>();
            //Configure<AppConfiguration>(opt =>
            //{
            //    opt.RabbitHost = configuration["RabbitMQ:Connections:Default:HostName"];

            //    opt.RabbitUserName = configuration["RabbitMQ:Connections:Default:UserName"];

            //    opt.RabbitPassword = configuration["RabbitMQ:Connections:Default:Password"];

            //    opt.ExChangeName = configuration["RabbitMQ:EventBus:ExchangeName"];

            //    //opt.QueName = configuration["RabbitMQ:EventBus:QueName"];

            //    //opt.RouteKey = configuration["RabbitMQ:EventBus:RouteKey"];

            //    int _port = 0;

            //    int.TryParse(configuration["RabbitMQ:Connections:Default:Port"] ?? "0", out _port);

            //    if (_port > 0) opt.RabbitPort = _port;
            //});


            ////注册mq常驻链接
            //var factory = new ConnectionFactory()
            //{
            //    HostName = configuration["RabbitMQ:Connections:Default:HostName"],
            //    UserName = configuration["RabbitMQ:Connections:Default:UserName"],
            //    Password = configuration["RabbitMQ:Connections:Default:Password"],
            //    Port = Convert.ToInt32(configuration["RabbitMQ:Connections:Default:Port"]),
            //    ClientProvidedName = "YiJian.BodyParts.api"
            //};
            //context.Services.AddSingleton(factory);

            //Configure<AbpBackgroundWorkerOptions>(options =>
            //{
            //    options.IsEnabled = preConfigOption.IsStartBackgroundWorker;
            //    ConsoleExten.WriteLine(
            //        "AbpBackgroundWorkerOptions.isEnabled:" + preConfigOption.IsStartBackgroundWorker);
            //});

            //Log.Information($"\n==>开启rabbitmq_s:{preConfigOption.IsStartRabbitMQ}\n");

            //#endregion

            //#region 添加RabbitMq生产者

            //context.Services.ConfigureRabbitMqProducer(option =>
            //{
            //    option.Address = configuration["RabbitMQ:Connections:Default:HostName"];
            //    option.Port = configuration["RabbitMQ:Connections:Default:Port"];
            //    option.VirtualHost = "/";
            //    option.UserName = configuration["RabbitMQ:Connections:Default:UserName"];
            //    option.Password = configuration["RabbitMQ:Connections:Default:Password"];
            //});

            //#endregion
            // context.Services.AddSignalR();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            //app.UseHttpsRedirection();
            var configuration = context.GetConfiguration();


            //if (env.IsDevelopment())
            //{
            // 测试环境下，自动生成数据库和种子数据
            SeedData(context);
            //} 

            //设置全局时间格式
            app.ConfigureDateTimeFormat();

            //启用Logs目录浏览，可以直接在url后面加/logs访问日志文件
            app.UseLogFiles();

            app.UseCorrelationId();
            app.UseVirtualFiles();

            app.UseRouting();

            app.UseCors(DefaultCorsPolicyName);
            app.UseAuthentication();

            app.UseAbpRequestLocalization();

            app.UseAuthorization();
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/api/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/api/{DefaultSystemName.ToLower()}/swagger.json", "YiJian.BodyParts API");
                options.DocumentTitle = $"😄 {DefaultSystemName.ToUpper()}- 接口文档 👍👍👍";
                options.DocExpansion(DocExpansion.None);
                options.DefaultModelExpandDepth(1);
            });
            app.UseAuditing();

            app.UseAbpSerilogEnrichers();

            app.UseConfiguredEndpoints();

            app.UseMiddleware<GlobalMiddleware>();

        }

        private void SeedData(ApplicationInitializationContext context)
        {
            //AsyncHelper.RunSync(async () =>
            //{
            using (var scope = context.ServiceProvider.CreateScope())
            {
                //await scope.ServiceProvider.GetRequiredService<DbContext>().Database.EnsureCreatedAsync();
                //await scope.ServiceProvider
                //    .GetRequiredService<IDataSeeder>()
                //    .SeedAsync();

                //启动生成数据库                        
                var dbMigrationsContext = scope.ServiceProvider.GetRequiredService<DbMigrationsContext>();
                if (dbMigrationsContext != null)
                {
                    dbMigrationsContext.Database.MigrateAsync().Wait();
                    GC.SuppressFinalize(dbMigrationsContext);
                }
                else
                {
                    scope.ServiceProvider.GetRequiredService<DbContext>().Database.EnsureCreatedAsync().Wait();
                }

                //启动发送种子数据
                scope.ServiceProvider.GetRequiredService<IDataSeeder>().SeedAsync().Wait();
            }
            //});
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            IdentityModelEventSource.ShowPII = true;
            Log.Information("===============Authority:" + configuration["AuthServer:Authority"]);
            Log.Information("===============Audience:" + configuration["AuthServer:Audience"]);

            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

                            var result = new JsonResult
                            { Code = 401, Msg = "未能获取到登录身份信息，请检查后重试 (×_×)", Data = "UnAuthorized" };

                            await context.Response.WriteAsync(result.ToString());
                        }
                    };
                });
        }
    }
}
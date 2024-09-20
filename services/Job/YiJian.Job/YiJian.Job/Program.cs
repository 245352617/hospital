using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using YiJian.Job.BackgroundService.Contract;
using YiJian.Job.BackgroundService.RabbitMQ;
using YiJian.Job.Extensions;
using YiJian.Job.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMemoryCache,MemoryCache>(); 
//注册作业配置项
builder.Services.Configure<List<HangfireJob>>(builder.Configuration.GetSection(HangfireJob.Jobs));

//注册CAP
builder.Services.AddCap(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    x.UseRabbitMQ(mq =>
    {
        mq.VirtualHost = builder.Configuration.GetSection("RabbitMQ").GetValue("VirtualHost", "/");
        mq.Port = builder.Configuration.GetSection("RabbitMQ").GetValue("Port", 5672);
        mq.ExchangeName = builder.Configuration.GetSection("RabbitMQ").GetValue("ExchangeName", "szyjian.cap");
        mq.HostName = builder.Configuration.GetSection("RabbitMQ").GetValue("HostName", "192.168.241.101");
        mq.UserName = builder.Configuration.GetSection("RabbitMQ").GetValue("UserName", "guest");
        mq.Password = builder.Configuration.GetSection("RabbitMQ").GetValue("Password", "guest");
    });
    x.FailedRetryCount = 3;
    x.UseDashboard();
    x.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
});

//注册缓存
builder.Services.AddDistributedRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetSection("Redis").GetValue<string>("Configuration");
    options.InstanceName = builder.Configuration.GetSection("Redis").GetValue("KeyPrefix", "YiJianJob");
});

// 注册 Hangfire 服务.
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("Default"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true,

    }));
  
// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//注册后台服务
builder.Services.AddServices();

var app = builder.Build();

//use hangfire dashboard
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = (IEnumerable<IDashboardAuthorizationFilter>)(new[]
    {
        new MyDashboardAuthorizeFilter()
    })
});

app.UseHangfireJob(builder.Configuration["HospitalCode"]);

app.UseAuthorization();

app.MapControllers();

app.Run();


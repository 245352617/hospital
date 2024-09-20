using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using YiJian.ECIS.ShareModel.Models;
using YiJian.EMR.DCWriter.Models;
using YiJian.EMR.DCWriter.Services; 

namespace YiJian.EMR.DCWriter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var minioSetting = Configuration.GetSection("MinioSetting"); 
            var remoteServices = Configuration.GetSection("RemoteServices");
            services.Configure<MinioSetting>(minioSetting);
            services.Configure<RemoteServices>(remoteServices);

            services.AddTransient<IEmrArchiveService, EmrArchiveService>();

            //// Add Hangfire services.
            //services.AddHangfire(configuration => configuration
            //    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //    .UseSimpleAssemblyNameTypeSerializer()
            //    .UseRecommendedSerializerSettings()
            //    .UseSqlServerStorage(Configuration.GetConnectionString("Default"), new SqlServerStorageOptions
            //    {
            //        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            //        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            //        QueuePollInterval = TimeSpan.Zero,
            //        UseRecommendedIsolationLevel = true,
            //        DisableGlobalLocks = true
            //    })); 
            //// Add the processing server as IHostedService
            //services.AddHangfireServer();

            //注册CAP
            services.AddCap(x =>
            {
                x.UseSqlServer(Configuration.GetConnectionString("Default"));
                x.UseRabbitMQ(mq =>
                {
                    mq.VirtualHost = Configuration.GetSection("RabbitMQ").GetValue("VirtualHost", "/");
                    mq.Port = Configuration.GetSection("RabbitMQ").GetValue("Port", 5672);
                    mq.ExchangeName = Configuration.GetSection("RabbitMQ").GetValue("ExchangeName", "szyjian.cap");
                    mq.HostName = Configuration.GetSection("RabbitMQ").GetValue("HostName", "192.168.241.101");
                    mq.UserName = Configuration.GetSection("RabbitMQ").GetValue("UserName", "guest");
                    mq.Password = Configuration.GetSection("RabbitMQ").GetValue("Password", "guest");
                });
                x.UseDashboard();
                x.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            });

            //Microsoft.AspNetCore.Hosting.IWebHostEnvironment e = null;//添加
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder => {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowCredentials();
            }));

            services.AddMvc().AddSessionStateTempDataProvider();//添加
            services.AddSession();//添加
            services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "DataProtection"));
             
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //添加
            services.AddSingleton<IMasterRemoteervice, MasterRemoteervice>();
            services.Configure<FormOptions>(options => { options.ValueCountLimit = 5000; options.ValueLengthLimit = 2097152000; }); //添加
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
             
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();//添加
            app.UseSession();//添加
            app.UseRouting();

            app.UseAuthorization();

            //app.UseHangfireDashboard();

            //RecurringJob.AddOrUpdate<IEmrArchiveService>(x=>x.SavePDF(), Cron.Hourly());
             
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=DCWriter}/{action=Index}/{id?}");
            });
        }
    }
}

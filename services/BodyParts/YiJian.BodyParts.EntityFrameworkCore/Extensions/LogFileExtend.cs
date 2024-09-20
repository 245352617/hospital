using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace YiJian.BodyParts.EntityFrameworkCore.Extensions
{
    public static class LogFileExtend
    {
        public static void UseLogFiles(this IApplicationBuilder app)
        {
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".log"] = "text/plain;charset=utf-8";
            //string basePath = AppDomain.CurrentDomain.BaseDirectory;
            
            string basePath = AppContext.BaseDirectory;
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(basePath, "Logs")),
                ServeUnknownFileTypes = true,
                RequestPath = new PathString("/logs"),
                ContentTypeProvider = provider,
                DefaultContentType = "application/x-msdownload", // 设置未识别的MIME类型一个默认z值

            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(basePath, "Logs")),
                RequestPath = new PathString("/logs"),
            });
        }
    }
}

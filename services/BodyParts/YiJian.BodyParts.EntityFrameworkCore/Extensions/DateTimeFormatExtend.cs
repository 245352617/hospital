using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace YiJian.BodyParts.EntityFrameworkCore.Extensions
{
    public static class DateTimeFormatExtend
    {
        public static void ConfigureDateTimeFormat(this IApplicationBuilder app)
        {
            //设置CultureInfo
            var zh = new CultureInfo("zh-CN");
            zh.DateTimeFormat.FullDateTimePattern = "yyyy-MM-dd HH:mm:ss";
            zh.DateTimeFormat.LongDatePattern = "yyyy-MM-dd";
            zh.DateTimeFormat.LongTimePattern = "HH:mm:ss";
            zh.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            zh.DateTimeFormat.ShortTimePattern = "HH:mm:ss";
            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                zh,
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("zh-CN"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
        }
    }
}

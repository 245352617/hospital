using Serilog;
using Serilog.Events;
using System;

namespace YiJian.ECIS.Core.Hosting.AspNetCore;

public static class SerilogConfigurationHelper
{
    public static void Configure(string applicationName, bool debug = true)
    {
        Log.Logger = new LoggerConfiguration()
          .MinimumLevel.Debug()
          .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
          .WriteTo.Logger(x => x.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
              .WriteTo.File(path: $"logs/{DateTime.Now.ToString("yyyy-MM")}/error.log", rollingInterval: RollingInterval.Day)
              .WriteTo.Console(LogEventLevel.Error)
          )
          .WriteTo.Logger(x => x.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
              .WriteTo.File(path: $"logs/{DateTime.Now.ToString("yyyy-MM")}/warn.log", rollingInterval: RollingInterval.Day)
              .WriteTo.Console(LogEventLevel.Warning)
          )
          .WriteTo.Logger(x => x.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
              .WriteTo.File(path: $"logs/{DateTime.Now.ToString("yyyy-MM")}/info.log", rollingInterval: RollingInterval.Day)
              .WriteTo.Console(LogEventLevel.Information)
          )
            // #if DEBUG
            //                 .WriteTo.File(path: $"logs/{DateTime.Now.ToString("yyyy-MM")}/all.log", rollingInterval: RollingInterval.Day)
            //                 .WriteTo.Async(c => c.Console())
            // #endif
            .CreateLogger();

    }
}
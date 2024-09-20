using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace YiJian.BodyParts.EntityFrameworkCore
{
    /// <summary>
    /// EF数据库专用日志记录器
    /// </summary>
    public class EFLogger : Microsoft.Extensions.Logging.ILogger
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        private readonly string categoryName;

        public EFLogger(string categoryName) => this.categoryName = categoryName;

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel) => true;

        /// <summary>
        /// 日志输出
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (categoryName == "Microsoft.EntityFrameworkCore.Database.Command" &&
                logLevel == LogLevel.Information)
            {
                var logContent = formatter(state, exception);
                Console.ForegroundColor = ConsoleColor.Green;
                Serilog.Log.Debug(logContent);
            }
        }

        /// <summary>
        /// 开始域
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state) => null;
    }
}

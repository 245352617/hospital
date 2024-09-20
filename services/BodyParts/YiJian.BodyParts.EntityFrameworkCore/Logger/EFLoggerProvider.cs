using Microsoft.Extensions.Logging;
using System;

namespace YiJian.BodyParts.EntityFrameworkCore
{
    /// <summary>
    /// EF日志提供者(供应商)
    /// </summary>
    public class EFLoggerProvider : ILoggerProvider, IDisposable
    {
        public ILogger CreateLogger(string categoryName) => new EFLogger(categoryName);
        public void Dispose() { }
    }
}

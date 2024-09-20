using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;
using NLog;
using NLog.Config;
using System;
using System.Linq;
using System.Text.Json;
using LogLevel = NLog.LogLevel;

namespace TriageService.Controllers
{
    [ApiController]
    [Route("api/Config")]
    public class NLogController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<NLogController> _log;

        public NLogController(IConfiguration configuration,ILogger<NLogController> logger)
        {
            _configuration = configuration;
            _log = logger;
        }

        /// <summary>
        /// 在修改Nlog配置后，手动重新加载NLog配置已在系统起效 LogManager.ReconfigExistingLoggers()
        /// </summary>
        [HttpGet("reload1")]
        public void ReloadNLogConfiguration1()
        {
            LoggingConfiguration logConfig = LogManager.Configuration;
            LogManager.ReconfigExistingLoggers();
            Console.WriteLine("*****重新加载NLog配置，LogManager.ReconfigExistingLoggers()*****");
        }

        /// <summary>
        /// 在修改Nlog配置后，手动重新加载NLog配置已在系统起效 LogManager.Configuration.Reload()
        /// </summary>
        [HttpGet("reload2")]
        public void ReloadNLogConfiguration2()
        {
            LogManager.Configuration.Reload();
            Console.WriteLine("*****重新加载NLog配置，LogManager.Configuration.Reload()*****");
        }

        [HttpGet("allLoggingRules")]
        public string GetAllLoggingRules()
        {
            LoggingConfiguration logConfig = LogManager.Configuration;
            return JsonSerializer.Serialize(logConfig.AllTargets);
        }

        /// <summary>
        /// 获得NLog配置文件rules
        /// </summary>
        /// <returns></returns>
        [HttpGet("loggingRules")]
        public string GetLoggingRules()
        {
            LoggingConfiguration logConfig = LogManager.Configuration;
            return JsonSerializer.Serialize(logConfig.LoggingRules);
        }

        /// <summary>
        /// 获得NLog配置文件地址
        /// </summary>
        /// <returns></returns>
        [HttpGet("fileNamesToWatch")]
        public string GetFileNamesToWatch()
        {
            LoggingConfiguration logConfig = LogManager.Configuration;
            return JsonSerializer.Serialize(logConfig.FileNamesToWatch);
        }

        /// <summary>
        /// 某特定日志级别是否在特定Rule中启用
        /// </summary>
        /// <param name="logLevel">日志级别：0-Trace, 1-Debug, 2-Info, 3-Warn, 4-Error, 5-Fatal, 6-Off</param>
        /// <param name="ruleName">rule名称</param>
        /// <returns></returns>
        [HttpGet("isEnabledForLevelOnSelectRule")]
        public bool GetIsEnabledForLevelOnRule(EnumLogLevel logLevel, string ruleName = "coloredConsole")
        {
            LoggingConfiguration logConfig = LogManager.Configuration;
            foreach (LoggingRule rule in logConfig.LoggingRules)
            {
                if (rule.Targets.First()?.Name == ruleName)
                {
                    return rule.IsLoggingEnabledForLevel(LogLevel.FromString(logLevel.GetDisplayName())); ;
                }
            }
            return false;
        }

        /// <summary>
        /// 设置特定Rule中启用的日志级别
        /// </summary>
        /// <param name="minLevel">最小日志级别：0-Trace, 1-Debug, 2-Info, 3-Warn, 4-Error, 5-Fatal, 6-Off</param>
        /// <param name="maxLevel">最大日志级别：0-Trace, 1-Debug, 2-Info, 3-Warn, 4-Error, 5-Fatal, 6-Off</param>
        /// <param name="ruleName">rule名称</param>
        /// <returns></returns>
        [HttpGet("setEnabledForLevelOnSelectRule")]
        public void SetEnabledForLevelOnRule(EnumLogLevel minLevel, EnumLogLevel maxLevel, string ruleName = "coloredConsole")
        {
            LoggingConfiguration logConfig = LogManager.Configuration;
            foreach (LoggingRule rule in logConfig.LoggingRules)
            {
                if (rule.Targets.First()?.Name == ruleName)
                {
                    rule.SetLoggingLevels(LogLevel.FromString(minLevel.GetDisplayName()), LogLevel.FromString(maxLevel.GetDisplayName()));
                    Console.WriteLine($"设置{ruleName}的日志级别为{minLevel.GetDisplayName()}到{maxLevel.GetDisplayName()}");
                    return;
                }
            }
        }

        [HttpGet("doLog")]
        public void DoLog()
        {
            _log.LogTrace("trace");
            _log.LogDebug("debug");
            _log.LogInformation("info");
            _log.LogWarning("warn");
            _log.LogError("error");
            _log.LogCritical("fatal");
        }

        public enum EnumLogLevel
        {
            Trace = 0,
            Debug = 1,
            Info = 2,
            Warn = 3,
            Error = 4,
            Fatal = 5,
            Off = 6
        }
    }
}

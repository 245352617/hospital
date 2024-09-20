using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;
using YiJian.MasterData.Domain;
using YiJian.MasterData.External.LongGang.Frequency;
using YiJian.MasterData.Treats;
using YiJian.MasterData.Departments;
using System.Linq;

namespace YiJian.MasterData.External.LongGang;

/// <summary>
/// 执行科室规则处理
/// </summary>
public class ExecuteDepRuleDicHandler : MasterDataAppService, ILongGangHospitalEventHandler, IDistributedEventHandler<ExecuteDepRuleDicEto>,ITransientDependency
{
    ///// <summary>
    ///// 日志工具
    ///// </summary>
    //public ILogger<ExecuteDepRuleDicHandler> Logger { get; set; }
    ///// <summary>
    ///// 执行科室规则存储注入（setter注入）
    ///// </summary>
    //public IExecuteDepRuleDicRepository ExecuteDepRuleDicRepository { get; set; }

    private readonly ILogger<ExecuteDepRuleDicHandler> _logger;
    private readonly IExecuteDepRuleDicRepository _executeDepRuleDicRepository;

    /// <summary>
    /// 执行科室规则处理
    /// </summary>
    public ExecuteDepRuleDicHandler(
        ILogger<ExecuteDepRuleDicHandler> logger,
        IExecuteDepRuleDicRepository executeDepRuleDicRepository)
    {
        _logger = logger;
        _executeDepRuleDicRepository = executeDepRuleDicRepository;
    }

    /// <summary>
    /// 事件处理
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns> 
    public async Task HandleEventAsync(ExecuteDepRuleDicEto eventData)
    {
        var uow = UnitOfWorkManager.Begin();
        if (eventData == null) return; 
        Logger.LogInformation($"执行科室规则数据:{JsonConvert.SerializeObject(eventData)}");

        try
        { 
            var input = eventData.DicDatas;
            if (!input.Any()) return;
            var entities = new List<ExecuteDepRuleDic>();
            input.ForEach(x =>
            {
                 entities.Add(new ExecuteDepRuleDic
                 {
                    ExeDepCode = x.ExeDepCode,
                    ExeDepName = x.ExeDepName,
                    RuleId = x.RuleId,
                    RuleName = x.RuleName,
                    SpellCode = x.SpellCode,
                 });
            });

            await _executeDepRuleDicRepository.DiffAndUpdateAsync(entities); 
            await uow.CompleteAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,$"订阅执行科室规则字典异常：{ex.Message}");
            await uow.RollbackAsync();
        }

    }
}
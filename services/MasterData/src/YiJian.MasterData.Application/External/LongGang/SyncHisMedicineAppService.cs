using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using YiJian.Apis;
using YiJian.ECIS.DDP;
using YiJian.ECIS.ShareModel.DDPs;
using YiJian.ECIS.ShareModel.DDPs.Responses;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;
using YiJian.MasterData.Domain;
using YiJian.MasterData.Exams;
using YiJian.MasterData.External.LongGang.Medicines;
using YiJian.MasterData.Medicines;

namespace YiJian.MasterData.External.LongGang;

/// <summary>
/// 同步HIS医嘱
/// </summary>
public class SyncHisMedicineAppService : ApplicationService, ISyncHisMedicineAppService, ICapSubscribe
{
    public readonly IHISMedicineRepository _hisMedicineRepository;

    private readonly ICapPublisher _capPublisher;
    private readonly ILogger<SyncHisMedicineAppService> _logger;
    private readonly IDistributedCache _cache;

    private readonly IOptionsMonitor<DdpHospital> _ddpHospitalOptionsMonitor;
    private readonly DdpHospital _ddpHospital;
    private readonly DdpSwitch _ddpSwitch;
    private readonly IDdpApiService _ddpApiService;
    private readonly ILabProjectRepository _labProjectRepository;
    private readonly IExamProjectRepository _examProjectRepository;
    private readonly ILabTreeRepository _labTreeRepository;
    private readonly IExamTreeRepository _examTreeRepository;

    /// <summary>
    /// 同步HIS医嘱
    /// </summary> 
    public SyncHisMedicineAppService(
        ICapPublisher capPublisher,
        IHISMedicineRepository hisMedicineRepository,
        ILogger<SyncHisMedicineAppService> logger,
        IDistributedCache cache,
        IOptionsMonitor<DdpHospital> ddpHospitalOptionsMonitor,
        IExamProjectRepository examProjectRepository,
        ILabProjectRepository labProjectRepository,
        ILabTreeRepository labTreeRepository,
        IExamTreeRepository examTreeRepository,
    DdpSwitch ddpSwitch)
    {
        _capPublisher = capPublisher;
        _hisMedicineRepository = hisMedicineRepository;
        _logger = logger;
        _cache = cache;

        _ddpHospitalOptionsMonitor = ddpHospitalOptionsMonitor;
        _ddpHospital = _ddpHospitalOptionsMonitor.CurrentValue;
        _ddpSwitch = ddpSwitch;
        _ddpApiService = _ddpSwitch.CreateService(_ddpHospital);
        _labProjectRepository = labProjectRepository;
        _examProjectRepository = examProjectRepository;
        _labTreeRepository = labTreeRepository;
        _examTreeRepository = examTreeRepository;
    }

    /// <summary>
    /// 同步药品信息
    /// </summary>
    /// <returns></returns> 
    public async Task SyncMedicineAsync()
    {
        //try
        //{
        //    var data = await (await _hisMedicineRepository.GetQueryableAsync()).OrderBy(o => o.InvId).ToListAsync();
        //    var list = new List<HISMedicine>();
        //    //推送数据到字典里的药品
        //    if (list.Any()) _capPublisher.Publish("sync.his.to.medicine", list);
        //}
        //catch (Exception ex)
        //{
        //    _logger.LogError(ex, "同步药品信息异常>>>" + ex.StackTrace);
        //}
    }
    /// <summary>
    /// 同步药理信息
    /// </summary>
    [CapSubscribe("job.masterdata.synctoxic")]
    public async Task PushHisMedicineToxicAsync()
    {
        try
        {
            //走DDP
            if (_ddpHospital.DdpSwitch)
            {
                await PushDdpHisMedicineToxicAsync();
            }
            else
            {
                //龙岗中心医院
                await PushLgzxHisMedicineToxicAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "同步药理信息异常>>>" + ex.StackTrace);
        }
    }

    /// <summary>
    /// 同步检查Project 删除Item 同时删除Project同时删除Tree  同步Item 删除之后 删除Project  删除TreeNode（北大这边  item 存储在 Treat 里面， 所以删除item 可能导致 检查检验item 删除 project 下面没挂item 则进行清理）
    /// </summary>
    /// <returns></returns>
    public async Task SyncExamProjectAsync()
    {
        //首先查询当前检查中Project 上面没挂Item 的 
        //删除Project  同时删除TreeNode  

        var labList = await _labProjectRepository.GetListAsync(c => c.IsActive==false);
        var labProjectCodeList = labList.Select(c => c.ProjectCode).ToList();
        var labTreeList = await _labTreeRepository.ListAsync(c => labProjectCodeList.Contains(c.ProjectCode));
        await _labProjectRepository.DeleteManyAsync(labList);
        await _labTreeRepository.DeleteAsync(labTreeList);

        var examList = await _examProjectRepository.GetListAsync(c => c.IsActive==false);
        var examProjectCodeList = examList.Select(c => c.ProjectCode).ToList();
        var examTreeList = await _examTreeRepository.ListAsync(c => examProjectCodeList.Contains(c.ProjectCode));
        await _examTreeRepository.DeleteAsync(examTreeList);
        await _examProjectRepository.DeleteManyAsync(examList);
    }

    /// <summary>
    /// 同步检验Project 删除Item 同时删除Project同时删除Tree 
    /// </summary>
    /// <returns></returns>
    public async Task SyncLabProjectAsync()
    {

    }

    /// <summary>
    /// 同步药理信息(Ddp)
    /// </summary>
    /// <returns></returns>
    private async Task PushDdpHisMedicineToxicAsync()
    {
        var ddpResponse = await _ddpApiService.GetHisMedicineAllAsync();
        if (ddpResponse.Code != 200)
        {
            _logger.LogError($"ddp返回异常：{ddpResponse.Code}，异常内容：{ddpResponse.Msg}");
            return;
        }
        var pushEto = ObjectMapper.Map<List<DdpHisMedicineResponse>, List<ToxicEto>>(ddpResponse.Data);
        await _capPublisher.PublishAsync<List<ToxicEto>>("sync.masterdata.toxic.all", pushEto);
    }


    /// <summary>
    /// 同步药理信息(龙岗中心医院)
    /// </summary>
    /// <returns></returns>
    private async Task PushLgzxHisMedicineToxicAsync()
    {
        var hisMedicines = await _hisMedicineRepository.GetListAsync();
        var pushEto = ObjectMapper.Map<List<HISMedicine>, List<ToxicEto>>(hisMedicines);
        await _capPublisher.PublishAsync<List<ToxicEto>>("sync.masterdata.toxic.all", pushEto);
    }

}

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.MasterData.Domain;

namespace YiJian.MasterData;

/// <summary>
/// 诊室应用服务
/// </summary>
[Authorize]
public class ConsultingRoomAppService : MasterDataAppService, IConsultingRoomAppService
{
    private readonly IConsultingRoomRepository _consultingRoomRepository;

    public ConsultingRoomAppService(IConsultingRoomRepository consultingRoomRepository)
    {
        _consultingRoomRepository = consultingRoomRepository;
    }
    
    /// <summary>
    /// 创建诊室
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<ConsultingRoomData> CreateAsync(ConsultingRoomCreation input)
    {
        if (await (await _consultingRoomRepository.GetQueryableAsync()).AnyAsync(p => p.Name == input.Name))
        {
            throw new EcisBusinessException(message: $"诊室名称 {input.Name} 已存在！");
        }
        if (await (await _consultingRoomRepository.GetQueryableAsync()).AnyAsync(p => p.Code == input.Code))
        {
            throw new EcisBusinessException(message: $"诊室编码 {input.Code} 已存在！");
        }
        var consultingRoom = await _consultingRoomRepository.InsertAsync(
            ConsultingRoom.Create(
                GuidGenerator.Create(),
                input.Name,
                input.Code,
                input.IP,
                input.CallScreenIp,
                input.IsActived
            )
        );
        
        return ObjectMapper.Map<ConsultingRoom, ConsultingRoomData>(consultingRoom);
    }

    /// <summary>
    /// 删除诊室
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Guid> DeleteAsync(Guid id)
    {
        await _consultingRoomRepository.DeleteAsync(id);

        return id;
    }

    /// <summary>
    /// 获取诊室信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ConsultingRoomData> GetAsync(Guid id)
    {
        var consultingRoom = await _consultingRoomRepository.GetAsync(id);
        return ObjectMapper.Map<ConsultingRoom, ConsultingRoomData>(consultingRoom);
    }

    /// <summary>
    /// 查询所有诊室
    /// </summary>
    /// <returns></returns>
    public async Task<List<ConsultingRoomData>> GetListAsync()
    {
        var consultingRooms = await (await _consultingRoomRepository.GetQueryableAsync())
            .OrderBy(x => x.CreationTime)
            .ToListAsync();

        return ObjectMapper.Map<List<ConsultingRoom>, List<ConsultingRoomData>>(consultingRooms);
    }

    /// <summary>
    /// 更新诊室
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<ConsultingRoomData> UpdateAsync(Guid id, ConsultingRoomUpdate input)
    {
        var consultingRoom = await _consultingRoomRepository.GetAsync(id);

        consultingRoom.Edit(input.Name, input.Code, input.IP, input.CallScreenIp, input.IsActived);

        await _consultingRoomRepository.UpdateAsync(consultingRoom);

        return ObjectMapper.Map<ConsultingRoom, ConsultingRoomData>(consultingRoom);
    }
}

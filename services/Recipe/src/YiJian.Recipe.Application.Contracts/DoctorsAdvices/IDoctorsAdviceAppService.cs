using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.DoctorsAdvices.Dto;
using YiJian.DoctorsAdvices.Entities;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

namespace YiJian.DoctorsAdvices;

/// <summary>
/// 医嘱
/// </summary>
public interface IDoctorsAdviceAppService : IApplicationService
{
    /// <summary>
    /// 获取医嘱列表集合
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    public Task<List<DoctorsAdvicesDto>> QueryDoctorsAdvicesAsync(QueryDoctorsAdviceDto model);

    /// <summary>
    /// 添加医嘱（合集，包括 药方，检查，检验，诊疗）-- 院前急救使用
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    public Task<bool> AddFullAdviceAsync(AddFullAdviceDto model);

    /// <summary>
    /// 新增同组药品信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>  
    public Task<List<Guid>> AddGroupPrescribeAsync(AddGroupPrescribesDto model);

    /// <summary>
    /// 新增开方
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    public Task<Guid> AddPrescribeAsync(AddPrescribeDto model);

    /// <summary>
    ///  新增检验
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    public Task<List<Guid>> AddLisAsync(AddLisDto model);

    /// <summary>
    ///  新增检查
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    public Task<AddPacsResponseDto> AddPacsAsync(AddPacsDto model);

    /// <summary>
    /// 全景视图-检验列表
    /// </summary>
    /// <param name="PID"></param>
    /// <param name="StartTime"></param>
    /// <param name="EndTime"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<LisListDto>> GetAllViewLisListAsync(Guid PID, DateTime? StartTime, DateTime? EndTime, CancellationToken cancellationToken = default);

    /// <summary>
    /// 全景视图-检查列表
    /// </summary>
    /// <param name="PID"></param>
    /// <param name="StartTime"></param>
    /// <param name="EndTime"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<PacsListDto>> GetAllViewPacsListAsync(Guid PID, DateTime? StartTime, DateTime? EndTime, CancellationToken cancellationToken = default);

    /// <summary>
    /// 新增诊疗项
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    public Task<Guid> AddTreatAsync(AddTreatDto model);

    /// <summary>
    /// 保存记录
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Task<Guid> SaveRecoredAsync(DoctorsAdvicesDto model);

    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    public Task<SubmitDoctorsAdviceResponse> SubmitAsync(SubmitDoctorsAdviceDto model);

    /// <summary>
    /// 拷贝
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns> 
    public Task<List<Guid>> CloneAsync(List<Guid> ids);

    /// <summary>
    /// 停嘱
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    public Task<List<Guid>> StopAsync(StopDoctorsAdviceDto model);

    /// <summary>
    /// 作废-院前(已提交可作废)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>  
    public Task<List<Guid>> ObsPreAsync(ObsPreDoctorsAdviceDto model);

    /// <summary>
    /// 作废
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns> 
    public Task<List<Guid>> ObsAsync(ObsDoctorsAdviceDto model);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns> 
    public Task<List<Guid>> DeleteAsync(List<Guid> ids);

    /// <summary>
    /// 医嘱归组
    /// <![CDATA[医嘱号为最小医嘱号，子号从1开始自动增长]]>
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>  
    public Task<List<Guid>> GroupAsync(List<Guid> ids);

    /// <summary>
    /// 医嘱拆组
    /// </summary>
    /// <returns></returns>  
    public Task<List<Guid>> TakeApartGroupAsync(TakeApartGroupDto model);

    /// <summary>
    /// 已驳回,已确认,已执行... 
    /// </summary>
    /// <see cref="EDoctorsAdviceStatus"/>
    /// <returns></returns> 
    public Task SyncDoctorsAdviceAsync(SyncDoctorsAdviceEto eto);

    /// <summary>
    /// 订单状态变化订阅方法
    /// </summary>
    /// <returns></returns> 
    //public Task ChannelBillConsumeAsync(ChannelBillStatusEto eto);

    /// <summary>
    /// 根据Id获取医嘱详细信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns> 
    public Task<AdviceDetailDto> GetDetailAsync(Guid id);

    /// <summary>
    /// 更新医嘱详细内容
    /// </summary>
    /// <see cref="AdviceDetailDto"/>
    /// <returns></returns>
    public Task<Guid> ModifyDetailAsync(AdviceDetailDto model);

    /// <summary>
    /// 会诊和交接班以及患者360使用使用
    /// </summary>
    /// <param name="pIId">会诊和交接班必传</param>
    /// <param name="endTime"></param>
    /// <param name="prescribeTypeCode"></param>
    /// <param name="patientId">患者360必传</param>
    /// <param name="filter"></param>
    /// <param name="startTime"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    Task<List<DoctorsAdviceShowListDto>> GetDoctorsAdviceShowListAsync(Guid pIId,
        string patientId,
        string filter,
        DateTime? startTime,
        DateTime? endTime,
        string prescribeTypeCode, int status = -1);

    /// <summary>
    /// 判断患者是否已开医嘱，流转到就诊区
    /// </summary>
    /// <param name="pI_ID"></param>
    /// <returns></returns>
    Task<bool> GetIsTransferByRecipeAsync(Guid pI_ID);

    /// <summary>
    /// 查询医嘱状态并更新相关操作
    /// </summary>
    /// <returns></returns> 
    Task QueryAndOptMedicalInfoAsync();

    /// <summary>
    /// 获取狂犬疫苗记录信息
    /// </summary>
    /// <param name="patientId"></param>
    /// <returns></returns>
    Task<List<ImmunizationRecord>> GetImmunizationRecordAsync(string patientId);


    /// <summary>
    /// 同步患者医嘱状态
    /// </summary>
    /// <param name="pi_Id">患者Id</param>
    /// <returns></returns>
    public Task<bool> GetChangeBillStatusAsync(Guid pi_Id);

    /// <summary>
    /// HIS医嘱状态变更推送或调用接口(HIS直接提供状态变化的数据)
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task<int> HisChangeBillStatusAsync(HisChannelBillStatusDto dto);
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using YiJian.ECIS.Call.CallCenter.Dtos;

namespace YiJian.ECIS.Call.CallCenter
{
    /// <summary>
    /// 【叫号患者列表】应用服务层接口
    /// </summary>
    public interface ICallInfoAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input">分页查询参数</param>
        /// <returns></returns>
        Task<PagedResultDto<CallInfoData>> GetPagedListAsync(GetCallInfoInput input);

        /// <summary>
        /// 根据id获取记录
        /// </summary>
        /// <param name="registerNo">registerNo</param>
        /// <returns></returns>
        Task<CallInfoData> GetAsync(string registerNo);

        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="registerNo">registerNo</param>
        /// <returns></returns>
        Task<CallInfoData> MoveToTopAsync(string registerNo);

        /// <summary>
        /// 取消置顶
        /// </summary>
        /// <param name="registerNo">registerNo</param>
        /// <returns></returns>
        Task<CallInfoData> CancelMoveToTopAsync(string registerNo);

        /// <summary>
        /// 顺呼
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CallInfoData> CallNextAsync(CallNextDto input);

        /// <summary>
        /// 重呼
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CallInfoData> CallAgainAsync(CallAgainDto input);

        /// <summary>
        /// 转诊 （修改科室 ， 修改分诊level ,指定医生）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CallInfoData> CallReferralAsync(CallReferralDto input);

        /// <summary>
        /// 退回候诊
        /// </summary>
        /// <param name="registerNo">病人 ID</param>
        /// <returns></returns>
        Task<CallInfoData> SendBackWaittingAsync(string registerNo);

        /// <summary>
        /// 结束就诊
        /// </summary>
        /// <param name="registerNo">病人 ID</param>
        /// <returns></returns>
        Task<CallInfoData> TreatFinishAsync(string registerNo);

        /// <summary>
        /// 过号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CallInfoData> UntreatedOverAsync(UntreatedOverDto input);

        /// <summary>
        /// 过好 用Register No
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<CallInfoData> MissedTurnAsync(MissedTurnDto dto);

        /// <summary>
        /// 恢复候诊
        /// 只有已过号患者可以恢复候诊
        /// </summary>
        /// <param name="patientId">病人 ID</param>
        /// <returns></returns>
        Task<CallInfoData> ReturnToQueueAsync(string patientId);

        /// <summary>
        /// 获取叫号统计信息
        /// </summary>
        /// <returns></returns>
        Task<CallingStatisticsDto> GetStatisticsAsync();

        /// <summary>
        /// 分页获取叫号记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<CallingRecordData>> GetPagedRecordListAsync(GetCallingRecordInput input);

        /// <summary>
        /// 叫号历史查看
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedCallInfoHistoryDto> GetPagedHistoryAsync(GetHistoryInput input);

        /// <summary>
        /// 查询当前门诊医生
        /// </summary>
        /// <param name="departmentCode">科室编码</param>
        /// <returns></returns>
        Task<List<ConsultingDoctorDto>> GetConsultingDoctorListAsync(string departmentCode);
    }
}

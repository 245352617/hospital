using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// SMS 应用服务层接口
    /// </summary>
    public interface ISmsAppService : IApplicationService
    {
        /// <summary>
        /// 保存标签设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> SaveTagSettingsAsync(TagSettingsDto dto);

        /// <summary>
        /// 查询标签设置列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        Task<JsonResult<List<TagSettingsDto>>> GetTagSettingsListAsync(string name);
        
        /// <summary>
        /// 删除标签设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult> DelTagSettingsAsync(Guid id);

        /// <summary>
        /// 保存值班电话
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> SaveDutyTelephoneAsync(DutyTelephoneDto dto);

        /// <summary>
        /// 查询值班电话列表
        /// </summary>
        /// <param name="searchText">手机编号/手机号码 模糊查询</param>
        /// <param name="dept">科室</param>
        /// <param name="tagName">标签名</param>
        /// <returns></returns>
        Task<JsonResult<List<DutyTelephoneDto>>> GetDutyTelephoneListAsync(string searchText,string dept,string tagName);
        
        /// <summary>
        /// 删除值班电话
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult> DelDutyTelephoneAsync(Guid id);


        /// <summary>
        /// 查询短信模板
        /// </summary>
        /// <returns></returns>
        Task<JsonResult<TextMessageTemplateDto>> GetTextMessageTemplateAsync();

        /// <summary>
        /// 保存短信模板
        /// </summary>
        /// <returns></returns>
        Task<JsonResult> SaveTextMessageTemplateAsync(TextMessageTemplateDto dto);

        /// <summary>
        /// 删除短信模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult> DelTextMessageTemplateAsync(Guid id);

        /// <summary>
        /// 查询短信记录
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="taskInfoNum">任务单号</param>
        /// <param name="pageIndex" example="1">>当前页码</param>
        /// <param name="pageSize" example="50">当前页大小</param>
        /// <param name="isOrderByDesc" example="false">>是否降序排序</param>
        /// <returns></returns>
        Task<JsonResult<PagedResultDto<TextMessageRecordDto>>> GetTextMessageRecordListAsync(DateTime? startTime,
            DateTime? endTime, string taskInfoNum, int pageIndex = 1, int pageSize = 50,bool isOrderByDesc = false);
    }
}
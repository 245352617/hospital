using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.EMR.Libs.Dto;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.Writes.Dto;
using YiJian.EMR.Enums;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using YiJian.ECIS.ShareModel.Etos.EMRs;
using Microsoft.Extensions.Configuration;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Utils;

namespace YiJian.EMR.Writes
{
    /// <summary>
    /// 书写电子病历
    /// </summary>
    public interface IWriteAppService : IApplicationService
    {
        /// <summary>
        /// 获取患者电子病历列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseBase<List<PatientEmrDto>>> GetPatientEmrsAsync(PatientEmrRequestDto model);

        /// <summary>
        /// 获取患者电子病历列表(根据入院出院时间分组)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseBase<List<PatientEmrGroupDto>>> GetPatientEmrsGroupAsync(PatientEmrRequestDto model);

        /// <summary>
        /// 获取一天内的患者交接班的电子病历
        /// </summary>
        /// <param name="model">交接班请求参数，如果begintime ,endtime 不传过来则获取当前患者当天的所有电子病历</param>
        /// <returns></returns>
        public Task<ResponseBase<List<PatientEmrDto>>> GetPatientShiftChangeEmrsAsync(ShiftChangeRequestDto model);

        /// <summary>
        /// 获取指定患者电子病历的汇总数据
        /// </summary>
        /// <returns></returns> 
        public Task<ResponseBase<EmrCategoryDto>> GetAggregatePatientShiftChangeEmrAsync(EmrCategoryRequestDto model);
         
        /// <summary>
        /// 新增/更新操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseBase<PatientEmrDto>> ModifyEmrAsync(ModifyPatientEmrDto model);

        /// <summary>
        /// 获取单个书写过的xml病历文件
        /// </summary>
        /// <param name="patientEmrId"></param>
        /// <returns></returns>
        public Task<ResponseBase<PatientEmrXmlDto>> GetEmrAsync(Guid patientEmrId);

        /// <summary>
        /// 获取留痕历史记录列表(只查询最近50条)
        /// </summary>
        /// <param name="xmlId">患者病历Id</param>
        /// <param name="xmlCategory">xml 电子病例模板类型(0=电子病历库的模板，1=我的电子病历模板的模板，2=已存档的患者电子病历)</param>
        /// <param name="task">希望查询的最近记录数量，默认50</param>
        /// <returns></returns>
        public Task<ResponseBase<List<XmlHistoryDto>>> GetEmrXmlHistoriesAsync(Guid xmlId, EXmlCategory xmlCategory,
            int task = 50);

        /// <summary>
        /// 获取指定留痕记录的详细内容
        /// </summary>
        /// <param name="id">GetEmrXmlHistories 接口返回的Id</param>
        /// <returns></returns>
        public Task<ResponseBase<XmlHistoryFullDto>> GetHistoryXmlDetailAsync(Guid id);

        /// <summary>
        /// 判断患者是否存在已开病历，用于患者主页流转
        /// </summary>
        /// <param name="pI_ID"></param>
        /// <returns></returns>
        public Task<ResponseBase<bool>> GetIsTransferByEmrAsync(Guid pI_ID);

        /// <summary>
        /// 跟进患者的电子病历Id移除不需要的电子病历
        /// </summary>
        /// <param name="patientEmrId"></param>
        /// <returns></returns>
        public Task<ResponseBase<Guid>> RemoveAsync(Guid patientEmrId);

        /// <summary>
        /// 获取电子病历，护理文书的的id集合
        /// </summary> 
        /// <returns></returns>  
        public Task<List<PatientEmrSampleDto>> GetArchivePatientEmrsAsync();

        /// <summary>
        /// 归档
        /// </summary>
        /// <param name="patientEmrId">电子病历、文书id</param>
        /// <returns></returns> 
        public Task<string> ArchiveAsync(Guid patientEmrId);

        /// <summary>
        /// 异常回滚归档
        /// </summary>
        /// <param name="patientEmrId">电子病历、文书id</param>
        /// <returns></returns> 
        public Task RollbackArchiveAsync(Guid patientEmrId);

        /// <summary>
        /// 全景视图-患者病历列表
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param> 
        /// <returns></returns>
        Task<List<PatientEmrDto>> GetAllViewPatientEmrListAsync(Guid PID, DateTime? StartTime, DateTime? EndTime);

        /// <summary>
        /// 获取预览地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ResponseBase<string>> GetUrlAsync(Guid id);
         
        /// <summary>
        /// 获取电子病历水印配置
        /// </summary>
        /// <returns></returns> 
        public ResponseBase<EmrWatermarkModel> GetWatermark();

        /// <summary>
        /// 根据医生
        /// </summary>
        /// <param name="doctorCode"></param>
        /// <param name="piid"></param>
        /// <returns></returns> 
        public Task<ResponseBase<IList<PatientEmrTrashDto>>> GetPatientEmrTrashAsync(string doctorCode, Guid piid);

        /// <summary>
        /// 还原垃圾桶里的电子病历
        /// </summary>
        /// <param name="id"></param>
        /// <param name="doctorCode"></param>
        /// <returns></returns>
        public Task<ResponseBase<bool>> RestorePatientEmrAsync(Guid id, string doctorCode);

        /// <summary>
        /// 合并留观病程电子病历
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns> 
        public Task<ResponseBase<bool>> MergeCourseOfObservationAsync(MergeCourseOfObservationRequestEto request);

        /// <summary>
        /// 打印合并的电子病历
        /// </summary>
        /// <param name="piid">患者唯一Id</param>
        /// <param name="originalId">电子病历模板地址</param>
        /// <returns></returns>
        public Task<ResponseBase<PrintMergeEmrDto>> GetPrintMergeEmrAsync(Guid piid, Guid originalId);
          
        /// <summary>
        /// 获取可以合并的电子病历模板源
        /// </summary>
        /// <returns></returns>
        public Task<ResponseBase<List<string>>> GetMergeEmrOriginalIdsAsync();


        #region 留痕恢复操作

        /*
         * 1. 点击病历展示该病历的所有痕迹病历（倒序排）
         * 2. 点击指定痕迹病历预览
         * 3. 选择指定预览过的痕迹病历点击恢复使用中的病历
         */

        /// <summary>
        /// 根据病历的ID获取病历痕迹的列表数据
        /// </summary>
        /// <param name="patientEmrId"></param>
        /// <returns></returns>
        public Task<ResponseBase<List<XmlHistoryTraceDto>>> GetTraceXmlHistoryListAsync(Guid patientEmrId);

        /// <summary>
        ///  展示病历痕迹用的病历接口
        /// </summary>
        /// <param name="traceId">痕迹病历的ID</param>
        /// <param name="patientEmrId">原病历的id,有这个ID才能获取绑定的数据字典</param>
        /// <returns></returns>
        public Task<ResponseBase<PatientEmrXmTraceDto>> GetTraceEmrAsync(Guid traceId, Guid patientEmrId);


        /// <summary>
        /// 替换痕迹的病历XML
        /// </summary>
        /// <param name="xmlId">需要被替换的XMLID</param>
        /// <param name="traceId">病历痕迹XMLid,最终需要用上的XMLID</param>
        /// <returns></returns>
        public Task<ResponseBase<bool>> PutTraceReplaceAsync(Guid xmlId, Guid traceId);

        #endregion

    }
}
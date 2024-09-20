using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.Documents.Dto;
using YiJian.Documents.Enums;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Documents
{
    /// <summary>
    /// 打印服务（分方打印）
    /// </summary>
    public interface IDocumentsAppService : IApplicationService
    {

        /// <summary>
        /// 打印回调操作，打印完之后调用一下，告知医嘱服务打印情况（每打印一次就记录一次）
        /// </summary>
        /// <param name="model">打印反馈的记录数据</param>
        /// <returns></returns>  
        public Task<bool> HasBeenPrintedAsync(List<PrintInfoDto> model);


        /// <summary>
        /// 勾选打印，用户决定
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="comm"></param>
        /// <returns></returns> 
        public Task<DocumentResponseDto> PrintChecksAsync(List<Guid> ids, ECommandParam comm);

        ///// <summary>
        ///// 获取各种分方单据的数据
        ///// </summary>
        ///// <param name="pI_ID">piid</param> 
        ///// <param name="comm">命令参数: 0=获取处方单,1=获取注射单,2=获取输液单,3=获取检验单,4=获取检查单,5=获取处置单,6=获取治疗单,7=获取物化单,8=获取预防接种单 </param>
        ///// <param name="platformType">系统标识:0=急诊，1=院前 ,默认=0(急诊)</param> 
        ///// <returns></returns>
        //public Task<DocumentResponseDto> GetAsync(Guid pI_ID, ECommandParam comm, EPlatformType platformType = EPlatformType.EmergencyTreatment);


        /// <summary>
        /// 报表的Schema，方便用户配置
        /// </summary>
        /// <returns></returns>  
        public DocumentResponseDto GetReportSchema();

        /// <summary>
        /// 根据预先配置号的分方Id获取各种单的数据
        /// </summary>
        /// <param name="piid"></param>
        /// <param name="comm">命令参数: 0=获取处方单,1=获取注射单,2=获取输液单,3=获取检验单,4=获取检查单,5=获取处置单,6=获取治疗单,7=获取物化单,8=获取预防接种单</param>
        /// <param name="platformType"></param> 
        /// <param name="separationId"></param>
        /// <param name="token"></param>
        /// <param name="reprint"></param>
        /// <param name="prescriptionNo"></param>
        /// <param name="templateId"></param>
        /// <param name="forcePrintFlag"></param>
        /// <returns></returns>      
        public Task<DocumentResponseDto> GetAsync(Guid piid, ECommandParam comm, EPlatformType platformType = EPlatformType.EmergencyTreatment,
            Guid? separationId = null, string token = "", int reprint = -1, string prescriptionNo = "", Guid templateId = default, int forcePrintFlag = -1);


        /// <summary>
        /// 获取患者信息
        /// </summary>
        /// <param name="piid"></param>
        /// <param name="input_token"></param>
        /// <returns></returns>  
        public Task<AdmissionRecordDto> GetPatientInfoAsync(Guid piid, string input_token = "");

    }
}

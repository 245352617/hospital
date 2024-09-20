using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Etos.NurseExecutes;
using YiJian.ECIS.ShareModel.Etos.Patients;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.NursingDocuments.Dto;
using YiJian.Health.Report.NursingSettings.Dto;

namespace YiJian.Health.Report.NursingDocuments
{
    /// <summary>
    /// 护理单
    /// </summary>
    public interface INursingDocumentAppService : IApplicationService
    {
        /// <summary>
        /// 查询护理单信息
        /// </summary>
        /// <see cref="PatientNursingDocumentRequestDto"/>
        /// <returns></returns>
        public Task<ResponseBase<NursingDocumentDto>> ShowNursingDocumentAsync(PatientNursingDocumentRequestDto model);

        /// <summary>
        /// 保存整个记录单
        /// </summary>
        /// <see cref="BulkChangesDto"/>
        /// <returns></returns>
        public Task<ResponseBase<bool>> BulkChangesAsync(BulkChangesDto model);

        /// <summary>
        /// 修改Sheet操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public Task<ResponseBase<List<DynamicFieldDto>>> ModifySheetAsync(SheetSetDto model);

        /// <summary>
        /// 删除空的Sheet护理单据
        /// </summary>
        /// <param name="dynamicFieldId"></param>
        /// <returns></returns>
        public Task<ResponseBase<bool>> RemoveEmptySheetAsync(Guid dynamicFieldId);

        /// <summary>
        /// 修改动态六项表头内容
        /// </summary>
        /// <see cref="DynamicFieldDto"/>
        /// <returns></returns>
        public Task<ResponseBase<Guid>> ModifySixSettingAsync(DynamicSixFieldDto model);

        /// <summary>
        /// 添加新的入院护理单(入院只有新增，无修改)
        /// </summary>
        /// <see cref="AddNursingDocumentDto"/>
        /// <returns></returns> 
        public Task<ResponseBase<Guid>> AddNursingDocumentAsync(AddNursingDocumentDto model);

        /// <summary>
        /// 根据护理单记录的Id获取护理单记录信息
        /// </summary>
        /// <param name="id">护理单记录的Id</param>
        /// <returns></returns> 
        public Task<ResponseBase<NursingRecordDto>> GetNursingRecordAsync(Guid id);

        /// <summary>
        /// 新建一个空记录
        /// </summary>
        /// <see cref="NewNursingRecordDto"/>
        /// <returns></returns>
        public Task<ResponseBase<NursingRecordDto>> NewNursingRecordAsync(NewNursingRecordDto param);

        /// <summary>
        /// 更新护理单记录信息(新增不需要传Id，更新需要)
        /// </summary>
        /// <see cref="ModifyNursingRecordDto"/>
        /// <returns></returns> 
        public Task<ResponseBase<Guid>> ModifyNursingRecordAsync(ModifyNursingRecordDto model);

        /// <summary>
        /// 移除指定的护理单记录(支持多个)
        /// </summary>
        /// <param name="ids">护理单记录Id集合</param>
        /// <returns></returns> 
        public Task<ResponseBase<bool>> RemoveNursingRecordAsync(List<Guid> ids);

        /// <summary>
        /// 保存特殊护理记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseBase<Guid>> ModifyCharacteristicAsync(CharacteristicDto model);

        /// <summary>
        /// 获取回填特殊护理的记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseBase<BackfillSpecialCareDto>> BackfillSpecialCareAsync(SpecialCareDto model);

        /// <summary>
        /// 打印多个护理单
        /// </summary>
        /// <param name="model">护理记录单请求参数</param>
        /// <returns></returns> 
        public Task<ResponseBase<List<NursingDocumentDto>>> PrintMoreAsync(PrintRequestDto model);

        /// <summary>
        /// 全景视图-生命体征列表
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<NursingRecordDto>> GetAllViewVitalSignsListAsync(Guid PID, DateTime? StartTime, DateTime? EndTime, CancellationToken cancellationToken = default);

        /// <summary>
        /// 全景视图-出入量列表
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<IntakeDto>> GetAllViewInOutPutListAsync(Guid PID, DateTime? StartTime, DateTime? EndTime, CancellationToken cancellationToken = default);

        /// <summary>
        /// 同步患者的生命体征信息
        /// </summary>
        /// <param name="eto"></param>
        Task AddFirstRecordAsync(VitalSignInfoMqEto eto);

        /// <summary>
        /// 同步执行记录到护理记录单
        /// </summary>
        /// <param name="recipeExecEtos"></param>
        /// <returns></returns> 
        public Task AddRecipeExecRecordAsync(List<RecipeExecEto> recipeExecEtos);

        /// <summary>
        /// 取消执行记录
        /// </summary>
        /// <param name="recipeExecEto"></param>
        /// <returns></returns> 
        public Task CancelRecipeExecRecordAsync(RecipeExecEto recipeExecEto);

        /// <summary>
        /// 同步护理记录到护理记录单
        /// </summary>
        /// <param name="canulaRecordEto"></param>
        /// <returns></returns> 
        public Task AddNursingRecordAsync(CanulaRecordEto canulaRecordEto);
    }
}

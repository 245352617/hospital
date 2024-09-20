using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.Recipe.OperApply
{
    /// <summary>
    /// 手术申请 API
    /// </summary>
    [Authorize]
    public class OperationApplyAppService : RecipeAppService, IOperationApplyAppService, ICapSubscribe
    {
        private readonly OperationApplyManager _operationApplyManager;
        private readonly IOperationApplyRepository _operationApplyRepository;
        private readonly ICapPublisher _capPublisher;

        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="operationApplyRepository"></param>
        /// <param name="operationApplyManager"></param>
        /// <param name="capPublisher">cap</param>
        public OperationApplyAppService(IOperationApplyRepository operationApplyRepository,
            OperationApplyManager operationApplyManager, ICapPublisher capPublisher)
        {
            _operationApplyRepository = operationApplyRepository;
            _operationApplyManager = operationApplyManager;
            _capPublisher = capPublisher;
        }

        #endregion constructor

        #region Create

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        // [Authorize(RecipePermissions.OperationApplies.Create)]
        public async Task<Guid> CreateOperationApplyAsync(OperationApplyCreation input)
        {
            var splitProposed = input.ProposedOperationCode.Split(",");
            if (splitProposed.Length != splitProposed.Distinct().Count())
            {
                throw new EcisBusinessException(message: "拟施手术重复");
            }
            if (input.Id == Guid.Empty)
            {
                var operationApply = await _operationApplyManager.CreateAsync(GuidGenerator.Create(),
                    input.PI_Id, // 分诊患者id
                    patientId: input.PatientId, // 患者唯一标识(HIS)
                    patientName: input.PatientName, // 患者姓名
                    sex: input.Sex, // 患者性别
                    sexName: input.SexName,
                    age: input.Age, // 年龄
                    iDNo: input.IDNo, // 身份证号
                    birthDay: input.BirthDay.IsNullOrWhiteSpace() ? null : DateTime.Parse(input.BirthDay), // 生日
                    applyNum: new Random().Next(10000000, 99999999).ToString(), // 申请单号
                    applicantId: CurrentUser.FindClaimValue("name"), // 申请人Id
                    applicantName: CurrentUser.FindClaimValue("fullName"), // 申请人名称
                    applyTime: DateTime.Now, // 开嘱时间
                    bloodType: input.BloodType, // 血型
                    rHCode: input.RHCode, // RH阴性阳性
                    height: input.Height, // 身高
                    weight: input.Weight, // 体重
                    proposedOperationCode: input.ProposedOperationCode, // 拟施手术编码
                    proposedOperationName: input.ProposedOperationName, // 拟施手术名称
                    operationLevel: input.OperationLevel, // 手术等级
                    applyDeptCode: input.ApplyDeptCode, // 申请科室编码
                    applyDeptName: input.ApplyDeptName, // 申请科室名称
                    operationLocation: input.OperationLocation, // 手术位置
                    doctorCode: input.DoctorCode, // 手术医生编码
                    doctorName: input.DoctorName, // 手术医生名称
                    operationAssistant: input.OperationAssistant, // 手术助手
                    operationPlanTime: input.OperationPlanTime.IsNullOrWhiteSpace()
                        ? null
                        : DateTime.Parse(input.OperationPlanTime), // 手术计划时间
                    operationDuration: input.OperationDuration, // 手术时长
                    remark: input.Remark, // 备注
                    status: input.Status, // 手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
                    submitDate: DateTime.Now, // 手术申请提交日期
                    operationType: input.OperationType, // 手术类型
                    diagnoseCode: input.DiagnoseCode, // 术前诊断编码
                    diagnoseName: input.DiagnoseName, // 术前诊断名称
                    anesthesiologist: "", // 麻醉医生
                    anesthesiologistAssistant: "", // 麻醉助手
                    tourNurse: "", // 巡回护士
                    instrumentNurse: "", // 器械护士
                    anaestheticCode: "", // 麻醉方式编码
                    anaestheticName: "", // 麻醉方式名称
                    sequence: 0 // 手术台次
                );
                await _capPublisher.PublishAsync("sync.timeAxisRecord.to.patient", new CreateTimeAxisRecordDto()
                {
                    TimePointCode = 18,
                    Time = DateTime.Now,
                    PI_ID = input.PI_Id
                });
                return operationApply.Id;
            }
            else
            {
                var operationApply = await _operationApplyRepository.GetAsync(input.Id);
                if (operationApply == null)
                {
                    throw new EcisBusinessException(message: "手术申请不存在");
                }

                operationApply.Modify(
                    bloodType: input.BloodType, // 血型
                    rHCode: input.RHCode, // RH阴性阳性
                    height: input.Height, // 身高
                    weight: input.Weight, // 体重
                    proposedOperationCode: input.ProposedOperationCode, // 拟施手术编码
                    proposedOperationName: input.ProposedOperationName, // 拟施手术名称
                    operationLevel: input.OperationLevel, // 手术等级
                    applyDeptCode: input.ApplyDeptCode, // 申请科室编码
                    applyDeptName: input.ApplyDeptName, // 申请科室名称
                    operationLocation: input.OperationLocation, // 手术位置
                    doctorCode: input.DoctorCode, // 手术医生编码
                    doctorName: input.DoctorName, // 手术医生名称
                    operationAssistant: input.OperationAssistant, // 手术助手
                    operationPlanTime: input.OperationPlanTime.IsNullOrWhiteSpace()
                        ? null
                        : DateTime.Parse(input.OperationPlanTime), // 手术计划时间
                    operationDuration: input.OperationDuration, // 手术时长
                    remark: input.Remark, // 备注
                    status: input.Status, // 手术状态 0:申请中，1:申请通过，2：已撤回，3：已驳回
                    operationType: input.OperationType, // 手术类型
                    diagnoseCode: input.DiagnoseCode, // 术前诊断编码
                    diagnoseName: input.DiagnoseName // 术前诊断名称
                );

                await _operationApplyRepository.UpdateAsync(operationApply);
                return operationApply.Id;
            }
        }

        #endregion Create

        #region Get

        /// <summary>
        /// 根据id获取手术申请单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // [Authorize(RecipePermissions.OperationApplies.Default)]
        public async Task<OperationApplyData> GetOperationApplyAsync(Guid id)
        {
            if (!await _operationApplyRepository.AnyAsync(a => a.Id == id))
            {
                throw new EcisBusinessException(message: "手术申请不存在");
            }

            var operationApply = await _operationApplyRepository.GetAsync(id);

            return ObjectMapper.Map<OperationApply, OperationApplyData>(operationApply);
        }

        #endregion Get

        #region Delete

        /// <summary>
        /// 删除手术申请
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public async Task DeleteOperationApplyAsync([Required] Guid id)
        {
            var result = await _operationApplyRepository.FirstOrDefaultAsync(a => a.Id == id);
            if (result == null)
            {
                throw new EcisBusinessException(message: "手术申请不存在");
            }

            if (result.Status is OperationApplyStatus.申请中 or OperationApplyStatus.申请通过)
            {
                throw new EcisBusinessException(message: "无法删除");
            }

            await _operationApplyRepository.DeleteAsync(id: id);
        }

        #endregion Delete

        #region GetList

        /// <summary>
        /// 根据患者id获取列表信息
        /// </summary>
        /// <param name="pI_ID"></param>
        /// <returns></returns>
        public async Task<List<OperationApplyDataList>> GetOperationApplyListAsync([Required] Guid pI_ID)
        {
            var list = await _operationApplyRepository.GetListByPIIDAsync(pI_ID);
            return ObjectMapper.Map<List<OperationApply>, List<OperationApplyDataList>>(list);
        }

        #endregion GetList

        /// <summary>
        /// 提交申请状态修改 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operationStatus">0:申请中，1:申请通过，2：已撤回，3：已驳回</param>
        /// <exception cref="Exception"></exception>
        public async Task SubmitOperationApplyAsync([Required] Guid id, [Required] int operationStatus)
        {
            var data = await _operationApplyRepository.FirstOrDefaultAsync(f => f.Id == id);
            if (data == null)
            {
                throw new EcisBusinessException(message: "数据不存在");
            }

            if (data.Status == OperationApplyStatus.申请通过)
            {
                throw new EcisBusinessException(message: "手术申请已通过");
            }

            switch (operationStatus)
            {
                case 0:
                    data.Status = OperationApplyStatus.申请中;
                    data.SubmitDate = DateTime.Now;
                    break;
                case 1:
                    data.Status = OperationApplyStatus.申请通过;
                    break;
                case 2:
                    data.Status = OperationApplyStatus.已撤回;
                    break;
                case 3:
                    data.Status = OperationApplyStatus.已驳回;
                    break;
            }

            await _operationApplyRepository.UpdateAsync(data);
        }

        #region GetPagedList

        /// <summary>
        /// 按照筛选条件获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<List<OperationApplyDataList>> GetOperationApplyDataListAsync(OperationApplyInput input)
        {
            var list = await _operationApplyRepository.GetListAsync(input.StartTime, input.EndTime, input.ApplicantId);
            return ObjectMapper.Map<List<OperationApply>, List<OperationApplyDataList>>(
                list.Where(x => x.Status != OperationApplyStatus.已驳回).ToList());
        }

        #endregion GetPagedList
        /// <summary>
        /// 同步手术申请信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="EcisBusinessException"></exception>
        [CapSubscribe("sync.applyoperation.to.recipeservice")]
        public async Task SyncApplyOperationStatusAsync(OperationApplyStatusSync input)
        {

            var operationApply = await _operationApplyRepository.FirstOrDefaultAsync(x => x.ApplyNum == input.ApplyNum && x.PatientId == input.PatientId);
            if (operationApply == null)
            {
                throw new EcisBusinessException(message: "手术申请不存在");
            }
            operationApply.Sync(
                     anesthesiologist: input.Anesthesiologist, // 麻醉医生
                    anesthesiologistAssistant: input.AnesthesiologistAssistant, // 麻醉助手
                    tourNurse: input.TourNurse, // 巡回护士
                    instrumentNurse: input.InstrumentNurse, // 器械护士
                    status: input.Status,
                    anaestheticCode: input.AnaestheticCode, // 麻醉方式编码
                    anaestheticName: input.AnaestheticName, // 麻醉方式名称
                    sequence: input.Sequence // 手术台次
            );
            await _operationApplyRepository.UpdateAsync(operationApply);

        }


        #region 全景视图-手术申请列表

        /// <summary>
        /// 全景视图-手术申请列表
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public async Task<List<OperationApplyDataList>> GetAllViewOperApplyListAsync(Guid PID, DateTime? StartTime, DateTime? EndTime, CancellationToken cancellationToken = default)
        {

            try
            {
                var operApplyList = await (await this._operationApplyRepository.GetQueryableAsync()).AsNoTracking()
                 .Where(w => w.PI_Id == PID)
                .WhereIf(StartTime.HasValue, w => w.ApplyTime >= Convert.ToDateTime(StartTime))
                .WhereIf(EndTime.HasValue, w => w.ApplyTime <= Convert.ToDateTime(EndTime))
                .OrderBy(p => p.ApplyTime).ToListAsync();


                var list = ObjectMapper.Map<List<OperationApply>, List<OperationApplyDataList>>(operApplyList);

                return list;
                //_log.LogInformation("Get admissionRecord by id success");
                //return RespUtil.Ok<List<OperationApplyDataList>>(data: list);

            }
            catch (Exception)
            {
                return null;
                //_log.LogError("Get admissionRecord by id error.ErrorMsg:{Msg}", e);
                //return RespUtil.InternalError<List<OperationApplyDataList>>(extra: ex.Message);
            }
        }



        #endregion

    }
}
using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Uow;
using Volo.Abp.Users;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.ECIS.ShareModel.Utils;
using YiJian.Health.Report.Common;
using YiJian.Health.Report.Enums;
using YiJian.Health.Report.Hospitals;
using YiJian.Health.Report.NursingDocuments.Contracts;
using YiJian.Health.Report.NursingDocuments.Dto;
using YiJian.Health.Report.NursingDocuments.Entities;
using YiJian.Health.Report.NursingSettings.Contracts;
using YiJian.Health.Report.NursingSettings.Dto;
using YiJian.Health.Report.NursingSettings.Entities;
using YiJian.Health.Report.Patients.Dto;

namespace YiJian.Health.Report.NursingDocuments
{
    /// <summary>
    /// 护理单
    /// </summary>
    [Authorize]
    public partial class NursingDocumentAppService : ReportAppService, INursingDocumentAppService, ICapSubscribe
    {
        private readonly INursingDocumentRepository _nursingDocumentRepository;
        private readonly INursingRecordRepository _nursingRecordRepository;
        private readonly IDynamicFieldRepository _dynamicFieldRepository;
        private readonly ICriticalIllnessRepository _criticalIllnessRepository;
        private readonly IIntakeRepository _intakeRepository;
        private readonly IPupilRepository _pupilRepository;
        private readonly IMmolRepository _mmolRepository;
        private readonly ICharacteristicRepository _characteristicRepository;
        private readonly IDynamicDataRepository _dynamicDataRepository;
        private readonly INursingSettingRepository _nursingSettingRepository;
        private readonly INursingSettingHeaderRepository _nursingSettingHeaderRepository;
        private readonly IUnitOfWorkManager _uowManager;
        private readonly ILogger<NursingDocumentAppService> _logger;
        private readonly IDataFilter _dataFilter;
        private readonly IHospitalClientAppService _hospitalClientAppService;
        private readonly IConfiguration _configuration;
        private readonly IWardRoundRepository _wardRoundRepository;
        private readonly IIntakeStatisticsRepository _intakeStatisticsRepository;
        private readonly ISpecialNursingRecordRepository _specialNursingRecordRepository;
        private readonly PatientAppService _patientAppService;
        private readonly ICapPublisher _capPublisher;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="nursingDocumentRepository"></param>
        /// <param name="nursingRecordRepository"></param>
        /// <param name="dynamicFieldRepository"></param>
        /// <param name="criticalIllnessRepository"></param>
        /// <param name="intakeRepository"></param>
        /// <param name="pupilRepository"></param>
        /// <param name="mmolRepository"></param>
        /// <param name="characteristicRepository"></param>
        /// <param name="dynamicDataRepository"></param>
        /// <param name="nursingSettingRepository"></param>
        /// <param name="nursingSettingHeaderRepository"></param>
        /// <param name="uowManager"></param>
        /// <param name="logger"></param>
        /// <param name="hospitalClientAppService"></param>
        /// <param name="configuration"></param>
        /// <param name="capPublisher"></param>
        /// <param name="dataFilter"></param>
        /// <param name="wardRoundRepository"></param>
        /// <param name="patientAppService"></param>
        /// <param name="specialNursingRecordRepository"></param>
        /// <param name="intakeStatisticsRepository"></param>
        public NursingDocumentAppService(INursingDocumentRepository nursingDocumentRepository
            , INursingRecordRepository nursingRecordRepository
            , IDynamicFieldRepository dynamicFieldRepository
            , ICriticalIllnessRepository criticalIllnessRepository
            , IIntakeRepository intakeRepository
            , IPupilRepository pupilRepository
            , IMmolRepository mmolRepository
            , ICharacteristicRepository characteristicRepository
            , IDynamicDataRepository dynamicDataRepository
            , INursingSettingRepository nursingSettingRepository
            , INursingSettingHeaderRepository nursingSettingHeaderRepository
            , IUnitOfWorkManager uowManager
            , ILogger<NursingDocumentAppService> logger
            , IHospitalClientAppService hospitalClientAppService
            , IConfiguration configuration
            , ICapPublisher capPublisher
            , IDataFilter dataFilter
            , IWardRoundRepository wardRoundRepository
            , PatientAppService patientAppService
            , ISpecialNursingRecordRepository specialNursingRecordRepository
            , IIntakeStatisticsRepository intakeStatisticsRepository)
        {
            _nursingDocumentRepository = nursingDocumentRepository;
            _nursingRecordRepository = nursingRecordRepository;
            _dynamicFieldRepository = dynamicFieldRepository;
            _criticalIllnessRepository = criticalIllnessRepository;
            _intakeRepository = intakeRepository;
            _pupilRepository = pupilRepository;
            _mmolRepository = mmolRepository;
            _characteristicRepository = characteristicRepository;
            _dynamicDataRepository = dynamicDataRepository;
            _nursingSettingRepository = nursingSettingRepository;
            _nursingSettingHeaderRepository = nursingSettingHeaderRepository;
            _uowManager = uowManager;
            _logger = logger;
            _hospitalClientAppService = hospitalClientAppService;
            _configuration = configuration;
            _dataFilter = dataFilter;
            _capPublisher = capPublisher;
            _wardRoundRepository = wardRoundRepository;
            _patientAppService = patientAppService;
            _specialNursingRecordRepository = specialNursingRecordRepository;
            _intakeStatisticsRepository = intakeStatisticsRepository;
        }

        /// <summary>
        /// 查询护理单信息[展示主表]
        /// </summary>
        /// <see cref="PatientNursingDocumentRequestDto"/>
        /// <returns></returns>
        public async Task<ResponseBase<NursingDocumentDto>> ShowNursingDocumentAsync(
            PatientNursingDocumentRequestDto model)
        {
            NursingDocumentDto data = new NursingDocumentDto();
            List<NursingDocument> nursingDocuments = await _nursingDocumentRepository.GetListAsync(x => x.PI_ID == model.PI_ID);
            NursingDocument nursingDocument = nursingDocuments.OrderByDescending(x => x.CreationTime).FirstOrDefault();
            if (nursingDocument == null) return new ResponseBase<NursingDocumentDto>(EStatusCode.CNULL);

            //处理患者信息
            try
            {
                AdmissionRecordDto patient = await _patientAppService.GetPatientInfoAsync(model.PI_ID);
                if (patient != null) //处理患者信息更新的问题
                {
                    await UpdatePatientInfoAsync(nursingDocument, patient); //对比并且更新患者相关的信息
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"处理患者信息异常：{ex.Message}");
            }

            data = ObjectMapper.Map<NursingDocument, NursingDocumentDto>(nursingDocument);
            List<CriticalIllness> criticalIllness = (await _criticalIllnessRepository.GetListAsync(w => w.NursingDocumentId == nursingDocument.Id))
                .OrderByDescending(o => o.CreationTime).ToList();

            //二、三级查房内容
            List<WardRound> wardRounds = await (await _wardRoundRepository.GetQueryableAsync())
                .Where(w => w.NursingDocumentId == nursingDocument.Id)
                .WhereIf(model.SheetIndex.HasValue, w => w.SheetIndex == model.SheetIndex)
                .OrderBy(o => o.SheetIndex)
                .ToListAsync();

            if (wardRounds.Count > 0)
            {
                List<WardRoundDto> wardRoundDtos = ObjectMapper.Map<List<WardRound>, List<WardRoundDto>>(wardRounds);
                data.WardRounds = wardRoundDtos;
            }
            else
            {
                data.WardRounds = new List<WardRoundDto>() { new WardRoundDto(), new WardRoundDto() };
            }

            if (criticalIllness.Count > 0)
            {
                List<CriticalIllnessDto> criticalIllnessDtos = ObjectMapper.Map<List<CriticalIllness>, List<CriticalIllnessDto>>(criticalIllness);
                data.CriticalIllnessList = criticalIllnessDtos;
            }

            DynamicField dynamicField = await (await _dynamicFieldRepository.GetQueryableAsync())
                .Where(w => w.NursingDocumentId == nursingDocument.Id)
                .WhereIf(model.SheetIndex.HasValue, w => w.SheetIndex == model.SheetIndex)
                .OrderBy(o => o.SheetIndex)
                .FirstOrDefaultAsync();

            if (dynamicField != null)
            {
                DynamicFieldDto dynamicFieldDto = ObjectMapper.Map<DynamicField, DynamicFieldDto>(dynamicField);
                await SetFiledNameAsync(dynamicFieldDto);
                data.DynamicField = dynamicFieldDto;
            }
            else
            {
                data.DynamicField = new DynamicFieldDto() { Id = data.Id, FieldName = new FieldNameDto() };
            }

            List<NursingRecord> nursingRecords = await _nursingRecordRepository.GetRecordListAsync(nursingDocument.Id, model.Begintime, model.Endtime, dynamicField.SheetIndex);
            if (nursingRecords.Count > 0)
            {
                List<NursingRecordDto> nursingRecordDtos = ObjectMapper.Map<List<NursingRecord>, List<NursingRecordDto>>(nursingRecords);

                //添加出入量统计项
                List<IntakeStatistics> statisticsList = await _intakeStatisticsRepository.GetIntakeStatisticsListAsync(nursingDocument.Id, model.Begintime, model.Endtime, dynamicField.SheetIndex);
                foreach (IntakeStatistics item in statisticsList)
                {
                    data.NursingRecords.Add(new NursingRecordDto()
                    {
                        IsStatistics = true,
                        InIntakesTotal = item.InIntakesTotal,
                        OutIntakesTotal = item.OutIntakesTotal,
                        IntakeStatisticsId = item.Id,
                        Begintime = item.Begintime,
                        Endtime = item.Endtime,
                        RecordTime = item.Endtime
                    });
                }

                foreach (NursingRecordDto nursingRecordDto in nursingRecordDtos)
                {
                    if (float.TryParse(nursingRecordDto.T, out float t))
                    {
                        nursingRecordDto.T = t.ToString("#.0");
                    }

                    IEnumerable<string> recipeNos = nursingRecordDto.IntakeDtos.Where(x => !string.IsNullOrEmpty(x.RecipeNo)).Select(x => x.RecipeNo).Distinct();
                    foreach (string recipeNo in recipeNos)
                    {
                        List<IntakeDto> intakes = nursingRecordDto.IntakeDtos.Where(x => x.RecipeNo == recipeNo).ToList();
                        if (intakes.Count() < 2) continue;
                        for (int i = 0; i < intakes.Count(); i++)
                        {
                            if (i == 0) continue;
                            intakes[i].InputMode = string.Empty;
                        }

                        int count = intakes.Select(x => x.UnitCode).Distinct().Count();
                        if (count > 1)
                        {
                            foreach (IntakeDto item in intakes)
                            {
                                item.ContentUnit = $"({item.RecipeQty}{item.UnitCode})";
                            }
                        }
                    }

                    nursingRecordDto.IntakeDtos = nursingRecordDto.IntakeDtos.OrderBy(x => x.RecipeNo).ToList();
                    nursingRecordDto.SpecialNursings = nursingRecordDto.SpecialNursings.OrderBy(x => x.CreationTime).ToList();
                }

                data.NursingRecords.AddRange(nursingRecordDtos);

                //访问来源如果是交接班或者会诊的，时间降序 --yanghong
                if (model.QueryFrom == "HandoverOrGroupConsultation")
                {
                    data.NursingRecords = data.NursingRecords.OrderByDescending(o => o.RecordTime).ThenByDescending(o => o.IsStatistics).ToList();
                }
                else
                {
                    data.NursingRecords = data.NursingRecords.OrderBy(o => o.RecordTime).ThenBy(o => o.IsStatistics).ToList();
                }



                data.InIntakesTotal = data.NursingRecords.Select(p => p.InIntakes.Sum(s => Convert.ToDecimal(s.Quantity))).Sum().ToString();
                data.OutIntakesTotal = data.NursingRecords.Select(p => p.OutIntakes.Sum(s => Convert.ToDecimal(s.Quantity))).Sum().ToString();
            }
            data.Sheet = await (await _dynamicFieldRepository.GetQueryableAsync())
            .Where(w => w.NursingDocumentId == nursingDocument.Id)
            .Select(s => new SheetDto
            {
                Id = s.Id,
                NursingDocumentId = s.NursingDocumentId,
                SheetIndex = s.SheetIndex,
                SheetName = s.SheetName
            })
            .OrderBy(o => o.SheetIndex)
            .ToListAsync();

            return new ResponseBase<NursingDocumentDto>(EStatusCode.COK, data);
        }

        /// <summary>
        /// 对比并且更新患者相关的信息
        /// </summary>
        /// <param name="nursingDocument"></param>
        /// <param name="patient"></param>
        /// <returns></returns>
        private async Task UpdatePatientInfoAsync(NursingDocument nursingDocument, AdmissionRecordDto patient)
        {
            var idCard = patient.IDNo;
            var gender = patient.Sex == "Sex_Man" ? "男" : (patient.Sex == "Sex_Woman" ? "女" : "未知");
            var birthday = patient.Birthday;
            if (!idCard.IsNullOrEmpty())
            {
                var idcard = IDCard.IDCard.Verify(idCard);
                if (idcard.IsVerifyPass)
                {
                    gender = idcard.Gender;
                    birthday = idcard.DayOfBirth;
                }
            }

            //判断患者信息是否有变化，有变化就更新 
            if (patient.CardNo != nursingDocument.CardNo
                || patient.IDNo != nursingDocument.IDCard
                || patient.Bed != nursingDocument.BedNumber
                || patient.DiagnoseName != nursingDocument.Diagnose
                || patient.InDeptWay != nursingDocument.EmergencyWay)
            {
                nursingDocument.UpdatePatientInfo(patient.CardNo, patient.IDNo, gender, birthday,
                    patient.Bed, patient.DiagnoseName, patient.InDeptWay);
                _ = await _nursingDocumentRepository.UpdateAsync(nursingDocument);
            }
        }

        /// <summary>
        /// 设置动态字段的名称
        /// </summary>
        /// <param name="df"></param>
        /// <returns></returns>
        private async Task SetFiledNameAsync(DynamicFieldDto df)
        {
            var headerIds = new List<Guid>();
            if (df.Field1.HasValue) headerIds.Add(df.Field1.Value);
            if (df.Field2.HasValue) headerIds.Add(df.Field2.Value);
            if (df.Field3.HasValue) headerIds.Add(df.Field3.Value);
            if (df.Field4.HasValue) headerIds.Add(df.Field4.Value);
            if (df.Field5.HasValue) headerIds.Add(df.Field5.Value);
            if (df.Field6.HasValue) headerIds.Add(df.Field6.Value);
            if (df.Field7.HasValue) headerIds.Add(df.Field7.Value);
            if (df.Field8.HasValue) headerIds.Add(df.Field8.Value);
            if (df.Field9.HasValue) headerIds.Add(df.Field9.Value);

            if (headerIds.Count > 0)
            {
                var fn = new FieldNameDto();
                using (_dataFilter.Disable<ISoftDelete>())
                {
                    var headers = await (await _nursingSettingHeaderRepository.GetQueryableAsync())
                    .Where(w => headerIds.Contains(w.Id))
                    .Select(s => new { s.Id, s.Header })
                    .ToListAsync();

                    var h1 = headers.FirstOrDefault(w => w.Id == df.Field1);
                    fn.Field1 = h1 == null ? "" : h1.Header;
                    var h2 = headers.FirstOrDefault(w => w.Id == df.Field2);
                    fn.Field2 = h2 == null ? "" : h2.Header;
                    var h3 = headers.FirstOrDefault(w => w.Id == df.Field3);
                    fn.Field3 = h3 == null ? "" : h3.Header;
                    var h4 = headers.FirstOrDefault(w => w.Id == df.Field4);
                    fn.Field4 = h4 == null ? "" : h4.Header;
                    var h5 = headers.FirstOrDefault(w => w.Id == df.Field5);
                    fn.Field5 = h5 == null ? "" : h5.Header;
                    var h6 = headers.FirstOrDefault(w => w.Id == df.Field6);
                    fn.Field6 = h6 == null ? "" : h6.Header;
                    var h7 = headers.FirstOrDefault(w => w.Id == df.Field7);
                    fn.Field7 = h7 == null ? "" : h7.Header;
                    var h8 = headers.FirstOrDefault(w => w.Id == df.Field8);
                    fn.Field8 = h8 == null ? "" : h8.Header;
                    var h9 = headers.FirstOrDefault(w => w.Id == df.Field9);
                    fn.Field9 = h9 == null ? "" : h9.Header;
                    df.FieldName = fn;
                }
            }
        }

        /// <summary>
        /// 保存整个记录单
        /// </summary>
        /// <see cref="BulkChangesDto"/>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<ResponseBase<bool>> BulkChangesAsync(BulkChangesDto model)
        {
            var nurse = CurrentUser.FindClaimValue("fullName"); //获取操作人的名称
            var nurseCode = CurrentUser.UserName;
            var recordIds = new List<Guid>();
            var addModels = model.NursingRecords.Where(w => !w.Id.HasValue).ToList(); //待新增的记录
            var updateModels = model.NursingRecords.Where(w => w.Id.HasValue).ToList(); //待更新的记录
            var updateRecordIds = updateModels.Select(s => s.Id.Value).ToList();
            recordIds.AddRange(updateRecordIds); //添加更的记录Id集合

            var records = new List<NursingRecord>();
            var intakes = new List<Intake>();
            var mmols = new List<Mmol>();
            var pupils = new List<Pupil>();
            var characteristics = new List<Characteristic>();

            foreach (var item in addModels)
            {
                //护理记录单
                var entity = new NursingRecord(GuidGenerator.Create(), item.RecordTime, item.T, item.P, item.HR, item.R,
                    item.BP, item.BP2, item.SPO2, item.Consciousness,
                    item.Field1, item.Field2, item.Field3, item.Field4, item.Field5, item.Field6, item.Field7,
                    item.Field8, item.Field9,
                    item.Remark, nurseCode, nurse, model.SheetIndex, model.NursingDocumentId, item.Signature, item.Collator, item.CollatorCode, item.CollatorImage);
                records.Add(entity);

                //护理记录单-> 入量出量记录
                foreach (IntakeDto intakeDto in item.IntakeDtos)
                {
                    var intakeEntity = new Intake(GuidGenerator.Create(), intakeDto.IntakeType, intakeDto.Code, intakeDto.InputMode, intakeDto.Content, intakeDto.Quantity, intakeDto.RecipeQty, intakeDto.UnitCode, intakeDto.Unit, intakeDto.TraitsCode, intakeDto.Traits, intakeDto.Source, entity.Id, intakeDto.RecipeExecId, intakeDto.RecipeId, intakeDto.RecipeNo, intakeDto.Color);
                    intakes.Add(intakeEntity);
                }

                //护理记录单-> 指尖血糖记录 
                var mmolEntity = new Mmol(GuidGenerator.Create(), item.Mmol.MealTimeType, item.Mmol.Value, entity.Id);
                mmols.Add(mmolEntity);

                //护理单记录-> 瞳孔对光反应
                foreach (var pup in item.Pupil)
                {
                    var pupil = new Pupil(GuidGenerator.Create(), pup.PupilType, pup.Diameter, pup.LightReaction,
                        pup.OtherTrait, pup.Other, entity.Id);
                    pupils.Add(pupil);
                }

                /*
                #region 动态多项表单域

                foreach (var ddi in item.DynamicDataList)
                {
                    var dd = new Characteristic(GuidGenerator.Create(), ddi.JsonData, entity.Id, ddi.HeaderId);
                    characteristics.Add(dd);
                }
               
                #endregion
                */

                //特殊护理记录回填内容 (只做新增)
                var characteristic = new Characteristic(GuidGenerator.Create(), "", entity.Id, null);
                characteristics.Add(characteristic);
            }

            if (records.Count > 0)
            {
                await _nursingRecordRepository.InsertManyAsync(records); //1.添加护理记录单 
                var newRecordIds = records.Select(s => s.Id).ToList();
                recordIds.AddRange(newRecordIds); //添加新增的几率Id集合
                //添加动态记录数据
                await AddDynamicDataListAsync(records);
            }

            var updateIds = updateModels.Select(s => s.Id.Value).ToList();
            //var updateEntities = await _nursingRecordRepository.Where(w => updateIds.Contains(w.Id)).ToListAsync(); 
            var updateEntities = await _nursingRecordRepository.GetNursingRecordListAsync(updateIds);

            var deleteIntake = new List<Intake>();
            var deleteMmols = new List<Mmol>();
            var deletePupils = new List<Pupil>();

            foreach (var item in updateEntities)
            {
                var nm = updateModels.FirstOrDefault(w => w.Id.Value == item.Id);
                if (nm != null)
                {
                    item.Update(nm.RecordTime, nm.T, nm.P, nm.HR, nm.R, nm.BP, nm.BP2, nm.SPO2, nm.Consciousness,
                        nm.Field1, nm.Field2, nm.Field3, nm.Field4, nm.Field5, nm.Field6, nm.Field7, nm.Field8,
                        nm.Field9,
                        nm.Remark, nm.Signature, nm.Collator, nm.CollatorCode, nm.CollatorImage);

                    #region 新增

                    var nmNewIntakes = nm.IntakeDtos;
                    if (nmNewIntakes.Any())
                    {
                        //护理记录单-> 入量出量记录
                        foreach (var intake in nmNewIntakes)
                        {
                            var intakeEntity = new Intake(GuidGenerator.Create(), intake.IntakeType, intake.Code, intake.InputMode, intake.Content, intake.Quantity, intake.RecipeQty, intake.UnitCode, intake.Unit, intake.TraitsCode, intake.Traits, intake.Source, item.Id, intake.RecipeExecId, intake.RecipeId, intake.RecipeNo, intake.Color);
                            intakes.Add(intakeEntity);
                        }
                    }

                    var nmNewPupils = nm.Pupil;
                    if (nmNewPupils.Any())
                    {
                        //护理单记录-> 瞳孔对光反应
                        foreach (var pup in nmNewPupils)
                        {
                            var pupil = new Pupil(GuidGenerator.Create(), pup.PupilType, pup.Diameter,
                                pup.LightReaction, pup.OtherTrait, pup.Other, item.Id);
                            pupils.Add(pupil);
                        }
                    }

                    if (nm.Mmol != null)
                    {
                        //护理记录单-> 指尖血糖记录 
                        var mmolEntity = new Mmol(GuidGenerator.Create(), nm.Mmol.MealTimeType, nm.Mmol.Value, item.Id);
                        mmols.Add(mmolEntity);
                    }
                    else
                    {
                        if (item.Mmol != null)
                        {
                            deleteMmols.Add(item.Mmol);
                        }
                    }

                    if (item.Intakes.Any()) deleteIntake.AddRange(item.Intakes);
                    if (item.Pupil.Any()) deletePupils.AddRange(item.Pupil);

                    #endregion

                    /* 独立模块处理
                    foreach (var ddi in nm.DynamicDataList)
                    {
                        var dd = new Characteristic(GuidGenerator.Create(), ddi.JsonData, item.Id, ddi.HeaderId);
                        characteristics.Add(dd);
                    }
                    */
                }
            }

            if (updateEntities.Count > 0)
            {
                var nursingRecordIds = updateEntities.Select(s => s.Id).ToList();

                //先删除记录再新增
                var df = await (await _dynamicFieldRepository.GetQueryableAsync()).FirstOrDefaultAsync(w =>
                    w.NursingDocumentId == updateEntities[0].NursingDocumentId &&
                    w.SheetIndex == updateEntities[0].SheetIndex);
                if (df != null)
                {
                    var fileds = new List<Guid?>()
                    {
                        df.Field1, df.Field2, df.Field3, df.Field4, df.Field5, df.Field6, df.Field7, df.Field8,
                        df.Field9
                    };
                    var dEntities = await (await _dynamicDataRepository.GetQueryableAsync())
                        .Where(w => w.NursingDocumentId == updateEntities[0].NursingDocumentId &&
                                    w.SheetIndex == updateEntities[0].SheetIndex && fileds.Contains(w.Header))
                        .ToListAsync();
                    await _dynamicDataRepository.DeleteManyAsync(dEntities);
                }

                //新增动态多项内容到备份表中 
                await AddDynamicDataListAsync(updateEntities);
            }

            if (deleteIntake.Count > 0) await _intakeRepository.DeleteManyAsync(deleteIntake);
            if (deletePupils.Count > 0) await _pupilRepository.DeleteManyAsync(deletePupils);
            if (deleteMmols.Count > 0) await _mmolRepository.DeleteManyAsync(deleteMmols);

            if (intakes.Count > 0) await _intakeRepository.InsertManyAsync(intakes); //1.1 添加入量出量记录集合  
            if (mmols.Count > 0) await _mmolRepository.InsertManyAsync(mmols); // 1.2 添加指尖血糖记录集合  
            if (pupils.Count > 0) await _pupilRepository.InsertManyAsync(pupils); //1.3 添加瞳孔评估记录集合 
            //添加特殊护理记录回填数据

            #region 操作动态六项表单域内容

            var deleteCharacteristics = await (await _characteristicRepository.GetQueryableAsync())
                .Where(w => recordIds.Contains(w.NursingRecordId)).ToListAsync();
            //if (recordIds.Count > 0) await _characteristicRepository.DeleteManyAsync(deleteCharacteristics); //删除所有的动态多项护理单记录相关的表单域数据(有独立模块处理)
            if (characteristics.Count > 0)
                await _characteristicRepository.InsertManyAsync(characteristics); //覆盖之前的动态多项护理记录相关的表单域

            #endregion

            return new ResponseBase<bool>(EStatusCode.COK, true);
        }

        /// <summary>
        /// 添加动态记录数据
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        private async Task AddDynamicDataListAsync(List<NursingRecord> records)
        {
            if (records == null || records.Count == 0)
            {
                return;
            }

            var df = await (await _dynamicFieldRepository.GetQueryableAsync()).FirstOrDefaultAsync(w =>
                w.NursingDocumentId == records[0].NursingDocumentId && w.SheetIndex == records[0].SheetIndex);
            if (df == null)
            {
                return;
            }

            var dynamicDataList = new List<DynamicData>();
            foreach (var rec in records)
            {
                //新增动态多项内容到备份表中  
                if (df.Field1.HasValue && !rec.Field1.IsNullOrEmpty())
                    dynamicDataList.Add(new DynamicData(GuidGenerator.Create(), df.Field1.Value, rec.Field1,
                        rec.SheetIndex, rec.Id, rec.NursingDocumentId));
                if (df.Field2.HasValue && !rec.Field2.IsNullOrEmpty())
                    dynamicDataList.Add(new DynamicData(GuidGenerator.Create(), df.Field2.Value, rec.Field2,
                        rec.SheetIndex, rec.Id, rec.NursingDocumentId));
                if (df.Field3.HasValue && !rec.Field3.IsNullOrEmpty())
                    dynamicDataList.Add(new DynamicData(GuidGenerator.Create(), df.Field3.Value, rec.Field3,
                        rec.SheetIndex, rec.Id, rec.NursingDocumentId));
                if (df.Field4.HasValue && !rec.Field4.IsNullOrEmpty())
                    dynamicDataList.Add(new DynamicData(GuidGenerator.Create(), df.Field4.Value, rec.Field4,
                        rec.SheetIndex, rec.Id, rec.NursingDocumentId));
                if (df.Field5.HasValue && !rec.Field5.IsNullOrEmpty())
                    dynamicDataList.Add(new DynamicData(GuidGenerator.Create(), df.Field5.Value, rec.Field5,
                        rec.SheetIndex, rec.Id, rec.NursingDocumentId));
                if (df.Field6.HasValue && !rec.Field6.IsNullOrEmpty())
                    dynamicDataList.Add(new DynamicData(GuidGenerator.Create(), df.Field6.Value, rec.Field6,
                        rec.SheetIndex, rec.Id, rec.NursingDocumentId));
                if (df.Field7.HasValue && !rec.Field7.IsNullOrEmpty())
                    dynamicDataList.Add(new DynamicData(GuidGenerator.Create(), df.Field7.Value, rec.Field7,
                        rec.SheetIndex, rec.Id, rec.NursingDocumentId));
                if (df.Field8.HasValue && !rec.Field8.IsNullOrEmpty())
                    dynamicDataList.Add(new DynamicData(GuidGenerator.Create(), df.Field8.Value, rec.Field8,
                        rec.SheetIndex, rec.Id, rec.NursingDocumentId));
                if (df.Field9.HasValue && !rec.Field9.IsNullOrEmpty())
                    dynamicDataList.Add(new DynamicData(GuidGenerator.Create(), df.Field9.Value, rec.Field9,
                        rec.SheetIndex, rec.Id, rec.NursingDocumentId));
            }

            await _dynamicDataRepository.InsertManyAsync(dynamicDataList);
        }

        /// <summary>
        /// 修改Sheet操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<ResponseBase<List<DynamicFieldDto>>> ModifySheetAsync(SheetSetDto model)
        {
            Guid? nursingDocumentId = model.NursingDocumentId;
            if (model.Id.HasValue)
            {
                DynamicField dynamicField = await _dynamicFieldRepository.GetAsync(x => x.Id == model.Id);
                if (dynamicField == null) return new ResponseBase<List<DynamicFieldDto>>(EStatusCode.CNULL, "找不到指定的Sheet");

                dynamicField.UpdateSheet(model.SheetName);
                await _dynamicFieldRepository.UpdateAsync(dynamicField, true);

                nursingDocumentId = dynamicField.NursingDocumentId;
            }
            else
            {
                if (!nursingDocumentId.HasValue)
                {
                    return new ResponseBase<List<DynamicFieldDto>>(EStatusCode.CNULL, "新增时[nursingDocumentId]字段是必传参数");
                }

                List<DynamicField> dynamicFields = await _dynamicFieldRepository.GetListAsync(x => x.NursingDocumentId == model.NursingDocumentId);
                int index = dynamicFields.Max(a => a.SheetIndex);
                int sheetIndex = index + 1;

                DynamicField newDynamicField = new DynamicField(GuidGenerator.Create(), sheetIndex, model.SheetName, model.NursingDocumentId.Value);
                _ = await _dynamicFieldRepository.InsertAsync(newDynamicField, true);
                nursingDocumentId = newDynamicField.NursingDocumentId;
            }

            List<DynamicField> dynamicFieldList = await _dynamicFieldRepository.GetListAsync(w => w.NursingDocumentId == nursingDocumentId);
            List<DynamicFieldDto> dynamicFieldDtos = ObjectMapper.Map<List<DynamicField>, List<DynamicFieldDto>>(dynamicFieldList);
            return new ResponseBase<List<DynamicFieldDto>>(EStatusCode.COK, dynamicFieldDtos);
        }

        /// <summary>
        /// 删除空的Sheet护理单据
        /// </summary>
        /// <param name="dynamicFieldId"></param>
        /// <returns></returns>
        public async Task<ResponseBase<bool>> RemoveEmptySheetAsync(Guid dynamicFieldId)
        {
            var df = await (await _dynamicFieldRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == dynamicFieldId);
            if (df == null) return new ResponseBase<bool>(EStatusCode.CNULL, "无数据");

            var count = await (await _nursingRecordRepository.GetQueryableAsync())
                .Where(w => w.NursingDocumentId == df.NursingDocumentId && w.SheetIndex == df.SheetIndex).CountAsync();
            if (count > 0)
            {
                return new ResponseBase<bool>(EStatusCode.CFail, false, "当前护理单有护理记录数据无法强制删除");
            }

            await _dynamicFieldRepository.DeleteAsync(w => w.Id == dynamicFieldId);
            return new ResponseBase<bool>(EStatusCode.COK, true);
        }

        /// <summary>
        /// 修改动态六项表头内容
        /// </summary>
        /// <see cref="DynamicFieldDto"/>
        /// <returns></returns>
        public async Task<ResponseBase<Guid>> ModifySixSettingAsync(DynamicSixFieldDto model)
        {
            var entity = await (await _dynamicFieldRepository.GetQueryableAsync()).FirstOrDefaultAsync(w =>
                w.NursingDocumentId == model.NursingDocumentId && w.SheetIndex == model.SheetIndex);
            if (entity == null)
            {
                var newEntity = new DynamicField(GuidGenerator.Create(),
                    model.SheetIndex, model.SheetName, model.NursingDocumentId,
                    model.Field1, model.Field2, model.Field3, model.Field4, model.Field5, model.Field6, model.Field7,
                    model.Field8, model.Field9);
                _ = await _dynamicFieldRepository.InsertAsync(newEntity);
                return new ResponseBase<Guid>(EStatusCode.COK, newEntity.Id);
            }
            else
            {
                //先保存一个副本
                var oldFiled = new
                {
                    f1 = entity.Field1,
                    f2 = entity.Field2,
                    f3 = entity.Field3,
                    f4 = entity.Field4,
                    f5 = entity.Field5,
                    f6 = entity.Field6,
                    f7 = entity.Field7,
                    f8 = entity.Field8,
                    f9 = entity.Field9
                };

                entity.Update(model.Field1, model.Field2, model.Field3, model.Field4, model.Field5, model.Field6,
                    model.Field7, model.Field8, model.Field9);
                _ = await _dynamicFieldRepository.UpdateAsync(entity);

                //如果有数据则需要迁移
                Dictionary<string, Guid?> headers = new Dictionary<string, Guid?>();
                //如果字段目前不一致了那么回填数据
                if (oldFiled.f1 != model.Field1) headers.Add(nameof(model.Field1), model.Field1);
                if (oldFiled.f2 != model.Field2) headers.Add(nameof(model.Field2), model.Field2);
                if (oldFiled.f3 != model.Field3) headers.Add(nameof(model.Field3), model.Field3);
                if (oldFiled.f4 != model.Field4) headers.Add(nameof(model.Field4), model.Field4);
                if (oldFiled.f5 != model.Field5) headers.Add(nameof(model.Field5), model.Field5);
                if (oldFiled.f6 != model.Field6) headers.Add(nameof(model.Field6), model.Field6);
                if (oldFiled.f7 != model.Field7) headers.Add(nameof(model.Field7), model.Field7);
                if (oldFiled.f8 != model.Field8) headers.Add(nameof(model.Field8), model.Field8);
                if (oldFiled.f9 != model.Field9) headers.Add(nameof(model.Field9), model.Field9);

                var headerValue = headers.Where(w => w.Value.HasValue).Select(s => s.Value.Value).ToList();

                var ddList =
                    await GetDynamicDataAsync(entity.NursingDocumentId, entity.SheetIndex,
                        headerValue); //获取当前护理单的所有的动态数据
                var records = await (await _nursingRecordRepository.GetQueryableAsync())
                    .Where(w => w.NursingDocumentId == entity.NursingDocumentId && w.SheetIndex == entity.SheetIndex)
                    .ToListAsync(); //获取当前护理单的所有记录

                foreach (var item in headers) //只处理有变化的字段
                {
                    var list = ddList.Where(w => w.Header == item.Value).ToList();
                    foreach (var rec in records) //循环回填内容
                    {
                        var filed = item.Key.ToLower(); //获取需要更新的字段名称
                        if (list.Count == 0)
                        {
                            EmptyFiled(rec, filed);
                            continue;
                        }

                        var recordEntity = list.FirstOrDefault(w => w.NursingRecordId == rec.Id);
                        if (recordEntity == null) //查找不到记录，清空当前cell
                        {
                            EmptyFiled(rec, filed);
                            continue;
                        }

                        FillFiled(rec, recordEntity, filed);
                    }
                }

                if (records.Count > 0)
                {
                    await _nursingRecordRepository.UpdateManyAsync(records); //全部更新 
                }

                return new ResponseBase<Guid>(EStatusCode.COK, entity.Id);
            }
        }

        /// <summary>
        /// 填充动态字段
        /// </summary>
        /// <param name="rec"></param>
        /// <param name="recordEntity"></param>
        /// <param name="filed"></param>
        private static void FillFiled(NursingRecord rec, DynamicData recordEntity, string filed)
        {
            switch (filed)
            {
                case "field1":
                    rec.Field1 = recordEntity.Text;
                    break;
                case "field2":
                    rec.Field2 = recordEntity.Text;
                    break;
                case "field3":
                    rec.Field3 = recordEntity.Text;
                    break;
                case "field4":
                    rec.Field4 = recordEntity.Text;
                    break;
                case "field5":
                    rec.Field5 = recordEntity.Text;
                    break;
                case "field6":
                    rec.Field6 = recordEntity.Text;
                    break;
                case "field7":
                    rec.Field7 = recordEntity.Text;
                    break;
                case "field8":
                    rec.Field8 = recordEntity.Text;
                    break;
                case "field9":
                    rec.Field9 = recordEntity.Text;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 置空
        /// </summary>
        /// <param name="rec"></param>
        /// <param name="filed"></param>
        private static void EmptyFiled(NursingRecord rec, string filed)
        {
            switch (filed)
            {
                case "field1":
                    rec.Field1 = "";
                    break;
                case "field2":
                    rec.Field2 = "";
                    break;
                case "field3":
                    rec.Field3 = "";
                    break;
                case "field4":
                    rec.Field4 = "";
                    break;
                case "field5":
                    rec.Field5 = "";
                    break;
                case "field6":
                    rec.Field6 = "";
                    break;
                case "field7":
                    rec.Field7 = "";
                    break;
                case "field8":
                    rec.Field8 = "";
                    break;
                case "field9":
                    rec.Field9 = "";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 根据header 获取当前护理单下的所有动态数据
        /// </summary>
        /// <param name="did">NursingDocumentId</param>
        /// <param name="sheetIndex">SheetIndex</param>
        /// <param name="headers">多个Header</param>
        /// <returns></returns>
        public async Task<List<DynamicData>> GetDynamicDataAsync(Guid did, int sheetIndex, List<Guid> headers)
        {
            var data = await (await _dynamicDataRepository.GetQueryableAsync())
                .Where(w => w.NursingDocumentId == did && w.SheetIndex == sheetIndex)
                .WhereIf(headers.Any(), w => headers.Contains(w.Header))
                .ToListAsync();
            return data;
        }

        /// <summary>
        /// 添加新的入院护理单(入院只有新增，无修改)
        /// </summary>
        /// <see cref="AddNursingDocumentDto"/>
        /// <returns></returns>
        public async Task<ResponseBase<Guid>> AddNursingDocumentAsync(AddNursingDocumentDto model)
        {
            IUnitOfWork uow = _uowManager.Begin();
            AdmissionRecordDto patient = await _patientAppService.GetPatientInfoAsync(model.PI_ID);
            if (patient == null) return new ResponseBase<Guid>(EStatusCode.CNULL, $"找不到患者[{model.PI_ID}]信息");
            DateTime? inDeptTime = patient.InDeptTime;

            if (!inDeptTime.HasValue) return new ResponseBase<Guid>(EStatusCode.CNULL, "患者未入抢救区或留观区，无法生成护理单");

            int count = await (await _nursingDocumentRepository.GetQueryableAsync()).CountAsync(w => w.PI_ID == model.PI_ID);
            if (count > 0) return new ResponseBase<Guid>(EStatusCode.COK, model.PI_ID, "当前护理单已经存在,往下放心操作即可");

            string idCard = patient.IDNo;
            string gender = patient.Sex == "Sex_Man" ? "男" : (patient.Sex == "Sex_Woman" ? "女" : "未知");
            DateTime? birthday = patient.Birthday;
            if (!idCard.IsNullOrEmpty())
            {
                var idcard = IDCard.IDCard.Verify(idCard);
                if (idcard.IsVerifyPass)
                {
                    gender = idcard.Gender;
                    birthday = idcard.DayOfBirth;
                }
            }

            NursingDocument nursingDocument = new NursingDocument(GuidGenerator.Create(), model.PI_ID, model.Title, patient.CardNo,
                patient.PatientID, patient.PatientName,
                patient.IDNo, gender, birthday, patient.Bed, patient.Age, inDeptTime.Value, patient.DiagnoseName,
                patient.TriageDeptCode, patient.TriageDeptName,
                patient.InDeptWay, model.NursingCode);
            _ = await _nursingDocumentRepository.InsertAsync(nursingDocument);

            ECriticalIllness status = (ECriticalIllness)patient.EmergencyLevel;
            CriticalIllness criticalIllness = new CriticalIllness(GuidGenerator.Create(), model.PI_ID, status: status, inDeptTime.Value, null, patient.PatientID, patient.PatientName, nursingDocument.Id);
            _ = await _criticalIllnessRepository.InsertAsync(criticalIllness);

            //创建当前入院字段的表头 
            DynamicField dynamicField = new DynamicField(GuidGenerator.Create(), 0, "护理记录单1", nursingDocument.Id, null, null, null, null,
                null, null, null, null, null);
            _ = await _dynamicFieldRepository.InsertAsync(dynamicField);

            await uow.SaveChangesAsync();
            return new ResponseBase<Guid>(EStatusCode.COK, nursingDocument.Id);
        }

        /// <summary>
        /// 更新病危病重信息
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateSeriouslyIllAsync(UpdateSeriouslyIllDto updateSeriouslyIllDto)
        {
            if (updateSeriouslyIllDto == null || updateSeriouslyIllDto.Id == Guid.Empty)
            {
                Oh.Error("请求参数为空");
            }

            NursingDocument nursingDocument = await _nursingDocumentRepository.GetAsync(x => x.Id == updateSeriouslyIllDto.Id);
            if (nursingDocument == null)
            {
                Oh.Error("没有找到护理记录单");
            }

            nursingDocument.IsCriticallyIll = updateSeriouslyIllDto.IsCriticallyIll;
            nursingDocument.IsSeriouslyIll = updateSeriouslyIllDto.IsSeriouslyIll;
            nursingDocument.SeriouslyTime = updateSeriouslyIllDto.SeriouslyTime;
            nursingDocument.GreenTime = updateSeriouslyIllDto.GreenTime;
            nursingDocument.IsGreen = updateSeriouslyIllDto.IsGreen;
            await _nursingDocumentRepository.UpdateAsync(nursingDocument);
            return true;
        }

        /// <summary>
        /// 根据护理单记录的Id获取护理单记录信息
        /// </summary>
        /// <param name="id">护理单记录的Id</param>
        /// <returns></returns> 
        public async Task<ResponseBase<NursingRecordDto>> GetNursingRecordAsync(Guid id)
        {
            var entity = await (await _nursingRecordRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == id);
            if (entity == null) return new ResponseBase<NursingRecordDto>(EStatusCode.CNULL);
            var map = ObjectMapper.Map<NursingRecord, NursingRecordDto>(entity);
            //var df = await _dynamicFieldRepository.FirstOrDefaultAsync(w => w.NursingDocumentId == entity.NursingDocumentId && w.SheetIndex == entity.SheetIndex);
            return new ResponseBase<NursingRecordDto>(EStatusCode.COK, map);
        }

        #region 更新护理单

        /// <summary>
        /// 新建一个空记录
        /// </summary>
        /// <see cref="NewNursingRecordDto"/>
        /// <returns></returns>
        public async Task<ResponseBase<NursingRecordDto>> NewNursingRecordAsync(NewNursingRecordDto param)
        {
            var nurse = CurrentUser.FindClaimValue("fullName"); //获取操作人的名称
            var nurseCode = CurrentUser.UserName;
            var model = new ModifyNursingRecordDto(param.NursingDocumentId, param.SheetIndex, param.Signature);

            NursingRecord nursingRecord = new NursingRecord(GuidGenerator.Create(), model.RecordTime, model.T, model.P, model.HR,
                model.R, model.BP, model.BP2, model.SPO2, model.Consciousness,
                model.Field1, model.Field2, model.Field3, model.Field4, model.Field5, model.Field6, model.Field7,
                model.Field8, model.Field9,
                model.Remark, nurseCode, nurse, model.SheetIndex, model.NursingDocumentId, model.Signature, model.Collator, model.CollatorCode, model.CollatorImage);
            _ = await _nursingRecordRepository.InsertAsync(nursingRecord);

            var characteristic = new Characteristic(GuidGenerator.Create(), "", nursingRecord.Id, null);
            _ = await _characteristicRepository.InsertAsync(characteristic);

            var map = ObjectMapper.Map<NursingRecord, NursingRecordDto>(nursingRecord);
            return new ResponseBase<NursingRecordDto>(EStatusCode.COK, map);
        }

        /// <summary>
        /// 更新护理单记录信息(新增不需要传Id，更新需要)
        /// </summary>
        /// <see cref="ModifyNursingRecordDto"/>
        /// <returns></returns> 
        [UnitOfWork]
        public async Task<ResponseBase<Guid>> ModifyNursingRecordAsync(ModifyNursingRecordDto model)
        {
            //更新
            if (model.Id.HasValue)
            {
                return await UpdateNursingRecordAsync(model);
            }
            //新增
            else
            {
                return await AddNursingRecord2Async(model);
            }
        }

        /// <summary>
        /// 新增护理单记录信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<ResponseBase<Guid>> AddNursingRecord2Async(ModifyNursingRecordDto model)
        {
            string nurse = CurrentUser.FindClaimValue("fullName"); //获取操作人的名称
            string nurseCode = CurrentUser.UserName;

            NursingRecord nursingRecord = new NursingRecord(GuidGenerator.Create(), model.RecordTime, model.T, model.P, model.HR,
                model.R, model.BP, model.BP2, model.SPO2, model.Consciousness,
                model.Field1, model.Field2, model.Field3, model.Field4, model.Field5, model.Field6, model.Field7,
                model.Field8, model.Field9,
                model.Remark, nurseCode, nurse, model.SheetIndex, model.NursingDocumentId, model.Signature, model.Collator, model.CollatorCode, model.CollatorImage);

            _logger.LogInformation("新增护理记录单时，云签开始");
            //北大签名通过前端传到后端

            if (_configuration["HospitalCode"] == "LDC" && string.IsNullOrEmpty(model.Signature))
            {
                string signature = await _hospitalClientAppService.QueryStampBaseAsync(nurseCode);
                nursingRecord.Sign(signature);
            }
            _ = await _nursingRecordRepository.InsertAsync(nursingRecord);

            //新增指尖血糖
            MmolDto mmonModel = model.Mmol;
            if (model.Mmol != null)
            {
                Mmol mmonEntity = new Mmol(GuidGenerator.Create(), mmonModel.MealTimeType, mmonModel.Value, nursingRecord.Id);
                await _mmolRepository.InsertAsync(mmonEntity);
            }

            //新增瞳孔参数
            List<PupilDto> pupils = model.Pupil;
            if (pupils.Count > 0)
            {
                List<Pupil> pupilEntities = new List<Pupil>();
                foreach (PupilDto pupil in pupils)
                {
                    pupilEntities.Add(new Pupil(GuidGenerator.Create(), pupil.PupilType, pupil.Diameter,
                        pupil.LightReaction, pupil.OtherTrait, pupil.Other, nursingRecord.Id));
                }

                await _pupilRepository.InsertManyAsync(pupilEntities);
            }

            //新增入量出量
            List<IntakeDto> intakes = model.IntakeDtos;
            List<Intake> intakeEntities = new List<Intake>();
            if (intakes.Count > 0)
            {
                foreach (IntakeDto intake in intakes)
                {
                    intakeEntities.Add(new Intake(GuidGenerator.Create(), intake.IntakeType, intake.Code, intake.InputMode, intake.Content, intake.Quantity, intake.RecipeQty, intake.UnitCode, intake.Unit, intake.TraitsCode, intake.Traits, intake.Source, nursingRecord.Id, intake.RecipeExecId, intake.RecipeId, intake.RecipeNo, intake.Color));
                }

                await _intakeRepository.InsertManyAsync(intakeEntities);
            }

            var fields = new List<Characteristic>();
            var characteristic = new Characteristic(GuidGenerator.Create(), "", nursingRecord.Id, null);
            fields.Add(characteristic);

            //新增动态多项内容到备份表中  
            var dynamicDatas = new List<NursingRecord>() { nursingRecord };
            await AddDynamicDataListAsync(dynamicDatas);

            /* 有独立模块处理
            if (model.DynamicDataList.Count > 0)
            { 
                foreach (var item in model.DynamicDataList)
                {
                    if (item.HeaderId.HasValue)
                    {
                        //保存会写数据结构
                        var field = new Characteristic(GuidGenerator.Create(), item.JsonData, entity.Id, item.HeaderId.Value);
                        fields.Add(field);
                    } 
                }
            }
            */

            if (fields.Count > 0)
            {
                await _characteristicRepository.InsertManyAsync(fields);
            }

            await CapPublishToTemperatureAsync(nursingRecord, intakeEntities);
            return new ResponseBase<Guid>(EStatusCode.COK, nursingRecord.Id);
        }

        /// <summary>
        /// 更新护理单记录信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<ResponseBase<Guid>> UpdateNursingRecordAsync(ModifyNursingRecordDto model)
        {
            //var entity = await _nursingRecordRepository.FindAsync(model.Id.Value);
            using var uow = UnitOfWorkManager.Begin();
            var list = await _nursingRecordRepository.GetNursingRecordListAsync(new List<Guid>() { model.Id.Value });
            var entity = list.FirstOrDefault();

            if (entity == null)
            {
                return new ResponseBase<Guid>(EStatusCode.CNULL);
            }

            if (entity.NurseCode != CurrentUser.UserName) Oh.Error("非本人填写的护理记录单无法修改");

            int second = model.RecordTime.Second;
            DateTime startTime = model.RecordTime.AddSeconds(-second);
            DateTime endTime = startTime.AddMinutes(1);
            var nursingRecord = (await _nursingRecordRepository.GetQueryableAsync()).FirstOrDefault(x => x.NursingDocumentId == entity.NursingDocumentId && x.SheetIndex == entity.SheetIndex && x.Id != entity.Id && x.RecordTime >= startTime && x.RecordTime < endTime);
            if (nursingRecord != null)
            {
                Oh.Error("同一时间点已经存在护理记录");
            }

            entity.Update(model.RecordTime, model.T, model.P, model.HR, model.R, model.BP, model.BP2, model.SPO2,
                model.Consciousness,
                model.Field1, model.Field2, model.Field3, model.Field4, model.Field5, model.Field6, model.Field7,
                model.Field8, model.Field9,
                model.Remark, model.Signature, model.Collator, model.CollatorCode, model.CollatorImage);

            _logger.LogInformation("跟新护理记录单时，云签开始");
            //北大签名通过前端传到后端
            if (_configuration["HospitalCode"] == "LDC" && string.IsNullOrEmpty(model.Signature))
            {
                var signature = await _hospitalClientAppService.QueryStampBaseAsync(entity.NurseCode);
                entity.Sign(signature);
            }

            _ = await _nursingRecordRepository.UpdateAsync(entity);

            //指尖血糖
            var mmonModel = model.Mmol;
            if (mmonModel != null)
            {
                var mmol = await (await _mmolRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.NursingRecordId == entity.Id);
                if (mmol == null)
                {
                    var mmonEntity = new Mmol(GuidGenerator.Create(), mmonModel.MealTimeType, mmonModel.Value,
                        entity.Id);
                    await _mmolRepository.InsertAsync(mmonEntity);
                }
                else
                {
                    mmol.Update(mmonModel.MealTimeType, mmonModel.Value);
                    await _mmolRepository.UpdateAsync(mmol);
                }
            }
            else
            {
                await _mmolRepository.DeleteAsync(w => w.NursingRecordId == entity.Id);
            }

            //瞳孔参数 
            if (entity.Pupil.Count > 0) await _pupilRepository.DeleteManyAsync(entity.Pupil);
            var pupilModels = model.Pupil;
            if (pupilModels.Count > 0)
            {
                var pupilEntities = new List<Pupil>();
                foreach (var pupil in pupilModels)
                {
                    pupilEntities.Add(new Pupil(GuidGenerator.Create(), pupil.PupilType, pupil.Diameter,
                        pupil.LightReaction, pupil.OtherTrait, pupil.Other, entity.Id));
                }

                await _pupilRepository.InsertManyAsync(pupilEntities);
            }

            //入量出量
            if (entity.Intakes.Count > 0) await _intakeRepository.DeleteManyAsync(entity.Intakes);
            var intakeModels = model.IntakeDtos;
            var intakeEntities = new List<Intake>();
            if (intakeModels.Count > 0)
            {
                foreach (var intake in intakeModels)
                {
                    intakeEntities.Add(new Intake(GuidGenerator.Create(), intake.IntakeType, intake.Code, intake.InputMode, intake.Content, intake.Quantity, intake.RecipeQty, intake.UnitCode, intake.Unit, intake.TraitsCode, intake.Traits, intake.Source, entity.Id, intake.RecipeExecId, intake.RecipeId, intake.RecipeNo, intake.Color));
                }

                await _intakeRepository.InsertManyAsync(intakeEntities);
            }
            await uow.SaveChangesAsync();
            await uow.CompleteAsync();

            //先删除统计再新增
            await GetUpdateIntakeStatisticsAsync(model.NursingDocumentId, model.SheetIndex);

            var df = await (await _dynamicFieldRepository.GetQueryableAsync()).FirstOrDefaultAsync(w =>
                w.NursingDocumentId == entity.NursingDocumentId && w.SheetIndex == entity.SheetIndex);
            if (df != null)
            {
                var fileds = new List<Guid?>()
                {
                    df.Field1, df.Field2, df.Field3, df.Field4, df.Field5, df.Field6, df.Field7, df.Field8, df.Field9
                };
                var dEntities = await (await _dynamicDataRepository.GetQueryableAsync())
                    .Where(w => w.NursingDocumentId == entity.NursingDocumentId && w.SheetIndex == entity.SheetIndex && w.NursingRecordId == entity.Id &&
                                fileds.Contains(w.Header))
                    .ToListAsync();
                await _dynamicDataRepository.DeleteManyAsync(dEntities);
            }

            //新增动态多项内容到备份表中  
            var dynamicDatas = new List<NursingRecord>() { entity };
            await AddDynamicDataListAsync(dynamicDatas);
            await uow.CompleteAsync();
            /* 有独立模块处理
            if (model.DynamicDataList.Count>0)
            { 
                var newFields = new  List<Characteristic>();
                foreach (var item in model.DynamicDataList)
                {
                    if (item.HeaderId.HasValue)
                    { 
                        await _characteristicRepository.DeleteAsync(w => w.NursingRecordId == entity.Id && w.HeaderId == item.HeaderId.Value);
                        //保存会写数据结构
                        var field = new Characteristic(GuidGenerator.Create(), item.JsonData, entity.Id, item.HeaderId.Value);
                        newFields.Add(field); 
                    }
                }
                if (newFields.Count>0)
                {
                    await _characteristicRepository.InsertManyAsync(newFields);
                } 
            } 
            */
            await CapPublishToTemperatureAsync(entity, intakeEntities);
            return new ResponseBase<Guid>(EStatusCode.COK, entity.Id);
        }

        #endregion

        /// <summary>
        /// 移除指定的护理单记录(支持多个)
        /// </summary>
        /// <param name="ids">护理单记录Id集合</param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<ResponseBase<bool>> RemoveNursingRecordAsync(List<Guid> ids)
        {
            var nursingRecords = await (await _nursingRecordRepository.GetQueryableAsync()).Where(w => ids.Contains(w.Id) && (w.NurseCode == CurrentUser.UserName)).ToListAsync();
            var nursingRecordIds = nursingRecords.Select(x => x.Id);

            List<Intake> intakes = await (await _intakeRepository.GetQueryableAsync()).Where(x => nursingRecordIds.Contains(x.NursingRecordId)).ToListAsync();
            if (intakes.Any(x => x.RecipeExecId != Guid.Empty))
            {
                Oh.Error("护理记录存在医嘱执行记录不准删除");
            }

            List<SpecialNursingRecord> specialNursings = await (await _specialNursingRecordRepository.GetQueryableAsync()).Where(x => nursingRecordIds.Contains(x.NursingRecordId)).ToListAsync();
            if (specialNursings.Any())
            {
                Oh.Error("护理记录存在导管和皮肤记录不准删除");
            }

            if (nursingRecordIds.Any())
            {
                await _intakeRepository.DeleteAsync(x => nursingRecordIds.Contains(x.NursingRecordId));
            }

            if (nursingRecords.Any()) await _nursingRecordRepository.DeleteManyAsync(nursingRecords);

            await _capPublisher.PublishAsync("deletetemperature.reportservice.to.nursingservice", nursingRecords.Select(x => x.Id));
            return new ResponseBase<bool>(EStatusCode.COK, true);
        }

        /// <summary>
        /// 保存特殊护理记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseBase<Guid>> ModifyCharacteristicAsync(CharacteristicDto model)
        {
            var entity =
                await (await _characteristicRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.NursingRecordId == model.NursingRecordId);
            if (entity == null)
            {
                var newEntity = new Characteristic(GuidGenerator.Create(), model.JsonData, model.NursingRecordId, null);
                var retEntity = await _characteristicRepository.InsertAsync(newEntity);
                return new ResponseBase<Guid>(EStatusCode.COK, retEntity.Id);
            }
            else
            {
                entity.Update(model.JsonData);
                _ = await _characteristicRepository.UpdateAsync(entity);
                return new ResponseBase<Guid>(EStatusCode.COK, entity.Id);
            }
        }

        /// <summary>
        /// 获取回填特殊护理的记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseBase<BackfillSpecialCareDto>> BackfillSpecialCareAsync(SpecialCareDto model)
        {
            BackfillSpecialCareDto data = new BackfillSpecialCareDto();

            var headers = new List<Guid>();
            var notinheaders = new List<Guid>();

            var dfFileds = new List<Guid>();
            var df = await (await _dynamicFieldRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.DynamicFieldId);
            if (df != null)
            {
                if (df.Field1.HasValue) dfFileds.Add(df.Field1.Value);
                if (df.Field2.HasValue) dfFileds.Add(df.Field2.Value);
                if (df.Field3.HasValue) dfFileds.Add(df.Field3.Value);
                if (df.Field4.HasValue) dfFileds.Add(df.Field4.Value);
                if (df.Field5.HasValue) dfFileds.Add(df.Field5.Value);
                if (df.Field6.HasValue) dfFileds.Add(df.Field6.Value);
                if (df.Field7.HasValue) dfFileds.Add(df.Field7.Value);
                if (df.Field8.HasValue) dfFileds.Add(df.Field8.Value);
                if (df.Field9.HasValue) dfFileds.Add(df.Field9.Value);
            }

            if (model.IsDynamicSix)
            {
                if (model.Headers.Count > 0)
                {
                    headers = model.Headers;
                }
                else
                {
                    headers = dfFileds;
                }

                var dds = await (await _characteristicRepository.GetQueryableAsync())
                    .Where(w => w.NursingRecordId == model.NursingRecordId && w.HeaderId.HasValue &&
                                headers.Contains(w.HeaderId.Value))
                    .ToListAsync();
                if (dds.Count > 0)
                {
                    var map = ObjectMapper.Map<List<Characteristic>, List<CharacteristicDto>>(dds);
                    data.DynamicDataList = map;
                }
            }
            else
            {
                notinheaders = dfFileds;
                var characteristic = await (await _characteristicRepository.GetQueryableAsync()).FirstOrDefaultAsync(w =>
                    w.NursingRecordId == model.NursingRecordId && w.HeaderId == null);
                if (characteristic != null)
                {
                    var map2 = ObjectMapper.Map<Characteristic, CharacteristicDto>(characteristic);
                    data.Characteristic = map2;
                }
            }

            var inputSettings =
                await _nursingSettingRepository.GetAllSettingsAsync(model.IsDynamicSix, headers, notinheaders);
            if (inputSettings.Any())
            {
                List<NursingSettingDto> ret_data = new List<NursingSettingDto>();
                var map = ObjectMapper.Map<List<NursingSetting>, List<NursingSettingDto>>(inputSettings);

                foreach (var item in map)
                {
                    if (item.Headers.Count > 0)
                    {
                        foreach (var header in item.Headers)
                        {
                            header.Items = header.Items.OrderByDescending(o => o.Sort).ToList();
                        }

                        ret_data.Add(item);
                    }
                }

                data.InputSettings = ret_data.OrderByDescending(o => o.Sort).ToList();
            }

            return new ResponseBase<BackfillSpecialCareDto>(EStatusCode.COK, data);
        }

        /// <summary>
        /// 打印多个护理单
        /// </summary>
        /// <param name="model">护理记录单请求参数</param>
        /// <returns></returns> 
        public async Task<ResponseBase<List<NursingDocumentDto>>> PrintMoreAsync(PrintRequestDto model)
        {
            List<NursingDocumentDto> ret = new List<NursingDocumentDto>();
            NursingDocument nursingDocument = await (await _nursingDocumentRepository.GetQueryableAsync())
                .Include(i => i.DynamicFields).OrderByDescending(o => o.CreationTime)
                .FirstOrDefaultAsync(w => w.PI_ID == model.PI_ID);

            if (nursingDocument == null) return new ResponseBase<List<NursingDocumentDto>>(EStatusCode.CNULL, "查不到当前患者的护理记录单");

            var documentModel = ObjectMapper.Map<NursingDocument, NursingDocumentDto>(nursingDocument); //同一个用户护理单只有一个 

            var dynamicFields = nursingDocument
                .DynamicFields
                .WhereIf(model.SheetId.Count > 0, w => model.SheetId.Contains(w.Id))
                .ToList(); //查询需要合并打印的所有动态项分页

            if (dynamicFields.Count > 0)
            {
                var criticalIllness = await (await _criticalIllnessRepository.GetQueryableAsync())
                    .Where(w => w.NursingDocumentId == nursingDocument.Id)
                    .OrderByDescending(o => o.CreationTime)
                    .ToListAsync();

                var criticalIllnessList = new List<CriticalIllnessDto>();

                if (criticalIllness.Count > 0)
                {
                    criticalIllnessList =
                        ObjectMapper.Map<List<CriticalIllness>, List<CriticalIllnessDto>>(criticalIllness);
                }

                List<NursingRecord> nursingRecords = await _nursingRecordRepository.GetPrintMoreRecordListAsync(nursingDocument.Id,
                    model.Begintime, model.Endtime);
                if (nursingRecords.Count == 0)
                {
                    ret.Add(documentModel);
                    return new ResponseBase<List<NursingDocumentDto>>(EStatusCode.COK, ret);
                }

                List<NursingRecordDto> nursingRecordDtos = ObjectMapper.Map<List<NursingRecord>, List<NursingRecordDto>>(nursingRecords);
                foreach (NursingRecordDto nursingRecordDto in nursingRecordDtos)
                {
                    if (float.TryParse(nursingRecordDto.T, out float t))
                    {
                        nursingRecordDto.T = t.ToString("#.0");
                    }

                    IEnumerable<string> recipeNos = nursingRecordDto.IntakeDtos.Where(x => !string.IsNullOrEmpty(x.RecipeNo)).Select(x => x.RecipeNo).Distinct();
                    foreach (string recipeNo in recipeNos)
                    {
                        List<IntakeDto> intakes = nursingRecordDto.IntakeDtos.Where(x => x.RecipeNo == recipeNo).ToList();
                        if (intakes.Count() < 2) continue;
                        for (int i = 0; i < intakes.Count(); i++)
                        {
                            if (i == 0) continue;
                            intakes[i].InputMode = string.Empty;
                        }

                        int count = intakes.Select(x => x.UnitCode).Distinct().Count();
                        if (count > 1)
                        {
                            foreach (IntakeDto item in intakes)
                            {
                                item.ContentUnit = $"({item.RecipeQty}{item.UnitCode})";
                            }
                        }
                    }

                    nursingRecordDto.IntakeDtos = nursingRecordDto.IntakeDtos.OrderBy(x => x.RecipeNo).ToList();
                    nursingRecordDto.SpecialNursings = nursingRecordDto.SpecialNursings.OrderBy(x => x.CreationTime).ToList();
                }
                var dfmaps = ObjectMapper.Map<List<DynamicField>, List<DynamicFieldDto>>(dynamicFields);

                foreach (var dfmap in dfmaps)
                {
                    var data = CloneUtil.CloneJson(documentModel);
                    //二、三级查房内容
                    var wardRounds = await (await _wardRoundRepository.GetQueryableAsync())
                        .Where(w => w.NursingDocumentId == nursingDocument.Id)
                        .Where(w => w.SheetIndex == dfmap.SheetIndex)
                        .OrderBy(o => o.SheetIndex)
                        .ToListAsync();

                    if (wardRounds.Count > 0)
                    {
                        var maps = ObjectMapper.Map<List<WardRound>, List<WardRoundDto>>(wardRounds);
                        data.WardRounds = maps;
                    }
                    else
                    {
                        data.WardRounds = new List<WardRoundDto>() { new WardRoundDto(), new WardRoundDto() };
                    }

                    //添加出入量统计项
                    if (nursingRecordDtos.Count > 0)
                    {
                        //添加出入量统计项
                        var statisticsList = await _intakeStatisticsRepository.GetIntakeStatisticsListAsync(nursingDocument.Id, model.Begintime,
                        model.Endtime, dfmap.SheetIndex);
                        foreach (var item in statisticsList)
                        {
                            nursingRecordDtos.Add(new NursingRecordDto()
                            {
                                IsStatistics = true,
                                InIntakesTotal = item.InIntakesTotal,
                                OutIntakesTotal = item.OutIntakesTotal,
                                IntakeStatisticsId = item.Id,
                                Begintime = item.Begintime,
                                Endtime = item.Endtime,
                                RecordTime = item.Endtime,
                                SheetIndex = dfmap.SheetIndex
                            });
                        }
                    }
                    var records = nursingRecordDtos.Where(w => w.SheetIndex == dfmap.SheetIndex).OrderBy(o => o.RecordTime).ThenBy(o => o.IsStatistics)
                        .ToList();
                    data.InIntakesTotal = records.Select(p => p.InIntakes.Sum(s => Convert.ToDecimal(s.Quantity))).Sum().ToString();
                    data.OutIntakesTotal = records.Select(p => p.OutIntakes.Sum(s => Convert.ToDecimal(s.Quantity))).Sum().ToString();
                    if (records.Count > 0)
                    {
                        data.CriticalIllnessList = criticalIllnessList;
                        data.NursingRecords = records;
                        await SetFiledNameAsync(dfmap);
                        data.DynamicField = dfmap;
                        ret.Add(data);
                    }

                }

                return new ResponseBase<List<NursingDocumentDto>>(EStatusCode.COK, ret);
            }

            return new ResponseBase<List<NursingDocumentDto>>(EStatusCode.CNULL, "找不到当前患者的护理记录单");
        }


        #region 全景视图-生命体征列表，出入量列表

        /// <summary>
        /// 全景视图-生命体征列表
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<NursingRecordDto>> GetAllViewVitalSignsListAsync(Guid PID, DateTime? StartTime, DateTime? EndTime, CancellationToken cancellationToken = default)
        {

            try
            {
                var nursingDocument = await (await this._nursingDocumentRepository.GetQueryableAsync()).AsNoTracking().Where(w => w.PI_ID == PID).ToListAsync();
                List<NursingRecord> nursingRecordList = new List<NursingRecord>();
                foreach (var doc in nursingDocument)
                {
                    var recordList = await (await this._nursingRecordRepository.GetQueryableAsync()).AsNoTracking().Where(w => w.NursingDocumentId == doc.Id)
                    .WhereIf(StartTime.HasValue, w => w.RecordTime >= Convert.ToDateTime(StartTime))
                    .WhereIf(EndTime.HasValue, w => w.RecordTime <= Convert.ToDateTime(EndTime))
                    .OrderBy(p => p.RecordTime).ToListAsync();

                    nursingRecordList.AddRange(recordList);
                }

                nursingRecordList = nursingRecordList.OrderBy(a => a.RecordTime).ToList();

                var list = ObjectMapper.Map<List<NursingRecord>, List<NursingRecordDto>>(nursingRecordList);

                return list;

                //_log.LogInformation("Get admissionRecord by id success");
                //return RespUtil.Ok<List<NursingRecordDto>>(data: list);

            }
            catch (Exception ex)
            {
                return null;
                //_log.LogError("Get admissionRecord by id error.ErrorMsg:{Msg}", e);
                //return RespUtil.InternalError<List<NursingRecordDto>>(extra: ex.Message);
            }
        }


        /// <summary>
        /// 全景视图-出入量列表
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<IntakeDto>> GetAllViewInOutPutListAsync(Guid PID, DateTime? StartTime, DateTime? EndTime, CancellationToken cancellationToken = default)
        {

            try
            {
                var nursingDocument = await (await this._nursingDocumentRepository.GetQueryableAsync()).AsNoTracking().Where(w => w.PI_ID == PID).ToListAsync();
                List<IntakeDto> resultList = new List<IntakeDto>();
                foreach (var doc in nursingDocument)
                {
                    var recordList = await (await this._nursingRecordRepository.GetQueryableAsync()).AsNoTracking().Where(w => w.NursingDocumentId == doc.Id)
                    .WhereIf(StartTime.HasValue, w => w.RecordTime >= Convert.ToDateTime(StartTime))
                    .WhereIf(EndTime.HasValue, w => w.RecordTime <= Convert.ToDateTime(EndTime))
                    .OrderBy(p => p.RecordTime).ToListAsync();

                    foreach (var record in recordList)
                    {
                        var inTakeList = await (await _intakeRepository.GetQueryableAsync()).AsNoTracking().Where(w => w.NursingRecordId == record.Id)
                            .Select(p => new IntakeDto()
                            {
                                Id = p.Id,
                                IntakeType = p.IntakeType,
                                Code = p.Code,
                                InputMode = p.InputMode,
                                Content = p.Content,
                                Quantity = p.Quantity,
                                UnitCode = p.UnitCode,
                                Unit = p.Unit,
                                TraitsCode = p.TraitsCode,
                                Traits = p.Traits,
                                NursingRecordId = p.NursingRecordId,
                                RecordTime = record.RecordTime
                            }).ToListAsync();
                        resultList.AddRange(inTakeList);
                    }
                    //nursingRecordList.AddRange(recordList);
                }

                resultList = resultList.OrderBy(a => a.RecordTime).ToList();

                return resultList;

                //_log.LogInformation("Get admissionRecord by id success");
                //return RespUtil.Ok<List<NursingRecordDto>>(data: list);

            }
            catch (Exception ex)
            {
                return null;
                //_log.LogError("Get admissionRecord by id error.ErrorMsg:{Msg}", e);
                //return RespUtil.InternalError<List<NursingRecordDto>>(extra: ex.Message);
            }
        }

        /// <summary>
        /// 批量核对护理单记录信息
        /// </summary>
        /// <param name="nursingRecordCollatorDto"></param>
        /// <returns></returns>
        public async Task<ResponseBase<bool>> BulkCheckNursingRecordAsync(NursingRecordCollatorDto nursingRecordCollatorDto)
        {
            if (!nursingRecordCollatorDto.Ids.Any())
            {
                return new ResponseBase<bool>(EStatusCode.CFail, "请勾选护理记录单后核对");
            }
            if (string.IsNullOrWhiteSpace(nursingRecordCollatorDto.CollatorCode))
            {
                return new ResponseBase<bool>(EStatusCode.CFail, "核对人不存在");
            }

            string signature = await _hospitalClientAppService.QueryStampBaseAsync(nursingRecordCollatorDto.CollatorCode);
            List<NursingRecord> nursingRecords = await (await _nursingRecordRepository.GetQueryableAsync()).Where(w => nursingRecordCollatorDto.Ids.Contains(w.Id)).ToListAsync();

            if (nursingRecords.Any())
            {
                foreach (var item in nursingRecords)
                {
                    if (!string.IsNullOrWhiteSpace(item.CollatorCode))
                    {
                        if (item.CollatorCode == nursingRecordCollatorDto.CollatorCode)
                        {
                            item.Collator = null;
                            item.CollatorCode = null;
                            item.CollatorImage = null;
                        }
                        else
                        {
                            return new ResponseBase<bool>(EStatusCode.CFail, item.CollatorCode + "已核对");
                        }

                    }
                    else
                    {
                        item.Collator = nursingRecordCollatorDto.Collator;
                        item.CollatorCode = nursingRecordCollatorDto.CollatorCode;
                        item.CollatorImage = signature;
                    }

                }
                await _nursingRecordRepository.UpdateManyAsync(nursingRecords);
            };
            return new ResponseBase<bool>(EStatusCode.COK, true);
        }

        #endregion


        /// <summary>
        /// 查房签名
        /// </summary>
        /// <param name="model">查房签名请求参数</param>
        /// <returns></returns> 
        public async Task<ResponseBase<Guid>> ModifyWardRoundSignAsync(WardRoundDto model)
        {
            var entity =
               await (await _wardRoundRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.NursingDocumentId == model.NursingDocumentId && w.SheetIndex == model.SheetIndex && w.Level == model.Level);
            if (entity == null)
            {
                var newEntity = ObjectMapper.Map<WardRoundDto, WardRound>(model);
                var retEntity = await _wardRoundRepository.InsertAsync(newEntity);
                return new ResponseBase<Guid>(EStatusCode.COK, retEntity.Id);
            }
            else
            {
                entity.Level = model.Level;
                entity.Signature = JsonConvert.SerializeObject(model.Signature);
                entity.SheetIndex = model.SheetIndex;
                entity.NursingDocumentId = model.NursingDocumentId;

                _ = await _wardRoundRepository.UpdateAsync(entity);
                return new ResponseBase<Guid>(EStatusCode.COK, entity.Id);
            }
        }


        /// <summary>
        /// 添加出入量统计
        /// </summary>
        /// <param name="eto"></param>
        public async Task<ResponseBase<bool>> AddIntakeStatisticsAsync(IntakeStatisticsDto eto)
        {
            var entity = await (await _intakeStatisticsRepository.GetQueryableAsync()).FirstOrDefaultAsync(p => p.NursingDocumentId == eto.NursingDocumentId && p.SheetIndex == (eto.SheetIndex ?? 0) &&
                   p.Begintime == eto.Begintime && p.Endtime == eto.Endtime);
            var query = await _nursingRecordRepository.GetRecordListAsync(eto.NursingDocumentId, eto.Begintime,
            eto.Endtime, eto.SheetIndex ?? 0);
            if (query.Count > 0)
            {
                var maps = ObjectMapper.Map<List<NursingRecord>, List<NursingRecordDto>>(query);

                if (entity != null)
                {
                    entity.InIntakesTotal = maps.Where(i => i.RecordTime >= eto.Begintime && i.RecordTime < eto.Endtime).Select(p => p.InIntakes.Sum(s => Convert.ToDecimal(s.Quantity))).Sum().ToString();
                    entity.OutIntakesTotal = maps.Where(i => i.RecordTime >= eto.Begintime && i.RecordTime < eto.Endtime).Select(p => p.OutIntakes.Sum(s => Convert.ToDecimal(s.Quantity))).Sum().ToString();
                    _ = await _intakeStatisticsRepository.UpdateAsync(entity);
                }
                else
                {
                    entity = new IntakeStatistics();
                    entity.NursingDocumentId = eto.NursingDocumentId;
                    entity.SheetIndex = eto.SheetIndex;
                    entity.Begintime = eto.Begintime;
                    entity.Endtime = eto.Endtime;
                    entity.InIntakesTotal = maps.Where(i => i.RecordTime >= eto.Begintime && i.RecordTime < eto.Endtime).Select(p => p.InIntakes.Sum(s => Convert.ToDecimal(s.Quantity))).Sum().ToString();
                    entity.OutIntakesTotal = maps.Where(i => i.RecordTime >= eto.Begintime && i.RecordTime < eto.Endtime).Select(p => p.OutIntakes.Sum(s => Convert.ToDecimal(s.Quantity))).Sum().ToString();
                    _ = await _intakeStatisticsRepository.InsertAsync(entity);
                }
                return new ResponseBase<bool>(EStatusCode.COK, true);
            }
            return new ResponseBase<bool>(EStatusCode.COK, false, "未找到该时间段内的护理记录信息");
        }

        /// <summary>
        /// 删除出入量统计
        /// </summary>
        /// <param name="intakeStatisticsId">出入量统计ID</param>
        public async Task<ResponseBase<bool>> RemoveIntakeStatisticsAsync(Guid intakeStatisticsId)
        {
            await _intakeStatisticsRepository.DeleteAsync(w => w.Id == intakeStatisticsId);
            return new ResponseBase<bool>(EStatusCode.COK, true);
        }

        /// <summary>
        /// 先删除统计再新增
        /// </summary>
        /// <param name="nursingDocumentId"></param>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        private async Task GetUpdateIntakeStatisticsAsync(Guid nursingDocumentId, int sheetIndex)
        {
            var intakeStatistics = await (await _intakeStatisticsRepository.GetQueryableAsync()).Where(p => p.NursingDocumentId == nursingDocumentId && p.SheetIndex == sheetIndex).ToListAsync();
            var timeSlot = intakeStatistics.Select(p => { return new IntakeStatistics() { Begintime = p.Begintime, Endtime = p.Endtime }; });
            await _intakeStatisticsRepository.DeleteManyAsync(intakeStatistics);
            var newIntakeStatistics = new List<IntakeStatistics>();
            var query = await _nursingRecordRepository.GetRecordListAsync(nursingDocumentId, null, null, sheetIndex);
            foreach (var item in timeSlot)
            {
                var maps = ObjectMapper.Map<List<NursingRecord>, List<NursingRecordDto>>(query);
                newIntakeStatistics.Add(new IntakeStatistics()
                {
                    NursingDocumentId = nursingDocumentId,
                    SheetIndex = sheetIndex,
                    Begintime = item.Begintime,
                    Endtime = item.Endtime,
                    InIntakesTotal = maps.Where(i => i.RecordTime >= item.Begintime && i.RecordTime < item.Endtime).Select(p => p.InIntakes.Sum(s => Convert.ToDecimal(s.Quantity))).Sum().ToString(),
                    OutIntakesTotal = maps.Where(i => i.RecordTime >= item.Begintime && i.RecordTime < item.Endtime).Select(p => p.OutIntakes.Sum(s => Convert.ToDecimal(s.Quantity))).Sum().ToString()
                });
            }
            await _intakeStatisticsRepository.InsertManyAsync(newIntakeStatistics);
        }
    }
}
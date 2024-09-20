using Hangfire.Annotations;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Uow;
using YiJian.Common;
using YiJian.Documents.Dto;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.OwnMedicines.Contracts;
using YiJian.OwnMedicines.Entities;
using YiJian.OwnMediciness;
using YiJian.OwnMediciness.Dto;
using YiJian.Recipe;

namespace YiJian.OwnMedicines
{
    /// <summary>
    /// 自备药
    /// </summary>
    [Authorize]
    public class OwnMedicineAppService : RecipeAppService, IOwnMedicineAppService
    {
        private readonly IOwnMedicineRepository _ownMedicineRepository;
        private readonly PatientAppService _patientAppService;

        /// <summary>
        /// 自备药
        /// </summary>
        /// <param name="ownMedicineRepository"></param>
        /// <param name="patientAppService"></param>
        public OwnMedicineAppService(IOwnMedicineRepository ownMedicineRepository, PatientAppService patientAppService)
        {
            _ownMedicineRepository = ownMedicineRepository;
            _patientAppService = patientAppService;
        }

        /// <summary>
        /// 查询患者所有的自备药
        /// </summary>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<List<OwnMedicineDto>> GetMyAllAsync([NotNull] Guid piid)
        {
            var data = await _ownMedicineRepository.GetMyAllAsync(piid);
            return ObjectMapper.Map<List<OwnMedicine>, List<OwnMedicineDto>>(data);
        }

        /// <summary>
        /// 更新我的自备药
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<bool> ModifyOwnMedicinesAsync([NotNull] ModifyOwnMedicineDto model)
        {
            var entities = ObjectMapper.Map<List<OwnMedicineDto>, List<OwnMedicine>>(model.OwnMedicineDtos);
            entities.ForEach(x =>
            {
                x.PlatformType = model.PlatformType;
                x.PIID = model.PIID;
                x.PatientId = model.PatientId;
                x.PatientName = model.PatientName;
            });

            var patientInfo = await _patientAppService.GetPatientInfoAsync(model.PIID);
            if (patientInfo == null)
            {
                Oh.Error(message: "未查询到患者信息");
            }

            if (patientInfo.VisitStatus == EVisitStatus.已就诊 || patientInfo.VisitStatus == EVisitStatus.过号)
            {
                Oh.Error(message: "已经就诊或过号的患者，不能操作自备药");
            }

            // 删除原有的自备药信息，全量插入新的自备药信息
            var oldOwnMedicines = await this._ownMedicineRepository.GetListAsync(x => x.PIID == model.PIID);
            await _ownMedicineRepository.DeleteManyAsync(oldOwnMedicines);
            await _ownMedicineRepository.InsertManyAsync(entities);
            return true;
        }

        // /// <summary>
        // /// 移除选中的一批自备药
        // /// </summary>
        // /// <param name="ids"></param>
        // /// <returns></returns>
        // [UnitOfWork]
        // public async Task<bool> RemoveOwnMedicinesAsync([NotNull] List<int> ids)
        // {
        //     await _ownMedicineRepository.RemoveOwnMedicinesAsync(ids);
        //     return true;
        // }
    }
}

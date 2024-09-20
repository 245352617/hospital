using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Handover
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Volo.Abp.Application.Dtos;
    using Volo.Abp.Application.Services;
    using Volo.Abp.DependencyInjection;

    /// <summary>
    /// 医生交班患者API
    /// </summary>
    public class DoctorPatientsAppService : HandoverAppService, IDoctorPatientsAppService
    {
        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="doctorPatientsRepository"></param>
        /// <param name="doctorPatientsManager"></param>
        public DoctorPatientsAppService(IDoctorPatientsRepository doctorPatientsRepository,
            DoctorPatientsManager doctorPatientsManager)
        {
            _doctorPatientsRepository = doctorPatientsRepository;
            _doctorPatientsManager = doctorPatientsManager;
        }

        #endregion

        /// <summary>
        /// 保存交班患者
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<Guid> SaveHandoverPatientAsync(DoctorPatientsUpdate dto)
        {
            if (dto.Id == Guid.Empty)
            {
                var result = await _doctorPatientsManager.CreateAsync(dto.DoctorHandoverId, dto.PI_ID, dto.PatientId, dto.VisitNo,
                    dto.PatientName, dto.Sex, dto.SexName, dto.Age, dto.TriageLevelName, dto.DiagnoseName,
                    dto.Bed,
                    dto.Content,
                    dto.Test, dto.Inspect, dto.Emr, dto.InOutVolume, dto.VitalSigns, dto.Medicine, dto.Status);
                return result.Id;
            }
            var patient = await _doctorPatientsRepository.FirstOrDefaultAsync(x => x.Id == dto.Id);
            patient.Modify(dto.PI_ID, dto.PatientId, dto.VisitNo,
                dto.PatientName, dto.Sex, dto.SexName, dto.Age, dto.TriageLevelName, dto.DiagnoseName,
                dto.Bed,
                dto.Content,
                dto.Test, dto.Inspect, dto.Emr, dto.InOutVolume, dto.VitalSigns, dto.Medicine, dto.Status);
            await _doctorPatientsRepository.UpdateAsync(patient);
            return patient.Id;
        }

        #region Get

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DoctorPatientsData> GetHandoverPatientAsync(Guid id)
        {
            var doctorPatients = await _doctorPatientsRepository.GetAsync(id);

            return ObjectMapper.Map<DoctorPatients, DoctorPatientsData>(doctorPatients);
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteHandoverPatientAsync(Guid id)
        {
            if (!await _doctorPatientsRepository.AnyAsync(a => a.Id == id))
            {
                throw new BusinessException(message: "数据不存在,无法删除");
            }

            await _doctorPatientsRepository.DeleteAsync(id);
        }

        #endregion


        #region Private Fields

        private readonly IDoctorPatientsRepository _doctorPatientsRepository;
        private readonly DoctorPatientsManager _doctorPatientsManager;

        #endregion
    }
}
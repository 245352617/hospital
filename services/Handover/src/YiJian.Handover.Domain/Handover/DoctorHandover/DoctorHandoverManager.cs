using System.Collections.Generic;

namespace YiJian.Handover
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Domain.Services;
    using Volo.Abp.Guids;

    public class DoctorHandoverManager : DomainService
    {
        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="doctorHandoverRepository"></param>
        /// <param name="guidGenerator"></param>
        public DoctorHandoverManager(IDoctorHandoverRepository doctorHandoverRepository, IGuidGenerator guidGenerator)
        {
            _doctorHandoverRepository = doctorHandoverRepository;
            _guidGenerator = guidGenerator;
        }

        #endregion

        #region Create

        public async Task<DoctorHandover> CreateAsync(string handoverDate, DateTime handoverTime,
            string handoverDoctorCode, string handoverDoctorName, Guid shiftSettingId, string shiftSettingName,
            string otherMatters, DoctorPatientStatistic patientStatistics, List<DoctorPatients> doctorPatients,
            int status)
        {
            
           var doctorHandover = new DoctorHandover(_guidGenerator.Create(), handoverDate, handoverTime, handoverDoctorCode,
                handoverDoctorName, shiftSettingId, shiftSettingName, otherMatters, patientStatistics, doctorPatients,status);

            return await _doctorHandoverRepository.InsertAsync(doctorHandover);
        }

        #endregion

        #region Private Fields

        private readonly IDoctorHandoverRepository _doctorHandoverRepository;
        private readonly IGuidGenerator _guidGenerator;

        #endregion
    }
}
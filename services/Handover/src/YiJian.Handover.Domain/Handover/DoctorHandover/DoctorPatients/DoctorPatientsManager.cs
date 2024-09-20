using Microsoft.EntityFrameworkCore;

namespace YiJian.Handover
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Domain.Services;
    using Volo.Abp.Guids;

    public class DoctorPatientsManager : DomainService
    {         
        #region constructor
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="doctorPatientsRepository"></param>
        /// <param name="guidGenerator"></param>
        public DoctorPatientsManager(IDoctorPatientsRepository doctorPatientsRepository, IGuidGenerator guidGenerator)
        {
            _doctorPatientsRepository = doctorPatientsRepository;
            _guidGenerator = guidGenerator;
        }        
        #endregion

        #region Create

        public async Task<DoctorPatients> CreateAsync(Guid doctorHandover, Guid pIID, string patientId, int? visitNo, string patientName, string sex,string sexName, string age, string triageLevel, string diagnose, string bed, string content, string test, string inspect, string emr, string inOutVolume, string vitalSigns, string medicine,bool status) 
        {
            var doctorPatients = await _doctorPatientsRepository.FirstOrDefaultAsync(d => d.DoctorHandoverId == doctorHandover);
            
            if (doctorPatients != null)
            {
                throw new DoctorPatientsAlreadyExistsException("患者不存在");
            }

            doctorPatients = new DoctorPatients(_guidGenerator.Create(), doctorHandover, pIID, patientId, visitNo, patientName, sex, sexName,age, triageLevel, diagnose, bed, content, test, inspect, emr, inOutVolume, vitalSigns, medicine,status);

            return await _doctorPatientsRepository.InsertAsync(doctorPatients);
        }
        #endregion

        #region Private Fields
        private readonly IDoctorPatientsRepository _doctorPatientsRepository;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

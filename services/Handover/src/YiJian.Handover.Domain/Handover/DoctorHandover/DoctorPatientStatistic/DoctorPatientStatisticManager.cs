namespace YiJian.Handover
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Domain.Services;
    using Volo.Abp.Guids;

    public class DoctorPatientStatisticManager : DomainService
    {         
        #region constructor
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="doctorPatientStatisticRepository"></param>
        /// <param name="guidGenerator"></param>
        public DoctorPatientStatisticManager(IDoctorPatientStatisticRepository doctorPatientStatisticRepository, IGuidGenerator guidGenerator)
        {
            _doctorPatientStatisticRepository = doctorPatientStatisticRepository;
            _guidGenerator = guidGenerator;
        }        
        #endregion

        #region Create

        public async Task<DoctorPatientStatistic> CreateAsync(Guid doctorHandoverId, int total, int classI, int classII, int classIII, int classIV, int preOperation, int existingDisease, int outDept, int rescue, int visit, int death, int cPR, int admission) 
        {
            var doctorPatientStatistic = await _doctorPatientStatisticRepository.FirstOrDefaultAsync(d => d.DoctorHandoverId == doctorHandoverId);
            
            if (doctorPatientStatistic != null)
            {
                throw new DoctorPatientStatisticAlreadyExistsException("数据不存在");
            }

            doctorPatientStatistic = new DoctorPatientStatistic(_guidGenerator.Create(), doctorHandoverId, total, classI, classII, classIII, classIV, preOperation, existingDisease, outDept, rescue, visit, death, cPR, admission);

            return await _doctorPatientStatisticRepository.InsertAsync(doctorPatientStatistic);
        }
        #endregion

        #region Private Fields
        private readonly IDoctorPatientStatisticRepository _doctorPatientStatisticRepository;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

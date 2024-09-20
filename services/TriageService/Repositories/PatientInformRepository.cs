using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分诊患者信息仓储
    /// </summary>
    public class PatientInformRepository : BaseRepository<PreHospitalTriageDbContext, InformPatInfo, Guid>,
        IPatientInformRepository
    {
        private readonly ILogger<PatientInfoRepository> _log;
        private readonly ICurrentUser _currentUser;
        private readonly IConfiguration _configuration;

        public PatientInformRepository(IDbContextProvider<PreHospitalTriageDbContext> dbContextProvider,
            ILogger<PatientInfoRepository> log, ICurrentUser currentUser, IConfiguration configuration)
            : base(dbContextProvider)
        {
            _log = log;
            _currentUser = currentUser;
            _configuration = configuration;
        }


    }
}
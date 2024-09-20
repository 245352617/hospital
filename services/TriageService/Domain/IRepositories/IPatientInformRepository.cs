using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Domain.Repositories;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IPatientInformRepository : IRepository<InformPatInfo, Guid>, IBaseRepository<InformPatInfo, Guid>
    {

    }
}
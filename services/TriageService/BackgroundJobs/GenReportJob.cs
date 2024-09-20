using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class GenReportJob : ISingletonDependency
    {
        private readonly IReportAppService _reportAppService;

        public GenReportJob(IReportAppService reportAppService)
        {
            this._reportAppService = reportAppService;
        }

        public async Task Execute()
        {
            await this._reportAppService.GenHotMorningAndNightReport(null, null);
            await this._reportAppService.GenDeathCountReport(null);       
            await this._reportAppService.GenFeverCountReport(null);
            await this._reportAppService.GenRescueAndViewReport(null);
            await this._reportAppService.GenTriageCountReport(null);
            
        }
    }
}

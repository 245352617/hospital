using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Uow;
using YiJian.Cases.Contracts;
using YiJian.Cases.Entities;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;
using YiJian.Recipe;

namespace YiJian.Cases
{
    /// <summary>
    /// 电子病历回来的病历信息
    /// </summary>
    public class CasesAppService : RecipeAppService, ICasesAppService, ICapSubscribe
    {
        private readonly ILogger<CasesAppService> _logger;
        private readonly IPatientCaseRepository _patientCaseRepository;

        /// <summary>
        /// 电子病历回来的病历信息
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="patientCaseRepository"></param>
        public CasesAppService(ILogger<CasesAppService> logger,
            IPatientCaseRepository patientCaseRepository)
        {
            _logger = logger;
            _patientCaseRepository = patientCaseRepository;
        }

        /// <summary>
        /// 同步电子病历病历信息
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("emr.patientInfo.case")]
        public async Task SyncEmrCaseAsync(PushEmrDataEto eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                _logger.LogInformation("同步电子病历病历信息收到的记录：" + Newtonsoft.Json.JsonConvert.SerializeObject(eto));
                var oldEntities = await (await _patientCaseRepository.GetQueryableAsync()).Where(w => w.Piid == eto.Piid).ToListAsync();
                var ids = oldEntities.Select(s => s.Id).ToList();
                await _patientCaseRepository.DeleteManyAsync(ids); //1.软删除久的记录 
                await _patientCaseRepository.InsertAsync(new PatientCase(eto.Piid, eto.PatientId, eto.PatientName, eto.Pastmedicalhistory, eto.Presentmedicalhistory, eto.Physicalexamination, eto.Narrationname));

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"同步电子病历病历信息异常:{ex.Message},请求参数：{JsonConvert.SerializeObject(eto)}");
                await uow.RollbackAsync();
                throw;
            }


        }


    }
}

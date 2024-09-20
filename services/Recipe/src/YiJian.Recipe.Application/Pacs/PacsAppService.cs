using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using WebApiClientCore.Attributes;
using YiJian.ECIS.Core.Utils;
using YiJian.Recipes.DoctorsAdvices.Contracts;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipe.Pacss
{
    /// <summary>
    /// 检查服务
    /// </summary>
    [AllowAnonymous]
    public class PacsAppService : ApplicationService
    {
        private readonly IDoctorsAdviceRepository _doctorsAdviceRepository;
        private readonly IPacsRepository _pacsRepository;
        private readonly ILogger<PacsAppService> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="doctorsAdviceRepository"></param>
        /// <param name="pacsRepository"></param>
        /// <param name="logger"></param>
        public PacsAppService(IDoctorsAdviceRepository doctorsAdviceRepository
            , IPacsRepository pacsRepository
            , ILogger<PacsAppService> logger)
        {
            _doctorsAdviceRepository = doctorsAdviceRepository;
            _pacsRepository = pacsRepository;
            _logger = logger;
        }

        /// <summary>
        /// 更新检查申请单状态（明天医网用）
        /// </summary>
        /// <param name="pacsUpdateStatusDtos"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdatePacsStatusAsync(List<PacsUpdateStatusDto> pacsUpdateStatusDtos)
        {
            if (pacsUpdateStatusDtos == null || !pacsUpdateStatusDtos.Any())
            {
                return false;
            }
            _logger.LogInformation("明天医网回传的数据:{0}", pacsUpdateStatusDtos.ToJsonString());

            IEnumerable<string> hisOrderNos = pacsUpdateStatusDtos.Select(x => x.Sqdh);
            List<DoctorsAdvice> doctorsAdvices = await _doctorsAdviceRepository.GetListAsync(x => hisOrderNos.Contains(x.HisOrderNo));

            IEnumerable<Guid> doctorsAdviceIds = doctorsAdvices.Select(x => x.Id);
            List<Pacs> pacss = await _pacsRepository.GetListAsync(x => doctorsAdviceIds.Contains(x.DoctorsAdviceId));

            foreach (Pacs pacs in pacss)
            {
                DoctorsAdvice doctorsAdvice = doctorsAdvices.FirstOrDefault(x => x.Id == pacs.DoctorsAdviceId);
                if (doctorsAdvice == null) continue;
                PacsUpdateStatusDto dto = pacsUpdateStatusDtos.FirstOrDefault(x => x.Sqdh == doctorsAdvice.HisOrderNo);
                if (dto == null) continue;

                pacs.PacsStatus = dto.Mode;
                pacs.ReservationPlace = dto.Address;
            }

            if (pacss.Any()) await _pacsRepository.UpdateManyAsync(pacss);
            return true;
        }
    }
}

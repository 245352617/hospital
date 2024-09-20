using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.Emrs.Dto;
using YiJian.Health.Report.NursingDocuments;
using YiJian.Health.Report.NursingDocuments.Contracts;

namespace YiJian.Health.Report.Emrs
{
    /// <summary>
    /// 电子病历相关的服务
    /// </summary>
    [Authorize]
    public class EmrAppService : ReportAppService, IEmrAppService
    {
        private readonly INursingDocumentRepository _nursingDocumentRepository;
        private readonly INursingRecordRepository _nursingRecordRepository;

        public EmrAppService(
            INursingDocumentRepository nursingDocumentRepository,
            INursingRecordRepository nursingRecordRepository)
        {
            _nursingDocumentRepository = nursingDocumentRepository;
            _nursingRecordRepository = nursingRecordRepository;
        }

        /// <summary>
        /// 获取生命体征记录信息集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseBase<List<EmrNursingRecordDto>>> NursingRecordsAsync(NursingRecordRequestDto model)
        {
            NursingDocument nursingDocument = await _nursingDocumentRepository.FirstOrDefaultAsync(w => w.PI_ID == model.PI_ID);

            if (nursingDocument == null) return new ResponseBase<List<EmrNursingRecordDto>>(EStatusCode.CNULL);
            List<NursingRecord> data = await _nursingRecordRepository.GetEmrNursingRecordsAsync(nursingDocument.Id, model.BeginDate, model.EndDate);
            var map = ObjectMapper.Map<List<NursingRecord>, List<EmrNursingRecordDto>>(data);
            return new ResponseBase<List<EmrNursingRecordDto>>(EStatusCode.COK, map);
        }

    }
}

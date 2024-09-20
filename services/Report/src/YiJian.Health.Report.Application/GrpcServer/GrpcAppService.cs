using Grpc.Core;
using HealthReportService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;
using YiJian.ECIS.Grpc;
using YiJian.Health.Report.NursingDocuments;
using YiJian.Health.Report.NursingDocuments.Contracts;

namespace YiJian.Health.Report.GrpcServer
{
    /// <summary>
    /// GRPC Service
    /// </summary>
    public class GrpcAppService : GrpcHealthReport.GrpcHealthReportBase, IGrpcAppService
    {
        private readonly IObjectMapper ObjectMapper;
        private readonly IUnitOfWorkManager UnitOfWorkManager;

        private readonly INursingDocumentRepository _nursingDocumentRepository;
        private readonly INursingRecordRepository _nursingRecordRepository;

        /// <summary>
        /// GRPC Service
        /// </summary> 
        public GrpcAppService(IObjectMapper objectMapper,
            IUnitOfWorkManager unitOfWorkManager,
            INursingDocumentRepository nursingDocumentRepository,
            INursingRecordRepository nursingRecordRepository)
        {
            this.ObjectMapper = objectMapper;
            this.UnitOfWorkManager = unitOfWorkManager;
            this._nursingDocumentRepository = nursingDocumentRepository;
            this._nursingRecordRepository = nursingRecordRepository;
        }

        /// <summary>
        /// 生命体征列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpGet]
        public override async Task<VitalSignsListResponse> GetVitalSignsList(GetVitalSignsListRequest request, ServerCallContext context)
        {
            using var uow = UnitOfWorkManager.Begin();

            try
            {
                List<NursingDocument> nursingDocument = await (await this._nursingDocumentRepository.GetQueryableAsync()).AsNoTracking()
                    .WhereIf(!string.IsNullOrEmpty(request.PID), w => w.PI_ID == new Guid(request.PID)).ToListAsync();
                var response = new VitalSignsListResponse();
                List<NursingRecord> allList = new List<NursingRecord>();
                foreach (var doc in nursingDocument)
                {
                    var recordList = await (await this._nursingRecordRepository.GetQueryableAsync()).AsNoTracking().Where(w => w.NursingDocumentId == doc.Id)
                    .WhereIf(!string.IsNullOrEmpty(request.StartTime), w => w.RecordTime >= Convert.ToDateTime(request.StartTime))
                    .WhereIf(!string.IsNullOrEmpty(request.EndTime), w => w.RecordTime <= Convert.ToDateTime(request.EndTime))
                    .OrderBy(p => p.RecordTime).ToListAsync();

                    allList.AddRange(recordList);
                }

                allList = allList.OrderBy(a => a.RecordTime).ToList();
                foreach (var item in allList)
                {
                    var responseRecord = ObjectMapper.Map<NursingRecord, GrpcVitalSignsModel>(item);
                    response.VitalSigns.Add(responseRecord);
                }

                await uow.CompleteAsync();

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await uow.RollbackAsync();
                throw;
            }
        }
    }
}

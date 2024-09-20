using EMRService;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;
using YiJian.ECIS.Grpc;
using YiJian.EMR.Writes.Contracts;
using YiJian.EMR.Writes.Entities;

namespace YiJian.EMR.GrpcServer
{
    public class GrpcAppService : GrpcEMR.GrpcEMRBase, IGrpcAppService
    {
        //protected IObjectMapper ObjectMapper => LazyServiceProvider.LazyGetService<IObjectMapper>((IServiceProvider provider) => provider.GetRequiredService<IObjectMapper>());
        //public IUnitOfWorkManager UnitOfWorkManager => LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>();

        //public AbpLazyServiceProvider LazyServiceProvider { get; }

        private IObjectMapper ObjectMapper;
        private IUnitOfWorkManager UnitOfWorkManager;

        private readonly IPatientEmrRepository _patientEmrRepository;


        public GrpcAppService(
            //AbpLazyServiceProvider lazyServiceProvider, 
            IPatientEmrRepository patientEmrRepository,
            IObjectMapper objectMapper,
            IUnitOfWorkManager unitOfWorkManager)
        {
            //this.LazyServiceProvider = lazyServiceProvider;
            this._patientEmrRepository = patientEmrRepository;
            this.ObjectMapper = objectMapper;
            this.UnitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 病历列表统计
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<PatientEmrListResponse> GetPatientEmrList(GetPatientEmrListRequest request, ServerCallContext context)
        {
            using var uow = UnitOfWorkManager.Begin();

            try
            {               
                var patientEmrList = await ( await _patientEmrRepository.GetQueryableAsync()).AsNoTracking()
                .WhereIf(!string.IsNullOrEmpty(request.PID), w => w.PI_ID == new Guid(request.PID))
                //.WhereIf(!Convert.IsDBNull(request.StartTime), w => w.CreationTime >= Convert.ToDateTime(request.StartTime))
                //.WhereIf(!Convert.IsDBNull(request.EndTime), w => w.CreationTime <= Convert.ToDateTime(request.EndTime))
                .WhereIf(!string.IsNullOrEmpty(request.StartTime), w => w.CreationTime >= Convert.ToDateTime(request.StartTime))
                .WhereIf(!string.IsNullOrEmpty(request.EndTime), w => w.CreationTime <= Convert.ToDateTime(request.EndTime))
                .OrderBy(o => o.CreationTime)
                .ToListAsync();

                var response = new PatientEmrListResponse();

                foreach (var patientEmr in patientEmrList)
                {
                    var responseRecipeExec = ObjectMapper.Map<PatientEmr, GrpcPatientEmrModel>(patientEmr);
                    response.PatientEmr.Add(responseRecipeExec);
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

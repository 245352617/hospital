using MasterDataService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// Grpc请求
    /// </summary>
    public class GrpcClient : EcisPatientAppService
    {
        private readonly GrpcMasterData.GrpcMasterDataClient _grpcMasterDataClient;

        public GrpcClient(GrpcMasterData.GrpcMasterDataClient grpcMasterDataClient)
        {
            _grpcMasterDataClient = grpcMasterDataClient;
        }

        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<DepartmentModel>> GetDepartmentsAsync()
        {
            DepartmentsResponse depts = await _grpcMasterDataClient.GetDepartmentsAsync(new GetDepartmentsRequest());

            return depts.Items.ToList();
        }

        /// <summary>
        /// 获取出科字典名称
        /// </summary>
        /// <param name="outDeptReason"></param>
        /// <returns></returns>
        public async Task<GetDictionariesResponse> GetOutDeptReason(string outDeptReason)
        {
            return await _grpcMasterDataClient.GetDictionariesAsync(new GetDictionariesRequest() { DictionariesTypeCode = "OutDeptReason", DictionariesCode = outDeptReason });
        }
    }
}

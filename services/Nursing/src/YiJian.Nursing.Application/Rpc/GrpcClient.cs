using MasterDataService;
using System.Collections.Generic;
using Volo.Abp;
using YiJian.Nursing;
using YiJian.Nursing.RecipeExecutes;
using YiJian.Nursing.Recipes;
using static MasterDataService.GrpcMasterData;

namespace YiJian.Rpc
{
    /// <summary>
    /// grpc调用客户端
    /// </summary>
    [RemoteService(false)]
    public class GrpcClient : NursingAppService
    {
        private readonly GrpcMasterDataClient _grpcMasterDataClient;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="grpcMasterDataClient"></param>
        public GrpcClient(GrpcMasterDataClient grpcMasterDataClient)
        {
            _grpcMasterDataClient = grpcMasterDataClient;
        }

        /// <summary>
        /// 获取分方途径配置
        /// </summary>
        /// <returns></returns>
        public List<SeparationUsages> GetSeparationUsages()
        {
            GetAllSeparationRequest allSeparationRequest = new GetAllSeparationRequest();
            AllSeparationResponse allSeparationResponse = _grpcMasterDataClient.GetSeparationsList(allSeparationRequest);
            List<SeparationUsages> separationUsagesList = new List<SeparationUsages>();
            foreach (SeparationResponse separationResponse in allSeparationResponse.Separations)
            {
                foreach (UsagesModel usagesModel in separationResponse.Usages)
                {
                    SeparationUsages separationUsages = new SeparationUsages();
                    separationUsages.Title = separationResponse.Title;
                    separationUsages.Code = separationResponse.Code;
                    separationUsages.UsageCode = usagesModel.UsageCode;
                    separationUsages.UsageName = usagesModel.UsageName;
                    separationUsagesList.Add(separationUsages);
                }
            }
            return separationUsagesList;
        }

        /// <summary>
        /// 获取频次信息
        /// </summary>
        /// <returns></returns>
        public List<Frequency> GetAllFrequencies()
        {
            GetAllFrequenciesRequest request = new GetAllFrequenciesRequest();
            FrequenciesResponse response = _grpcMasterDataClient.GetAllFrequencies(request);
            List<Frequency> list = new List<Frequency>();
            foreach (GrpcFrequencyModel fre in response.Frequencies)
            {
                Frequency pf = new Frequency()
                {
                    FrequencyCode = fre.FrequencyCode,
                    FrequencyName = fre.FrequencyName,
                    Unit = fre.Unit,
                    Times = fre.Times,
                    ExecuteDayTime = fre.ExecDayTimes
                };
                list.Add(pf);
            }

            return list;
        }

        /// <summary>
        /// 获取所有的医嘱类型配置
        /// </summary>
        /// <returns></returns>
        public List<NursingRecipeTypeDto> GetAllNursingRecipeTypes()
        {
            GetNursingRecipeTypesRequest request = new GetNursingRecipeTypesRequest();
            NursingRecipeTypesResponse response = _grpcMasterDataClient.GetNursingRecipeTypes(request);
            List<NursingRecipeTypeDto> nursingRecipeTypeDtos = new List<NursingRecipeTypeDto>();
            foreach (NursingRecipeTypeModel item in response.NursingRecipeTypes)
            {
                NursingRecipeTypeDto nursingRecipeTypeDto = new NursingRecipeTypeDto()
                {
                    TypeName = item.TypeName,
                    UsageCode = item.UsageCode,
                    UsageName = item.UsageName
                };
                nursingRecipeTypeDtos.Add(nursingRecipeTypeDto);
            }

            return nursingRecipeTypeDtos;
        }
    }
}

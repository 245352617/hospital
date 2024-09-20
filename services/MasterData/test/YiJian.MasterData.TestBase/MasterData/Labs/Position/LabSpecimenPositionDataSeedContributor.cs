namespace YiJian.MasterData.Labs.Position
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 检验标本采集部位 DataSeedContributor
    /// </summary>
    public class LabSpecimenPositionDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _labSpecimenPositionRepository.GetCountAsync() > 0)
			{
				return;
			}            
            /*
            // 标本编码
            string specimenCode = "";
            // 标本名称
            string specimenName = "";
            // 采集部位编码
            string positionCode = "";
            // 采集部位名称
            string positionName = "";
            // 排序号
            int indexNo = 0;
            // 拼音码
            string pyCode = "";
            // 
            bool isActive = true;

            var labSpecimenPosition = new LabSpecimenPosition(specimenCode,// 标本编码
                specimenName,   // 标本名称
                positionCode,   // 采集部位编码
                positionName,   // 采集部位名称
                indexNo,        // 排序号
                pyCode,         // 拼音码
                isActive
                );

			await _labSpecimenPositionRepository.InsertAsync(labSpecimenPosition); 
            */
        }

        #region constructor
        public LabSpecimenPositionDataSeedContributor(
            ILabSpecimenPositionRepository labSpecimenPositionRepository, 
            IGuidGenerator guidGenerator)
        {
            _labSpecimenPositionRepository = labSpecimenPositionRepository;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly ILabSpecimenPositionRepository _labSpecimenPositionRepository;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

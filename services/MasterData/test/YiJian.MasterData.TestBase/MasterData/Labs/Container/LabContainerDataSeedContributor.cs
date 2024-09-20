namespace YiJian.MasterData.Labs.Container
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 容器编码 DataSeedContributor
    /// </summary>
    public class LabContainerDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _labContainerRepository.GetCountAsync() > 0)
			{
				return;
			}            
            /*
            // 容器编码
            string containerCode = "";
            // 容器名称
            string containerName = "";
            // 容器颜色
            string containerColor = "";
            // 
            bool isActive = true;

            var labContainer = new LabContainer(containerCode,// 容器编码
                containerName,  // 容器名称
                containerColor, // 容器颜色
                isActive
                );

			await _labContainerRepository.InsertAsync(labContainer); 
            */
        }

        #region constructor
        public LabContainerDataSeedContributor(
            ILabContainerRepository labContainerRepository, 
            IGuidGenerator guidGenerator)
        {
            _labContainerRepository = labContainerRepository;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly ILabContainerRepository _labContainerRepository;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

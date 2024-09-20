namespace YiJian.MasterData.Labs
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 检验目录 DataSeedContributor
    /// </summary>
    public class LabCatalogDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _labCatalogRepository.GetCountAsync() > 0)
			{
				return;
			}

            //TODO: (检验目录 种下数据)……

			///await _labCatalogRepository.InsertAsync(book);
        }

        #region constructor
        public LabCatalogDataSeedContributor(
            ILabCatalogRepository labCatalogRepository, 
            IGuidGenerator guidGenerator)
        {
            _labCatalogRepository = labCatalogRepository;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly ILabCatalogRepository _labCatalogRepository;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

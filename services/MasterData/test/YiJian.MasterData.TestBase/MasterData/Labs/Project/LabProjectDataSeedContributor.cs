namespace YiJian.MasterData.Labs
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 检验项目 DataSeedContributor
    /// </summary>
    public class LabProjectDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _labProjectRepository.GetCountAsync() > 0)
			{
				return;
			}

            //TODO: (检验项目 种下数据)……

			///await _labProjectRepository.InsertAsync(book);
        }

        #region constructor
        public LabProjectDataSeedContributor(
            ILabProjectRepository labProjectRepository, 
            IGuidGenerator guidGenerator)
        {
            _labProjectRepository = labProjectRepository;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly ILabProjectRepository _labProjectRepository;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

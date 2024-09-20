namespace YiJian.MasterData.Labs
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 检验明细项 DataSeedContributor
    /// </summary>
    public class LabTargetDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _labTargetRepository.GetCountAsync() > 0)
			{
				return;
			}

            //TODO: (检验明细项 种下数据)……

			///await _labTargetRepository.InsertAsync(book);
        }

        #region constructor
        public LabTargetDataSeedContributor(
            ILabTargetRepository labTargetRepository, 
            IGuidGenerator guidGenerator)
        {
            _labTargetRepository = labTargetRepository;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly ILabTargetRepository _labTargetRepository;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

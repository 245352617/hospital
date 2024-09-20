namespace YiJian.MasterData.Labs
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 检验标本 DataSeedContributor
    /// </summary>
    public class LabSpecimenDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _labSpecimenRepository.GetCountAsync() > 0)
			{
				return;
			}

            //TODO: (检验标本 种下数据)……

			///await _labSpecimenRepository.InsertAsync(book);
        }

        #region constructor
        public LabSpecimenDataSeedContributor(
            ILabSpecimenRepository labSpecimenRepository, 
            IGuidGenerator guidGenerator)
        {
            _labSpecimenRepository = labSpecimenRepository;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly ILabSpecimenRepository _labSpecimenRepository;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

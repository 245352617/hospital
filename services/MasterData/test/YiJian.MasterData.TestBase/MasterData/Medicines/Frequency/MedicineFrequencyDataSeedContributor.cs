namespace YiJian.MasterData.Medicines
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 药品频次字典 DataSeedContributor
    /// </summary>
    public class MedicineFrequencyDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _medicineFrequencyRepository.GetCountAsync() > 0)
			{
				return;
			}

            //TODO: (药品频次字典 种下数据)……

			///await _medicineFrequencyRepository.InsertAsync(book);
        }

        #region constructor
        public MedicineFrequencyDataSeedContributor(
            IMedicineFrequencyRepository medicineFrequencyRepository, 
            IGuidGenerator guidGenerator)
        {
            _medicineFrequencyRepository = medicineFrequencyRepository;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly IMedicineFrequencyRepository _medicineFrequencyRepository;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

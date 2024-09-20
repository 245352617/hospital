namespace YiJian.MasterData.Medicines
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 药品用法字典 DataSeedContributor
    /// </summary>
    public class MedicineUsageDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _medicineUsageRepository.GetCountAsync() > 0)
            {
                return;
            }

            //TODO: (药品用法字典 种下数据)……

            ///await _medicineUsageRepository.InsertAsync(book);
        }

        #region constructor

        public MedicineUsageDataSeedContributor(
            IMedicineUsageRepository medicineUsageRepository,
            IGuidGenerator guidGenerator)
        {
            _medicineUsageRepository = medicineUsageRepository;

            _guidGenerator = guidGenerator;
        }

        #endregion

        #region Private Fields

        private readonly IMedicineUsageRepository _medicineUsageRepository;
        private readonly IGuidGenerator _guidGenerator;

        #endregion
    }
}
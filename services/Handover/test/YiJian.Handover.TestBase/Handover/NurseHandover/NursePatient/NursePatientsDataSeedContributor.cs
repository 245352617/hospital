namespace YiJian.Handover
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 护士交班id DataSeedContributor
    /// </summary>
    public class NursePatientsDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _nursePatientsRepository.GetCountAsync() > 0)
			{
				return;
			}

            //TODO: (护士交班id 种下数据)……

			///await _nursePatientsRepository.InsertAsync(book);
        }

        #region constructor
        public NursePatientsDataSeedContributor(
            INursePatientsRepository nursePatientsRepository, 
            NursePatientsManager nursePatientsManager,
            IGuidGenerator guidGenerator)
        {
            _nursePatientsRepository = nursePatientsRepository;
            _nursePatientsManager = nursePatientsManager;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly INursePatientsRepository _nursePatientsRepository;
        private readonly NursePatientsManager _nursePatientsManager;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

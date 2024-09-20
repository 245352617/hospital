namespace YiJian.Handover
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 交班日期 DataSeedContributor
    /// </summary>
    public class NurseHandoverDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _nurseHandoverRepository.GetCountAsync() > 0)
			{
				return;
			}

            //TODO: (交班日期 种下数据)……

			///await _nurseHandoverRepository.InsertAsync(book);
        }

        #region constructor
        public NurseHandoverDataSeedContributor(
            INurseHandoverRepository nurseHandoverRepository, 
            NurseHandoverManager nurseHandoverManager,
            IGuidGenerator guidGenerator)
        {
            _nurseHandoverRepository = nurseHandoverRepository;
            _nurseHandoverManager = nurseHandoverManager;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly INurseHandoverRepository _nurseHandoverRepository;
        private readonly NurseHandoverManager _nurseHandoverManager;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

namespace YiJian.MasterData.DictionariesType
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 字典类型编码 DataSeedContributor
    /// </summary>
    public class DictionariesTypeDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _dictionariesTypeRepository.GetCountAsync() > 0)
			{
				return;
			}

            //TODO: (字典类型编码 种下数据)……

			///await _dictionariesTypeRepository.InsertAsync(book);
        }

        #region constructor
        public DictionariesTypeDataSeedContributor(
            IDictionariesTypeRepository dictionariesTypeRepository, 
            DictionariesTypeManager dictionariesTypeManager,
            IGuidGenerator guidGenerator)
        {
            _dictionariesTypeRepository = dictionariesTypeRepository;
            _dictionariesTypeManager = dictionariesTypeManager;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly IDictionariesTypeRepository _dictionariesTypeRepository;
        private readonly DictionariesTypeManager _dictionariesTypeManager;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

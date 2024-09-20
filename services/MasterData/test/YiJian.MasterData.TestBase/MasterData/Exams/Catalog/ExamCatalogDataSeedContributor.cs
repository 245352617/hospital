namespace YiJian.MasterData.Exams
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 检查目录 DataSeedContributor
    /// </summary>
    public class ExamCatalogDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _examCatalogRepository.GetCountAsync() > 0)
			{
				return;
			}

            //TODO: (检查目录 种下数据)……

			///await _examCatalogRepository.InsertAsync(book);
        }

        #region constructor
        public ExamCatalogDataSeedContributor(
            IExamCatalogRepository examCatalogRepository, 
            IGuidGenerator guidGenerator)
        {
            _examCatalogRepository = examCatalogRepository;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly IExamCatalogRepository _examCatalogRepository;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

namespace YiJian.MasterData.Exams
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 检查部位 DataSeedContributor
    /// </summary>
    public class ExamPartDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _examPartRepository.GetCountAsync() > 0)
			{
				return;
			}

            //TODO: (检查部位 种下数据)……

			///await _examPartRepository.InsertAsync(book);
        }

        #region constructor
        public ExamPartDataSeedContributor(
            IExamPartRepository examPartRepository, 
            IGuidGenerator guidGenerator)
        {
            _examPartRepository = examPartRepository;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly IExamPartRepository _examPartRepository;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

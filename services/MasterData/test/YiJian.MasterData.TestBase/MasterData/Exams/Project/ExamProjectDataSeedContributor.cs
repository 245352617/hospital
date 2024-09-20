namespace YiJian.MasterData.Exams
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 检查申请项目 DataSeedContributor
    /// </summary>
    public class ExamProjectDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _examProjectRepository.GetCountAsync() > 0)
			{
				return;
			}

            //TODO: (检查申请项目 种下数据)……

			///await _examProjectRepository.InsertAsync(book);
        }

        #region constructor
        public ExamProjectDataSeedContributor(
            IExamProjectRepository examProjectRepository, 
            IGuidGenerator guidGenerator)
        {
            _examProjectRepository = examProjectRepository;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly IExamProjectRepository _examProjectRepository;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

namespace YiJian.MasterData.Exams
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 检查明细项 DataSeedContributor
    /// </summary>
    public class ExamTargetDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _examTargetRepository.GetCountAsync() > 0)
			{
				return;
			}

            //TODO: (检查明细项 种下数据)……

			///await _examTargetRepository.InsertAsync(book);
        }

        #region constructor
        public ExamTargetDataSeedContributor(
            IExamTargetRepository examTargetRepository, 
            IGuidGenerator guidGenerator)
        {
            _examTargetRepository = examTargetRepository;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly IExamTargetRepository _examTargetRepository;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

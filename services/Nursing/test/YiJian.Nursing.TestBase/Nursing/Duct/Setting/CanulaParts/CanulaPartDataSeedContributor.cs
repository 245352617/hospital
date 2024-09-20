namespace YiJian.Nursing
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 表:人体图-编号字典 DataSeedContributor
    /// </summary>
    public class CanulaPartDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _canulaPartRepository.GetCountAsync() > 0)
			{
				return;
			}            
            /*
            // 
            Guid id = default;
            // 科室代码
            string deptCode = "";
            // 模块代码
            string moduleCode = "";
            // 部位名称
            string partName = "";
            // 部位编号
            string partNumber = "";
            // 排序
            int sort = 0;
            // 是否可用
            bool isEnable = true;
            // 是否删除
            bool isDeleted = true;

            var canulaPart = new CanulaPart(_guidGenerator.Create(), 
                deptCode,// 科室代码
                moduleCode,     // 模块代码
                partName,       // 部位名称
                partNumber,     // 部位编号
                sort,           // 排序
                isEnable,       // 是否可用
                isDeleted       // 是否删除
                );

			await _canulaPartRepository.InsertAsync(canulaPart); 
            */
        }

        #region constructor
        public CanulaPartDataSeedContributor(
            ICanulaPartRepository canulaPartRepository, 
            IGuidGenerator guidGenerator)
        {
            _canulaPartRepository = canulaPartRepository;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly ICanulaPartRepository _canulaPartRepository;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

namespace YiJian.MasterData.AllItems
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 诊疗检查检验药品项目合集 DataSeedContributor
    /// </summary>
    public class AllItemDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _allItemRepository.GetCountAsync() > 0)
			{
				return;
			}            
            /*
            // 分类编码
            string categoryCode = "";
            // 分类名称
            string categoryName = "";
            // 编码
            string code = "";
            // 名称
            string name = "";
            // 单位
            string unit = "";
            // 价格
            decimal charge = 0;
            // 排序
            int indexNo = 0;
            // 类型编码
            string typeCode = "";
            // 类型名称
            string typeName = "";

            var allItem = new AllItem(categoryCode,// 分类编码
                categoryName,   // 分类名称
                code,           // 编码
                name,           // 名称
                unit,           // 单位
                charge,         // 价格
                indexNo,        // 排序
                typeCode,       // 类型编码
                typeName        // 类型名称
                );

			await _allItemRepository.InsertAsync(allItem); 
            */
        }

        #region constructor
        public AllItemDataSeedContributor(
            IAllItemRepository allItemRepository, 
            AllItemManager allItemManager,
            IGuidGenerator guidGenerator)
        {
            _allItemRepository = allItemRepository;
            _allItemManager = allItemManager;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly IAllItemRepository _allItemRepository;
        private readonly AllItemManager _allItemManager;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

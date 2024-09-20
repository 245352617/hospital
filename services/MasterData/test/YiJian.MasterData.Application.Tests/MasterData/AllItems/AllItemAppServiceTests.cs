namespace YiJian.MasterData.AllItems
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 诊疗检查检验药品项目合集 应用服务测试
    /// </summary>
    public class AllItemAppServiceTests : MasterDataApplicationTestBase
    {      
        private readonly IAllItemRepository _allItemRepository;
        private readonly AllItemManager _allItemManager;
        private readonly IAllItemByThreePartyAppService _allItemAppService;
        
        public AllItemAppServiceTests()
        {
            _allItemRepository = GetRequiredService<IAllItemRepository>();
            _allItemManager = GetRequiredService<AllItemManager>();  
            _allItemAppService = GetRequiredService<IAllItemByThreePartyAppService>(); 
        }

        [Fact]
        public async Task CreateAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作
            var input = new AllItemCreation()
            {
                CategoryCode = "",// 分类编码
                CategoryName = "",// 分类名称
                Code = "",      // 编码
                Name = "",      // 名称
                Unit = "",      // 单位
                Charge = 0,     // 价格
                IndexNo = 0,    // 排序
                TypeCode = "",  // 类型编码
                TypeName = ""   // 类型名称
            };


            // Act 运行实际测试的代码

            var id = await WithUnitOfWorkAsync(() =>
                                        _allItemAppService.CreateAsync(input)
                                        );

            // Assert 断言，检验结果

            id.ShouldNotBeNull();
            */
        }

        [Fact]
        public async Task UpdateAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作
            var input = new AllItemUpdate()
            {
                CategoryName = "",// 分类名称
                Code = "",      // 编码
                Name = "",      // 名称
                Unit = "",      // 单位
                Charge = 0,     // 价格
                IndexNo = 0,    // 排序
                TypeCode = "",  // 类型编码
                TypeName = ""   // 类型名称
            };


            // Act 运行实际测试的代码

            await WithUnitOfWorkAsync(() =>
                            _allItemAppService.UpdateAsync(input)
                            );

            // Assert 断言，检验结果

            
            */
        }

        [Fact]
        public async Task GetAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            int id;

            // Act 运行实际测试的代码

            var allItemData = await _allItemAppService.GetAsync(id);

            // Assert 断言，检验结果

            allItemData.ShouldNotBeNull();
            */
        }

        [Fact]
        public async Task DeleteAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            int id;

            // Act 运行实际测试的代码

            await _allItemAppService.DeleteAsync(id);

            // Assert 断言，检验结果

            (await _allItemRepository.GetCountAsync()).ShouldBe(0);
            */
        }

        [Fact]
        public async Task GetListAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            string filter = null;
            string sorting = null;

            // Act 运行实际测试的代码

            var allItemData = await _allItemAppService.GetListAsync(filter, sorting);

            // Assert 断言，检验结果

            allItemData.Items.Count.ShouldBe(1);
            */
        }

        [Fact]
        public async Task GetPagedListAsyncTestAsync()
        {   
            /*
            // Arrange 为测试做准备工作

            var input = new GetAllItemPagedInput();

            // Act 运行实际测试的代码

            var allItemData = await _allItemAppService.GetPagedListAsync(input);

            // Assert 断言，检验结果

            allItemData.Items.Count.ShouldBe(1);
            */
        }
    }
}

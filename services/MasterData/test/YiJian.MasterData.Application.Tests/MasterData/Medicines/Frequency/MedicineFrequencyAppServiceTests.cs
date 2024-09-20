namespace YiJian.MasterData.Medicines
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 药品频次字典 应用服务测试
    /// </summary>
    public class MedicineFrequencyAppServiceTests : MasterDataApplicationTestBase
    {      
        private readonly IMedicineFrequencyAppService _medicineFrequencyAppService;
        
        public MedicineFrequencyAppServiceTests()
        {
            _medicineFrequencyAppService = GetRequiredService<IMedicineFrequencyAppService>(); 

            //TODO: (药品频次字典 应用服务单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）……  
        }

        [Fact]
        public async Task CreateAsyncTest()
        {
            //TODO: (准备新增测试)……
        }

        [Fact]
        public async Task UpdateAsyncTest()
        {
            //TODO: (准备修改测试)……
        }

        [Fact]
        public async Task GetAsyncTest()
        {
            //TODO: (准备获取测试)……
        }

        [Fact]
        public async Task DeleteAsyncTest()
        {
            //TODO: (准备删除测试)……
        }

    }
}

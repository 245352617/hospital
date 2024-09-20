namespace YiJian.MasterData.Medicines
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 药品频次字典 仓储测试
    /// </summary>
    public class MedicineFrequencyRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly IMedicineFrequencyRepository _medicineFrequencyRepository;

        public MedicineFrequencyRepositoryTests()
        {
            _medicineFrequencyRepository = GetRequiredService<IMedicineFrequencyRepository>();

            //TODO: (药品频次字典 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}

namespace YiJian.MasterData.Medicines
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 药品用法字典 仓储测试
    /// </summary>
    public class MedicineUsageRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly IMedicineUsageRepository _medicineUsageRepository;

        public MedicineUsageRepositoryTests()
        {
            _medicineUsageRepository = GetRequiredService<IMedicineUsageRepository>();

            //TODO: (药品用法字典 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}

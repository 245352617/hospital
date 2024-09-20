namespace YiJian.MasterData.Medicines
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using YiJian.MasterData.EntityFrameworkCore;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// 药品字典 仓储测试
    /// </summary>
    public class MedicineRepositoryTests : MasterDataEntityFrameworkCoreTestBase
    {
        private readonly IMedicineRepository _medicineRepository;

        public MedicineRepositoryTests()
        {
            _medicineRepository = GetRequiredService<IMedicineRepository>();

            //TODO: (药品字典 仓储单元测试)……

            //UNDONE:（待继续）……

            //HACK：（待修改）…… 
        }
        
    }
}

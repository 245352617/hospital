namespace YiJian.MasterData.Treats
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 诊疗项目字典 DataSeedContributor
    /// </summary>
    public class TreatDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _treatRepository.GetCountAsync() > 0)
			{
				return;
			}            
            /*
            // 编码
            string code = "";
            // 名称
            string name = "";
            // 拼音码
            string pyCode = "";
            // 五笔
            string wbCode = "";
            // 单价
            decimal price = 0;
            // 其它价格
            decimal? otherPrice = null;
            // 诊疗处置类别代码
            string categoryCode = "";
            // 诊疗处置类别
            string category = "";
            // 规格
            string specification = "";
            // 单位
            string unit = "";
            // 默认频次代码
            string frequencyCode = "";
            // 执行科室代码
            string execDeptCode = "";
            // 执行科室
            string execDept = "";
            // 收费大类代码
            string feeTypeMain = "";
            // 收费小类代码
            string feeTypeSub = "";

            var treat = new Treat(code,   // 编码
                name,           // 名称
                pyCode,         // 拼音码
                wbCode,         // 五笔
                price,          // 单价
                otherPrice,     // 其它价格
                categoryCode,   // 诊疗处置类别代码
                category,       // 诊疗处置类别
                specification,  // 规格
                unit,           // 单位
                frequencyCode,  // 默认频次代码
                execDeptCode,   // 执行科室代码
                execDept,       // 执行科室
                feeTypeMain,    // 收费大类代码
                feeTypeSub      // 收费小类代码
                );

			await _treatRepository.InsertAsync(treat); 
            */
        }

        #region constructor
        public TreatDataSeedContributor(
            ITreatRepository treatRepository, 
            IGuidGenerator guidGenerator)
        {
            _treatRepository = treatRepository;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly ITreatRepository _treatRepository;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

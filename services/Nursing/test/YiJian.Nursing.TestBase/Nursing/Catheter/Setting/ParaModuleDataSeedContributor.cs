namespace YiJian.Nursing.Settings
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 表:模块参数 DataSeedContributor
    /// </summary>
    public class ParaModuleDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _paraModuleRepository.GetCountAsync() > 0)
			{
				return;
			}            
            /*
            // 
            Guid id = default;
            // 模块代码
            string moduleCode = "";
            // 模块名称
            string moduleName = "";
            // 模块显示名称
            string displayName = "";
            // 科室代码
            string deptCode = "";
            // 模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）
            string moduleType = "";
            // 是否血流内导管
            bool isBloodFlow = true;
            // 模块拼音
            string py = "";
            // 排序
            int sortNum = 0;
            // 是否启用
            bool isEnable = true;
            // 是否有效(1-有效，0-无效)
            int validState = 0;

            var paraModule = new ParaModule(_guidGenerator.Create(), 
                moduleCode,// 模块代码
                moduleName,     // 模块名称
                displayName,    // 模块显示名称
                deptCode,       // 科室代码
                moduleType,     // 模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）
                isBloodFlow,    // 是否血流内导管
                py,             // 模块拼音
                sortNum,        // 排序
                isEnable,       // 是否启用
                validState      // 是否有效(1-有效，0-无效)
                );

			await _paraModuleRepository.InsertAsync(paraModule); 
            */
        }

        #region constructor
        public ParaModuleDataSeedContributor(
            IParaModuleRepository paraModuleRepository, 
            ParaModuleManager paraModuleManager,
            IGuidGenerator guidGenerator)
        {
            _paraModuleRepository = paraModuleRepository;
            _paraModuleManager = paraModuleManager;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly IParaModuleRepository _paraModuleRepository;
        private readonly ParaModuleManager _paraModuleManager;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

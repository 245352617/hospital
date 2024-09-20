namespace YiJian.Nursing
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 表:导管字典-通用业务 DataSeedContributor
    /// </summary>
    public class DictDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _dictRepository.GetCountAsync() > 0)
			{
				return;
			}            
            /*
            // 
            Guid id = default;
            // 参数代码
            string paraCode = "";
            // 参数名称
            string paraName = "";
            // 字典代码
            string dictCode = "";
            // 字典值
            string dictValue = "";
            // 字典值说明
            string dictDesc = "";
            // 上级代码
            string parentId = "";
            // 字典标准（国标、自定义）
            string dictStandard = "";
            // HIS对照代码
            string hisCode = "";
            // HIS对照
            string hisName = "";
            // 科室代码
            string deptCode = "";
            // 模块代码
            string moduleCode = "";
            // 排序
            int sort = 0;
            // 是否默认
            bool isDefault = true;
            // 是否启用
            bool isEnable = true;
            // 有效状态（1-有效，0-无效）
            int validState = 0;

            var dict = new Dict(_guidGenerator.Create(), 
                paraCode,// 参数代码
                paraName,       // 参数名称
                dictCode,       // 字典代码
                dictValue,      // 字典值
                dictDesc,       // 字典值说明
                parentId,       // 上级代码
                dictStandard,   // 字典标准（国标、自定义）
                hisCode,        // HIS对照代码
                hisName,        // HIS对照
                deptCode,       // 科室代码
                moduleCode,     // 模块代码
                sort,           // 排序
                isDefault,      // 是否默认
                isEnable,       // 是否启用
                validState      // 有效状态（1-有效，0-无效）
                );

			await _dictRepository.InsertAsync(dict); 
            */
        }

        #region constructor
        public DictDataSeedContributor(
            IDictRepository dictRepository, 
            DictManager dictManager,
            IGuidGenerator guidGenerator)
        {
            _dictRepository = dictRepository;
            _dictManager = dictManager;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly IDictRepository _dictRepository;
        private readonly DictManager _dictManager;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}

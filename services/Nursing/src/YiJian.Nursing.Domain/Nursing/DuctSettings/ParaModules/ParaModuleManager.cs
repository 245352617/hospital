using JetBrains.Annotations;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace YiJian.Nursing.Settings
{
    /// <summary>
    /// 表:模块参数 领域服务
    /// </summary>
    public class ParaModuleManager : DomainService
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IParaModuleRepository _paraModuleRepository;

        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paraModuleRepository"></param>
        /// <param name="guidGenerator"></param>
        public ParaModuleManager(IParaModuleRepository paraModuleRepository, IGuidGenerator guidGenerator)
        {
            _paraModuleRepository = paraModuleRepository;
            _guidGenerator = guidGenerator;
        }

        #endregion

        #region Create

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="displayName">模块显示名称</param>
        /// <param name="deptCode">科室代码</param>
        /// <param name="moduleType">模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）</param>
        /// <param name="isBloodFlow">是否血流内导管</param>
        /// <param name="py">模块拼音</param>
        /// <param name="sort">排序</param>
        /// <param name="isEnable">是否启用</param>
        /// <returns></returns>
        public async Task<ParaModule> CreateAsync(
            [NotNull] string moduleCode, // 模块代码
            [NotNull] string moduleName, // 模块名称
            string displayName, // 模块显示名称
            [NotNull] string deptCode, // 科室代码
            string moduleType, // 模块类型：（CANULA：导管，SKIN：皮肤，VS：观察项目，IO：出入量，EM：ECMO，BP：血液净化，PC：PICCO）
            bool isBloodFlow, // 是否血流内导管
            string py, // 模块拼音
            int sort, // 排序
            bool isEnable // 是否启用
        )
        {
            var paraModule = new ParaModule(_guidGenerator.Create(), moduleCode, moduleName, displayName, deptCode,
                moduleType, isBloodFlow, py, sort, isEnable);
            return await _paraModuleRepository.InsertAsync(paraModule);
        }

        #endregion Create
    }
}
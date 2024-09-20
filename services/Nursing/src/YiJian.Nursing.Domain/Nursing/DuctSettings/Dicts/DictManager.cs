using JetBrains.Annotations;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;


namespace YiJian.Nursing
{
    /// <summary>
    /// 表:导管字典-通用业务 领域服务
    /// </summary>
    public class DictManager : DomainService
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IDictRepository _dictRepository;

        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dictRepository"></param>
        /// <param name="guidGenerator"></param>
        public DictManager(IDictRepository dictRepository, IGuidGenerator guidGenerator)
        {
            _dictRepository = dictRepository;
            _guidGenerator = guidGenerator;
        }

        #endregion

        #region Create

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="paraCode">参数代码</param>
        /// <param name="paraName">参数名称</param>
        /// <param name="dictCode">字典代码</param>
        /// <param name="dictValue">字典值</param>
        /// <param name="dictDesc">字典值说明</param>
        /// <param name="parentId">上级代码</param>
        /// <param name="dictStandard">字典标准（国标、自定义）</param>
        /// <param name="hisCode">HIS对照代码</param>
        /// <param name="hisName">HIS对照</param>
        /// <param name="deptCode">科室代码</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="sort">排序</param>
        /// <param name="isDefault">是否默认</param>
        /// <param name="isEnable">是否启用</param>
        public async Task<Dict> CreateAsync([NotNull] string paraCode, // 参数代码
            [NotNull] string paraName, // 参数名称
            [NotNull] string dictCode, // 字典代码
            [NotNull] string dictValue, // 字典值
            string dictDesc, // 字典值说明
            string parentId, // 上级代码
            string dictStandard, // 字典标准（国标、自定义）
            string hisCode, // HIS对照代码
            string hisName, // HIS对照
            string deptCode, // 科室代码
            string moduleCode, // 模块代码
            [NotNull] int sort, // 排序
            bool isDefault, // 是否默认
            bool isEnable // 是否启用
        )
        {
            var dict = new Dict(_guidGenerator.Create(),
                paraCode, // 参数代码
                paraName, // 参数名称
                dictCode, // 字典代码
                dictValue, // 字典值
                dictDesc, // 字典值说明
                parentId, // 上级代码
                dictStandard, // 字典标准（国标、自定义）
                hisCode, // HIS对照代码
                hisName, // HIS对照
                deptCode, // 科室代码
                moduleCode, // 模块代码
                sort, // 排序
                isDefault, // 是否默认
                isEnable // 是否启用
            );

            return await _dictRepository.InsertAsync(dict);
        }

        #endregion Create
    }
}
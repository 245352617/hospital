using JetBrains.Annotations;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace YiJian.MasterData.AllItems;

/// <summary>
/// 诊疗检查检验药品项目合集 领域服务
/// </summary>
public class AllItemManager : DomainService
{
    private readonly IAllItemRepository _allItemRepository;

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="allItemRepository"></param>
    public AllItemManager(IAllItemRepository allItemRepository)
    {
        _allItemRepository = allItemRepository;
    }

    #endregion

    #region Create

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="categoryCode">分类编码</param>
    /// <param name="categoryName">分类名称</param>
    /// <param name="code">编码</param>
    /// <param name="name">名称</param>
    /// <param name="unit">单位</param>
    /// <param name="charge">价格</param>
    /// <param name="indexNo">排序</param>
    /// <param name="typeCode">类型编码</param>
    /// <param name="typeName">类型名称</param>
    /// <param name="chargeCode"></param>
    /// <param name="chargeName"></param>
    public async Task<AllItem> CreateAsync([NotNull] string categoryCode, // 分类编码
        [NotNull] string categoryName, // 分类名称
        [NotNull] string code, // 编码
        [NotNull] string name, // 名称
        [NotNull] string unit, // 单位
        decimal charge, // 价格
        int indexNo, // 排序
        string typeCode, // 类型编码
        string typeName, // 类型名称
        string chargeCode,
        string chargeName
    )
    {
        var allItem = new AllItem(categoryCode, // 分类编码
            categoryName, // 分类名称
            code, // 编码
            name, // 名称
            unit, // 单位
            charge, // 价格
            indexNo, // 排序
            typeCode, // 类型编码
            typeName, // 类型名称
            chargeCode,
            chargeName
        );

        return await _allItemRepository.InsertAsync(allItem);
    }

    #endregion Create
}
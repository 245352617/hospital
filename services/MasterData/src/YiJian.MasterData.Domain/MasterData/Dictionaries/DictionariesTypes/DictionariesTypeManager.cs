using JetBrains.Annotations;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace YiJian.MasterData.DictionariesTypes;

/// <summary>
/// 字典类型编码 领域服务
/// </summary>
public class DictionariesTypeManager : DomainService
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly IDictionariesTypeRepository _dictionariesTypeRepository;

    #region constructor

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictionariesTypeRepository"></param>
    /// <param name="guidGenerator"></param>
    public DictionariesTypeManager(IDictionariesTypeRepository dictionariesTypeRepository,
        IGuidGenerator guidGenerator)
    {
        _dictionariesTypeRepository = dictionariesTypeRepository;
        _guidGenerator = guidGenerator;
    }

    #endregion

    #region Create

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="dictionariesTypeCode">字典类型编码</param>
    /// <param name="dictionariesTypeName">字典类型名称</param>
    /// <param name="remark">备注</param>
    /// <param name="status"></param>
    /// <param name="dataFrom">数据来源，1：预检分诊</param>
    public async Task<DictionariesType> CreateAsync([NotNull] string dictionariesTypeCode, // 字典类型编码
        string dictionariesTypeName, // 字典类型名称
        string remark,// 备注
        bool status,//状态
        int dataFrom = 0//数据来源，1：预检分诊
    )
    {

        var dictionariesType = new DictionariesType(_guidGenerator.Create(),
               dictionariesTypeCode, // 字典类型编码
               dictionariesTypeName, // 字典类型名称
               remark, dataFrom,status // 备注
           );
        return await _dictionariesTypeRepository.InsertAsync(dictionariesType);
    }

    #endregion Create
}
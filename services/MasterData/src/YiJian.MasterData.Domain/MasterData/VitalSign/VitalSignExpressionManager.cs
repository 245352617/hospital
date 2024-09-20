using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace YiJian.MasterData.VitalSign;


/// <summary>
/// 评分项 领域服务
/// </summary>
public class VitalSignExpressionManager : DomainService
{   
    private readonly IGuidGenerator _guidGenerator;
    private readonly IVitalSignExpressionRepository _vitalSignExpressionRepository;

    #region constructor
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="vitalSignExpressionRepository"></param>
    /// <param name="guidGenerator"></param>
    public VitalSignExpressionManager(IVitalSignExpressionRepository vitalSignExpressionRepository, IGuidGenerator guidGenerator)
    {
        _vitalSignExpressionRepository = vitalSignExpressionRepository;
        _guidGenerator = guidGenerator;
    }        
    #endregion

    #region Create
    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="itemName">评分项</param>
    /// <param name="stLevelExpression">Ⅰ级评分表达式</param>
    /// <param name="ndLevelExpression">Ⅱ级评分表达式</param>
    /// <param name="rdLevelExpression">Ⅲ级评分表达式</param>
    /// <param name="thALevelExpression">Ⅳa级评分表达式</param>
    /// <param name="thBLevelExpression">Ⅳb级评分表达式</param>
    /// <param name="defaultStLevelExpression">默认Ⅰ级评分表达式</param>
    /// <param name="defaultNdLevelExpression">默认Ⅱ级评分表达式</param>
    /// <param name="defaultRdLevelExpression">默认Ⅲ级评分表达式</param>
    /// <param name="defaultThALevelExpression">默认Ⅳa级评分表达式</param>
    /// <param name="defaultThBLevelExpression">默认Ⅳb级评分表达式</param>
    public async Task<VitalSignExpression> CreateAsync(string itemName,  // 评分项
        string stLevelExpression,     // Ⅰ级评分表达式
        string ndLevelExpression,     // Ⅱ级评分表达式
        string rdLevelExpression,     // Ⅲ级评分表达式
        string thALevelExpression,    // Ⅳa级评分表达式
        string thBLevelExpression,    // Ⅳb级评分表达式
        string defaultStLevelExpression,// 默认Ⅰ级评分表达式
        string defaultNdLevelExpression,// 默认Ⅱ级评分表达式
        string defaultRdLevelExpression,// 默认Ⅲ级评分表达式
        string defaultThALevelExpression,// 默认Ⅳa级评分表达式
        string defaultThBLevelExpression// 默认Ⅳb级评分表达式
        ) 
    {
        var vitalSignExpression = await _vitalSignExpressionRepository.FirstOrDefaultAsync(v => v.ItemName == itemName);
        
        if (vitalSignExpression != null) throw new VitalSignExpressionAlreadyExistsException(itemName);

        vitalSignExpression = new VitalSignExpression(_guidGenerator.Create(), 
            itemName,// 评分项
            stLevelExpression,// Ⅰ级评分表达式
            ndLevelExpression,// Ⅱ级评分表达式
            rdLevelExpression,// Ⅲ级评分表达式
            thALevelExpression,// Ⅳa级评分表达式
            thBLevelExpression,// Ⅳb级评分表达式
            defaultStLevelExpression,// 默认Ⅰ级评分表达式
            defaultNdLevelExpression,// 默认Ⅱ级评分表达式
            defaultRdLevelExpression,// 默认Ⅲ级评分表达式
            defaultThALevelExpression,// 默认Ⅳa级评分表达式
            defaultThBLevelExpression// 默认Ⅳb级评分表达式
            );

        return await _vitalSignExpressionRepository.InsertAsync(vitalSignExpression);
    }
    #endregion Create

}

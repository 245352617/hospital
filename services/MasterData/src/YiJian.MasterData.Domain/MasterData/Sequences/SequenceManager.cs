using JetBrains.Annotations;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using YiJian.ECIS;

namespace YiJian.MasterData.Sequences;


/// <summary>
/// 编码 领域服务
/// </summary>
public class SequenceManager : DomainService
{

    private readonly ISequenceRepository _sequenceRepository;

    #region constructor
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sequenceRepository"></param>
    public SequenceManager(ISequenceRepository sequenceRepository)
    {
        _sequenceRepository = sequenceRepository;
    }
    #endregion

    #region Create
    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="code">编码</param>
    /// <param name="name">名称</param>
    /// <param name="value">序列值</param>
    /// <param name="format">格式</param>
    /// <param name="length">序列值长度</param>
    /// <param name="date">日期</param>
    /// <param name="memo">备注</param>
    public async Task<Sequence> CreateAsync([NotNull] string code,// 编码
        [NotNull] string name,        // 名称
        int value,                    // 序列值
        string format,                // 格式
        int length,                   // 序列值长度
        DateTime date,                // 日期
        string memo                   // 备注
        )
    {
        var sequence = await _sequenceRepository.FirstOrDefaultAsync(s => s.Code == code);

        if (sequence != null) throw new SequenceAlreadyExistsException(code);

        sequence = new Sequence(code,   // 编码
            name,           // 名称
            value,          // 序列值
            format,         // 格式
            length,         // 序列值长度
            date,           // 日期
            memo            // 备注
            );

        return await _sequenceRepository.InsertAsync(sequence);
    }
    #endregion Create

    #region GetSequenceAsync
    /// <summary>
    /// 获取流水号
    /// </summary>
    /// <param name="code">编码</param>
    /// <returns></returns>
    public async Task<string> GetSequenceAsync(string code)
    {
        Check.NotNull(code, nameof(code), SequenceConsts.MaxCodeLength);

        var sequence = await _sequenceRepository.FirstOrDefaultAsync(s => s.Code == code);

        if (sequence == null) throw new SequenceCodeNotExistsException(code);

        sequence.Increase();

        await _sequenceRepository.UpdateAsync(sequence);

        return sequence.GetResult();
    }
    #endregion

    #region GetSequencesAsync
    /// <summary>
    /// 获取多个流水号
    /// </summary>
    /// <param name="code">编码</param>
    /// <param name="count">数量</param>
    /// <returns></returns>
    public async Task<string[]> GetSequencesAsync(string code, int count)
    {
        Check.NotNull(code, nameof(code), SequenceConsts.MaxCodeLength);

        var sequence = await _sequenceRepository.FirstOrDefaultAsync(s => s.Code == code);

        if (sequence == null) throw new SequenceCodeNotExistsException(code);

        var result = sequence.GetMutilResult(count).ToArray();

        await _sequenceRepository.UpdateAsync(sequence);

        return result;
    }
    #endregion
}

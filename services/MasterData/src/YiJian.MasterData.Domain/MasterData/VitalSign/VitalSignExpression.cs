using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using YiJian.ECIS;

namespace YiJian.MasterData.VitalSign;

/// <summary>
/// 生命体征表达式
/// </summary>
[Comment("生命体征表达式")]
public class VitalSignExpression : Entity<Guid>
{
    public VitalSignExpression SetId(Guid id)
    {
        Id = id;
        return this;
    }

    /// <summary>
    /// 评分项
    /// </summary>
    [StringLength(200)]
    [Comment("评分项")]
    public string ItemName { get; private set; }

    /// <summary>
    /// Ⅰ级评分表达式
    /// </summary>
    [StringLength(200)]
    [Comment("Ⅰ级评分表达式")]
    public string StLevelExpression { get; private set; }

    /// <summary>
    /// Ⅱ级评分表达式
    /// </summary>
    [StringLength(200)]
    [Comment("Ⅱ级评分表达式")]
    public string NdLevelExpression { get; private set; }

    /// <summary>
    /// Ⅲ级评分表达式
    /// </summary>
    [StringLength(200)]
    [Comment("Ⅲ级评分表达式")]
    public string RdLevelExpression { get; private set; }

    /// <summary>
    /// Ⅳa级评分表达式
    /// </summary>
    [StringLength(200)]
    [Comment("Ⅳa级评分表达式")]
    public string ThALevelExpression { get; private set; }

    /// <summary>
    /// Ⅳb级评分表达式
    /// </summary>
    [StringLength(200)]
    [Comment("Ⅳb级评分表达式")]
    public string ThBLevelExpression { get; private set; }

    /// <summary>
    /// 默认Ⅰ级评分表达式
    /// </summary>
    [StringLength(200)]
    [Comment("默认Ⅰ级评分表达式")]
    public string DefaultStLevelExpression { get; private set; }

    /// <summary>
    /// 默认Ⅱ级评分表达式
    /// </summary>
    [StringLength(200)]
    [Comment("默认Ⅱ级评分表达式")]
    public string DefaultNdLevelExpression { get; private set; }

    /// <summary>
    /// 默认Ⅲ级评分表达式
    /// </summary>
    [StringLength(200)]
    [Comment("默认Ⅲ级评分表达式")]
    public string DefaultRdLevelExpression { get; private set; }

    /// <summary>
    /// 默认Ⅳa级评分表达式
    /// </summary>
    [StringLength(200)]
    [Comment("默认Ⅳa级评分表达式")]
    public string DefaultThALevelExpression { get; private set; }

    /// <summary>
    /// 默认Ⅳb级评分表达式
    /// </summary>
    [StringLength(300)]
    [Comment("默认Ⅳb级评分表达式")]
    public string DefaultThBLevelExpression { get; private set; }

    #region constructor

    /// <summary>
    /// 评分项构造器
    /// </summary>
    /// <param name="id"></param>
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
    public VitalSignExpression(Guid id,
        string itemName, // 评分项
        string stLevelExpression, // Ⅰ级评分表达式
        string ndLevelExpression, // Ⅱ级评分表达式
        string rdLevelExpression, // Ⅲ级评分表达式
        string thALevelExpression, // Ⅳa级评分表达式
        string thBLevelExpression, // Ⅳb级评分表达式
        string defaultStLevelExpression, // 默认Ⅰ级评分表达式
        string defaultNdLevelExpression, // 默认Ⅱ级评分表达式
        string defaultRdLevelExpression, // 默认Ⅲ级评分表达式
        string defaultThALevelExpression, // 默认Ⅳa级评分表达式
        string defaultThBLevelExpression // 默认Ⅳb级评分表达式
    ) : base(id)
    {

        //默认Ⅰ级评分表达式
        DefaultStLevelExpression = Check.Length(defaultStLevelExpression, "默认Ⅰ级评分表达式",
            VitalSignExpressionConsts.MaxDefaultStLevelExpressionLength);

        //默认Ⅱ级评分表达式
        DefaultNdLevelExpression = Check.Length(defaultNdLevelExpression, "默认Ⅱ级评分表达式",
            VitalSignExpressionConsts.MaxDefaultNdLevelExpressionLength);

        //默认Ⅲ级评分表达式
        DefaultRdLevelExpression = Check.Length(defaultRdLevelExpression, "默认Ⅲ级评分表达式",
            VitalSignExpressionConsts.MaxDefaultRdLevelExpressionLength);

        //默认Ⅳa级评分表达式
        DefaultThALevelExpression = Check.Length(defaultThALevelExpression, "默认Ⅳa级评分表达式",
            VitalSignExpressionConsts.MaxDefaultThALevelExpressionLength);

        //默认Ⅳb级评分表达式
        DefaultThBLevelExpression = Check.Length(defaultThBLevelExpression, "默认Ⅳb级评分表达式",
            VitalSignExpressionConsts.MaxDefaultThBLevelExpressionLength);
        Modify(itemName,
            stLevelExpression, // Ⅰ级评分表达式
            ndLevelExpression, // Ⅱ级评分表达式
            rdLevelExpression, // Ⅲ级评分表达式
            thALevelExpression, // Ⅳa级评分表达式
        thBLevelExpression // Ⅳb级评分表达式
            );
    }

    #endregion

    #region Modify

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="stLevelExpression">Ⅰ级评分表达式</param>
    /// <param name="ndLevelExpression">Ⅱ级评分表达式</param>
    /// <param name="rdLevelExpression">Ⅲ级评分表达式</param>
    /// <param name="thALevelExpression">Ⅳa级评分表达式</param>
    /// <param name="thBLevelExpression">Ⅳb级评分表达式</param>
    public void Modify(
        string itemName, // 评分项
        string stLevelExpression, // Ⅰ级评分表达式
        string ndLevelExpression, // Ⅱ级评分表达式
        string rdLevelExpression, // Ⅲ级评分表达式
        string thALevelExpression, // Ⅳa级评分表达式
        string thBLevelExpression // Ⅳb级评分表达式
    )
    {
        //评分项
        ItemName = Check.Length(itemName, "评分项", VitalSignExpressionConsts.MaxItemNameLength);
        //Ⅰ级评分表达式
        StLevelExpression = Check.Length(stLevelExpression, "Ⅰ级评分表达式",
            VitalSignExpressionConsts.MaxStLevelExpressionLength);

        //Ⅱ级评分表达式
        NdLevelExpression = Check.Length(ndLevelExpression, "Ⅱ级评分表达式",
            VitalSignExpressionConsts.MaxNdLevelExpressionLength);

        //Ⅲ级评分表达式
        RdLevelExpression = Check.Length(rdLevelExpression, "Ⅲ级评分表达式",
            VitalSignExpressionConsts.MaxRdLevelExpressionLength);

        //Ⅳa级评分表达式
        ThALevelExpression = Check.Length(thALevelExpression, "Ⅳa级评分表达式",
            VitalSignExpressionConsts.MaxThALevelExpressionLength);

        //Ⅳb级评分表达式
        ThBLevelExpression = Check.Length(thBLevelExpression, "Ⅳb级评分表达式",
            VitalSignExpressionConsts.MaxThBLevelExpressionLength);
    }

    #endregion



    #region constructor

    private VitalSignExpression()
    {
        // for EFCore
    }

    #endregion
}
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;

namespace YiJian.MasterData.AllItems;

/// <summary>
/// 诊疗检查检验药品项目合集
/// </summary>
public class AllItem : FullAuditedAggregateRoot<int>,IHasExtraProperties
{
    /// <summary>
    /// 分类编码
    /// </summary>
    [StringLength(50)]
    [Required]
    public string CategoryCode { get; private set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [StringLength(100)]
        [Required]
        [Comment("分类名称")]
        public string CategoryName { get; private set; }

        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(50)]
        [Required]
        [Comment("编码")]
        public string Code { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(200)]
        [Required]
        [Comment("名称")]
        public string Name { get; private set; }

    private string _py;

    /// <summary>
    /// 拼音首字母
    /// </summary>
    [Comment("拼音首字母")]
    [StringLength(200)]
    public string PY
    {
        get { return _py; }
        set { _py = Name.ParsePY(); }
    }

        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(20)]
        [Required]
        [Comment("单位")]
        public string Unit { get; private set; }

        /// <summary>
        /// 价格
        /// </summary>
        [Comment("价格")]
        public decimal Price { get; private set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Comment("排序")]
        public int IndexNo { get; private set; }

        /// <summary>
        /// 类型编码
        /// </summary>
        [Comment("类型编码")]
        public string TypeCode { get; private set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [Comment("类型名称")]
        public string TypeName { get; private set; }

        /// <summary>
        /// 收费分类编码
        /// </summary>
        [StringLength(50)]
        [Comment("收费分类编码")]
        public string ChargeCode { get; private set; }

        /// <summary>
        /// 收费分类名称
        /// </summary>
        [StringLength(100)]
        [Comment("收费分类名称")]
        public string ChargeName { get; private set; }

    #region constructor

    /// <summary>
    /// 诊疗检查检验药品项目合集构造器
    /// </summary>
    /// <param name="categoryCode">分类编码</param>
    /// <param name="categoryName">分类名称</param>
    /// <param name="code">编码</param>
    /// <param name="name">名称</param>
    /// <param name="unit">单位</param>
    /// <param name="price">价格</param>
    /// <param name="indexNo">排序</param>
    /// <param name="typeCode">类型编码</param>
    /// <param name="typeName">类型名称</param>
    /// <param name="chargeCode"></param>
    /// <param name="chargeName"></param>
    public AllItem([NotNull] string categoryCode, // 分类编码
        [NotNull] string categoryName, // 分类名称
        [NotNull] string code, // 编码
        [NotNull] string name, // 名称
        [NotNull] string unit, // 单位
        decimal price, // 价格
        int indexNo, // 排序
        string typeCode, // 类型编码
        string typeName, // 类型名称
        string chargeCode,
        string chargeName
    )
    {
        //分类编码
        CategoryCode = Check.NotNull(categoryCode, "分类编码", AllItemConsts.MaxCategoryCodeLength);
        //分类名称
        CategoryName = Check.NotNull(categoryName, "分类名称", AllItemConsts.MaxCategoryNameLength);
        //编码
        Code = Check.NotNull(code, "编码", AllItemConsts.MaxCodeLength);

        //类型编码
        TypeCode = Check.Length(typeCode, "类型编码", AllItemConsts.MaxTypeCodeLength);

        //类型名称
        TypeName = Check.Length(typeName, "类型名称", AllItemConsts.MaxTypeNameLength);
        Modify(
            name, // 名称
            unit, // 单位
            price, // 价格
            indexNo, // 排序
            chargeCode, chargeName
        );
    }

    #endregion

    #region Modify

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="unit">单位</param>
    /// <param name="price">价格</param>
    /// <param name="indexNo">排序</param>
    /// <param name="chargeCode"></param>
    /// <param name="chargeName"></param>
    public void Modify(
        string name,
        [NotNull] string unit, // 单位
        decimal price, // 价格
        int indexNo, // 排序
        string chargeCode,
        string chargeName
    )
    {
        //名称
        Name = Check.NotNull(name, "名称", AllItemConsts.MaxNameLength);
        //单位
        Unit = Check.NotNull(unit, "单位", AllItemConsts.MaxUnitLength);

        //价格
        Price = price;

        //排序
        IndexNo = indexNo;
        ChargeCode = Check.Length(chargeCode, "收费分类编码", AllItemConsts.MaxChargeCodeLength);

        ChargeName = Check.Length(chargeName, "收费分类名称", AllItemConsts.MaxChargeNameLength);
    }

    #endregion

    #region constructor

    private AllItem()
    {
        // for EFCore
    }

    #endregion
}
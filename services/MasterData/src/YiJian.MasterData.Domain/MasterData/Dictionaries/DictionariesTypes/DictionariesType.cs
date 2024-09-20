using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;

namespace YiJian.MasterData.DictionariesTypes;

public class DictionariesType : FullAuditedAggregateRoot<Guid>
{
    public DictionariesType SetId(Guid id)
    {
        this.Id = id;
        return this;
    }

    /// <summary>
    /// 字典类型编码
    /// </summary> 
    [StringLength(50)]
    [Required]
    [Comment("字典类型编码")]
    public string DictionariesTypeCode { get; set; }

    /// <summary>
    /// 字典类型名称
    /// </summary> 
    [StringLength(100)]
    [Comment("字典类型名称")]
    public string DictionariesTypeName { get; set; }

    /// <summary>
    /// 数据来源，0：急诊添加，1：预检分诊同步
    /// </summary>
    [Comment("数据来源，0：急诊添加，1：预检分诊同步")]
    public int DataFrom { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    [Comment("状态")]
    public bool Status { get;private set; }

    /// <summary>
    /// 备注
    /// </summary>
    [Comment("备注")]
    public string Remark { get; set; }
    #region constructor
    /// <summary>
    /// 字典类型编码构造器
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dictionariesTypeCode">字典类型编码</param>
    /// <param name="dictionariesTypeName">字典类型名称</param>
    /// <param name="remark">备注</param>
    /// <param name="dataFrom">数据来源，1：预检分诊</param>
    /// <param name="status"></param>
    public DictionariesType(Guid id,
        [NotNull] string dictionariesTypeCode,// 字典类型编码
        string dictionariesTypeName,  // 字典类型名称
        string remark,               // 备注
        int dataFrom, //数据来源，1：预检分诊
        bool status = true
        ) : base(id)
    {
        //字典类型编码
        DictionariesTypeCode = Check.NotNull(dictionariesTypeCode, "字典类型编码");
        DataFrom = dataFrom;
        Modify(dictionariesTypeName,// 字典类型名称
            remark,   // 备注
            status
            );
    }
    #endregion

    #region Modify
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="dictionariesTypeName">字典类型名称</param>
    /// <param name="remark">备注</param>
    /// <param name="status"></param>
    public void Modify(string dictionariesTypeName,// 字典类型名称
        string remark,       // 备注
        bool status
        )
    {

        //字典类型名称
        DictionariesTypeName = dictionariesTypeName;

        //备注
        Remark = remark;
        Status = status;

    }
    #endregion

    #region constructor
    private DictionariesType()
    {
        // for EFCore
    }
    #endregion
}

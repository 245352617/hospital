using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace YiJian.MasterData;

[Serializable]
public class BaseEntity<TKey> : Entity<TKey>, IHasCreationTime,IHasModificationTime,IHasDeletionTime
{
    public BaseEntity()
    {
        this.CreationTime = DateTime.Now;
    }

    [Description( "排序")]
    public int? Sort { get; set; }

    [Description("备注")]
    [MaxLength(256, ErrorMessage = "{0}最大长度{1}")]
    public string Remark { get; set; }

    [Description("添加人")]
    [MaxLength(50)]
    public string AddUser { get; set; }

    [Description("创建时间")]
    public DateTime CreationTime { get; set; }

    [Description("修改人")]
    [MaxLength(50)]
    public string ModUser { get; set; }


    [Description("修改时间")]
    public DateTime? LastModificationTime { get; set; }

    [Description("是否删除")]
    public bool IsDeleted { get; set; }
    
    [Description("修改人")]
    [MaxLength(50)]
    public string DeleteUser { get; set; }


    [Description("删除时间")]
    public DateTime? DeletionTime { get; set; }

    [Description("医院编码")]
    [MaxLength(250)]
    public string HospitalCode { get; set; }

    [Description("医院名称")]
    [MaxLength(250)]
    public string HospitalName { get; set; }

    [Description("扩展字段1")]
    [MaxLength(250)]
    public string ExtensionField1 { get; set; }

    [Description("扩展字段2")]
    [MaxLength(250)]
    public string ExtensionField2 { get; set; }

    [Description("扩展字段3")]
    [MaxLength(250)]
    public string ExtensionField3 { get; set; }

    [Description("扩展字段4")]
    [MaxLength(250)]
    public string ExtensionField4 { get; set; }

    [Description("扩展字段5")]
    [MaxLength(250)]
    public string ExtensionField5 { get; set; }

}
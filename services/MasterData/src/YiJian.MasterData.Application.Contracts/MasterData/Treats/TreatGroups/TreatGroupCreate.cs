using System.ComponentModel.DataAnnotations;

namespace YiJian.MasterData;

public class TreatGroupCreate //: EntityDto<Guid?>
{
    /// <summary>
    /// 目录编码
    /// </summary>
    [Required, StringLength(50)]
    public string CatalogCode { get; set; }

    /// <summary>
    /// 目录名称
    /// </summary>
    public string CatalogName { get; set; }
}
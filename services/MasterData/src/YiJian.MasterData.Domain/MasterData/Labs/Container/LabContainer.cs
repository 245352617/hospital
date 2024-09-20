using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.Labs.Container;

/// <summary>
/// 容器
/// </summary>
[Comment("容器")]
public class LabContainer : FullAuditedAggregateRoot<int>, IIsActive
{
    /// <summary>
    /// 容器编码
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("容器编码")]
    public string ContainerCode { get; private set; }

    /// <summary>
    /// 容器名称
    /// </summary>
    [Required]
    [StringLength(100)]
    [Comment("容器名称")]
    public string ContainerName { get; private set; }

    /// <summary>
    /// 容器颜色
    /// </summary>
    [Required]
    [StringLength(20)]
    [Comment("容器颜色")]
    public string ContainerColor { get; private set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [Comment("是否启用")]
    public bool IsActive { get; private set; } = true;

    #region constructor

    /// <summary>
    /// 容器编码构造器
    /// </summary>
    /// <param name="containerCode"></param>
    /// <param name="containerName">容器名称</param>
    /// <param name="containerColor">容器颜色</param>
    /// <param name="isActive"></param>
    public LabContainer(string containerCode,
        [NotNull] string containerName, // 容器名称
        [NotNull] string containerColor, // 容器颜色
        bool isActive
    )
    {
        //容器编码
        ContainerCode = Check.NotNull(containerCode, "容器编码", LabContainerConsts.MaxContainerCodeLength);

        Modify(containerName, // 容器名称
            containerColor, // 容器颜色
            isActive
        );
    }

    #endregion

    #region Modify

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="containerName">容器名称</param>
    /// <param name="containerColor">容器颜色</param>
    /// <param name="isActive"></param>
    public void Modify([NotNull] string containerName, // 容器名称
        [NotNull] string containerColor, // 容器颜色
        bool isActive
    )
    {
        //容器名称
        ContainerName = Check.NotNull(containerName, "容器名称", LabContainerConsts.MaxContainerNameLength);

        //容器颜色
        ContainerColor = Check.NotNull(containerColor, "容器颜色", LabContainerConsts.MaxContainerColorLength);
        //
        IsActive = isActive;
    }

    #endregion

    #region constructor

    private LabContainer()
    {
        // for EFCore
    }

    #endregion
}
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using YiJian.ECIS.Core;
using YiJian.ECIS.ShareModel;

namespace YiJian.MasterData.Domain
{
    /// <summary>
    /// 检验目树型实体类 （用于自己维护树形构造）
    /// </summary>
    public class LabTree : TreeNode
    { 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatTime { set; get; }

    }
}

using DbPrimaryType = System.String;
using NullableDbPrimaryType = System.String;

namespace YiJian.ECIS.ShareModel
{
    public interface ITreeNode
    {
        DbPrimaryType Id { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        NullableDbPrimaryType ParentId { get; set; }
        /// <summary>
        /// 当前节点全路径
        /// </summary>
        string FullPath { get; set; }
        /// <summary>
        /// 当前节点名称
        /// </summary>
        string NodeName { get; set; }

        /// <summary>
        /// 逻辑软删除true 删除
        /// </summary>
        bool IsDelete { get; set; }
        ///// <summary>
        ///// 子节点list
        ///// </summary>
        //IList<ITreeNode> ChildNodes { get; set; }

    }
}

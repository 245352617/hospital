using System.Collections.Generic;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel;
using DbPrimaryType = System.String;
using NullableDbPrimaryType = System.String;

namespace YiJian.ECIS.Core
{
    public interface ITreeRepository<TTreeNode> : IRepository<TTreeNode>
         where TTreeNode : TreeNode, new()
    {
        /// <summary>
        /// 删除某个节点（物理删除）
        /// </summary>
        /// <param name="id">当前节点id</param> 
        /// <param name="options">选项 1允许字段 2.不自动调用saveChange</param>
        Task<TTreeNode> DeleteNodeAsync(DbPrimaryType id, IRepoOptions options = null);
        /// <summary>
        /// 根据id获取全路径
        /// </summary>
        /// <param name="id">当前节点id</param>
        /// <param name="options">选项 1允许字段 2.不自动调用saveChange</param>
        /// <returns>string</returns>
        Task<string> GetFullPathByIdAsync(DbPrimaryType id, IRepoOptions options = null);

        /// <summary>
        /// 根据全路径获取id
        /// </summary>
        /// <param name="path"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<TTreeNode> GetNodeByFullPathAsync(string path, IRepoOptions options = null);

        /// <summary>
        /// 根据id获取节点下所有节点数据
        /// </summary>
        /// <param name="id">当前节点id</param>
        /// <param name="options">选项 1允许字段 2.不自动调用saveChange</param>
        /// <returns></returns>
        Task<IList<TTreeNode>> ListAllNodesByIdAsync(DbPrimaryType id, IRepoOptions options = null);

        /// <summary>
        /// 获取当前节点下面的所有子节点信息
        /// </summary>
        /// <param name="parentFullPath"></param>
        /// <param name="options">选项 1允许字段 2.不自动调用saveChange</param>
        /// <returns></returns>
        Task<IList<TTreeNode>> ListNodesByFullPathAsync(string parentFullPath, IRepoOptions options = null);
        /// <summary>
        ///   根据id获取上个节点id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options">选项 1允许字段 2.不自动调用saveChange</param>
        /// <returns></returns>
        Task<NullableDbPrimaryType> GetParentIdAsync(DbPrimaryType id, IRepoOptions options = null);
        /// <summary>
        /// 根据全路径获去节点
        /// </summary>
        /// <param name="parentFullPath">全路径</param>
        /// <param name="options"></param>
        /// <returns></returns>
        TTreeNode GetNodeByFullPathNo(string parentFullPath, IRepoOptions options = null);
        /// <summary>
        /// 根据数据源和当前树构建树形关系
        /// </summary>
        /// <param name="all">所有数据源</param>
        /// <param name="curItem">当前的树节点</param>
        void BuildTree(IList<TTreeNode> all, TTreeNode curItem);
        Task<TTreeNode> GetTreeByRootIdAsync(DbPrimaryType id, IRepoOptions options = null);
        /// <summary>
        /// 根据当前的节点id获取树
        /// </summary>
        /// <param name="Soure"></param>
        IList<TTreeNode> BuildTrees(IList<TTreeNode> Soure);
        /// <summary>
        /// 根据节点名称查询出当前treenode集合
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options">选项 1允许字段 2.不自动调用saveChange</param>
        Task<IList<TTreeNode>> ListTreesByNameAsync(string name, IRepoOptions options = null);
        /// <summary>
        /// 插入新节点到指定节点下
        /// </summary>
        /// <param name="node">父节点id</param>
        /// <param name="options">部门名称</param>
        /// <returns></returns>
        Task<TTreeNode> InsertNodeAsync(TTreeNode node, IRepoOptions options = null);

        /// <summary>
        /// 批量新增节点到指定节点下
        /// </summary>
        /// <param name="nodes">ParentId必填</param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<bool> BatchInsertNodeAsync(List<TTreeNode> nodes, IRepoOptions options = null);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id">删除该节点id</param>
        /// <returns></returns>
        Task<TTreeNode> RemoveNodeAsync(DbPrimaryType id, IRepoOptions options = null);

        /// <summary>
        /// 批量删除 先查再删除 物理删除
        /// </summary>
        /// <param name="list"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<bool> DeleteManyAsync(List<TTreeNode> list, IRepoOptions options = null);
        /// <summary>
        ///  修改节点到指定节点下面
        /// </summary>
        /// <param name="id">当前节点id</param>
        /// <param name="parentid">新节点的id</param>
        /// <returns></returns>
        Task<TTreeNode> ChangeParentAsync(DbPrimaryType id, NullableDbPrimaryType parentid, IRepoOptions options = null);

        /// <summary>
        /// 根据filter 筛选所有树形节点 / 如果不传递filter 则显示所有节点
        /// </summary>
        /// <param name="filte"></param>
        /// <returns></returns>

        //Task<IList<IList<ITreeNode>>> SearchTreeListAsync(string filte);
    }
}

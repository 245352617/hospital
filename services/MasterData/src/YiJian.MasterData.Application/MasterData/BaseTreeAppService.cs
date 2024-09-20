using System.Collections.Generic;
using System.Threading.Tasks;
using YiJian.ECIS.Core;
using YiJian.ECIS.ShareModel;
using DbPrimaryType = System.String;
using NullableDbPrimaryType = System.String;


namespace YiJian.MasterData.MasterData
{
    /// <summary>
    /// 所有的树的Base方法 (因为ABP垃圾这一层抽不动了)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseTreeAppService<TEntity> where TEntity : TreeNode, new()
    {
        private ITreeRepository<TEntity> _treeRepository;

        /// <summary>
        /// 所有的树的Base方法
        /// </summary>
        public BaseTreeAppService(ITreeRepository<TEntity> repo)
        {
            _treeRepository = repo;
        }

        #region 插入新节点到指定节点下 InsertNode
        /// <summary>
        /// 插入新节点到指定节点下
        /// </summary>
        /// <param name="entity">parentid 父节点id 必须传</param>
        /// <returns></returns>
        public async Task<TEntity> InsertNodeAsync(TEntity entity)
        {
            return await this._treeRepository.InsertNodeAsync(entity);
        }
        #endregion

        #region 根据id删除节点 DelNode RemoveNode
        /// <summary>
        /// 删除某个节点（物理删除）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> DeleteNodeAsync(DbPrimaryType id)
        {
            return await _treeRepository.DeleteNodeAsync(id);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id">删除该节点id</param>
        /// <returns></returns>
        public async Task<TEntity> RemoveNodeAsync(DbPrimaryType id)
        {
            return await _treeRepository.RemoveNodeAsync(id);
        }
        #endregion

        #region 变更节点到指定节点下
        /// <summary>
        /// 变更节点位置
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="parentid">父节点id</param>
        /// <returns></returns>
        public async Task<TEntity> ChangeParentAsync(DbPrimaryType id, NullableDbPrimaryType parentid)
        {
            return await this._treeRepository.ChangeParentAsync(id, parentid);
        }
        #endregion

        #region 关于节点的查询方法 GetParentIdAsync、GetFullPathByIdAsync、 GetByIdAsync、ListNodesByFullPathAsync 、ListAllNodesByIdAsync
        /// <summary>
        /// 根据id获取全路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetFullPathByIdAsync(DbPrimaryType id)
        {
            return await _treeRepository.GetFullPathByIdAsync(id);
        }

        /// <summary>
        /// 根据id获取上个节点id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetParentIdAsync(DbPrimaryType id)
        {
            return await _treeRepository.GetParentIdAsync(id);
        }
        /// <summary>
        /// 根据id获取节点信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> GetByIdAsync(DbPrimaryType id)
        {
            return await _treeRepository.GetByIdAsync(id);
        }
        /// <summary>
        /// 获取当前节点下面的所有子节点信息
        /// </summary>
        /// <param name="parentFullPath"> 父节点路径</param>
        /// <returns></returns>
        public async Task<IList<TEntity>> ListNodesByFullPathAsync(string parentFullPath)
        {
            return await _treeRepository.ListNodesByFullPathAsync(parentFullPath);
        }
        /// <summary>
        /// 根据id获取节点下所有节点数据
        /// </summary>
        /// <param name="id">当前节点id</param>
        /// <returns></returns>
        public async Task<IList<TEntity>> ListAllNodesByIdAsync(DbPrimaryType id)
        {
            return await _treeRepository.ListAllNodesByIdAsync(id);
        }
        #endregion

        #region 获取树 GetTreeByRootIdAsync
        /// <summary>
        /// 根据当前的节点id获取树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> GetTreeByRootIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = _treeRepository.Get(x => x.ParentId == null).Id;
            }
            return await _treeRepository.GetTreeByRootIdAsync(id);
        }

        /// <summary>
        /// 根据根节点名称获取包含子节点的树
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<TEntity> GetTreeByRootPathAsync(string name)
        {
            var root = await _treeRepository.GetNodeByFullPathAsync(name);
            if (root != null)
            {
                return await GetTreeByRootIdAsync(root.Id);
            }
            return null;
        }

        /// <summary>
        /// 根据当前的节点id获取树List
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IList<TEntity>> ListTreeListByRootIdAsync(string id)
        {
            List<TEntity> treelist = new List<TEntity>
            {
                await _treeRepository.GetTreeByRootIdAsync(id)
            };
            return treelist;
        }
        /// <summary>
        /// 根据节点名称查询出当前treenode集合
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IList<TEntity>> GetTreesByNameAsync(string name)
        {
            return await _treeRepository.ListTreesByNameAsync(name);
        }

        /// <summary>
        /// 根据上级节点id获取下一级的节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        public async Task<IList<TEntity>> GetChildrenByIdAsync(DbPrimaryType parentId, string KeyWord)
        {
            //根节点标志
            if (parentId == "0")
            {
                var rootNode = await _treeRepository.GetAsync(t => (string.IsNullOrEmpty(t.ParentId) || t.ParentId == "0"));
                if (string.IsNullOrWhiteSpace(KeyWord))
                {
                    return new List<TEntity> { rootNode };
                }
                return await _treeRepository.ListAsync(t => t.FullPath.Contains(rootNode.FullPath) && t.NodeName.Contains(KeyWord.Trim()), s => s.FullPath);
            }
            return await _treeRepository.ListAsync(t => t.ParentId == parentId, s => s.FullPath);
        }
        #endregion
    }
}

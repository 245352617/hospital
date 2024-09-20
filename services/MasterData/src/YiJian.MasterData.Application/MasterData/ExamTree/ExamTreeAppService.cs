using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client.Cache;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel;
using YiJian.MasterData.Domain;
using DbPrimaryType = System.String;
using NullableDbPrimaryType = System.String;

namespace YiJian.MasterData.MasterData
{
    /// <summary>
    /// ExamTreeAppService
    /// </summary>
    public class ExamTreeAppService : MasterDataAppService // BaseTreeAppService<ExamTree>,
    {
        private IExamTreeRepository _treeRepository;
        private readonly IMemoryCache _cache;
        private const string Exam_TreeList_Key = "exam:tree:list:all";
        /// <summary>
        /// 所有的树的Base方法
        /// </summary>
        public ExamTreeAppService(IExamTreeRepository repo, IMemoryCache cache)
        {
            _treeRepository = repo;
            _cache = cache;
        }

        #region 插入新节点到指定节点下 InsertNode
        /// <summary>
        /// 插入新节点到指定节点下
        /// </summary>
        /// <param name="dto">parentid 父节点id为0则表示根节点</param>
        /// <returns></returns>
        public async Task<ExamTree> InsertNodeAsync(AddTreeDto dto)
        {
            var entity = new ExamTree()
            {
                NodeName = dto.NodeName,
                CreatTime = dto.CreatTime,
                IsDelete = dto.IsDelete,
                ProjectName = dto.ProjectName,
                ProjectCode = dto.ProjectCode,
                ParentId = dto.ParentId,
                Sort = dto.Sort,
            };
            return await _treeRepository.InsertNodeAsync(entity);
        }

        /// <summary>
        /// 批量新增节点到对应节点下面
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public async Task<bool> BatchInsertNodeAsync(List<AddTreeDto> dtos)
        {
            var list = new List<ExamTree>();
            foreach (var dto in dtos)
            {
                var entity = new ExamTree()
                {
                    NodeName = dto.NodeName,
                    CreatTime = dto.CreatTime,
                    IsDelete = dto.IsDelete,
                    ProjectName = dto.ProjectName,
                    ProjectCode = dto.ProjectCode,
                    ParentId = dto.ParentId,
                    Sort = dto.Sort,
                };
                list.Add(entity);
            }
            return await _treeRepository.BatchInsertNodeAsync(list);
        }
        #endregion

        #region 根据id删除节点 DelNode RemoveNode
        /// <summary>
        /// 删除某个节点（物理删除）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ExamTree> DeleteNodeAsync(DbPrimaryType id)
        {
            return await _treeRepository.DeleteNodeAsync(id);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id">删除该节点id</param>
        /// <returns></returns>
        //public async Task<ExamTree> RemoveNodeAsync(DbPrimaryType id)
        //{
        //    return await _treeRepository.RemoveNodeAsync(id);
        //}
        #endregion

        #region 变更节点到指定节点下
        /// <summary>
        /// 变更节点位置
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="parentid">父节点id</param>
        /// <returns></returns>
        public async Task<ExamTree> ChangeParentAsync(DbPrimaryType id, NullableDbPrimaryType parentid)
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
        public async Task<ExamTree> GetByIdAsync(DbPrimaryType id)
        {
            return await _treeRepository.GetByIdAsync(id);
        }
        /// <summary>
        /// 获取当前节点下面的所有子节点信息
        /// </summary>
        /// <param name="parentFullPath"> 父节点路径</param>
        /// <returns></returns>
        public async Task<IList<ExamTree>> ListNodesByFullPathAsync(string parentFullPath)
        {
            return await _treeRepository.ListNodesByFullPathAsync(parentFullPath);
        }
        /// <summary>
        /// 根据id获取节点下所有节点数据
        /// </summary>
        /// <param name="id">当前节点id</param>
        /// <returns></returns>
        public async Task<IList<ExamTree>> ListAllNodesByIdAsync(DbPrimaryType id)
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
        public async Task<ExamTree> GetTreeByRootIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = _treeRepository.Get(x => x.ParentId == null).Id;
            }
            return await _treeRepository.GetTreeByRootIdAsync(id);
        }

        /// <summary>
        /// 根据根节点Path 获取包含子节点的树 eg: /影像/
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ExamTree> GetTreeByRootPathAsync(string name)
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
        public async Task<IList<ExamTree>> ListTreeListByRootIdAsync(string id)
        {
            List<ExamTree> treelist = new List<ExamTree>
            {
                await _treeRepository.GetTreeByRootIdAsync(id)
            };

            return treelist;
        }
        /// <summary>
        /// 根据节点名称查询出当前treenode集合
        /// </summary>
        /// <param name="name"></param>
        /// <param name="useCache">默认false 是否使用缓存</param>
        /// <returns></returns>
        public async Task<IList<ExamTree>> GetTreesByNameAsync(string name, bool useCache = false)
        {
            //查询出Project 有的数据但是 Tree 没有的数据
            var noBind = await _treeRepository.GetNoBindTreeProjectAsync();
            if (useCache)
            {
                var cacheData = _cache.Get<IList<ExamTree>>(Exam_TreeList_Key);
                if (cacheData == null)
                {
                    cacheData = await _treeRepository.ListTreesByNameAsync(name);
                    BindProject(cacheData, noBind);
                    _cache.Set(Exam_TreeList_Key, cacheData, TimeSpan.FromHours(CacheKey.EXAMCATALOGTIME));
                }
                return cacheData;
            }
            else
            {
                var list = await _treeRepository.ListTreesByNameAsync(name);
                BindProject(list, noBind);
                return list;
            }
        }

        /// <summary>
        /// 将未绑定的数据帮到未定义的节点上面
        /// </summary>
        /// <param name="examTrees"></param>
        /// <param name="noBindList"></param>
        private void BindProject(IList<ExamTree> examTrees, IList<ExamTree> noBindList)
        {
            foreach (var examTree in examTrees)
            {
                if (examTree.NodeName == "未分类")
                {
                    List<TreeNode> noBinds = new List<TreeNode>();
                    foreach (var item in noBindList)
                    {
                        item.FullPath = "/" + examTree.NodeName + "/" + item.FullPath + "/";
                        item.ParentId = examTree.Id;
                        noBinds.Add(item);
                    }
                    examTree.ChildNodes = noBinds;
                }
            }
        }

        /// <summary>
        /// 根据上级节点id获取下一级的节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="KeyWord"></param>
        /// <returns></returns>
        public async Task<IList<ExamTree>> GetChildrenByIdAsync(DbPrimaryType parentId, string KeyWord)
        {
            //根节点标志
            if (parentId == "0")
            {
                var rootNode = await _treeRepository.GetAsync(t => (string.IsNullOrEmpty(t.ParentId) || t.ParentId == "0"));
                if (string.IsNullOrWhiteSpace(KeyWord))
                {
                    return new List<ExamTree> { rootNode };
                }
                return await _treeRepository.ListAsync(t => t.FullPath.Contains(rootNode.FullPath) && t.NodeName.Contains(KeyWord.Trim()), s => s.FullPath);
            }

            if (!string.IsNullOrWhiteSpace(KeyWord))
            {
                return await _treeRepository.ListAsync(t => t.ParentId == parentId && t.NodeName.Contains(KeyWord.Trim()), s => s.FullPath);
            }
            return await _treeRepository.ListAsync(t => t.ParentId == parentId, s => s.FullPath);
        }

        /// <summary>
        /// 绑定ProjectCode
        /// </summary>
        /// <param name="updateTree"> 更新树</param>
        /// <returns></returns>
        /// <returns></returns>
        public Task<bool> UpdateProjectCodeAsync(UpdateProjectCodeDto updateTree)
        {
            return _treeRepository.UpdateProjectCodeAsync(updateTree.Id, updateTree.ProjectCode, updateTree.ProjectName);
        }

        /// <summary>
        /// 修改当前节点的节点名称和排序字段
        /// </summary>
        /// <param name="updateTree"> 更新树</param>
        /// <returns></returns>
        public Task<bool> UpdateAsync(UpdateTreeNodeDto updateTree)
        {
            return _treeRepository.UpdateTreeNodeAsync(updateTree.Id, updateTree.NodeName, updateTree.Sort);
        }

        /// <summary>
        /// 修改当前树的节点排序和项目名和code
        /// </summary>
        /// <param name="updateTree"></param>
        /// <returns></returns>
        public Task<bool> UpdateTreeAsync(UpdateTreeDto updateTree)
        {
            return _treeRepository.UpdateTreeAsync(updateTree.Id, updateTree.NodeName, updateTree.Sort, updateTree.ProjectCode, updateTree.ProjectName);
        }
        #endregion

    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel;
using DbPrimaryType = System.String;
using NullableDbPrimaryType = System.String;

namespace YiJian.ECIS.Core
{

    public class TreeRepository<TTreeNode> : Repository<TTreeNode>, ITreeRepository<TTreeNode>
        where TTreeNode : TreeNode, new()
    {
        public TreeRepository(DbContext dbContext) : base(dbContext)
        {

        }

        #region 插入新节点到指定节点下 InsertNode
        /// <summary>
        /// 插入新节点到指定节点下
        /// </summary>
        /// <param name="node">父节点id</param>
        /// <param name="options">选项 1允许字段 2.不自动调用saveChange</param>
        /// <returns></returns>
        public async Task<TTreeNode> InsertNodeAsync(TTreeNode node, IRepoOptions options = null)
        {
            if (string.IsNullOrEmpty(node.NodeName))
            {
                throw new InvalidProgramException("NodeName can not be null");
            }
            string parentPath = await GetFullPathByIdAsync(node.ParentId);//获取parentpath
            node.PyCode = node.NodeName.FirstLetterPY();
            node.FullPath = string.IsNullOrEmpty(parentPath) ? "/" + node.NodeName + "/" : parentPath + node.NodeName + "/";
            node.Id = GuidExtensions.Create(node.Id);
            return await CreateAsync(node, options);
        }

        /// <summary>
        /// 批量插入节点
        /// </summary>
        /// <param name="nodes">节点list</param>
        /// <param name="options"></param>
        /// <returns></returns> 
        public async Task<bool> BatchInsertNodeAsync(List<TTreeNode> nodes, IRepoOptions options = null)
        {
            if (nodes == null || nodes.Count == 0)
            {
                throw new InvalidProgramException("nodes 不能为空！");
            }

            var parentIds = nodes.Select(x => x.ParentId).Distinct().ToList();
            var parents = await GetListAsync(c => parentIds.Contains(c.Id));//获取parents

            //给node 赋值 id 和fullpath 
            foreach (var node in nodes)
            {
                var parent = parents.Where(c => c.Id == node.ParentId).FirstOrDefault();
                node.PyCode = node.NodeName.FirstLetterPY();
                node.FullPath = string.IsNullOrEmpty(parent?.FullPath) ? "/" + node.NodeName + "/" : parent.FullPath + node.NodeName + "/";
                node.Id = GuidExtensions.Create(node.Id);
            }
            try
            {
                await CreateRangeAsync(nodes, options);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidProgramException("新增 nodes 出现错误！Message:" + ex.Message);
            }
        }


        #endregion

        #region 根据id删除节点 DelNode RemoveNode
        /// <summary>
        /// 删除某个节点（物理删除）
        /// </summary>
        /// <param name="id">当前节点id</param> 
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<TTreeNode> DeleteNodeAsync(DbPrimaryType id, IRepoOptions options = null)
        {

            //查询出当前节点的相关信息
            var NowNode = await GetByIdAsync(id);
            if (NowNode != null)
            {
                //查询出所有的子节点
                var departList = await ListNodesByFullPathAsync(NowNode.FullPath);
                if (options == null || !options.SaveChangeBySelf)
                {
                    using (var tran = DbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            //删除该节点下面的所有子节点
                            var repoOpts = new RepoOptions() { SaveChangeBySelf = true };
                            foreach (var item in departList)
                            {
                                //不默认调用savechange 
                                await DeleteAsync(item, repoOpts);
                            }
                            //删除当前节点
                            var returnStatus = await DeleteAsync(NowNode);
                            tran.Commit();
                            return returnStatus;
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            //  如果没有异常捕获不建议这样做
                            throw new Exception(ex.Message);
                        }
                    }

                }
                else
                {
                    foreach (var item in departList)
                    {
                        await DeleteAsync(item);
                    }
                    //删除当前节点
                    var returnStatus = await DeleteAsync(NowNode);
                }
            }
            return null;
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id">删除该节点id</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<TTreeNode> RemoveNodeAsync(DbPrimaryType id, IRepoOptions options = null)
        {

            //查询出当前节点的相关信息
            var NowNode = await GetByIdAsync(GuidExtensions.Create(id), options);
            if (NowNode != null)
            {
                //查询出所有的子节点
                var departList = await ListNodesByFullPathAsync(NowNode.FullPath, options);
                if (options == null || !options.SaveChangeBySelf)
                {
                    using (var tran = DbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            var repoOpts = new RepoOptions() { SaveChangeBySelf = true };
                            //删除该节点下面的所有子节点
                            foreach (var item in departList)
                            {
                                item.IsDelete = true;
                                await ModifyAsync(item, repoOpts);
                            }
                            //删除当前节点
                            NowNode.IsDelete = true;
                            var returnStatus = await ModifyAsync(NowNode);
                            tran.Commit();
                            return returnStatus;
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            //  如果没有异常捕获不建议这样做
                            throw new Exception(ex.Message);
                        }
                    }
                }
                else
                {
                    //删除该节点下面的所有子节点
                    foreach (var item in departList)
                    {
                        item.IsDelete = true;
                        await ModifyAsync(item, options);
                        //_departmant.Update(item);
                    }
                    //删除当前节点
                    NowNode.IsDelete = true;
                    return await ModifyAsync(NowNode);
                }

            }
            return null;
        }

        /// <summary>
        /// 批量删除 先查再删除 物理删除
        /// </summary>
        /// <param name="list"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<bool> DeleteManyAsync(List<TTreeNode> list, IRepoOptions options = null)
        {
            var delList = await DeleteAsync(list);
            return delList?.Count == list.Count;
        }
        #endregion

        #region  修改节点到指定节点下面 UpdateNode
        /// <summary>
        ///  修改节点到指定节点下面
        /// </summary>
        /// <param name="id">当前节点id</param>
        /// <param name="parentid">新节点的id</param>
        /// <param name="options"> 是否自己执行saveChange</param>
        /// <returns></returns>
        public async Task<TTreeNode> ChangeParentAsync(DbPrimaryType id, NullableDbPrimaryType parentid, IRepoOptions options = null)
        {

            //查询出父节点id
            if (parentid != string.Empty)
            {
                var parentNode = parentid == GuidExtensions.Empty ? null : await GetByIdAsync(parentid);

                //查询出当前节点信息
                var nowNode = await GetByIdAsync(id, options);
                if (nowNode != null)
                {
                    if (nowNode.ParentId == parentNode.Id)//判断当前节点是否已经在parentid父节点下
                    {
                        return parentNode;
                    }
                    //查询出当前节点的子节点信息
                    var childList = await ListNodesByFullPathAsync(nowNode.FullPath, options);
                    if (options == null || !options.SaveChangeBySelf)
                    {
                        using (var tran = DbContext.Database.BeginTransaction())
                        {
                            try
                            {
                                if (childList != null)
                                {
                                    var repoOpts = new RepoOptions() { SaveChangeBySelf = true };
                                    //修改子节点的fullname
                                    foreach (var item in childList)
                                    {
                                        //新的父节点的fullpath+源fullpath删除父节点的fullpath之后的字符
                                        item.FullPath = parentNode.FullPath + nowNode.NodeName + "/" + GetBehindString(item.FullPath, nowNode.FullPath);
                                        await ModifyAsync(item, repoOpts);

                                    }
                                }
                                //修改now节点的parentid和fullname=父节点的fullpath+当前节点的部门名称
                                nowNode.FullPath = parentNode.FullPath + nowNode.NodeName + "/";
                                nowNode.ParentId = parentNode.Id;
                                var returnstatus = await ModifyAsync(nowNode);
                                tran.Commit();
                                return returnstatus;
                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();
                                throw new Exception(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        if (childList != null)
                        {
                            //修改子节点的fullname
                            foreach (var item in childList)
                            {
                                //新的父节点的fullpath+源fullpath删除父节点的fullpath之后的字符
                                item.FullPath = parentNode.FullPath + nowNode.NodeName + "/" + GetBehindString(item.FullPath, nowNode.FullPath);
                                await ModifyAsync(item, options);

                            }
                        }
                        //修改now节点的parentid和fullname=父节点的fullpath+当前节点的部门名称
                        nowNode.FullPath = parentNode.FullPath + nowNode.NodeName + "/";
                        nowNode.ParentId = parentNode.Id;
                        var returnstatus = await ModifyAsync(nowNode, options);
                    }

                }
            }
            return null;
        }
        #endregion

        #region 关于节点的查询方法 GetParentId、GetFullPathById、 GetNodeById、GetNodeListByFullPath 、GetFullTreeById
        /// <summary>
        /// 根据id获取全路径
        /// </summary>
        /// <param name="id">当前节点id</param>
        /// <returns></returns>
        public async Task<string> GetFullPathByIdAsync(DbPrimaryType id, IRepoOptions options = null)
        {
            if (!ToGuid(id))
            {
                return string.Empty;
            }
            var Node = await GetByIdAsync(id, options);
            if (Node != null)
            {
                return Node.FullPath;
            }
            return string.Empty;
        }

        public async Task<TTreeNode> GetNodeByFullPathAsync(string path, IRepoOptions options = null)
        {
            var node = await GetAsync(n => n.FullPath == path && !n.IsDelete, null, false, options);
            return node;
        }

        public TTreeNode GetNodeByFullPathNo(string path, IRepoOptions options = null)
        {
            var node = Get(n => n.FullPath == path && !n.IsDelete, options);
            return node;
        }

        /// <summary>
        /// 判断当前是否为guid是则返回guid
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static bool ToGuid(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            Guid gv = new Guid();
            try
            {
                gv = new Guid(str);
            }
            catch (Exception)
            {

            }
            if (gv != Guid.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        ///   根据id获取上个节点id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options">选项 1允许字段 2.不自动调用saveChange</param>
        /// <returns></returns>
        public async Task<NullableDbPrimaryType> GetParentIdAsync(DbPrimaryType id, IRepoOptions options = null)
        {

            if (!ToGuid(id))
            {
                return null;
            }
            var Node = await GetByIdAsync(id, options);
            if (Node != null)
            {
                return (string)Node.ParentId;
            }
            return string.Empty;
        }


        /// <summary>
        /// 获取当前节点下面的所有子节点信息
        /// </summary>
        /// <param name="parentFullPath">全路径</param>
        /// <param name="options">选项 1允许字段 2.不自动调用saveChange</param>
        /// <returns></returns>
        public async Task<IList<TTreeNode>> ListNodesByFullPathAsync(string parentFullPath, IRepoOptions options = null)
        {
            return await ListAsync(c => c.FullPath.StartsWith(parentFullPath) && c.IsDelete == false, c => c.Sort, c => c.FullPath, false, options);
        }
        /// <summary>
        /// 根据id获取节点下所有节点数据
        /// </summary>
        /// <param name="id">当前节点id</param>
        /// <param name="options">选项 1允许字段 2.不自动调用saveChange</param>
        /// <returns></returns>
        public async Task<IList<TTreeNode>> ListAllNodesByIdAsync(DbPrimaryType id, IRepoOptions options = null)
        {
            if (!ToGuid(id))
            {
                return null;
            }
            var department = await GetByIdAsync(id, options);
            if (department == null)
            {
                return null;
            }
            var returnlist = await ListAsync(c => c.FullPath.Contains(department.FullPath) && c.IsDelete == false, c => c.Sort, c => c.FullPath, false, options);
            return returnlist != null ? returnlist.ToList() : null;
        }
        #endregion

        #region 获取树 GetTreeByRootId
        /// <summary>
        /// 根据当前的节点id获取树
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options">选项 1允许字段 2.不自动调用saveChange</param>
        /// <returns></returns>
        public async Task<TTreeNode> GetTreeByRootIdAsync(DbPrimaryType id, IRepoOptions options = null)
        {

            var Node = await GetByIdAsync(id, options);
            if (Node == null)
            {
                return null;
            }
            //TreeNode treeNodes = new TreeNode()
            //{
            //    Id = Node.Id,
            //    ParentId = Node.ParentId,
            //    NodeName = Node.FullPath,
            //    FullPath = Node.FullPath
            //};
            IList<TTreeNode> childNodes = await ListAllNodesByIdAsync(id, options);
            BuildTree(childNodes, Node);
            return Node;
        }
        /// <summary>
        /// 根据数据源和当前树构建树形关系
        /// </summary>
        /// <param name="all">所有数据源</param>
        /// <param name="curItem">当前的树节点</param>
        public void BuildTree(IList<TTreeNode> all, TTreeNode curItem)
        {
            var subItems = all.Where(ee => ee.ParentId == curItem.Id).ToList();
            curItem.ChildNodes = new List<TreeNode>();
            foreach (var subItem in subItems) curItem.ChildNodes.Add(subItem);
            foreach (var subItem in subItems)
            {
                BuildTree(all, subItem);
            }
        }
        /// <summary>
        /// 根据数据源找出当前树关系
        /// </summary>
        /// <param name="Soure"></param>
        /// <returns></returns>
        public IList<TTreeNode> BuildTrees(IList<TTreeNode> Soure)
        {
            TTreeNode beforeNode = new TTreeNode();//上一个节点
            TTreeNode PNode = new TTreeNode();//当前循环正标记的根Node
            List<TTreeNode> RootNodes = new List<TTreeNode>();//历史当过父节点的集合
            var result = new List<TTreeNode>();//返回的result
            int i = 1;
            foreach (var item in Soure)
            {
                if (i == 1)
                {
                    beforeNode = item;
                    result.Add(beforeNode);
                    RootNodes.Add(item);
                    i++;
                    continue;
                }
                if (item.ParentId == beforeNode.Id)//上一个节点是当前循环节点的根节点
                {
                    PNode = beforeNode;//Nodes=当前挂载的根节点
                    beforeNode.ChildNodes = new List<TreeNode> { item };//创建新的子节点
                    beforeNode = item;
                    continue;
                }
                if (item.ParentId == PNode.Id)//如果当前循环节点的父节点 Nodes
                {
                    PNode.ChildNodes.Add(item);
                    beforeNode = item;
                    continue;
                }
                if (RootNodes.Where(c => c.Id == item.ParentId).Any())//从维护的根节点找如果找到则添加到对应的根节点
                {
                    RootNodes.Find(c => c.Id == item.ParentId).ChildNodes.Add(item);
                    continue;
                }
                //以上情况都不属于
                PNode = item;
                RootNodes.Add(item);
                result.Add(PNode);
                PNode.ChildNodes = new List<TreeNode>();//创建新的子节点
                beforeNode = item;
                continue;
            }
            return result;
        }

        /// <summary>
        /// 根据节点名称查询出当前treenode集合
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<IList<TTreeNode>> ListTreesByNameAsync(string name, IRepoOptions options = null)
        {
            if (!string.IsNullOrEmpty(name))
            {
                //获取所有的打散的节点
                var childList = await ListAsync(c => (c.NodeName.Contains(name) || c.ProjectCode.Contains(name)) && c.IsDelete == false, c => c.Sort, c => c.FullPath, false, options);
                //获取最终的叶子节点 
                RemoveParentNode(childList);
                var nodeList = new List<NullableDbPrimaryType>();
                foreach (var node in childList)
                {
                    foreach (var child in node.FullPath.Split('/'))
                    {
                        if (!string.IsNullOrEmpty(child))
                        {
                            nodeList.Add(child);
                        }
                    }
                }
                nodeList = nodeList.Distinct().ToList();
                //查询所有的节点的数据  
                var allList = await ListAsync(c => nodeList.Contains(c.NodeName) && c.IsDelete == false, c => c.Sort, c => c.FullPath, false, options);
                var returnList = BuildTreeByChildList(childList, allList);
                for (int i = 0; i < returnList.Count; i++)
                {
                    for (int j = 1 + i; j < returnList.Count; j++)
                    {
                        var (list, canMerge) = MergeTree(returnList[i], returnList[j]);
                        if (canMerge)
                        {
                            returnList.Remove(returnList[j]);
                            returnList[i] = list[0];
                            j--;
                        }
                    }
                }
                //开始构建 从叶子节点开始构造
                return returnList;
            }
            else
            {
                var allList = await ListAsync(c => c.IsDelete == false, c => c.Sort, c => c.FullPath, false, options);
                var rootList = allList.Where(c => c.ParentId == "0" || string.IsNullOrEmpty(c.ParentId)).ToList();
                foreach (var root in rootList)
                    BuildRootNodeTree(root, allList);

                return rootList;
            }
        }

        /// <summary>
        /// 根据叶子节点和 所有节点来构建树
        /// </summary>
        /// <param name="childNodes">叶子节点</param>
        /// <param name="allNodes">叶子节点上所有路径节点</param>
        /// <returns></returns>
        public IList<TTreeNode> BuildTreeByChildList(IList<TTreeNode> childNodes, IList<TTreeNode> allNodes)
        {
            var resList = new List<TTreeNode>();
            foreach (var child in childNodes)
            {
                var node = DeepCopyByExpressionTrees.DeepCopy(BuildNodeTree(allNodes, child));
                resList.Add(node);
            }
            return resList;
        }

        /// <summary>
        /// 根据叶子节点构建树 (由叶子 往根节点构造 )
        /// </summary>
        /// <param name="allNodes"> 所有的查询出来的节点</param>
        /// <param name="seedNode">叶子节点</param>
        /// <returns></returns>
        public TTreeNode BuildNodeTree(IList<TTreeNode> allNodes, TTreeNode seedNode)
        {
            foreach (var node in allNodes)
            {
                if (node.Id == seedNode.ParentId)
                {
                    node.ChildNodes = new List<TreeNode> { seedNode };
                    if (node.ParentId == "0" || string.IsNullOrWhiteSpace(node.ParentId))
                        return node;
                    else
                        return BuildNodeTree(allNodes, node);

                }
            }
            return seedNode;
        }

        /// <summary>
        /// 根据root节点构建完整的树
        /// </summary>
        /// <param name="rootNode">根节点</param>
        /// <param name="allNodes">路径上的节点</param>
        /// <returns></returns>
        public void BuildRootNodeTree(TTreeNode rootNode, IList<TTreeNode> allNodes)
        {
            var childs = allNodes.Where(c => c.ParentId == rootNode.Id).ToList();
            if (childs == null || childs.Count == 0)
            {
                return;
            }
            rootNode.ChildNodes = new List<TreeNode>();
            foreach (var item in childs)
            {
                BuildRootNodeTree(item, allNodes);
                rootNode.ChildNodes.Add(item);
            }
            return;
        }

        /// <summary>
        /// 合并树节点 如果不能合并数据返回多条， 能合并返回一条 （todo 待多层级测试）
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns></returns>
        public (List<TTreeNode>, bool) MergeTree(TreeNode node1, TreeNode node2)
        {
            var list = new List<TTreeNode>();
            var canMerge = false;
            //当前1节点和二节点的parentId 一致
            if (node1.Id == node2.ParentId)
            {
                //判断当前1节点 child 是否和 二节点一致
                canMerge = true;
                foreach (var childNode in node1.ChildNodes)
                {
                    if (childNode.Id == node2.Id)
                    {
                        foreach (var childNode2 in node2.ChildNodes)
                        {
                            MergeTree(childNode, childNode2);
                        }
                        break;
                    }
                    //最后一条了
                    if (node1.ChildNodes.Count == node1.ChildNodes.IndexOf(childNode) + 1)
                    {
                        node1.ChildNodes.Add(node2);
                        return (list, canMerge);
                    }
                }
            }
            else if (node2.ChildNodes == null || node2.ChildNodes.Count == 0)
            {
                return (list, false);
            }
            else
            {
                foreach (var child in node2.ChildNodes)
                {
                    (list, canMerge) = MergeTree(node1, child);

                }
            }

            if (!canMerge)
            {
                list.Add((TTreeNode)node1);
                list.Add((TTreeNode)node2);
                return (list, canMerge);
            }
            else
            {
                list.Add((TTreeNode)node1);
                return (list, true);
            }
        }


        /// <summary>
        /// 从list中只筛选出叶子节点的List
        /// </summary>
        /// <param name="treeNodes"></param>
        /// <returns></returns>
        public void RemoveParentNode(IList<TTreeNode> treeNodes)
        {
            foreach (var node in treeNodes)
            {
                foreach (var item in treeNodes)
                {
                    if (IsChild(item.FullPath, node.FullPath))
                    {
                        treeNodes.Remove(node);
                        RemoveParentNode(treeNodes);
                        return;
                    }
                }
            }
            return;
        }

        public bool IsChild(string sourFullPath, string fullPath)
        {
            if (sourFullPath == fullPath) return false;
            if (sourFullPath.Contains(fullPath)) return true;
            return false;
        }
        #endregion

        #region 基础方法GetBehindString
        /// <summary>
        /// 从数据源stringSoure根据关键字seed截取后面字符
        /// </summary>
        /// <param name="stringSoure"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public string GetBehindString(string stringSoure, string seed)
        {
            if (stringSoure.Contains(seed))
            {
                return stringSoure.Split(seed)[1];
            }
            return string.Empty;
        }

        #endregion

    }
}

using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using YiJian.ECIS.Core;
using YiJian.MasterData.Domain;

namespace YiJian.MasterData.EntityFrameworkCore
{
    public class LabTreeRepository : TreeRepository<LabTree>, ILabTreeRepository
    {
        public LabTreeRepository(MasterDataDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> UpdateProjectCodeAsync(string Id, string Code, string ProjectName)
        {
            if (Id == null || string.IsNullOrEmpty(Id)) { return false; }
            var NowNode = await GetByIdAsync(Id);
            if (NowNode != null)
            {
                NowNode.ProjectCode = Code;
                NowNode.ProjectName = ProjectName;
                return Update(NowNode, new System.Collections.Generic.List<string>() { "ProjectCode", "ProjectName" }) > 0;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(string Id, string Name, decimal sort)
        {
            if (Id == null || string.IsNullOrEmpty(Id)) { return false; }
            var NowNode = await GetByIdAsync(Id);
            if (NowNode != null)
            {
                NowNode.NodeName = Name;
                NowNode.PyCode = Name.FirstLetterPY();
                NowNode.Sort = sort;
                return Update(NowNode, new System.Collections.Generic.List<string>() { "NodeName", "Sort", "PyCode" }) > 0;
            }
            return false;
        }

        public async Task<bool> UpdateTreeAsync(string Id, string Name, decimal sort, string Code, string ProjectName)
        {
            if (Id == null || string.IsNullOrEmpty(Id)) { return false; }
            var NowNode = await GetByIdAsync(Id);
            if (NowNode != null)
            {
                NowNode.NodeName = Name;
                NowNode.Sort = sort;
                NowNode.PyCode = Name.FirstLetterPY();
                NowNode.ProjectCode = Code;
                NowNode.ProjectName = ProjectName;
                return Update(NowNode, new System.Collections.Generic.List<string>() { "NodeName", "Sort", "ProjectCode", "ProjectName", "PyCode" }) > 0;
            }
            return false;
        }

        public async Task<IList<LabTree>> GetNoBindTreeProjectAsync()
        {
            SqlParameter isDelet = new SqlParameter("@delete", SqlDbType.Bit)
            {
                Value = false
            };
            // 一下执行方法 必须有唯一Id 否则to list 会有问题。 同时select 的结构必须和Entity 一一映射。 两个条件缺一不可。
            var examTrees = await ExecuteQueryAsync("select Dict_LabProject.ProjectCode as Id, Dict_LabProject.ProjectCode,Dict_LabProject.ProjectName ,GETDATE()as  CreatTime , Dict_LabProject.ProjectName as NodeName , @delete as IsDelete, 0.00 as Sort ,Dict_LabProject.PyCode,''as ParentId , Dict_LabProject.ProjectName as FullPath\r\nfrom Dict_LabProject left join Dict_LabTree on Dict_LabProject.ProjectCode = Dict_LabTree.ProjectCode \r\nwhere Dict_LabProject.IsDeleted = 0 and  Dict_LabTree.FullPath is null;", isDelet);
            return examTrees.ToList();
        }
    }
}

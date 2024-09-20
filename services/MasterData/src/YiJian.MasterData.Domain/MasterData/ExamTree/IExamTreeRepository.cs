using System.Collections.Generic;
using System.Threading.Tasks;
using YiJian.ECIS.Core;

namespace YiJian.MasterData.Domain
{
    /// <summary>
    /// IExamTreeRepository 使用的是非ABP 框架的写法。更自由更可控。
    /// </summary>
    public interface IExamTreeRepository : ITreeRepository<ExamTree>
    {
        /// <summary>
        /// 绑定projectCode
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Code">项目code</param>
        /// <param name="ProjectName">项目名</param>
        /// <returns></returns>
        public Task<bool> UpdateProjectCodeAsync(string Id, string Code, string ProjectName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name">节点名</param>
        /// <param name="sort">节点排序</param>
        /// <returns></returns>
        public Task<bool> UpdateTreeNodeAsync(string Id, string Name, decimal sort);

        /// <summary>
        /// 跟新树上的节点名和project 属性
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="sort"></param>
        /// <param name="Code"></param>
        /// <param name="ProjectName"></param>
        /// <returns></returns>
        public Task<bool> UpdateTreeAsync(string Id, string Name, decimal sort, string Code, string ProjectName);

        /// <summary>
        /// 获取未绑定的Project
        /// </summary>
        /// <returns></returns>
        public Task<IList<ExamTree>> GetNoBindTreeProjectAsync();

    }
}

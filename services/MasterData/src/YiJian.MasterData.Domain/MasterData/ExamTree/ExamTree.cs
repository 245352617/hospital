using System;
using YiJian.ECIS.ShareModel;

namespace YiJian.MasterData.Domain
{
    /// <summary>
    /// 检查项目树型实体类 （用于自己维护树形构造）
    /// </summary>
    public class ExamTree : TreeNode
    {
    

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatTime { set; get; }

    }
}

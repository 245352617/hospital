using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData.Exams
{
    /// <summary>
    /// 描    述:检查附加项仓储接口
    /// 创 建 人:杨凯
    /// 创建时间:2023/11/20 17:16:38
    /// </summary>
    public interface IExamAttachItemsRepository : IRepository<ExamAttachItem, Guid>
    {
    }
}

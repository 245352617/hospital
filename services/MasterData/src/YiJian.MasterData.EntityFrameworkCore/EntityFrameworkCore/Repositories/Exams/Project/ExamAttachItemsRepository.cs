using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.Exams;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 描    述:检查附加项仓储实现
    /// 创 建 人:杨凯
    /// 创建时间:2023/11/20 17:19:16
    /// </summary>
    public class ExamAttachItemsRepository : MasterDataRepositoryBase<ExamAttachItem, Guid>, IExamAttachItemsRepository
    {
        public ExamAttachItemsRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}

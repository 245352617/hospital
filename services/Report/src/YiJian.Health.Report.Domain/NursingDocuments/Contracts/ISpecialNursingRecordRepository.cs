using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Health.Report.NursingDocuments.Contracts
{
    /// <summary>
    /// 描述：特殊护理记录仓储接口
    /// 创建人： yangkai
    /// 创建时间：2023/3/31 10:07:44
    /// </summary>
    public interface ISpecialNursingRecordRepository : IRepository<SpecialNursingRecord, Guid>
    {
    }
}

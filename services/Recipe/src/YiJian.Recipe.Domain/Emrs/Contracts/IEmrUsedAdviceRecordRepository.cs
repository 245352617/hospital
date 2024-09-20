namespace YiJian.Emrs.Contracts
{
    using System;
    using Volo.Abp.Domain.Repositories;
    using YiJian.Emrs.Entities;

    /// <summary>
    /// 电子病历导入医嘱相关服务
    /// </summary>  
    public interface IEmrUsedAdviceRecordRepository : IRepository<EmrUsedAdviceRecord, Guid>
    {

    }

}
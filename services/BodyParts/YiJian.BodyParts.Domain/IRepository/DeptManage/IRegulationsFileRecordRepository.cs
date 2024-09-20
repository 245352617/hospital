using YiJian.BodyParts.Model;
using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.BodyParts.IRepository
{
    /// <summary>
    /// 表：文件记录表
    /// </summary>
    public interface IFileRecordRepository : IRepository<FileRecord, Guid>, IBaseRepository<FileRecord, Guid>
    {
    }
}

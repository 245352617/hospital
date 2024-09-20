using YiJian.BodyParts.EntityFrameworkCore;
using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.BodyParts.Repository
{
    /// <summary>
    /// 表：文件记录表
    /// </summary>
    public class FileRecordRepository : BaseRepository<DbContext, FileRecord, Guid>, IFileRecordRepository
    {
        public FileRecordRepository(IDbContextProvider<DbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        #region 实现接口

        #endregion
    }
}

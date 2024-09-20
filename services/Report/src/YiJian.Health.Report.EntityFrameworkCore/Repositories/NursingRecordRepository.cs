using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Health.Report.EntityFrameworkCore;
using YiJian.Health.Report.NursingDocuments;
using YiJian.Health.Report.NursingDocuments.Contracts;

namespace YiJian.Health.Report.Repositories
{
    /// <summary>
    /// 护理记录
    /// </summary>
    public class NursingRecordRepository : EfCoreRepository<ReportDbContext, NursingRecord, Guid>, INursingRecordRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public NursingRecordRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 查询护理记录单信息
        /// </summary>
        /// <param name="nursingDocumentId"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public async Task<List<NursingRecord>> GetRecordListAsync(Guid nursingDocumentId, DateTime? begintime, DateTime? endtime, int sheetIndex = 0)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .Include(i => i.Mmol)
                .Include(i => i.Pupil)
                .Include(i => i.Intakes)
                .Include(i => i.SpecialNursings)
                .Where(w => w.NursingDocumentId == nursingDocumentId && w.SheetIndex == sheetIndex)
                .WhereIf(begintime.HasValue, w => w.RecordTime >= begintime)
                .WhereIf(endtime.HasValue, w => w.RecordTime <= endtime)
                .OrderBy(o => o.CreationTime)
                .ToListAsync();
            return query;
        }

        /// <summary>
        /// 查询护理记录单信息
        /// </summary>
        /// <param name="nursingDocumentId"></param> 
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public async Task<List<NursingRecord>> GetPrintMoreRecordListAsync(Guid nursingDocumentId, DateTime? begintime, DateTime? endtime)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .Include(i => i.Mmol)
                .Include(i => i.Pupil)
                .Include(i => i.Intakes)
                .Include(i => i.SpecialNursings)
                .Where(w => w.NursingDocumentId == nursingDocumentId)
                .WhereIf(begintime.HasValue, w => w.CreationTime >= begintime)
                .WhereIf(endtime.HasValue, w => w.CreationTime <= endtime)
                .OrderBy(o => o.CreationTime)
                .ToListAsync();
            return query;
        }

        /// <summary>
        /// 获取指定记录的信息集合
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<NursingRecord>> GetNursingRecordListAsync(List<Guid> ids)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                 .Include(i => i.Mmol)
                 .Include(i => i.Pupil)
                 .Include(i => i.Intakes)
                 .Where(w => ids.Contains(w.Id))
                 .ToListAsync();
            return query;
        }

        /// <summary>
        /// 获取整个表单的记录信息集合
        /// </summary>
        /// <returns></returns> 
        public async Task<List<NursingRecord>> GetEmrNursingRecordsAsync(Guid nursingDocumentId, DateTime? begintime, DateTime? endtime)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .Include(i => i.Mmol)
                .Include(i => i.Pupil)
                .Include(i => i.Intakes)
                .Where(w => w.NursingDocumentId == nursingDocumentId)
                .WhereIf(begintime.HasValue, w => w.CreationTime >= begintime.Value)
                .WhereIf(endtime.HasValue, w => w.CreationTime < endtime.Value.AddDays(1))
                .OrderBy(o => o.SheetIndex)
                .ThenBy(o => o.RecordTime)
                .ToListAsync();

            var sql = query.ToString();

            return query;
        }

    }
}

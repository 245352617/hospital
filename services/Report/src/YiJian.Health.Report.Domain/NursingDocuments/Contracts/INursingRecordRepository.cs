using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Health.Report.NursingDocuments.Contracts
{
    /// <summary>
    /// 护理记录
    /// </summary>
    public interface INursingRecordRepository : IRepository<NursingRecord, Guid>
    {
        /// <summary>
        /// 查询护理记录单信息
        /// </summary>
        /// <param name="nursingDocumentId"></param> 
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <param name="sheetIndex"></param>
        /// <returns></returns>
        public Task<List<NursingRecord>> GetRecordListAsync(Guid nursingDocumentId, DateTime? begintime, DateTime? endtime, int sheetIndex = 0);

        /// <summary>
        /// 查询护理记录单信息
        /// </summary>
        /// <param name="nursingDocumentId"></param> 
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public Task<List<NursingRecord>> GetPrintMoreRecordListAsync(Guid nursingDocumentId, DateTime? begintime, DateTime? endtime);

        /// <summary>
        /// 获取指定记录的信息集合
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task<List<NursingRecord>> GetNursingRecordListAsync(List<Guid> ids);


        /// <summary>
        /// 获取整个表单的记录信息集合
        /// </summary>
        /// <returns></returns> 
        public Task<List<NursingRecord>> GetEmrNursingRecordsAsync(Guid nursingDocumentId, DateTime? begintime, DateTime? endtime);
    }
}

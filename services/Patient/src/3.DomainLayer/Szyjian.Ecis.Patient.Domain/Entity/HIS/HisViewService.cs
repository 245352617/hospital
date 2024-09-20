using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 获取his视图
    /// </summary>
    public class HisViewService : DomainService, IHisViewService
    {
        private readonly ILogger<HisViewService> _log;
        private readonly IFreeSql<His_View> _freeSql;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="log"></param>
        /// <param name="freeSql"></param>
        public HisViewService(ILogger<HisViewService> log, IFreeSql<His_View> freeSql)
        {
            _log = log;
            _freeSql = freeSql;
        }

        /// <summary>
        /// 获取his挂号视图
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<V_JHJK_HZLB>> GetRegisterListAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _freeSql.Select<V_JHJK_HZLB>().Where(x => (x.DLID == 1909 || x.DLID == 1912)).ToListAsync(cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _log.LogError("查询his视图异常:{Msg}", e);
                return null;
            }
        }
    }
}
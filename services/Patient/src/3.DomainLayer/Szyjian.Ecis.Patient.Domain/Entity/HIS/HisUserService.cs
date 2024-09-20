using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 获取his用户
    /// </summary>
    public class HisUserService : DomainService, IHisUserService
    {
        private readonly ILogger<HisUserService> _log;
        private readonly IFreeSql<His_Ids> _freeSql;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="log"></param>
        /// <param name="freeSql"></param>
        public HisUserService(ILogger<HisUserService> log, IFreeSql<His_Ids> freeSql)
        {
            _log = log;
            _freeSql = freeSql;
        }

        /// <summary>
        /// 获取his用户信息
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<His_Users>> GetHisUsersAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _freeSql.Select<His_Users>()
                    .ToListAsync(cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _log.LogError("Expire GetHisUsersAsync Error.Msg:{Msg}", e);
                return null;
            }
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
                return await _freeSql.Select<V_JHJK_HZLB>()
                    .ToListAsync(cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                _log.LogError("Expire GetHisUsersAsync Error.Msg:{Msg}", e);
                return null;
            }
        }
    }
}
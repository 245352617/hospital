using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.Writes.Entities;

namespace YiJian.EMR.Writes.Contracts
{
    /// <summary>
    /// Minio对象存储采集表
    /// </summary>
    public interface IMinioEmrInfoRepository : IRepository<MinioEmrInfo, Guid>
    {
        /// <summary>
        /// 批量添加Minio采集到的PDF信息
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public  Task AddAsync(List<MinioEmrInfo> entities);
         
        /// <summary>
        /// 采集当天的所有病历记录
        /// </summary>
        /// <returns></returns>
        public  Task<List<AddMinioEmrInfo>> GetEmrDataAsync(bool isAll = false);
    }
}

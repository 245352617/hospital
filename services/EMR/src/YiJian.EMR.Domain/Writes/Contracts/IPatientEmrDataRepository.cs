using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.Writes.Entities;

namespace YiJian.EMR.Writes.Contracts
{
    /// <summary>
    /// 绑定的电子病历提取的数据
    /// </summary>
    public interface IPatientEmrDataRepository : IRepository<PatientEmrData, Guid>
    { 

    }
}

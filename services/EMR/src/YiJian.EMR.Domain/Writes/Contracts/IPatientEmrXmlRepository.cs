using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.Writes.Entities;

namespace YiJian.EMR.Writes.Contracts
{
    /// <summary>
    /// 患者的电子病历xml文档
    /// </summary>
    public interface IPatientEmrXmlRepository : IRepository<PatientEmrXml, Guid>
    {

    }
}

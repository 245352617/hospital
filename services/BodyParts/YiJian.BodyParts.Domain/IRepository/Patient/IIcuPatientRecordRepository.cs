//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using YiJian.BodyParts.Application.Contracts.Dtos.Patient;
//using YiJian.BodyParts.Dtos;
//using YiJian.BodyParts.Model;
//using Volo.Abp.Domain.Repositories;

//namespace YiJian.BodyParts.IRepository
//{
//    /// <summary>
//    /// 表:患者记录表
//    /// </summary>
//    public interface IIcuPatientRecordRepository : IRepository<IcuPatientRecord, Guid>, IBaseRepository<IcuPatientRecord, Guid>
//    {
//        List<IcuPatientRecord> GetDeathNumberAsync(List<IcuPatientRecord> records);

//        Task<IcuPatientRecord> GetPatientRecord(string PI_ID);

//        /// <summary>
//        /// 查询所有在科患者
//        /// </summary>
//        /// <param name="dateTime"></param>
//        /// <returns></returns>
//        Task<List<IcuPatientRecord>> GetPatientRecordList(string PI_ID);

//        /// <summary>
//        /// 查询所有在科患者和24小时内出科的患者
//        /// </summary>
//        /// <param name="PI_ID"></param>
//        /// <returns></returns>
//        Task<List<IcuPatientRecord>> GetPatientIndeptOrOutdept24(DateTime dateTime);

//        /// <summary>
//        /// 通过patientid获取对应当前在科的PI_ID
//        /// </summary>
//        /// <param name="patientId"></param>
//        /// <returns></returns>
//        Task<string> GetPI_IDByPatientIdAsync(string patientId);
//    }
//}

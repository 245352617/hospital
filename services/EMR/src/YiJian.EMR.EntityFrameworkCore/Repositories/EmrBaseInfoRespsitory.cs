using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.EntityFrameworkCore;
using YiJian.EMR.Writes.Contracts;
using YiJian.EMR.Writes.Entities;

namespace YiJian.EMR.Repositories
{
    /// <summary>
    /// 电子病例采集的基础信息
    /// </summary>
    public class EmrBaseInfoRepository : EfCoreRepository<EMRDbContext, EmrBaseInfo, Guid>, IEmrBaseInfoRepository
    {
        /// <summary>
        /// 电子病例采集的基础信息
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public EmrBaseInfoRepository(IDbContextProvider<EMRDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 根据挂号识别号获取采集过来的患者的电子病例基础信息
        /// </summary>
        /// <param name="registerNo"></param>
        /// <returns></returns> 
        public async Task<EmrBaseInfo> GetByAsync(string registerNo)
        {
            var db = await GetDbContextAsync();
            return await db.EmrBaseInfos.FirstOrDefaultAsync(w => w.RegisterNo == registerNo);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(EmrBaseInfo model)
        {
            var db = await GetDbContextAsync();
            var ret = await db.EmrBaseInfos.AddAsync(model);
            return ret.Entity.Id;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> ModifyAsync(EmrBaseInfo model)
        {
            var db = await GetDbContextAsync();
            var entity = await db.EmrBaseInfos.FirstOrDefaultAsync(w => w.Id == model.Id);
            entity.Update(
                chiefComplaint: model.ChiefComplaint,
                historyPresentIllness: model.HistoryPresentIllness,
                allergySign: model.AllergySign,
                medicalHistory: model.MedicalHistory,
                bodyExam: model.BodyExam,
                preliminaryDiagnosis: model.PreliminaryDiagnosis,
                handlingOpinions: model.HandlingOpinions,
                outpatientSurgery: model.OutpatientSurgery,
                auxiliaryExamination: model.AuxiliaryExamination);

            var ret = db.EmrBaseInfos.Update(entity);
            return ret.Entity.Id;
        }


    }
}

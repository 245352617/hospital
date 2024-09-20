using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.DoctorsAdvices.Entities;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipes.DoctorsAdvices.Contracts;

namespace YiJian.Recipe.Repositories.DoctorsAdvices
{
    /// <summary>
    /// 疫苗接种记录仓储
    /// </summary>
    public class ImmunizationRecordRepository : EfCoreRepository<RecipeDbContext, ImmunizationRecord, Guid>, IImmunizationRecordRepository
    {
        public ImmunizationRecordRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
         : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 记录一条新的接种记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddRecordAsync(ImmunizationRecord model)
        {
            var db = await GetDbSetAsync();
            //var res = await db.Where(w => model.DoctorAdviceId == w.DoctorAdviceId).OrderByDescending(o => o.RecordTime).FirstOrDefaultAsync(); 
            //if (res != null)
            //{
            //    var date = res.RecordTime;
            //    //当天只能记录一次，多次就跳过了
            //    if (model.RecordTime.Year == date.Year && model.RecordTime.Month == date.Month && model.RecordTime.Day == date.Day) Oh.Error("同一天不能重复开同一个疫苗");
            //}

            var entity = await db.AddAsync(model);
        }

        /// <summary>
        /// 获取一条还未提交过的接种记录信息
        /// </summary>
        /// <param name="doctorAdviceId"></param>
        /// <returns></returns>
        public async Task<ImmunizationRecord> GetByAdviceIdAsync(Guid doctorAdviceId)
        {
            var db = await GetDbSetAsync();
            var entity = await db.OrderByDescending(o => o.RecordTime).FirstOrDefaultAsync(w => w.DoctorAdviceId == w.DoctorAdviceId && !w.Confirmed);
            return entity;
        }

        /// <summary>
        /// 根据患者Id获取患者所有的接种记录
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<IList<ImmunizationRecord>> GetListByPatientIdAsync(string patientId)
        {
            var db = await GetDbSetAsync();
            var list = await db.Where(w => w.Confirmed && w.PatientId == patientId).ToListAsync();
            return list;
        }

        /// <summary>
        /// 更新指定的疫苗接种记录为已提交
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Guid> ConfirmedAsync(Guid id)
        {
            var db = await GetDbSetAsync();
            var entity = await db.FirstOrDefaultAsync(w => w.Id == id);
            entity.Confirmed = true;
            db.Update(entity);
            return entity.Id;
        }

    }
}

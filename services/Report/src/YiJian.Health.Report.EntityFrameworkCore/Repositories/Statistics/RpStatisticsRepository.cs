using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Health.Report.EntityFrameworkCore;
using YiJian.Health.Report.Statisticses.Contracts;
using YiJian.Health.Report.Statisticses.Entities;
using YiJian.Health.Report.Statisticses.Models;

namespace YiJian.Health.Report.Repositories.Statistics
{
    /// <summary>
    /// 数据采集
    /// </summary>
    public class RpStatisticsRepository : EfCoreRepository<ReportDbContext, StatisticsArea, int>, IStatisticsRepository
    {
        /// <summary>
        /// 数据采集
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public RpStatisticsRepository(IDbContextProvider<ReportDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 获取患者记录数(患者，诊断，病历)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> GetAdmissionRecordCountAsync(InputAdmissionRecordModel model)
        {
            var ctx = await GetDbContextAsync();
            var sql = @$"EXEC usp_GetAdmissionRecordCountByPage 
            @beginDate='{model.BeginDate.Value}',
           @endDate='{model.EndDate.Value}',
           @visitNo=N'{model.VisitNo}',
           @patientName=N'{model.PatientName}',
           @doctorName=N'{model.DoctorName}',
           @sex=N'{model.Sex}',
           @deptCode=N'{model.DeptCode}',
           @diagnose=N'{model.Diagnose}',
           @emrTitle=N'{model.EmrTitle}',
           @doctorsAdvice=N'{model.DoctorsAdvice}',

            @patientInfo_narrationName=N'{model.patientInfo_narrationName}',         --主诉
            @patientInfo_presentMedicalHistory=N'{model.patientInfo_presentMedicalHistory}', --现病史
            @patientInfo_pastMedicalHistory=N'{model.patientInfo_pastMedicalHistory}',    --既往史
            @patientInfo_Physicalexamination=N'{model.patientInfo_Physicalexamination}',   --体格检查
            @medicalInfo_aidPacs=N'{model.medicalInfo_aidPacs}',              --辅助检查结果
            @medicalInfo_treatOpinion=N'{model.medicalInfo_treatOpinion}',         --处理意见
            @medicalInfo_courseOfDisease=N'{model.medicalInfo_courseOfDisease}',       --病程记录
            @patientInfo_diagnoseName=N'{model.patientInfo_diagnoseName}',      --病历诊断

            @patientInfo_keyDiseasesName=N'{model.patientInfo_keyDiseasesName}',       --重点病种
            @patientInfo_allergyHistory=N'{model.patientInfo_allergyHistory}',       --过敏史
            @patientInfo_infectiousHistory=N'{model.patientInfo_infectiousHistory}',     --传染病史
            @patientInfo_idNo=N'{model.patientInfo_idNo}',              --身份证号
            @medicalInfo_changesInCondition=N'{model.medicalInfo_changesInCondition}',    --病情变化情况
            @medicalInfo_situationAfterRescue=N'{model.medicalInfo_situationAfterRescue}',  --抢救后情况
            @medicalInfo_obsRoundRemark=N'{model.medicalInfo_obsRoundRemark}',       --留观病程查房记录
            @medicalInfo_consultationDept=N'{model.medicalInfo_consultationDept}',      --会诊科室
            @medicalInfo_obsSituation=N'{model.medicalInfo_obsSituation}',         --留观情况	
            @medicalInfo_participateInRescuers=N'{model.medicalInfo_participateInRescuers}' --参与抢救人员 ";

            var query = ctx.TotalCount.FromSqlRaw<TotalCount>(sql);
            var list = await query.ToListAsync();
            var result = list.FirstOrDefault();

            return (int)result?.Cnt;
        }

        /// <summary>
        /// 分页获取患者列表(患者，诊断，病历)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<AdmissionRecord>> GetAdmissionRecordPageAsync(InputAdmissionRecordByPageModel model)
        {
            var ctx = await GetDbContextAsync();
            if (model.PageSize == 0 && model.PageIndex == 0)
            {
                model.PageSize = 50; model.PageIndex = 1;
            }

            var sql = @$"EXEC usp_GetAdmissionRecordByPage 
           @beginDate='{model.BeginDate.Value}',
           @endDate='{model.EndDate.Value}',
           @visitNo=N'{model.VisitNo}',
           @patientName=N'{model.PatientName}',
           @doctorName=N'{model.DoctorName}',
           @sex=N'{model.Sex}',
           @deptCode=N'{model.DeptCode}',
           @diagnose=N'{model.Diagnose}',
           @emrTitle=N'{model.EmrTitle}',
           @doctorsAdvice=N'{model.DoctorsAdvice}',

            @patientInfo_narrationName=N'{model.patientInfo_narrationName}',         --主诉
            @patientInfo_presentMedicalHistory=N'{model.patientInfo_presentMedicalHistory}', --现病史
            @patientInfo_pastMedicalHistory=N'{model.patientInfo_pastMedicalHistory}',    --既往史
            @patientInfo_Physicalexamination=N'{model.patientInfo_Physicalexamination}',   --体格检查
            @medicalInfo_aidPacs=N'{model.medicalInfo_aidPacs}',              --辅助检查结果
            @medicalInfo_treatOpinion=N'{model.medicalInfo_treatOpinion}',         --处理意见
            @medicalInfo_courseOfDisease=N'{model.medicalInfo_courseOfDisease}',       --病程记录
            @patientInfo_diagnoseName=N'{model.patientInfo_diagnoseName}',      --病历诊断

            @patientInfo_keyDiseasesName=N'{model.patientInfo_keyDiseasesName}',       --重点病种
            @patientInfo_allergyHistory=N'{model.patientInfo_allergyHistory}',       --过敏史
            @patientInfo_infectiousHistory=N'{model.patientInfo_infectiousHistory}',     --传染病史
            @patientInfo_idNo=N'{model.patientInfo_idNo}',              --身份证号
            @medicalInfo_changesInCondition=N'{model.medicalInfo_changesInCondition}',    --病情变化情况
            @medicalInfo_situationAfterRescue=N'{model.medicalInfo_situationAfterRescue}',  --抢救后情况
            @medicalInfo_obsRoundRemark=N'{model.medicalInfo_obsRoundRemark}',       --留观病程查房记录
            @medicalInfo_consultationDept=N'{model.medicalInfo_consultationDept}',      --会诊科室
            @medicalInfo_obsSituation=N'{model.medicalInfo_obsSituation}',         --留观情况	
            @medicalInfo_participateInRescuers=N'{model.medicalInfo_participateInRescuers}', --参与抢救人员 

           @pageSize={model.PageSize},
           @pageNumber={model.PageIndex}";

            var query = ctx.AdmissionRecord.FromSqlRaw<AdmissionRecord>(sql);
            var result = await query.ToListAsync();

            int index = 1;
            foreach (var item in result)
            {
                item.Row = index++;
            }

            return result;
        }

        /// <summary>
        /// 获取患者列表(患者，诊断，病历)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<AdmissionRecord>> GetAdmissionRecordAsync(InputAdmissionRecordModel model)
        {
            var ctx = await GetDbContextAsync();
            var sql = @$"EXEC usp_GetAdmissionRecord 
           @beginDate='{model.BeginDate}',
           @endDate='{model.EndDate}',
           @visitNo=N'{model.VisitNo}',
           @patientName=N'{model.PatientName}',
           @doctorName=N'{model.DoctorName}',
           @sex=N'{model.Sex}',
           @deptCode=N'{model.DeptCode}',
           @diagnose=N'{model.Diagnose}',
           @emrTitle=N'{model.EmrTitle}',
           @doctorsAdvice=N'{model.DoctorsAdvice}',

            @patientInfo_narrationName=N'{model.patientInfo_narrationName}',         --主诉
            @patientInfo_presentMedicalHistory=N'{model.patientInfo_presentMedicalHistory}', --现病史
            @patientInfo_pastMedicalHistory=N'{model.patientInfo_pastMedicalHistory}',    --既往史
            @patientInfo_Physicalexamination=N'{model.patientInfo_Physicalexamination}',   --体格检查
            @medicalInfo_aidPacs=N'{model.medicalInfo_aidPacs}',              --辅助检查结果
            @medicalInfo_treatOpinion=N'{model.medicalInfo_treatOpinion}',         --处理意见
            @medicalInfo_courseOfDisease=N'{model.medicalInfo_courseOfDisease}',       --病程记录
            @patientInfo_diagnoseName=N'{model.patientInfo_diagnoseName}',      --病历诊断

            @patientInfo_keyDiseasesName=N'{model.patientInfo_keyDiseasesName}',       --重点病种
            @patientInfo_allergyHistory=N'{model.patientInfo_allergyHistory}',       --过敏史
            @patientInfo_infectiousHistory=N'{model.patientInfo_infectiousHistory}',     --传染病史
            @patientInfo_idNo=N'{model.patientInfo_idNo}',              --身份证号
            @medicalInfo_changesInCondition=N'{model.medicalInfo_changesInCondition}',    --病情变化情况
            @medicalInfo_situationAfterRescue=N'{model.medicalInfo_situationAfterRescue}',  --抢救后情况
            @medicalInfo_obsRoundRemark=N'{model.medicalInfo_obsRoundRemark}',       --留观病程查房记录
            @medicalInfo_consultationDept=N'{model.medicalInfo_consultationDept}',      --会诊科室
            @medicalInfo_obsSituation=N'{model.medicalInfo_obsSituation}',         --留观情况	
            @medicalInfo_participateInRescuers=N'{model.medicalInfo_participateInRescuers}' --参与抢救人员 ";


            var query = ctx.AdmissionRecord.FromSqlRaw<AdmissionRecord>(sql);
            var result = await query.ToListAsync();


            return result;
        }
    }
}

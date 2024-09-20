using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SamJan.MicroService.PreHospital.Core;
using SamJan.MicroService.PreHospital.TriageService;
using Volo.Abp.Application.Dtos;
using Volo.Abp.EntityFrameworkCore;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class RegisterInfoRepository : BaseRepository<PreHospitalTriageDbContext, RegisterInfo, Guid>,
        IRegisterInfoRepository
    {
        private readonly NLogHelper _log;
        private readonly IConfiguration _configuration;
        private readonly IDapperRepository _dapperRepository;

        public RegisterInfoRepository(IDbContextProvider<PreHospitalTriageDbContext> dbContextProvider,
            NLogHelper log, IConfiguration configuration, IDapperRepository dapperRepository)
            : base(dbContextProvider)
        {
            _log = log;
            _configuration = configuration;
            _dapperRepository = dapperRepository;
        }


        /// <summary>
        /// 分页查询分诊患者基本信息以及挂号信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<PatientRegisterInfoDto>> GetPatientRegisterInfoListAsync(
            PagedPatientRegisterInput input)
        {
            var res = new PagedResultDto<PatientRegisterInfoDto>();
            try
            {
                var linq = (from a in DbContext.PatientInfo
                            join b in DbContext.RegisterInfo on a.Id equals b.PatientInfoId
                            select new PatientRegisterInfoDto
                            {
                                Id = a.Id,
                                Address = a.Address,
                                Age = a.Age,
                                Birthday = a.Birthday,
                                CardNo = a.CardNo,
                                ChargeType = a.ChargeType,
                                ContactsPerson = a.ContactsPerson,
                                ContactsPhone = a.ContactsPhone,
                                PatientId = a.PatientId,
                                PatientName = a.PatientName,
                                Sex = a.Sex,
                                IdentityNo = a.IdentityNo,
                                GreenRoad = a.GreenRoadCode,
                                RFID = a.RFID,
                                DiseaseCode = a.DiseaseCode,
                                Py = a.Py,
                                TaskInfoId = a.TaskInfoId,
                                CarNum = a.CarNum,
                                VisitNo = b.VisitNo,
                                ToHospitalWay = a.ToHospitalWayCode,
                                Identity = a.Identity,
                                Nation = a.Nation,
                                OnsetTime = a.OnsetTime,
                                StartTriageTime = a.StartTriageTime,
                                CreationTime = a.CreationTime,
                                IsBasicInfoReadOnly = a.IsBasicInfoReadOnly,
                                IsCovidExamFromOuterSystem = a.IsCovidExamFromOuterSystem,
                                RegisterInfo = new RegisterInfoDto
                                {
                                    RegisterNo = b.RegisterNo,
                                    RegisterDeptCode = b.RegisterDeptCode,
                                    RegisterDoctorCode = b.RegisterDoctorCode,
                                    RegisterDoctorName = b.RegisterDoctorName,
                                    RegisterTime = b.RegisterTime
                                }
                            })
                    .WhereIf(!string.IsNullOrWhiteSpace(input.DeptCode), x =>
                        x.RegisterInfo.RegisterDeptCode == input.DeptCode)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.DoctorCode), x =>
                        x.RegisterInfo.RegisterDoctorCode == input.DoctorCode)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.SearchText),
                        x => input.SearchText.Contains(x.PatientId)
                             || input.SearchText.Contains(x.PatientName)
                             || input.SearchText.Contains(x.CardNo));

                var count = await linq.CountAsync();
                var list = await linq.Skip((input.SkipCount - 1) * input.MaxResultCount)
                    .Take(input.MaxResultCount)
                    .ToListAsync();

                res.TotalCount = count;
                res.Items = list;
                return res;
            }
            catch (Exception e)
            {
                _log.Warning("【RegisterInfoRepository】【GetPatientRegisterInfoListAsync】" +
                             $"【获取分诊患者基本信息以及挂号信息错误】【Msg：{e}】");
                return res;
            }
        }


        /// <summary>
        /// 获取北大 HIS 急诊科挂号患者列表
        /// </summary>
        /// <returns></returns>
        public async Task<HisRegisterPatient> GetHisPatientInfoAsync(string registerNo)
        {
            string sql =
                $"Select top(1) PatientID, PatientName, GHXH RegisterNo, mzhm VisitNo, Sex, DateOfBirth, GHSJ RegisterTime, DLID QueueId, KSDM DepartCode, " +
                $"YSDM DoctorCode, KSSJ BeginTime, JSSJ EndTime, JZZT, THBZ RefundStatus, SFZH IdentityNo, BRXZ ChargeType, XZMC ChargeTypeName, " +
                $"JTDH ContactsPhone, FZJB TriageLevel, HKDZ, YGXM DoctorName " +
                $" from v_jhjk_hzlb Where GHSJ >= DATEADD(Day, -1, GetDate()) and GHXH='{registerNo}' order by GHSJ desc";
            var connectionStringKey = _configuration.GetConnectionString("PekingUniversityHIS");
            var hisPatientInfos = await this._dapperRepository.QueryFirstOrDefaultAsync<HisRegisterPatient>(sql,
                dbKey: "PekingUniversityHIS", connectionStringKey: connectionStringKey);

            return hisPatientInfos;
        }
    }
}
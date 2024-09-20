//using YiJian.BodyParts.Dtos;
//using YiJian.BodyParts.IRepository;
//using YiJian.BodyParts.IService;
//using YiJian.BodyParts.Model;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Volo.Abp.Application.Services;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Mvc;
//using Serilog;
//using Volo.Abp.Guids;
//using Microsoft.AspNetCore.Http;

//namespace YiJian.BodyParts.Service
//{
//    /// <summary>
//    /// 患者字典设置服务
//    /// </summary>
//    [DbDescription("患者字典设置服务")]
//    public class IcuPatientDicAppService : ApplicationService, IIcuPatientDicAppService
//    {
//        private readonly IGuidGenerator _guidGenerator;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private IIcuParaItemRepository _icuParaItemRepository;
//        private IIcuPatientRecordRepository _icuPatientRecordRepository;
//        private IIcuDeptScheduleRepository _icuDeptScheduleRepository;
//        private IIcuParaModuleRepository _icuParaModuleRepository;
//        private IIcuSysParaRepository _icuSysParaRepository;


//        /// <summary>
//        /// 依赖注入
//        /// </summary>
//        /// <param name="guidGenerator"></param>
//        /// <param name="httpContextAccessor"></param>
//        /// <param name="icuParaItemRepository">参数表</param>
//        /// <param name="icuPatientRecordRepository">患者记录表</param>
//        /// <param name="icuDeptScheduleRepository">科室班次表</param>
//        /// <param name="icuParaModuleRepository">参数模块表</param>
//        /// <param name="icuSysParaRepository"></param>
//        public IcuPatientDicAppService(IGuidGenerator guidGenerator,
//            IHttpContextAccessor httpContextAccessor,
//            IIcuParaItemRepository icuParaItemRepository,
//            IIcuPatientRecordRepository icuPatientRecordRepository,
//            IIcuDeptScheduleRepository icuDeptScheduleRepository,
//            IIcuParaModuleRepository icuParaModuleRepository,
//            IIcuSysParaRepository icuSysParaRepository)
//        {
//            _guidGenerator = guidGenerator;
//            _httpContextAccessor = httpContextAccessor;
//            _icuParaItemRepository = icuParaItemRepository;
//            _icuPatientRecordRepository = icuPatientRecordRepository;
//            _icuDeptScheduleRepository = icuDeptScheduleRepository;
//            _icuParaModuleRepository = icuParaModuleRepository;
//            _icuSysParaRepository = icuSysParaRepository;
//        }


//        /// <summary>
//        /// 根据流水号查询当前班次信息，班次列表
//        /// </summary>
//        /// <param name="PI_ID"></param>
//        /// <param name="currentTime"></param>
//        /// <returns></returns>
//        [HttpGet]
//        public async Task<JsonResult<ScheduleDto>> SelectDeptScheduleList(string PI_ID, DateTime currentTime, string deptCode)
//        {
//            if (string.IsNullOrWhiteSpace(PI_ID))
//            {
//                return JsonResult<ScheduleDto>.RequestParamsIsNull(msg: "请输入患者id！");
//            }
//            try
//            {
//                //查询科室信息
//                var icuPatientRecord = await _icuPatientRecordRepository.FindAsync(s => s.PI_ID == PI_ID);
//                if (icuPatientRecord == null)
//                {
//                    return JsonResult<ScheduleDto>.DataNotFound(msg: "无此患者！");
//                }
//                string deptCode = icuPatientRecord.DeptCode;

//                var icuDeptSchedules = await _icuDeptScheduleRepository.Where(s => s.DeptCode == deptCode).OrderBy(s => s.SortNum).ToListAsync();

//                if (icuDeptSchedules.Count <= 0)
//                {
//                    return JsonResult<ScheduleDto>.DataNotFound(msg: "患者没有班次信息！");
//                }

//                //获取当前时间
//                if (currentTime == null || currentTime == DateTime.MinValue)
//                {
//                    currentTime = DateTime.Now;
//                }

//                //获取班次日期
//                string startTime = icuDeptSchedules.Where(s => s.SortNum == 1).Select(s => s.StartTime).ToArray().FirstOrDefault();

//                //A班开始时间
//                int startHour = Convert.ToInt32(startTime.Substring(1, 1));

//                DateTime scheduleTime = currentTime.Hour >= startHour ? currentTime : currentTime.AddDays(-1);

//                DateTime startScheduleTime = icuPatientRecord.InDeptTime.Hour >= startHour ? icuPatientRecord.InDeptTime : icuPatientRecord.InDeptTime.AddDays(-1);

//                DateTime endScheduleTime = new DateTime();
//                if (icuPatientRecord.InDeptState == 0 && icuPatientRecord.OutDeptTime != null && icuPatientRecord.OutDeptTime.HasValue)
//                {
//                    DateTime outDeptTime = Convert.ToDateTime(icuPatientRecord.OutDeptTime);
//                    endScheduleTime = outDeptTime.Hour >= startHour ? outDeptTime : outDeptTime.AddDays(-1);
//                }
//                else { endScheduleTime = DateTime.MinValue; }

//                //当前班次日期
//                string ScheduleTime = endScheduleTime == DateTime.MinValue ? scheduleTime.ToString("yyyy-MM-dd") : endScheduleTime.ToString("yyyy-MM-dd");

//                //获取班次代码
//                string ScheduleCode = string.Empty;
//                DateTime StartTime = new DateTime();
//                DateTime EndTime = new DateTime();
//                foreach (IcuDeptSchedule icuDeptSchedule in icuDeptSchedules)
//                {
//                    if (icuDeptSchedule.Days == "1")
//                    {
//                        StartTime = Convert.ToDateTime(ScheduleTime + " " + icuDeptSchedule.StartTime);
//                        EndTime = Convert.ToDateTime(ScheduleTime + " " + icuDeptSchedule.EndTime);
//                    }
//                    else if (icuDeptSchedule.Days == "2" && int.Parse(icuDeptSchedule.StartTime.Substring(0, icuDeptSchedule.StartTime.IndexOf(':'))) > int.Parse(icuDeptSchedule.EndTime.Substring(0, icuDeptSchedule.EndTime.IndexOf(':'))))
//                    {
//                        StartTime = Convert.ToDateTime(ScheduleTime + " " + icuDeptSchedule.StartTime);
//                        EndTime = Convert.ToDateTime(ScheduleTime + " " + icuDeptSchedule.EndTime).AddDays(1);
//                    }
//                    else
//                    {
//                        StartTime = Convert.ToDateTime(ScheduleTime + " " + icuDeptSchedule.StartTime).AddDays(1);
//                        EndTime = Convert.ToDateTime(ScheduleTime + " " + icuDeptSchedule.EndTime).AddDays(1);
//                    }
//                    if (StartTime <= currentTime && currentTime <= EndTime)
//                    {
//                        ScheduleCode = icuDeptSchedule.ScheduleCode;
//                        break;
//                    }
//                }

//                var icuDeptScheduleDtos = ObjectMapper.Map<List<IcuDeptSchedule>, List<IcuDeptScheduleDto>>(icuDeptSchedules);

//                //返回参数
//                ScheduleDto scheduleDto = new ScheduleDto()
//                {
//                    ScheduleTime = ScheduleTime,
//                    StartScheduleTime = startScheduleTime.ToString("yyyy-MM-dd"),
//                    EndScheduleTime = endScheduleTime == DateTime.MinValue ? null : endScheduleTime.ToString("yyyy-MM-dd"),
//                    ScheduleCode = ScheduleCode,
//                    IcuDeptScheduleDto = icuDeptScheduleDtos
//                };

//                return JsonResult<ScheduleDto>.Ok(data: scheduleDto);
//            }
//            catch (Exception ex)
//            {
//                return JsonResult<ScheduleDto>.Fail(msg: ex.Message);
//            }
//        }
//    }
//}
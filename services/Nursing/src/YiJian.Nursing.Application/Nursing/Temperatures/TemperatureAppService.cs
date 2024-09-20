using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using YiJian.ECIS.ShareModel.Etos.Temperatures;
using YiJian.Nursing.Temperatures.Contracts;
using YiJian.Nursing.Temperatures.Dtos;

namespace YiJian.Nursing.Temperatures
{
    /// <summary>
    /// 描述：体温单服务
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 10:52:26
    /// </summary>
    [Authorize]
    public class TemperatureAppService : NursingAppService, ITemperatureAppService, ICapSubscribe
    {
        private readonly IClinicalEventRepository _clinicalEventRepository;
        private readonly ITemperatureDynamicRepository _temperatureDynamicRepository;
        private readonly ITemperatureRecordRepository _temperatureRecordRepository;
        private readonly ITemperatureRepository _temperatureRepository;
        private readonly ILogger<TemperatureAppService> _logger;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="clinicalEventRepository"></param>
        /// <param name="temperatureDynamicRepository"></param>
        /// <param name="temperatureRecordRepository"></param>
        /// <param name="temperatureRepository"></param>
        /// <param name="logger"></param>
        public TemperatureAppService(IClinicalEventRepository clinicalEventRepository
            , ITemperatureDynamicRepository temperatureDynamicRepository
            , ITemperatureRecordRepository temperatureRecordRepository
            , ITemperatureRepository temperatureRepository
            , ILogger<TemperatureAppService> logger)
        {
            _clinicalEventRepository = clinicalEventRepository;
            _temperatureDynamicRepository = temperatureDynamicRepository;
            _temperatureRecordRepository = temperatureRecordRepository;
            _temperatureRepository = temperatureRepository;
            _logger = logger;
        }

        /// <summary>
        /// 保存临床事件
        /// </summary>
        /// <param name="clinicalEventInput"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task SaveClinicalEventAsync(ClinicalEventDto clinicalEventInput)
        {
            if (clinicalEventInput == null || clinicalEventInput.PI_Id == Guid.Empty || clinicalEventInput.InHospital == DateTime.MinValue)
            {
                throw new Exception("缺少必填参数");
            }
            List<ClinicalEvent> oldClinicalEvents = await (await _clinicalEventRepository.GetQueryableAsync()).AsNoTracking().Where(x => x.PI_Id == clinicalEventInput.PI_Id).ToListAsync();
            ClinicalEvent InHospitalEvent = oldClinicalEvents.SingleOrDefault(x => x.EventCategory == "入院");
            if (InHospitalEvent == null)
            {
                InHospitalEvent = new ClinicalEvent(Guid.NewGuid());
                InHospitalEvent.PI_Id = clinicalEventInput.PI_Id;
                InHospitalEvent.NurseCode = clinicalEventInput.NurseCode;
                InHospitalEvent.NurseName = clinicalEventInput.NurseName;
                InHospitalEvent.EventCategory = "入院";
                InHospitalEvent.HappenTime = clinicalEventInput.InHospital;
                InHospitalEvent.UpDownFlag = "上标";
                InHospitalEvent.EventDescription = string.Join('|', InHospitalEvent.EventCategory, TimeToChinese(clinicalEventInput.InHospital));
                await _clinicalEventRepository.InsertAsync(InHospitalEvent);
            }

            if (clinicalEventInput.InHospital != InHospitalEvent.HappenTime)
            {
                InHospitalEvent.NurseCode = clinicalEventInput.NurseCode;
                InHospitalEvent.NurseName = clinicalEventInput.NurseName;
                InHospitalEvent.HappenTime = clinicalEventInput.InHospital;
                InHospitalEvent.EventDescription = string.Join('|', InHospitalEvent.EventCategory, TimeToChinese(clinicalEventInput.InHospital));
                await _clinicalEventRepository.UpdateAsync(InHospitalEvent);
            }

            if (clinicalEventInput.ClinicalEventDtos == null || !clinicalEventInput.ClinicalEventDtos.Any())
            {
                await _clinicalEventRepository.DeleteAsync(x => x.PI_Id == clinicalEventInput.PI_Id && x.EventCategory != "入院");
            }

            List<ClinicalEventDetailDto> addClinicalEventInputs = clinicalEventInput.ClinicalEventDtos.Where(x => x.Id == Guid.Empty).ToList();
            List<ClinicalEvent> addClinicalEvents = new();
            foreach (ClinicalEventDetailDto clinicalEventDto in addClinicalEventInputs)
            {
                ClinicalEvent clinicalEvent = new(Guid.NewGuid())
                {
                    PI_Id = clinicalEventInput.PI_Id,
                    NurseCode = clinicalEventDto.NurseCode,
                    NurseName = clinicalEventDto.NurseName,
                    EventCategoryCode = clinicalEventDto.EventCategoryCode,
                    EventCategory = clinicalEventDto.EventCategory,
                    HappenTime = clinicalEventDto.HappenTime,
                    UpDownFlagCode = clinicalEventDto.UpDownFlagCode,
                    UpDownFlag = clinicalEventDto.UpDownFlag,
                    EventDescription = clinicalEventDto.EventDescription
                };
                if (string.IsNullOrEmpty(clinicalEvent.EventDescription))
                {
                    clinicalEvent.EventDescription = string.Join('|', clinicalEvent.EventCategory, TimeToChinese(clinicalEvent.HappenTime));
                }
                addClinicalEvents.Add(clinicalEvent);
            }

            if (addClinicalEvents.Any())
            {
                await _clinicalEventRepository.InsertManyAsync(addClinicalEvents);
            }


            IEnumerable<Guid> newIds = clinicalEventInput.ClinicalEventDtos.Where(x => x.Id != Guid.Empty).Select(x => x.Id);
            IEnumerable<ClinicalEvent> updateClinicalEvents = oldClinicalEvents.Where(x => newIds.Contains(x.Id));
            foreach (ClinicalEvent clinicalEvent in updateClinicalEvents)
            {
                ClinicalEventDetailDto clinicalEventDto = clinicalEventInput.ClinicalEventDtos.First(x => x.Id == clinicalEvent.Id);
                if (clinicalEvent.EventCategory != clinicalEventDto.EventCategory
                    || clinicalEvent.HappenTime != clinicalEventDto.HappenTime
                    || clinicalEvent.UpDownFlag != clinicalEventDto.UpDownFlag)
                {
                    clinicalEvent.NurseCode = clinicalEventDto.NurseCode;
                    clinicalEvent.NurseName = clinicalEventDto.NurseName;
                    clinicalEvent.EventCategoryCode = clinicalEventDto.EventCategoryCode;
                    clinicalEvent.EventCategory = clinicalEventDto.EventCategory;
                    clinicalEvent.HappenTime = clinicalEventDto.HappenTime;
                    clinicalEvent.UpDownFlagCode = clinicalEventDto.UpDownFlagCode;
                    clinicalEvent.UpDownFlag = clinicalEventDto.UpDownFlag;
                    clinicalEvent.EventDescription = clinicalEventDto.EventDescription;
                }
            }

            if (updateClinicalEvents.Any())
            {
                await _clinicalEventRepository.UpdateManyAsync(updateClinicalEvents);
            }

            IEnumerable<ClinicalEvent> deleteClinicalEvents = oldClinicalEvents.Where(x => !newIds.Contains(x.Id) && x.EventCategory != "入院");
            if (deleteClinicalEvents.Any())
            {
                await _clinicalEventRepository.DeleteManyAsync(deleteClinicalEvents);
            }
        }

        /// <summary>
        /// 时间转成中文显示
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private string TimeToChinese(DateTime dateTime)
        {
            char[] strChinese = new char[] {
                  '零','一','二','三','四','五','六','七','八','九','十'
              };

            StringBuilder sb = new StringBuilder();
            int hour = dateTime.Hour;
            int DN1 = hour / 10;
            int DN2 = hour % 10;

            if (DN1 == 0 && DN2 == 0)
            {
                sb.Append(strChinese[DN1]);
            }

            if (DN1 > 1)
            {
                sb.Append(strChinese[DN1]);
            }
            if (DN1 > 0)
            {
                sb.Append(strChinese[10]);
            }
            if (DN2 != 0)
            {
                sb.Append(strChinese[DN2]);
            }
            sb.Append("时");

            int minute = dateTime.Minute;
            int MN1 = minute / 10;
            int MN2 = minute % 10;

            if (MN1 == 0 && MN2 == 0)
            {
                sb.Append(strChinese[MN1]);
                sb.Append(strChinese[MN2]);
            }

            if (MN1 > 1)
            {
                sb.Append(strChinese[MN1]);
            }
            if (MN1 > 0)
            {
                sb.Append(strChinese[10]);
            }
            if (MN2 != 0)
            {
                sb.Append(strChinese[MN2]);
            }
            sb.Append("分");
            return sb.ToString();
        }

        /// <summary>
        /// 获取临床事件
        /// </summary>
        /// <param name="pi_id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ClinicalEventDto> GetClinicalEventsAsync(Guid pi_id)
        {
            ClinicalEventDto clinicalEventDto = new ClinicalEventDto();
            List<ClinicalEvent> clinicalEvents = await (await _clinicalEventRepository.GetQueryableAsync()).AsNoTracking().Where(x => x.PI_Id == pi_id).ToListAsync();
            ClinicalEvent inHospitalEvent = clinicalEvents.FirstOrDefault(x => x.EventCategory == "入院");
            if (!clinicalEvents.Any() || inHospitalEvent == null)
            {
                return clinicalEventDto;
            }

            clinicalEventDto.InHospital = inHospitalEvent.HappenTime;
            clinicalEventDto.PI_Id = inHospitalEvent.PI_Id;
            clinicalEventDto.NurseCode = inHospitalEvent.NurseCode;
            clinicalEventDto.NurseName = inHospitalEvent.NurseName;
            clinicalEvents.RemoveAll(x => x.EventCategory == "入院");
            List<ClinicalEventDetailDto> details = new List<ClinicalEventDetailDto>();
            foreach (var clinicalEvent in clinicalEvents)
            {
                ClinicalEventDetailDto clinicalEventDetailDto = new ClinicalEventDetailDto();
                clinicalEventDetailDto.Id = clinicalEvent.Id;
                clinicalEventDetailDto.PI_Id = clinicalEvent.PI_Id;
                clinicalEventDetailDto.EventCategoryCode = clinicalEvent.EventCategoryCode;
                clinicalEventDetailDto.EventCategory = clinicalEvent.EventCategory;
                clinicalEventDetailDto.HappenTime = clinicalEvent.HappenTime;
                clinicalEventDetailDto.UpDownFlagCode = clinicalEvent.UpDownFlagCode;
                clinicalEventDetailDto.UpDownFlag = clinicalEvent.UpDownFlag;
                clinicalEventDetailDto.EventDescription = clinicalEvent.EventDescription;
                clinicalEventDetailDto.NurseCode = clinicalEvent.NurseCode;
                clinicalEventDetailDto.NurseName = clinicalEvent.NurseName;
                clinicalEventDetailDto.CreationTime = clinicalEvent.CreationTime;
                details.Add(clinicalEventDetailDto);
            }
            clinicalEventDto.ClinicalEventDtos = details;
            return clinicalEventDto;
        }

        /// <summary>
        /// 删除临床事件
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task DeleteClinicalEventsAsync(List<Guid> ids)
        {
            await _clinicalEventRepository.DeleteManyAsync(ids);
        }

        /// <summary>
        /// 保存体温单
        /// </summary>
        /// <param name="temperatureEto"></param>
        /// <returns></returns>
        [CapSubscribe("addtemperature.reportservice.to.nursingservice")]
        public async Task SaveTemperatureAsync(TemperatureEto temperatureEto)
        {
            var uow = UnitOfWorkManager.Begin();
            try
            {
                DateTime MeasureDate = temperatureEto.MeasureTime.Date;
                Temperature temperature = await (await _temperatureRepository.GetQueryableAsync()).Where(x => x.PI_Id == temperatureEto.PI_Id && x.MeasureDate == MeasureDate).FirstOrDefaultAsync();
                if (temperature == null)
                {
                    temperature = new Temperature(Guid.NewGuid());
                    temperature.PI_Id = temperatureEto.PI_Id;
                    temperature.MeasureDate = MeasureDate;
                    await _temperatureRepository.InsertAsync(temperature);
                }

                var temperatureRecord = await (await _temperatureRecordRepository.GetQueryableAsync()).Where(x => x.NursingRecordId == temperatureEto.NursingRecordId).FirstOrDefaultAsync();
                if (temperatureRecord == null)
                {
                    await InsertTemperatureRecordAsync(temperatureEto, temperature);
                }
                else
                {
                    await UpdateTemperatureRecordAsync(temperatureEto, temperatureRecord, temperature);
                }
                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                _logger.LogException(ex);
            }
        }

        /// <summary>
        /// 删除体温记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [CapSubscribe("deletetemperature.reportservice.to.nursingservice")]
        public async Task DeleteTemperatureAsync(List<Guid> ids)
        {
            var uow = UnitOfWorkManager.Begin();
            try
            {
                IQueryable<TemperatureRecord> temperatureRecords = (await _temperatureRecordRepository.GetQueryableAsync()).Where(x => ids.Contains(x.NursingRecordId));
                if (temperatureRecords != null || temperatureRecords.Any())
                {
                    IQueryable<Guid> temperatureRecordIds = temperatureRecords.Select(x => x.Id);
                    await _temperatureRecordRepository.DeleteManyAsync(temperatureRecords);
                    await _temperatureDynamicRepository.HardDeleteAsync(x => temperatureRecordIds.Contains(x.TemperatureRecordId));
                }
                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                _logger.LogException(ex);
            }
        }

        /// <summary>
        /// 获取体温单
        /// </summary>
        /// <param name="pi_id"></param>
        /// <param name="measureDate"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<TemperatureDto> GetTemperatureAsync(Guid pi_id, DateTime measureDate)
        {
            measureDate = measureDate.Date;
            Temperature temperature = await (await _temperatureRepository.GetQueryableAsync()).Include(x => x.TemperatureRecords).Include(x => x.TemperatureDynamics).Where(x => x.PI_Id == pi_id && x.MeasureDate == measureDate).AsNoTracking().FirstOrDefaultAsync();
            if (temperature == null) return new TemperatureDto();

            List<TemperatureRecord> temperatureRecords = GetTimePointRecord(temperature.TemperatureRecords);
            List<TemperatureDynamic> temperatureDynamics = GetDynamicsData(temperature.TemperatureDynamics);
            TemperatureDto temperatureDto = new()
            {
                Id = temperature.Id,
                PI_Id = temperature.PI_Id,
                MeasureDate = temperature.MeasureDate,
                TemperatureRecords = temperatureRecords,
                TemperatureDynamics = temperatureDynamics
            };
            return temperatureDto;
        }

        /// <summary>
        /// 动态数据处理
        /// </summary>
        /// <param name="temperatureDynamics"></param>
        /// <returns></returns>
        private List<TemperatureDynamic> GetDynamicsData(IEnumerable<TemperatureDynamic> temperatureDynamics)
        {
            List<TemperatureDynamic> dynamicDatas = new();
            var groups = temperatureDynamics.GroupBy(x => new { x.PropertyCode, x.Unit, x.ExtralFlag });
            foreach (var group in groups)
            {
                var tempDynamics = group.ToList();
                if (tempDynamics.Count() == 1)
                {
                    dynamicDatas.Add(tempDynamics.First());
                    continue;
                }

                TemperatureDynamic sumTemperatureDynamic = tempDynamics.First();
                List<double> sumValue = new List<double>();
                foreach (var tempDynamic in tempDynamics)
                {
                    if (double.TryParse(tempDynamic.PropertyValue, out double value))
                    {
                        sumValue.Add(value);
                    }
                }
                sumTemperatureDynamic.PropertyValue = sumValue.Sum().ToString();
                dynamicDatas.Add(sumTemperatureDynamic);
            }
            return dynamicDatas;
        }

        /// <summary>
        /// 获取时刻点最后一条数据
        /// </summary>
        /// <param name="temperatureRecords"></param>
        /// <returns></returns>
        private List<TemperatureRecord> GetTimePointRecord(IEnumerable<TemperatureRecord> temperatureRecords)
        {
            List<TemperatureRecord> records = new List<TemperatureRecord>();
            IEnumerable<IGrouping<int, TemperatureRecord>> groups = temperatureRecords.GroupBy(x => x.TimePoint);
            foreach (IGrouping<int, TemperatureRecord> group in groups)
            {
                TemperatureRecord temperatureRecord = group.ToList().OrderByDescending(x => x.MeasureTime).FirstOrDefault();
                if (temperatureRecord != null)
                {
                    records.Add(temperatureRecord);
                }
            }

            return records;
        }

        /// <summary>
        /// 获取体温记录
        /// </summary>
        /// <param name="pi_id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<TemperatureRecordDto>> GetTemperatureRecordsAsync(Guid pi_id)
        {
            IQueryable<Guid> ids = (await _temperatureRepository.GetQueryableAsync()).AsNoTracking().Where(x => x.PI_Id == pi_id).Select(x => x.Id);
            List<TemperatureRecord> temperatureRecords = await (await _temperatureRecordRepository.GetQueryableAsync()).Where(x => ids.Contains(x.TemperatureId) && x.Temperature != null).ToListAsync();
            List<TemperatureRecordDto> temperatureRecordDtos = new();
            foreach (var temperatureRecord in temperatureRecords)
            {
                TemperatureRecordDto temperatureRecordDto = new()
                {
                    Id = temperatureRecord.Id,
                    TimePoint = temperatureRecord.TimePoint,
                    MeasureTime = temperatureRecord.MeasureTime,
                    Temperature = temperatureRecord.Temperature,
                    CoolingWay = temperatureRecord.CoolingWay,
                    MeasurePosition = temperatureRecord.MeasurePosition,
                    RetestTemperature = temperatureRecord.RetestTemperature,
                    NurseCode = temperatureRecord.NurseCode,
                    NurseName = temperatureRecord.NurseName
                };
                temperatureRecordDtos.Add(temperatureRecordDto);
            }
            return temperatureRecordDtos;
        }

        /// <summary>
        /// 更新体温记录
        /// </summary>
        /// <param name="temperatureRecordDtos"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [UnitOfWork]
        public async Task<bool> UpdateTemperatureRecordsAsync(List<TemperatureRecordDto> temperatureRecordDtos)
        {
            if (temperatureRecordDtos == null || !temperatureRecordDtos.Any())
            {
                throw new Exception("请求参数错误");
            }

            IEnumerable<Guid> ids = temperatureRecordDtos.Select(x => x.Id);
            List<TemperatureRecord> temperatureRecords = await (await _temperatureRecordRepository.GetQueryableAsync()).Where(x => ids.Contains(x.Id)).ToListAsync();
            foreach (var temperatureRecord in temperatureRecords)
            {
                TemperatureRecordDto temperatureRecordDto = temperatureRecordDtos.Single(x => x.Id == temperatureRecord.Id);
                temperatureRecord.CoolingWay = temperatureRecordDto.CoolingWay;
                temperatureRecord.MeasurePosition = temperatureRecordDto.MeasurePosition;
                temperatureRecord.RetestTemperature = temperatureRecordDto.RetestTemperature;
            }
            await _temperatureRecordRepository.UpdateManyAsync(temperatureRecords);
            return true;
        }

        /// <summary>
        /// 获取体温单明细信息
        /// </summary>
        /// <param name="pi_id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<TemperatureDetailDto>> GetTemperatureDetailsAsync(Guid pi_id)
        {
            List<TemperatureDetailDto> temperatureDetailDtos = new();
            List<Temperature> temperatures = await (await _temperatureRepository.GetQueryableAsync()).Include(x => x.TemperatureRecords).Include(x => x.TemperatureDynamics).Where(x => x.PI_Id == pi_id).ToListAsync();
            if (!temperatures.Any())
            {
                return temperatureDetailDtos;
            }

            foreach (Temperature temperature in temperatures)
            {
                foreach (TemperatureRecord temperatureRecord in temperature.TemperatureRecords)
                {
                    PropertyInfo[] properties = typeof(TemperatureRecord).GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        DescriptionAttribute attribute = property.GetCustomAttribute<DescriptionAttribute>();
                        if (attribute == null) continue;

                        TemperatureDetailDto temperatureDetailDto = new()
                        {
                            PI_Id = temperature.PI_Id,
                            MeasureDate = temperature.MeasureDate,
                            MeasureTime = temperatureRecord.MeasureTime,
                            TimePoint = temperatureRecord.TimePoint,
                            PropertyName = attribute.Description,
                            PropertyValue = property.GetValue(temperatureRecord)?.ToString()
                        };
                        temperatureDetailDtos.Add(temperatureDetailDto);
                    }
                }

                foreach (var temperatureDynamic in temperature.TemperatureDynamics)
                {
                    TemperatureDetailDto temperatureDetailDto = new()
                    {
                        PI_Id = temperature.PI_Id,
                        MeasureDate = temperature.MeasureDate,
                        MeasureTime = temperatureDynamic.CreationTime,
                        PropertyName = temperatureDynamic.PropertyName,
                        PropertyValue = temperatureDynamic.PropertyValue
                    };
                    temperatureDetailDtos.Add(temperatureDetailDto);
                }
            }

            temperatureDetailDtos = temperatureDetailDtos.OrderBy(x => x.MeasureDate).ThenBy(x => x.MeasureTime).ToList();
            return temperatureDetailDtos;
        }

        /// <summary>
        /// 获取体温报表数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        public async Task<TemperatureReportDto> GetTemperatureReportAsync(TemperatureReportInput input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            DateTime startTime = input.StartTime.Date;
            DateTime endTime = startTime.AddDays(7);

            List<Temperature> temperatures = await (await _temperatureRepository.GetQueryableAsync()).Include(x => x.TemperatureRecords).Include(x => x.TemperatureDynamics).Where(x => x.PI_Id == input.PI_Id && x.MeasureDate >= startTime && x.MeasureDate < endTime).ToListAsync();

            List<ClinicalEvent> clinicalEvents = await (await _clinicalEventRepository.GetQueryableAsync()).Where(x => x.PI_Id == input.PI_Id).ToListAsync();

            TemperatureReportDto temperatureReportDto = new TemperatureReportDto();
            temperatureReportDto.TemperaturesHead = GetHeadData(startTime, clinicalEvents);
            temperatureReportDto.TemperatureData = GetTemperatureData(temperatures, clinicalEvents, startTime);
            temperatureReportDto.TemperatureDynamic = GetDynamicData(startTime, temperatures);
            return temperatureReportDto;
        }

        /// <summary>
        /// 获取其他动态数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="temperatures"></param>
        /// <returns></returns>
        private List<TemperatureOtherRecordDto> GetDynamicData(DateTime startTime, List<Temperature> temperatures)
        {
            List<TemperatureOtherRecordDto> dynamicDatas = new List<TemperatureOtherRecordDto>();

            IEnumerable<TemperatureDynamic> temperatureDynamicsList = temperatures.SelectMany(x => x.TemperatureDynamics);
            IEnumerable<string> propertyNames = temperatures.SelectMany(x => x.TemperatureDynamics).Select(x => x.PropertyName).Distinct();
            foreach (string propertyName in propertyNames)
            {
                TemperatureDynamic temperatureDynamic = temperatureDynamicsList.FirstOrDefault(x => x.PropertyName == propertyName);
                TemperatureOtherRecordDto dynamicData = new TemperatureOtherRecordDto();
                dynamicData.PropertyName = propertyName;
                dynamicData.ExtralFlag = temperatureDynamic?.ExtralFlag;
                dynamicData.Unit = temperatureDynamic?.Unit;
                dynamicDatas.Add(dynamicData);
            }

            PropertyInfo[] properties = typeof(TemperatureOtherRecordDto).GetProperties();
            for (int i = 0; i < 7; i++)
            {
                string field = "Field" + (i + 1);
                PropertyInfo propertyInfo = properties.Single(x => x.Name == field);
                DateTime startTime1 = startTime.AddDays(i);

                Temperature temperature = temperatures.FirstOrDefault(x => x.MeasureDate.Date == startTime1.Date);
                if (temperature == null)
                {
                    continue;
                }

                List<TemperatureDynamic> temperatureDynamics = GetDynamicsData(temperature.TemperatureDynamics);
                foreach (TemperatureOtherRecordDto dynamicData in dynamicDatas)
                {
                    TemperatureDynamic temperatureDynamic = temperatureDynamics.FirstOrDefault(x => x.PropertyName == dynamicData.PropertyName);
                    if (temperatureDynamic == null)
                    {
                        continue;
                    }

                    propertyInfo.SetValue(dynamicData, temperatureDynamic.PropertyValue);
                }
            }

            return dynamicDatas;
        }

        /// <summary>
        /// 获取体温数据
        /// </summary>
        /// <param name="temperatures"></param>
        /// <param name="clinicalEvents"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        private List<TemperatureDataDto> GetTemperatureData(IEnumerable<Temperature> temperatures, IEnumerable<ClinicalEvent> clinicalEvents, DateTime startTime)
        {
            List<TemperatureDataDto> temperatureDataDtos = new List<TemperatureDataDto>();
            for (int i = 0; i < 7; i++)
            {
                DateTime measureDate = startTime.AddDays(i).Date;
                for (int j = 2; j < 24; j += 4)
                {
                    TemperatureDataDto temperatureDataDto = new TemperatureDataDto();
                    temperatureDataDto.MeasureDate = measureDate;
                    temperatureDataDto.TimePoint = j;
                    temperatureDataDtos.Add(temperatureDataDto);
                }
            }

            foreach (var temperature in temperatures)
            {
                List<TemperatureRecord> timePointRecords = GetTimePointRecord(temperature.TemperatureRecords);
                foreach (TemperatureRecord temperatureRecord in timePointRecords)
                {
                    TemperatureDataDto temperatureDataDto = new()
                    {
                        MeasureDate = temperature.MeasureDate,
                        TimePoint = temperatureRecord.TimePoint,
                        Temperature = temperatureRecord.Temperature,
                        Pulse = temperatureRecord.Pulse,
                        HeartRate = temperatureRecord.HeartRate,
                        Breathing = temperatureRecord.Breathing,
                        PainDegree = temperatureRecord.PainDegree,
                        PressureRecord = string.Join(',', temperatureRecord.DiastolicPressure, temperatureRecord.SystolicPressure),
                        CoolingWay = temperatureRecord.CoolingWay,
                        MeasurePosition = temperatureRecord.MeasurePosition,
                        RetestTemperature = temperatureRecord.RetestTemperature,
                        UpEventDescription = GetEventDescription(temperature.MeasureDate, temperatureRecord, clinicalEvents, "up"),
                        DownEventDescription = GetEventDescription(temperature.MeasureDate, temperatureRecord, clinicalEvents, "down")
                    };

                    temperatureDataDtos.ReplaceOne(x => x.MeasureDate.Date == temperatureDataDto.MeasureDate.Date && x.TimePoint == temperatureDataDto.TimePoint, temperatureDataDto);
                }
            }



            return temperatureDataDtos;
        }

        /// <summary>
        /// 获取体温单头部数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="clinicalEvents"></param>
        /// <returns></returns>
        private List<TemperatureOtherRecordDto> GetHeadData(DateTime startTime, IEnumerable<ClinicalEvent> clinicalEvents)
        {
            List<TemperatureOtherRecordDto> head = new List<TemperatureOtherRecordDto>();

            int? inHospitalOffset = null;
            ClinicalEvent inHospitalEvent = clinicalEvents.FirstOrDefault(x => x.EventCategory == "入院");
            if (inHospitalEvent != null)
            {
                TimeSpan timeSpan = startTime.Date - inHospitalEvent.HappenTime.Date;
                inHospitalOffset = timeSpan.Days;
            }

            int? operationOffset = null;
            ClinicalEvent operationEvent = clinicalEvents.OrderBy(x => x.HappenTime).FirstOrDefault(x => x.EventCategory == "手术");
            if (operationEvent != null)
            {
                TimeSpan timeSpan = startTime.Date - operationEvent.HappenTime.Date;
                operationOffset = timeSpan.Days;
            }

            TemperatureOtherRecordDto date = new TemperatureOtherRecordDto() { PropertyName = "日期" };
            TemperatureOtherRecordDto hospitalization = new TemperatureOtherRecordDto() { PropertyName = "住院天数" };
            TemperatureOtherRecordDto operation = new TemperatureOtherRecordDto() { PropertyName = "手术后天数" };

            PropertyInfo[] properties = typeof(TemperatureOtherRecordDto).GetProperties();
            int refreshOperationOffset = -1;
            for (int i = 1; i < 8; i++)
            {
                string field = "Field" + i;
                PropertyInfo propertyInfo = properties.Single(x => x.Name == field);

                propertyInfo.SetValue(date, startTime.AddDays(i - 1).ToString("yyyy-MM-dd"));
                if (inHospitalOffset.HasValue && inHospitalOffset.Value >= 0)
                {
                    propertyInfo.SetValue(hospitalization, (i + inHospitalOffset.Value).ToString());
                }

                if (operationOffset.HasValue && operationOffset.Value >= 0)
                {
                    propertyInfo.SetValue(operation, (i + operationOffset.Value).ToString());
                }

                ClinicalEvent currentDayOperation = clinicalEvents.FirstOrDefault(x => x.EventCategory == "手术" && x.HappenTime.Date == startTime.AddDays(i - 1).Date);
                if (currentDayOperation != null)
                {
                    operationOffset = -1;
                    refreshOperationOffset = 1;
                }

                if (refreshOperationOffset == -1)
                {
                    continue;
                }

                propertyInfo.SetValue(operation, refreshOperationOffset.ToString());
                refreshOperationOffset++;
            }

            head.Add(date);
            head.Add(hospitalization);
            head.Add(operation);
            return head;
        }

        /// <summary>
        /// 获取事件信息
        /// </summary>
        /// <param name="measureDate"></param>
        /// <param name="temperatureRecord"></param>
        /// <param name="clinicalEvents"></param>
        /// <param name="updownFlag"></param>
        /// <returns></returns>
        private string GetEventDescription(DateTime measureDate, TemperatureRecord temperatureRecord, IEnumerable<ClinicalEvent> clinicalEvents, string updownFlag)
        {
            string description = string.Empty;
            if (clinicalEvents == null || !clinicalEvents.Any())
            {
                return description;
            }
            DateTime startTime = measureDate.AddHours(temperatureRecord.TimePoint - 2);
            DateTime endTime = startTime.AddHours(4);

            var result = clinicalEvents.Where(x => x.HappenTime >= startTime && x.HappenTime < endTime && x.UpDownFlag.ToLower() == updownFlag.ToLower()).ToList();
            description = string.Join(",", result.Select(x => x.EventDescription));
            return description;
        }

        /// <summary>
        /// 更新体温单
        /// </summary>
        /// <param name="temperatureEto"></param>
        /// <param name="temperatureRecord"></param>
        /// <param name="temperature"></param>
        /// <returns></returns>
        private async Task UpdateTemperatureRecordAsync(TemperatureEto temperatureEto, TemperatureRecord temperatureRecord, Temperature temperature)
        {
            temperatureRecord.TemperatureId = temperature.Id;
            temperatureRecord.TimePoint = GetTimePoint(temperatureEto.MeasureTime);
            temperatureRecord.MeasureTime = temperatureEto.MeasureTime;
            temperatureRecord.Temperature = temperatureEto.Temperature;
            temperatureRecord.Pulse = temperatureEto.Pulse;
            temperatureRecord.Breathing = temperatureEto.Breathing;
            temperatureRecord.HeartRate = temperatureEto.HeartRate;
            temperatureRecord.SystolicPressure = temperatureEto.SystolicPressure;
            temperatureRecord.DiastolicPressure = temperatureEto.DiastolicPressure;
            temperatureRecord.Consciousness = temperatureEto.Consciousness;
            temperatureRecord.NurseCode = temperatureEto.NurseCode;
            temperatureRecord.NurseName = temperatureEto.NurseName;

            await _temperatureDynamicRepository.HardDeleteAsync(x => x.TemperatureRecordId == temperatureRecord.Id);
            await InsertDynamicAsync(temperatureEto.TemperatureDynamics, temperatureRecord);
            await _temperatureRecordRepository.UpdateAsync(temperatureRecord);
        }

        private async Task InsertDynamicAsync(List<IntakeEto> intakeEtos, TemperatureRecord temperatureRecord)
        {
            if (intakeEtos != null && intakeEtos.Any())
            {
                List<TemperatureDynamic> temperatureDynamics = new List<TemperatureDynamic>();
                foreach (IntakeEto item in intakeEtos)
                {
                    TemperatureDynamic temperatureDynamic = new(Guid.NewGuid())
                    {
                        TemperatureId = temperatureRecord.TemperatureId,
                        TemperatureRecordId = temperatureRecord.Id,
                        PropertyCode = item.PropertyCode,
                        PropertyName = item.PropertyName,
                        PropertyValue = item.PropertyValue,
                        Unit = item.Unit,
                        ExtralFlag = item.ExtralFlag,
                        NurseCode = temperatureRecord.NurseCode,
                        NurseName = temperatureRecord.NurseName
                    };
                    temperatureDynamics.Add(temperatureDynamic);

                }
                await _temperatureDynamicRepository.InsertManyAsync(temperatureDynamics);
            }
        }

        /// <summary>
        /// 插入体温单
        /// </summary>
        /// <param name="temperatureEto"></param>
        /// <param name="temperature"></param>
        /// <returns></returns>
        private async Task InsertTemperatureRecordAsync(TemperatureEto temperatureEto, Temperature temperature)
        {

            TemperatureRecord temperatureRecord = new(Guid.NewGuid())
            {
                TemperatureId = temperature.Id,
                TimePoint = GetTimePoint(temperatureEto.MeasureTime),
                MeasureTime = temperatureEto.MeasureTime,
                Temperature = temperatureEto.Temperature,
                Pulse = temperatureEto.Pulse,
                Breathing = temperatureEto.Breathing,
                HeartRate = temperatureEto.HeartRate,
                SystolicPressure = temperatureEto.SystolicPressure,
                DiastolicPressure = temperatureEto.DiastolicPressure,
                Consciousness = temperatureEto.Consciousness,
                NursingRecordId = temperatureEto.NursingRecordId,
                NurseCode = temperatureEto.NurseCode,
                NurseName = temperatureEto.NurseName
            };

            await InsertDynamicAsync(temperatureEto.TemperatureDynamics, temperatureRecord);
            await _temperatureRecordRepository.InsertAsync(temperatureRecord);
        }

        /// <summary>
        /// 划分时刻点
        /// </summary>
        /// <param name="measureTime"></param>
        /// <returns></returns>
        private int GetTimePoint(DateTime measureTime)
        {
            DateTime date = measureTime.Date;
            for (int i = 0; i < 6; i++)
            {
                if (date.AddHours(4 * i) <= measureTime && measureTime < date.AddHours(4 * (i + 1)))
                {
                    return 2 + (4 * i);
                }
            }
            return 0;
        }
    }
}

using HisApiMockService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using SQLitePCL;

namespace HisApiMockService.Controllers;

public class PatientController : Controller
{
    private readonly SqliteDbContext _sqliteDbContext;

    public PatientController(SqliteDbContext sqliteDbContext)
    {
        _sqliteDbContext = sqliteDbContext;
    }

    /// <summary>
    /// 建档接口
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("patient/buildPatientArchives")]
    public IActionResult BuildPatientArchives([FromBody] CreatePatientDto dto)
    {
        try
        {
            var buildPatient = new CreatePatientResponseDto
            {
                PatientId = new Random().Next().ToString("D6"),
                Address = dto.HomeAddress,
                Birthday = DateTime.Parse(dto.Birthday),
                CardNo = dto.CardNo,
                PatientName = dto.Name,
                IdentityNo = dto.IdNo,
                ContactsPhone = dto.ContactPhone,
            };
            var result = new CommonResult<CreatePatientResponseDto>
            {
                Code = 0,
                Msg = "",
                Data = buildPatient
            };
            // _sqliteDbContext.BuildPatient.Add(buildPatient);
            // _sqliteDbContext.SaveChanges();
            return this.Ok(result);
        }
        catch (Exception exception)
        {
            return this.Ok(new CommonResult<CreatePatientResponseDto>
            {
                Code = 1,
                Msg = exception.Message
            });
        }
    }

    /// <summary>
    /// 预约确认
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("patient/reservationConfirm")]
    public async Task<IActionResult> ReservationConfirm([FromBody] ReservationConfirmDto dto)
    {
        await Task.Delay(TimeSpan.FromSeconds(3));
        return this.Ok(new CommonResult<ReservationConfirmResultDto>
        {
            Code = 0,
            Msg = "",
            Data = new ReservationConfirmResultDto
            {
                RegDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                SeqNumber = "123123"
            }
        });
    }

    /// <summary>
    /// 挂号接口
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("patient/registerPatient")]
    public IActionResult RegisterPatient([FromBody] RegisterPatientRequest request)
    {
        var patient = new PatientDto
        {
            PatientId = request.PatientId,
            DeptId = request.DeptId,
            DoctorCode = request.DoctorId,
            PatientNo = new Random().Next().ToString("D6"),
            VisitNum = new Random().Next().ToString("D6"),
            RegisterDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
            RegisterId = new Random().Next().ToString("D6"),
            RegisterSequence = new Random().Next().ToString("D6"),
        };
        // _sqliteDbContext.RegisterPatient.Add(patient);
        // _sqliteDbContext.SaveChanges();
        return this.Ok(new CommonResult<PatientDto>
        {
            Code = 0,
            Msg = "",
            Data = patient
        });
    }

    [HttpGet("patient/doctorSchedule")]
    public IActionResult GetDoctorSchedule([FromQuery] string deptCode, [FromQuery] DateTime? regDate)
    {
        // 金湾医院测试数据
        var response = @"{
                                  ""code"": 0,
                                  ""msg"": null,
                                  ""data"": [
                                    {
                                      ""deptCode"": ""20400"",
                                      ""deptName"": ""消化内科"",
                                      ""doctorCode"": ""86"",
                                      ""doctorName"": ""陈静"",
                                      ""doctorTitle"": ""副主任医师"",
                                      ""regType"": ""副主任医师号"",
                                      ""fee"": 25,
                                      ""regFee"": 0,
                                      ""checkupFee"": 0,
                                      ""otherFee"": 0,
                                      ""clinicRoom"": 20400273,
                                      ""availableNum"": 12,
                                      ""workType"": ""1""
                                    },
                                    {
                                      ""deptCode"": ""20400"",
                                      ""deptName"": ""消化内科"",
                                      ""doctorCode"": ""3432"",
                                      ""doctorName"": ""罗钧沛"",
                                      ""doctorTitle"": ""主治医师"",
                                      ""regType"": ""医师号"",
                                      ""fee"": 17,
                                      ""regFee"": 0,
                                      ""checkupFee"": 0,
                                      ""otherFee"": 0,
                                      ""clinicRoom"": 20400273,
                                      ""availableNum"": 80,
                                      ""workType"": ""4""
                                    },
                                    {
                                      ""deptCode"": ""20400"",
                                      ""deptName"": ""消化内科"",
                                      ""doctorCode"": ""3052"",
                                      ""doctorName"": ""罗永燕"",
                                      ""doctorTitle"": ""主治医师"",
                                      ""regType"": ""医师号"",
                                      ""fee"": 17,
                                      ""regFee"": 0,
                                      ""checkupFee"": 0,
                                      ""otherFee"": 0,
                                      ""clinicRoom"": 20400273,
                                      ""availableNum"": 40,
                                      ""workType"": ""4""
                                    }
                                  ]
                                }".Replace("20400", deptCode ?? "20400");

        return this.Ok(response);
    }

    [HttpPost("/patient/cancelRegisterPatient")]
    public IActionResult CancelRegisterInfo([FromBody] CancelRegisterInfoRequest request)
    {
        Console.WriteLine("取消挂号，挂号流水号：" + request.RegSerialNo);
        var result = new CommonResult<string>
        {
            Code = 0,
            Msg = "",
        };
        return this.Ok(result);
    }
}

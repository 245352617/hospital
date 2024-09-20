using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 医生科室诊室
    /// </summary>
    [Authorize]
    public class DoctorDeptAppService : EcisPatientAppService, IDoctorDeptAppService
    {
        private readonly IFreeSql _freeSql;

        /// <summary>
        /// 医生科室诊室
        /// </summary>
        public DoctorDeptAppService(IFreeSql freeSql)
        {
            _freeSql = freeSql;
        }

        /// <summary>
        /// 保存医生选择的科室诊室
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseResult<string>> CreateAsync(DoctorDeptDto dto)
        {
            try
            {
                int rows = 0;
                string doctorCode = CurrentUser.UserName;
                if (await _freeSql.Select<DoctorDept>().AnyAsync(x => x.DoctorCode == doctorCode))
                {
                    rows = await _freeSql.Update<DoctorDept>()
                        .Set(s => s.Dept, dto.Dept)
                        .Set(s => s.Room, dto.Room)
                        .Where(x => x.DoctorCode == doctorCode)
                        .ExecuteAffrowsAsync();
                }
                else
                {
                    rows = await _freeSql.Insert(new DoctorDept(GuidGenerator.Create(), doctorCode, dto.Dept, dto.Room)).ExecuteAffrowsAsync();
                }

                if (rows > 0)
                {
                    return RespUtil.Ok<string>(msg: "保存成功");
                }

                return RespUtil.Error<string>(msg: "保存失败！原因：更新数据失败！");
            }
            catch (Exception e)
            {
                return RespUtil.InternalError<string>(msg: "保存失败！原因：" + e.Message);
            }
        }

        /// <summary>
        /// 查询当前医生的科室诊室
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseResult<DoctorDeptDto>> GetAsync()
        {
            try
            {
                string doctorCode = CurrentUser.UserName;
                var model = await _freeSql.Select<DoctorDept>().Where(x => x.DoctorCode == doctorCode)
                    .FirstAsync<DoctorDeptDto>();
                return RespUtil.Ok(data: model);
            }
            catch (Exception e)
            {
                return RespUtil.Error<DoctorDeptDto>(msg: e.Message);
            }
        }
    }
}
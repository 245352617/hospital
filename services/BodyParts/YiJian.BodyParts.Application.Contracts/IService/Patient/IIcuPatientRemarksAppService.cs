using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YiJian.BodyParts.Application.Contracts.Dtos.Patient;
using YiJian.BodyParts.Domain.Shared.Enum;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace YiJian.BodyParts.Application.Contracts.IService.Patient
{

    /// <summary>
    /// 标签
    /// </summary>
    public interface IIcuPatientRemarksAppService : ITransientDependency, IApplicationService
    {

        /// <summary>
        /// 标签添加
        /// </summary>
        /// <param name="Dto"></param>
        /// <returns></returns>
        Task<JsonResult> PostPatientRemarksAsync(PatientRemarksDto Dto, IcuPatientRemarksEunm Type = IcuPatientRemarksEunm.Remarks);


        /// <summary>
        /// 标签添加
        /// </summary>
        /// <param name="Dto"></param>
        /// <returns></returns>
        Task<JsonResult> PostPatientRemarksListAsync(string PI_ID, List<PatientRemarkslistDto> Dto, IcuPatientRemarksEunm Type = IcuPatientRemarksEunm.Remarks);


        /// <summary>
        /// 特护单标签添加或者修改
        /// </summary>
        /// <param name="Dto"></param>
        /// <returns></returns>
        Task<JsonResult> PutSpecialPatientRemarksAsync(TePatientRemarksDto Dto);

        /// <summary>
        /// 特护单标签
        /// </summary>
        /// <param name="Dto"></param>
        /// <returns></returns>
        Task<JsonResult> PostSpecialPatientRemarksAsync(TsePatientRemarksDto Dto);


        /// <summary>
        /// 标签修改
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Dto"></param>
        /// <returns></returns>
        Task<JsonResult> PutPatientRemarksAsync(Guid Id, PatientRemarksDto Dto, IcuPatientRemarksEunm Type = IcuPatientRemarksEunm.Remarks);


        /// <summary>
        /// 标签删除
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        Task<JsonResult> DeletePatientRemarksAsync(List<Guid> Ids);




        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="PI_ID">入科号码</param>
        /// <param name="Lable">标签名称</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">一页多少</param>
        /// <returns></returns>
        Task<JsonResult<List<PatientRemarksDto>>> GetPatientRemarksAsync(string PI_ID,string Lable);



        /// <summary>
        /// 获取特护单页签
        /// </summary>
        /// <param name="PI_ID"></param>
        /// <returns></returns>
        Task<JsonResult> GetSpecialPatientRemarksAsync(string PI_ID);

    }
}

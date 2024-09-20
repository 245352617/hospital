using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.EmrPermissions.Contracts;
using YiJian.EMR.EmrPermissions.Dto;
using YiJian.EMR.EmrPermissions.Entities;

namespace YiJian.EMR.EmrPermissions
{
    /// <summary>
    /// 权限后台配置管理功能
    /// </summary>
    [Authorize]
    public class PermissionAppService : EMRAppService, IPermissionAppService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionAppService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        /// <summary>
        /// 获取所有的权限列表内容
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseBase<List<PermissionDto>>> GetListAsync()
        {
            List<PermissionDto> data = new List<PermissionDto>();
            var list = await _permissionRepository.GetAllAsync();
            if (!list.Any()) return new ResponseBase<List<PermissionDto>>(EStatusCode.CNULL);
            foreach (var item in list)
            {
                var operatingAccounts = string.Join('、', item.OperatingAccounts.Select(s => s.DoctorName));
                var model = new PermissionDto(
                    id: item.Id,
                    module: item.Module,
                    permissionTitle: item.PermissionTitle,
                remark: item.Remark,
                    operatingAccounts: operatingAccounts);
                data.Add(model);
            }
            return new ResponseBase<List<PermissionDto>>(EStatusCode.COK, data);
        }

        /// <summary>
        /// 获取指定权限下的所有授权人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseBase<PermissionSampleDto>> GetOperatingAccountAsync(int id)
        {
            PermissionSampleDto data = new();
            var model = await _permissionRepository.GetOperatingAccountAsync(id);
            if (model == null) return new ResponseBase<PermissionSampleDto>(EStatusCode.CNULL);
            if (!model.OperatingAccounts.Any()) return new ResponseBase<PermissionSampleDto>(EStatusCode.CNULL);

            data.Id = model.Id;
            data.AllOperatingAccounts = model.OperatingAccounts
                .Select(s => new OperatingAccountBaseDto
                {
                    Id = s.Id,
                    DeptCode = s.DeptCode,
                    DeptName = s.DeptName,
                    Code = s.DoctorCode,
                    Name = s.DoctorName,
                    PermissionId = s.PermissionId
                }).ToList();
            data.GroupByDeptOperatingAccounts = model.OperatingAccounts
                .GroupBy(g => g.DeptCode)
                .Select(s => new GroupByDeptDto
                {
                    DeptCode = s.Key,
                    DeptName = s.Max(m => m.DeptName),
                    OperatingAccounts = s.Select(x => new OperatingAccountDto
                    {
                        Code = x.DoctorCode,
                        Name = x.DoctorCode,
                        Id = x.Id
                    }).ToList()
                }).ToList();
            return new ResponseBase<PermissionSampleDto>(EStatusCode.COK, data);
        }

        /// <summary>
        /// 更新权限内容
        /// </summary> 
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseBase<bool>> UpdateOperatingAccountsAsync(ModifyPermissionDto model)
        {
            if (!model.OperatingAccountDto.Any()) return new ResponseBase<bool>(EStatusCode.CNULL);

            List<OperatingAccount> accounts = model.OperatingAccountDto
                .Select(s => new OperatingAccount(
                    deptCode: s.DeptCode,
                    deptName: s.DeptName,
                    doctorCode: s.Code,
                    doctorName: s.Name,
                    permissionId: s.PermissionId
                    )
                )
                .ToList();

            var data = await _permissionRepository.UpdateOperatingAccountsAsync(model.Id, accounts);
            return new ResponseBase<bool>(EStatusCode.COK, data);
        } 
    }
}

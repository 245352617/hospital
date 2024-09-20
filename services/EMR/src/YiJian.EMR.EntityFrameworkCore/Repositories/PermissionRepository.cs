using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.EntityFrameworkCore;
using YiJian.EMR.EmrPermissions.Contracts;
using YiJian.EMR.EmrPermissions.Entities;
using Volo.Abp.Localization;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Repositories
{
    public class PermissionRepository : EfCoreRepository<EMRDbContext, Permission, int>, IPermissionRepository
    {
        /// <summary>
        /// 权限管理
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public PermissionRepository(IDbContextProvider<EMRDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 根据医生code获取操作人的权限信息
        /// </summary>
        /// <param name="docktorCode">医生编码</param>
        /// <param name="permissionCode">权限编码</param>
        /// <param name="deptCode">科室编码</param>
        /// <returns></returns>
        public async Task<List<Permission>> GetByDoctorCodeAsync(string docktorCode, EPermissionCode permissionCode, string deptCode = "")
        {
            if (permissionCode == EPermissionCode.DepartmentTemplate && deptCode.IsNullOrEmpty()) return new List<Permission>();

            var db = await GetDbContextAsync();
            var query = db.OperatingAccounts
                .Include(i => i.Permission)
                .Where(w => w.DoctorCode == docktorCode.Trim())
                // 与 Pat_DoctorDept 里面的科室编码存在冲突,讨论确认后去掉科室编码验证
                //.WhereIf(permissionCode == EPermissionCode.DepartmentTemplate, w => w.DeptCode == deptCode) 
                .Select(s => s.Permission)
                .Where(w => w.PermissionCode == permissionCode);
            //Console.WriteLine(query.ToQueryString()); 
            return await query.ToListAsync();
        }

        /// <summary>
        /// 医生编码获取权限
        /// </summary>
        /// <param name="docktorCode"></param>
        /// <returns></returns>
        public async Task<List<Permission>> GetPermissionByDoctorCodeAsync(string docktorCode)
        {
            var db = await GetDbContextAsync();
            var query = db.OperatingAccounts
                .Include(i => i.Permission)
                .Where(w => w.DoctorCode == docktorCode.Trim())
                .Select(s => s.Permission);
            return await query.ToListAsync();
        }


        /// <summary>
        /// 获取所有的权限内容
        /// </summary>
        /// <returns></returns>
        public async Task<List<Permission>> GetAllAsync()
        {
            var db = await GetDbContextAsync();
            return await db.Permissions.Include(i => i.OperatingAccounts).ToListAsync();
        }

        /// <summary>
        /// 获取指定权限下的所有授权人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Permission> GetOperatingAccountAsync(int id)
        {
            var db = await GetDbContextAsync();
            return await db.Permissions.Include(i => i.OperatingAccounts).FirstOrDefaultAsync(w => w.Id == id);
        }

        /// <summary>
        /// 更新权限内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accounts"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOperatingAccountsAsync(int id, List<OperatingAccount> accounts)
        {
            if (!accounts.Any()) return false;
            var db = await GetDbContextAsync();
            var entity = await db.Permissions.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id);
            if (entity == null) return false;
            var entities = await db.OperatingAccounts.Where(w => w.PermissionId == id).ToListAsync();
            List<OperatingAccount> additem = new();
            List<OperatingAccount> delitem = new();
            //求差集, 添加新数据
            foreach (var item in accounts)
            {
                var exist = entities.Exists(w => w.DeptCode == item.DeptCode && w.DoctorCode == item.DoctorCode);
                if (!exist) additem.Add(item);
            }
            //求差集，删除在库数据
            foreach (var item in entities)
            {
                var exist = accounts.Exists(w => w.DeptCode == item.DeptCode && w.DoctorCode == item.DoctorCode);
                if (!exist) delitem.Add(item);
            }
            await db.AddRangeAsync(additem);
            db.RemoveRange(delitem);
            return true;
        }

    }
}

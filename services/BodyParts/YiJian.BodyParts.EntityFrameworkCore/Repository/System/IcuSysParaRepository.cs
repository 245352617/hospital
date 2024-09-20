using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YiJian.BodyParts.Domain.Shared.Const;
using Microsoft.Extensions.Logging;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace YiJian.BodyParts.Repository
{
    /// <summary>
    /// 表:系统-参数设置表
    /// </summary>
    public class IcuSysParaRepository : BaseRepository<EntityFrameworkCore.DbContext, IcuSysPara, Guid>, IIcuSysParaRepository
    {
        public IcuSysParaRepository(IDbContextProvider<EntityFrameworkCore.DbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        #region 实现接口
        /// <summary>
        /// 获取集合的配置
        /// </summary>
        /// <param name="paraCode"></param>
        /// <returns></returns>
        public async Task<List<string>> getListValue(string paraCode)
        {
            var sysParam = await this.FirstOrDefaultAsync(s => s.ParaCode == paraCode && s.ParaType == Constants.paramTypeSys);
            if (sysParam == null)
            {
                Logger.LogWarning($"can not get sysPara by paraCode = {Constants.paraCodeCanModifyNursingRecordStaffCodes}");
                return new List<string>();
            }

            string canModifyStaffCodes = sysParam.ParaValue;
            if (string.IsNullOrEmpty(canModifyStaffCodes))
            {
                Logger.LogError("canModifyStaffCodes value is null or empty");
                return new List<string>();
            }

            // 配置项的值必须为数字母的混合
            Regex regex = new System.Text.RegularExpressions.Regex("^[0-9a-zA-Z]+(,[0-9a-zA-Z]+)*$");
            if (regex.IsMatch(canModifyStaffCodes))
            {
                return canModifyStaffCodes.Split(",").ToList();
            }
            else
            {
                Logger.LogError("canModifyStaffCodes value format error");
                return new List<string>();
            }
        }

        /// <summary>
        /// 获取系统参数设置
        /// </summary>
        /// <param name="paraType">参数类别(D-科室级 S-系统级别)</param>
        /// <returns></returns>
        public async Task<string> GetParaValue(string paraType, string deptCode, string paraCode)
        {
            IcuSysPara icuSysPara = null;

            if (paraType == "S")
            {
                icuSysPara = await DbContext.IcuSysPara.Where(s => s.ParaType == paraType && s.ParaCode.Trim() == paraCode.Trim())?.FirstOrDefaultAsync();
            }
            else if (paraType == "D")
            {
                icuSysPara = await DbContext.IcuSysPara.Where(s => s.ParaType == paraType && s.DeptCode == deptCode && s.ParaCode.Trim() == paraCode.Trim())?.FirstOrDefaultAsync();
            }
            //查询科室信息
            return icuSysPara == null ? null : icuSysPara.ParaValue;
        }

        /// <summary>
        /// 获取护理全局权限，包括：观察项、出入量、护理平台修改删除权限
        /// 护理记录模板删除权限，
        /// </summary>
        /// <param name="currentStaffcode">新记录护士</param>
        /// <param name="nurseCode">旧记录护士</param>
        /// <returns></returns>
        public async Task<AuthorityEnum> GetNursingAuthority(string currentStaffcode, string nurseCode)
        {
            try
            {
                AuthorityEnum re = AuthorityEnum.验证通过;

                // 获取当前登录用户信息
                if (string.IsNullOrEmpty(currentStaffcode))
                {
                    re = AuthorityEnum.session过期;
                    return re;
                }
                //只有配置了审核权限的人才可以删除护理记录模板数据
                List<string> canEditStaffCodes = await getListValue(Constants.paraCodeCanModifyNursingRecordStaffCodes);
                if (canEditStaffCodes != null && canEditStaffCodes.Count > 0)
                {
                    if (!canEditStaffCodes.Contains(currentStaffcode))
                    {
                        re = AuthorityEnum.您无权限修改;
                    }
                }
                else
                {
                    re = AuthorityEnum.您无权限修改;
                }

                //同一个人操作可修改
                if (!string.IsNullOrEmpty(nurseCode) && currentStaffcode == nurseCode)
                {
                    re = AuthorityEnum.验证通过;
                }

                //自动获取的数据首次可以任意修改
                if (nurseCode == "Auto" || nurseCode == "00000")
                {
                    re = AuthorityEnum.验证通过;
                }


                return re;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<IcuSysPara>> GetParaListAsync(string deptCode, string paraType, int type)
        {
            if (paraType == "S")
            {
                return await DbContext.IcuSysPara.Where(p => p.ParaType == paraType && (int)p.Type == type).ToListAsync();
            }
            return await DbContext.IcuSysPara.Where(p => p.DeptCode == deptCode && p.ParaType == paraType && (int)p.Type == type).ToListAsync();
        }

        #endregion
    }
}

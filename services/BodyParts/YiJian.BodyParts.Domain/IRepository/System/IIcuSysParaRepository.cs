using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.BodyParts.IRepository
{
    /// <summary>
    /// 表:系统-参数设置表
    /// </summary>
    public interface IIcuSysParaRepository : IRepository<IcuSysPara, Guid>, IBaseRepository<IcuSysPara, Guid>
    {
        #region 定义接口

        Task<List<string>> getListValue(string paraCode);

        Task<string> GetParaValue(string paraType, string deptCode, string paraCode);


        /// <summary>
        /// 获取护理全局权限，包括：观察项、出入量、护理平台修改删除权限，
        /// 护理记录模板删除权限，
        /// </summary>
        /// <param name="currentStaffcode">新记录护士</param>
        /// <param name="nurseCode">旧记录护士</param>
        /// <returns></returns>
        Task<AuthorityEnum> GetNursingAuthority(string currentStaffcode, string nurseCode);

        #endregion

        /// <summary>
        /// 获取当前科室所有配置
        /// </summary>
        /// <param name="deptCode">科室编号</param>
        /// <param name="paraType">参数类型 S：系统参数  D：科室参数</param>
        /// <param name="type">类型 1系统配置  2特护单配置</param>
        /// <returns></returns>
        Task<IEnumerable<IcuSysPara>> GetParaListAsync(string deptCode,string paraType, int type);
    }
}
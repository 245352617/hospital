using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 报表权限设置
    /// </summary>
    public class ReportPermission : Entity<int>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Description("用户名")]
        [StringLength(20)]
        public string UserName { get; set; }

        /// <summary>
        /// 报表
        /// </summary>
        [Description("报表名")]
        [StringLength(20)]
        public string ReportName { get; set; }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;

namespace Szyjian.Ecis.Patient.Application
{

    /// <summary>
    /// 急危重症一体化用户信息，扩展Abp当前用户
    /// </summary>
    public class EcisCurrentUser : CurrentUser, IEcisCurrentUser, ITransientDependency
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="principalAccessor"></param>
        public EcisCurrentUser(ICurrentPrincipalAccessor principalAccessor) : base(principalAccessor)
        {
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public override string UserName => this.FindClaimValue("name");

        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName => this.FindClaimValue("fullName");

        /// <summary>
        /// 姓名
        /// </summary>
        public override string Name => this.FindClaimValue("fullName");

        /// <summary>
        /// 用户所属科室
        /// </summary>
        public SzyjDepartment? Dept
        {
            get
            {
                var deptClaims = this.FindClaim("Department");
                if (deptClaims == null)
                {
                    return null;
                }

                var dept = JsonConvert.DeserializeObject<SzyjDepartment>(deptClaims.Value);
                return dept;
            }
        }

        /// <summary>
        /// 已授权的科室
        /// </summary>
        public IEnumerable<SzyjDepartment>? AuthDept
        {
            get
            {
                var deptClaims = this.FindClaim("Departments");
                if (deptClaims != null)
                {
                    var dept = JsonConvert.DeserializeObject<IEnumerable<SzyjDepartment>>(deptClaims.Value);
                    return dept;
                }
                return null;
            }
        }
    }
}
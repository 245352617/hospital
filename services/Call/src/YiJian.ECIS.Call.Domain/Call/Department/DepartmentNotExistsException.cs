using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace YiJian.ECIS.Call.Domain
{
    /// <summary>
    /// 科室不存在业务异常类
    /// </summary>
    public class DepartmentNotExistsException : BusinessException, IUserFriendlyException
    {
        /// <summary>
        /// 
        /// </summary>
        public DepartmentNotExistsException()
            : base(message: "科室不存在")
        {
        }
    }
}

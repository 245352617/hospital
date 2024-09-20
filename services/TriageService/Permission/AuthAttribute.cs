using System;
using Microsoft.AspNetCore.Authorization;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 认证特性
    /// </summary>
    public class AuthAttribute : Attribute
        //AuthorizeAttribute
    {
        
        public AuthAttribute()
        {
        }

        public AuthAttribute(string policy,string name = "")
        {
            //是否启用策略授权
            if (PermissionDefinition.IsEnabledPermission)
            {
                /*this.Policy = PermissionDefinition.AppName + PermissionDefinition.Separator + PermissionDefinition.ServiceName +
                              PermissionDefinition.Separator + policy;*/
            }
                
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 平台用户
    /// </summary>
    public class PlatformUser
    {
        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Position { get; set; }
    }
}

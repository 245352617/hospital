using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class FastTrackSettingWhereInput
    {
        /// <summary>
        /// id  
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 电话或姓名
        /// </summary>
        public string  PhoneOrName { get; set; }
        /// <summary>
        /// 启用就是true ，禁用就是false
        /// </summary>

        public string IsEnable { get; set; }


    }
}

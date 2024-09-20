using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class RegisterModeData
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 挂号模式代码
        /// </summary>
        public string TriageConfigCode { get; set; }

        /// <summary>
        /// 挂号模式名称
        /// </summary>
        public string TriageConfigName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsDisable { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}

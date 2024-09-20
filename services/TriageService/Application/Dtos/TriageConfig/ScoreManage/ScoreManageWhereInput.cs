using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class ScoreManageWhereInput
    {
        /// <summary>
        /// 评分名称
        /// </summary>
        public string ScoreName { get; set; }
        
        /// <summary>
        /// 启用禁用,传true或者false，没有该判断空值
        /// </summary>
        public string  IsEnable { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 删除导管感染Dto
    /// </summary>
    public class DeleteCanulaInfectDto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 导管类型：VAP导管 = 1,CRBSI导管 = 2,CATUI导管 = 3
        /// </summary>
        public CanulaTypeEnum CanulaType { get; set; }

        /// <summary>
        /// 确诊时间
        /// </summary>
        public DateTime InfectTime { get; set; }

        /// <summary>
        /// 导管分类
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 插管部位
        /// </summary>
        public string CanulaPart { get; set; }
    }
}

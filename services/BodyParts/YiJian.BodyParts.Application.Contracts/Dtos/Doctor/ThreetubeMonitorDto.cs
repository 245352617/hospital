using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 三管监测Dto
    /// </summary>
    public class ThreetubeMonitorDto
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
        /// 导管分类
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 导管名称
        /// </summary>
        /// <example></example>
        public string ModuleName { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParaName { get; set; }

        /// <summary>
        /// 插管部位
        /// </summary>
        public string CanulaPart { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public List<object> paraItems { get; set; }
    }

    /// <summary>
    /// 插管列表
    /// </summary>
    public class CanulaValue
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? Time { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 导管感染护理表 Dto
    /// </summary>
    public class CreateCanulaInfectDto
    {
        public Guid Id { get; set; }

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
        /// 是否48小时内
        /// </summary>
        /// <example></example>
        public bool IsFortyeight { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>
        /// <example></example>
        public string[] Diagnosis { get; set; }

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
        /// 管道名称
        /// </summary>
        public string CanulaName { get; set; }

        /// <summary>
        /// 插管部位
        /// </summary>
        public string CanulaPart { get; set; }
    }
}

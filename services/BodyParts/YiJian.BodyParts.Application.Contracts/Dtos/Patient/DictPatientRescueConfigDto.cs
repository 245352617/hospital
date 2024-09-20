using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 患者抢救配置表DTO
    /// </summary>
    public class DictPatientRescueConfigDto
    {
        public string Id { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string RescueConfigName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime RescueConfigTime { get; set; }

        /// <summary>
        /// 设置类型，0：抢救措施；1：常用药物
        /// </summary>
        public RescueConfigTypeEnum RescueConfigType { get; set; }
    }
}
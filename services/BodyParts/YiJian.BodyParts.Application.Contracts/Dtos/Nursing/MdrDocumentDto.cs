using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// MDR感控措施执行监控单
    /// </summary>
    public class MdrDocumentDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 所属文书
        /// </summary>
        public string BelongToKey { get; set; }

        /// <summary>
        /// 预防性隔离
        /// </summary>
        public string PreventiveIsolation { get; set; }

        /// <summary>
        /// 人员流水号
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 新增日期
        /// </summary>
        public DateTime UseTime { get; set; }

        /// <summary>
        /// 标本
        /// </summary>
        public string Specimen { get; set; }

        /// <summary>
        /// 病原体
        /// </summary>
        public string Pathogen { get; set; }

        /// <summary>
        /// 感染来源
        /// </summary>
        public string InfectionSource { get; set; }

        /// <summary>
        /// 隔离医嘱
        /// </summary>
        public string IsolationAdvice { get; set; }

        /// <summary>
        /// 患者安置
        /// </summary>
        public string PatientPut { get; set; }

        /// <summary>
        /// 隔离标识
        /// </summary>
        public string IsolationIdentification { get; set; }

        /// <summary>
        /// 护士编号
        /// </summary>
        /// <example></example>
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        /// <example></example>
        public string NurseName { get; set; }
    }
}

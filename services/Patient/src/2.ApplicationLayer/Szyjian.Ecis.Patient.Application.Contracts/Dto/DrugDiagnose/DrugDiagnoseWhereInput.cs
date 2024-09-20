using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class DrugDiagnoseWhereInput
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        /// <example>1</example>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        /// <example>20</example>
        public int PageSize { get; set; }

        /// <summary>
        /// 传入参数
        /// </summary>
        public string InputParam { get; set; }

        /// <summary>
        /// 患者分诊id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 中西医诊断
        /// </summary>
        public int DiagType { get; set; } = -1;
    }
}
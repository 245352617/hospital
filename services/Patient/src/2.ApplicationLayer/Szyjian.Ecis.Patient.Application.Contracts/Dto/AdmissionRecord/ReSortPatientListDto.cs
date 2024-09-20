using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 重新排序患者列表Dto
    /// </summary>
    public class ReSortPatientListDto
    {
        /// <summary>
        /// 新序号
        /// </summary>
        public int NewSort { get; set; }

        /// <summary>
        /// 患者分诊Id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 旧序号
        /// </summary>
        public int OldSort { get; set; }
    }
}
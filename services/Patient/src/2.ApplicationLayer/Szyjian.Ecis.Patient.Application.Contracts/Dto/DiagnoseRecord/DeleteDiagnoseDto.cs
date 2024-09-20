using System;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 删除诊断Dto
    /// </summary>
    public class DeleteDiagnoseDto
    {
        /// <summary>
        /// 诊断类型 1：开立  2：收藏
        /// </summary>
        public DiagnoseClass DiagnoseClass { get; set; }

        /// <summary>
        /// 自增Id
        /// </summary>
        public int PD_ID { get; set; }

        /// <summary>
        /// 患者分诊Id（取消收藏时不传此参数）
        /// </summary>
        public Guid PI_ID { get; set; }
    }
}
using System.Collections.Generic;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 诊断科室信息Dto
    /// </summary>
    public class DiagnoseWithDeptDto
    {
        /// <summary>
        /// 就诊日期
        /// </summary>
        public string VisitDate { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 分诊级别
        /// </summary>
        public string TriageLevelName { get; set; }

        /// <summary>
        /// 诊断数量
        /// </summary>
        public string DiagnoseCount { get; set; }

        /// <summary>
        /// 诊断列表
        /// </summary>
        public ICollection<HistoryDiagnoseDto> Diagnoses { get; set; }
    }
}
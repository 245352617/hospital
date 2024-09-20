using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class DrugDiagnoseDto
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 诊断id
        /// </summary>
        public int PD_ID { get; set; } = -1;
        /// <summary>
        /// 诊断代码
        /// </summary>
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 诊断码
        /// </summary>
        public string PyCode { get; set; }

        /// <summary>
        /// 是否收藏
        /// </summary>
        public bool IsCollected { get; set; } = false;

        /// <summary>
        /// Icd10
        /// </summary>
        public string Icd10 { get; set; }

        /// <summary>
        /// 报卡类型
        /// </summary>
        public ECardReportingType CardRepType { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 1.西医诊断2.中医诊断
        /// </summary>
        public int DiagType { get; set; }

    }
}
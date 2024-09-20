using System.Collections.Generic;
using Volo.Abp.EventBus;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 订阅危急值Eto
    /// </summary>
    [EventName("Crisis")]
    public class CrisisHandlerEto
    {
        /// <summary>
        /// 危机值信息
        /// </summary>
        public List<CrisisEto> DicDatas { get; set; }
    }


    /// <summary>
    /// 描    述:危机值信息
    /// 创 建 人:杨凯
    /// 创建时间:2023/10/10 16:05:53
    /// </summary>
    public class CrisisEto
    {
        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 病案号
        /// </summary>
        public string MedicalRecordNo { get; set; }

        /// <summary>
        /// 开单医生编码
        /// </summary>
        public string ApplyDoctorCode { get; set; }

        /// <summary>
        /// 开单医生
        /// </summary>
        public string ApplyDoctorName { get; set; }

        /// <summary>
        /// 危急值项目
        /// </summary>
        public string CrisisName { get; set; }

        /// <summary>
        /// 危急值数值
        /// </summary>
        public string CrisisValue { get; set; }

        /// <summary>
        /// 发报告号人编码
        /// </summary>
        public string ReporterCode { get; set; }

        /// <summary>
        /// 发报告号人
        /// </summary>
        public string ReporterName { get; set; }

        /// <summary>
        /// 发报告时间
        /// </summary>
        public string ReporterTime { get; set; }

        /// <summary>
        /// 样本号
        /// </summary>
        public string SampleNo { get; set; }

        /// <summary>
        /// 危急值项目及数值
        /// </summary>
        public string CrisisDetails { get; set; }
    }
}

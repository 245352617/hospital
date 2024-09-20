using System;

namespace SamJan.MicroService.PreHospital.TriageService.Application.Dtos.Triage.Patient
{
    /// <summary>
    /// 推送到His的分诊数据Dto
    /// </summary>
    public class PushTriageInfoToHisDto
    {
        /// <summary>
        /// 病患ID
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string deptName { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string visitNo { get; set; }
        /// <summary>
        /// 挂号流水号
        /// </summary>
        public string regSerialNo { get; set; }
        /// <summary>
        /// 分诊级别
        /// </summary>
        public string triageLevel { get; set; }

        /// <summary>
        /// 分诊级别名称
        /// </summary>
        public string triageLevelName { get; set; }

        /// <summary>
        /// 记录人编码
        /// </summary>
        public string recorderCode { get; set; }

        /// <summary>
        /// 记录人
        /// </summary>
        public string recorderName { get; set; }
        /// <summary>
        /// 绿通标识  
        /// </summary>
        public string greenLogo { get; set; }
        /// <summary>
        /// 收缩压
        /// </summary>
        public string sbp { get; set; }

        /// <summary>
        /// 舒张压
        /// </summary>
        public string sdp { get; set; }
        /// <summary>
        /// 血氧饱和度（%）
        /// </summary>
        public string spO2 { get; set; }
        /// <summary>
        /// 呼吸
        /// </summary>
        public string breathRate { get; set; }
        /// <summary>
        /// 体温（摄氏度）
        /// </summary>
        public string temp { get; set; }
        /// <summary>
        /// 心率
        /// </summary>
        public string heartRate { get; set; }
        /// <summary>
        /// 血糖（单位 mmol/L）
        /// </summary>
        public string bloodGlucose { get; set; }
        /// <summary>
        /// 体重（kg）
        /// </summary>
        public string weight { get; set; }
        /// <summary>
        /// 意识
        /// </summary>
        public string consciousness { get; set; }
    }
}
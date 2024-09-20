using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// CS 版急诊患者入科Dto
    /// </summary>
    public class CsEcisPatientInDeptDto
    {

        /// <summary>
        /// ECIS唯一标识
        /// </summary>
        public Guid PVID { get; set; }

        /// <summary>
        /// 患者ID
        /// </summary>
        public string PatientID { get; set; }

        /// <summary>
        /// 就诊序号
        /// </summary>
        public string VisitID { get; set; }

        /// <summary>
        /// 就诊时间
        /// </summary>
        public DateTime? VisitDate { get; set; }

        /// <summary>
        /// 0 已入科， 1已接诊，2 结束就诊，3 转区未接诊，5转住院，9 死亡
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 患者当前所在区域
        /// </summary>
        public string WardArea { get; set; }

        /// <summary>
        /// 患者当前所在科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 患者当前所在科室（名称）
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        public string BedNo { get; set; }

        /// <summary>
        /// 护理级别
        /// </summary>
        public string ClinicType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FstTreatCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FstTreatName { get; set; }

        /// <summary>
        /// 责任护士代码
        /// </summary>
        public string NurseCode { get; set; }

        /// <summary>
        /// 责任护士名
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 0 否 ，1 非计划重入抢救间
        /// </summary>
        public int IsPlanBackWard { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime? InDeptTime { get; set; }

        /// <summary>
        /// 入院方式： 1 步行，2 平车，3 轮椅，4 抱入
        /// </summary>
        public string InDeptWay { get; set; }

        /// <summary>
        /// 操作员代码
        /// </summary>
        public string OperatorCode { get; set; }

        /// <summary>
        /// 操作员名称
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 患者卡号
        /// </summary>
        public string Additional1 { get; set; }

        /// <summary>
        /// 分诊级别
        /// </summary>
        public string Additional2 { get; set; }

        /// <summary>
        /// 备用字段3
        /// </summary>
        public string Additional3 { get; set; }

        public string Irritability { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        public float Height { get; set; }

        public string PastHistory { get; set; }

        public string InfectiousHistory { get; set; }

        public string Announcements { get; set; }

        public string SpecialMark { get; set; }

        public string Criticallevel { get; set; }
    }
}
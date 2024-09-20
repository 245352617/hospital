using System;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 查询Dto
    /// </summary>
    public class AdmissionRecordInput
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
        /// 就诊状态(可传入多个状态，使用,拼接)  未挂号 = 0, 待就诊 = 1, 过号 = 2, 已退号 = 3, 正在就诊 = 4,已就诊 = 5, 出科 = 6,
        /// </summary>
        public VisitStatus VisitStatus { get; set; }

        /// <summary>
        /// 患者状态
        /// </summary>
        public PatientStatus? PatientStatus { get; set; }

        /// <summary>
        /// 检索文本支持模糊查询（患者病历号/姓名/首诊医生姓名/首诊医生编码）
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// 诊断检索文本
        /// </summary>
        public string DiagnoseSearcheText { get; set; }

        /// <summary>
        /// 分诊级别
        /// </summary>
        public string TriageLevel { get; set; }

        /// <summary>
        /// 关注人编码
        /// </summary>
        public string AttentionCode { get; set; }

        /// <summary>
        /// 接诊人编码
        /// </summary>
        public string OperatorCode { get; set; }
        /// <summary>
        /// 首诊医生编码
        /// </summary>
        public string FirstDoctorCode { get; set; }

        /// <summary>
        /// 分诊科室编码
        /// </summary>
        public string TriageDeptCode { get; set; }

        /// <summary>
        /// 就诊时间开始
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 就诊时间结束
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 就诊区域
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 是否查询历史就诊
        /// </summary>
        public bool IsHistory { get; set; }

        /// <summary>
        /// 是否住院
        /// </summary>
        public int HasBed { get; set; } = -1;

        /// <summary>
        /// 责任护士
        /// </summary>
        public string DutyNurseCode { get; set; }

        /// <summary>
        /// 我的患者
        /// </summary>
        public bool MyPatient { get; set; } = false;

        /// <summary>
        /// 责任医生
        /// </summary>
        public string DutyDoctorCode { get; set; }

        /// <summary>
        /// 交班开始时间
        /// </summary>
        public DateTime? HandoverStartTime { get; set; }

        /// <summary>
        /// 交班结束时间
        /// </summary>
        public DateTime? HandoverEndTime { get; set; }

        /// <summary>
        /// 分诊科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 护理等级
        /// </summary>
        public string Pflegestufe { get; set; }

        /// <summary>
        /// 床头贴
        /// </summary>
        public string BedHeadStickerList { get; set; }
    }
}
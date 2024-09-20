using System;
using System.Collections.Generic;
using YiJian.Hospitals.Enums;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 检验报告详情查询
    /// </summary>
    public class GetLisReportResponse
    {
        /// <summary>
        /// 就诊卡号		实体卡片号
        /// </summary>
        public string IcCardNo { get; set; }

        /// <summary>
        /// 病人ID 	内部唯一号
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊流水号  就诊唯一号
        /// </summary>
        public string VisitSerialNo { get; set; }

        /// <summary>
        /// 就诊类型	是	1门诊;2住院;3体检;
        /// </summary>
        public EPatientType PatientType { get; set; }

        /// <summary>
        /// 住院次数
        /// </summary>
        public string VisitNumber { get; set; }

        /// <summary>
        /// 医保号
        /// </summary>
        public string SafetyNo { get; set; }

        /// <summary>
        /// 费用类别
        /// </summary>
        public string ChargeType { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别编码
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 病人所在科室  申请手术科室
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 病人所在床号  门诊病人为空
        /// </summary>
        public string BedNo { get; set; }

        /// <summary>
        /// 病情说明	 对病人体征、病情等进一步说明
        /// </summary>
        public string PatientCondition { get; set; }

        /// <summary>
        /// 检查号
        /// </summary>
        public string LisNo { get; set; }

        /// <summary>
        /// 诊断代码	 多个用分号隔开
        /// </summary>
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称	  多个用分号隔开
        /// </summary>
        public string DiagnoseName { get; set; }

        public List<ListApplyInfoResponse> applyInfoList { get; set; } = new List<ListApplyInfoResponse>();

        /// <summary>
        /// 条码号
        /// </summary>
        public string BarcodeNo { get; set; }

        /// <summary>
        /// 急诊标志		0普通;1急诊
        /// </summary>
        public string EmergencyFlag { get; set; }

        /// <summary>
        /// 报告单号
        /// </summary>
        public string ReportNo { get; set; }

        /// <summary>
        /// 报告标题
        /// </summary>
        public string ReportTitle { get; set; }

        /// <summary>
        /// 科目类别		生化，免疫，细菌…
        /// </summary>
        public string SubjectClass { get; set; }

        /// <summary>
        /// 检验目的
        /// </summary>
        public string LabPurpose { get; set; }

        /// <summary>
        /// 检验方法
        /// </summary>
        public string LabMethod { get; set; }

        /// <summary>
        /// 标本代码
        /// </summary>
        public string SpecimenCode { get; set; }

        /// <summary>
        /// 标本名称
        /// </summary>
        public string SpecimenName { get; set; }

        /// <summary>
        /// 采集部位
        /// </summary>
        public string SpecimenCollectPart { get; set; }

        /// <summary>
        /// 部位描述
        /// </summary>
        public string SpecimenCollectPartDesc { get; set; }

        /// <summary>
        /// 危险程度
        /// </summary>
        public string SpecimenRiskFactor { get; set; }

        /// <summary>
        /// 程度描述
        /// </summary>
        public string SpecimenRiskFactorDesc { get; set; }

        /// <summary>
        /// 标本质量
        /// </summary>
        public string SpecimenQuality { get; set; }

        /// <summary>
        /// 质量描述
        /// </summary>
        public string SpecimenQualityDesc { get; set; }

        /// <summary>
        /// 仪器编码
        /// </summary>
        public string LabInstrument { get; set; }

        /// <summary>
        /// 仪器名称
        /// </summary>
        public string LabInstrumentName { get; set; }

        /// <summary>
        /// 采样时间
        /// </summary>
        public string SpecimenCollectTime { get; set; }

        /// <summary>
        /// 采样人
        /// </summary>
        public string SpecimenCollectOperator { get; set; }

        /// <summary>
        /// 采样人编号
        /// </summary>
        public string SpecimenCollectOperatorCode { get; set; }

        /// <summary>
        /// 标本接收时间
        /// </summary>
        public string SpecimenAcceptTime { get; set; }

        /// <summary>
        /// 标本接收人
        /// </summary>
        public string SpecimenAcceptOperator { get; set; }

        /// <summary>
        /// 标本接收人编号
        /// </summary>
        public string SpecimenAcceptOperatorCode { get; set; }

        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime? LabTime { get; set; }

        /// <summary>
        /// 检验科室
        /// </summary>
        public string LabDept { get; set; }

        /// <summary>
        /// 检验医生
        /// </summary>
        public string LabOperator { get; set; }

        /// <summary>
        /// 报告时间
        /// </summary>
        public string ReportTime { get; set; }

        /// <summary>
        /// 报告人
        /// </summary>
        public string ReportOperator { get; set; }

        /// <summary>
        /// 报告人编号
        /// </summary>
        public string ReportOperatorCode { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditTime { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string AuditOperator { get; set; }

        /// <summary>
        /// 审核人编号
        /// </summary>
        public string AuditOperatorCode { get; set; }

        /// <summary>
        /// 打印时间
        /// </summary>
        public DateTime? PrintTime { get; set; }

        /// <summary>
        /// 打印人
        /// </summary>
        public string PrintOperator { get; set; }

        /// <summary>
        /// 打印人编号
        /// </summary>
        public string PrintOperatorCode { get; set; }

        /// <summary>
        /// 模板编号
        /// </summary>
        public string ReportTemplateCode { get; set; }

        /// <summary>
        /// 报告url路径
        /// </summary>
        public string ReportUrl { get; set; }

        /// <summary>
        /// 报告类型		0:普通报告； 1：微生物报告
        /// </summary>
        public EReportType? ReportType { get; set; }

        /// <summary>
        /// 正常报告项列表
        /// </summary>
        public List<LisReportItemInfo> ReportItemList { get; set; } = new List<LisReportItemInfo>();

        /// <summary>
        /// 微生物报告项列表
        /// </summary>
        public List<LisMicroReportItemInfo> MicroReportItemList { get; set; } = new List<LisMicroReportItemInfo>();

    }


}
using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Patient
{

    public class PatientSurgeryDto
    {
        /// <summary>
        /// 患者Id "病人标识"
        /// </summary>
        public string PatientId { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string InpNo { get; set; }
        /// <summary>
        /// 本次住院标识
        /// </summary>
        public string VisitId { get; set; }
        /// <summary>
        ///  手术号=手术申请单号
        /// </summary>
        public string OperId { get; set; }
        /// <summary>
        /// 手术申请单号
        /// </summary>
        public string PlacerOrderNum { get; set; }
        /// <summary>
        /// 手术名称
        /// </summary>
        public string OperationName { get; set; }
        /// <summary>
        /// 手术开始日期及时间
        /// </summary>
        public DateTime? StartDateTime { get; set; }
        /// <summary>
        /// 手术结束日期及时间
        /// </summary>
        public DateTime? EndDateTime { get; set; }
        /// <summary>
        /// 麻醉开始日期及时间
        /// </summary>
        public DateTime? AnesStartTime { get; set; }
        /// <summary>
        /// 麻醉结束日期及时间
        /// </summary>
        public DateTime? AnesEndTime { get; set; }
        /// <summary>
        /// 手术等级
        /// </summary>
        public string OperationScale { get; set; }
        /// <summary>
        /// 手术部位
        /// </summary>
        public string OperationPosition { get; set; }
        /// <summary>
        /// 麻醉方式
        /// </summary>
        public string AnesthesiaMethod { get; set; }
        /// <summary>
        /// 麻醉医生姓名
        /// </summary>
        public string AnesthesiaDoctor { get; set; }
        /// <summary>
        /// 麻醉医生代码
        /// </summary>
        public string AnesthesiaDoctorCode { get; set; }
        /// <summary>
        /// 手术医生姓名
        /// </summary>
        public string Surgeon { get; set; }
        /// <summary>
        /// 手术医生编码
        /// </summary>
        public string SurgeonCode { get; set; }
        /// <summary>
        /// 是否择期
        /// </summary>
        public string EmergencyIndicator { get; set; }
        /// <summary>
        /// 术前主要诊断
        /// </summary>
        public string DiagBeforeOperation { get; set; }
        /// <summary>
        /// 术后诊断
        /// </summary>
        public string DiagAfterOperation { get; set; }
        /// <summary>
        /// 切口等级
        /// </summary>
        public string IncisionLevel { get; set; }
        /// <summary>
        /// 出量—尿量，单位：ml
        /// </summary>
        public string Vol { get; set; }
        /// <summary>
        /// 出量—出血量，单位：ml
        /// </summary>
        public string BloodLossed { get; set; }
        /// <summary>
        /// 输血
        /// </summary>
        public string BloodTransfered { get; set; }
        /// <summary>
        /// 输液
        /// </summary>
        public string Infusion { get; set; }
        /// <summary>
        /// 血浆
        /// </summary>
        public string Plasma { get; set; }
        /// <summary>
        /// 白蛋白
        /// </summary>
        public string Albumin { get; set; }
        /// <summary>
        /// 冷沉淀
        /// </summary>
        public string ColdPrecipitation { get; set; }
        /// <summary>
        /// 血小板
        /// </summary>
        public string Platelet { get; set; }
        /// <summary>
        /// 自体血
        /// </summary>
        public string Pebp { get; set; }
        /// <summary>
        /// 红细胞
        /// </summary>
        public string RedCell { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

    }



}

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.EMR.Writes.Entities
{
    /// <summary>
    /// 电子病例采集的基础信息
    /// </summary>
    [Comment("电子病例采集的基础信息")]
    public class EmrBaseInfo : FullAuditedAggregateRoot<Guid>
    { 
        public EmrBaseInfo(
            Guid id,
            string orgCode, 
            string deptCode, 
            string doctorCode, 
            string patientId, 
            string visitNo, 
            string registerNo, 
            string chiefComplaint, 
            string historyPresentIllness, 
            string allergySign, 
            string medicalHistory, 
            string bodyExam, 
            string preliminaryDiagnosis, 
            string handlingOpinions, 
            string outpatientSurgery, 
            string auxiliaryExamination, 
            string channel)
        {
            Id = id;
            OrgCode = orgCode;
            DeptCode = deptCode;
            DoctorCode = doctorCode;
            PatientId = patientId;
            VisitNo = visitNo;
            RegisterNo = registerNo;
            ChiefComplaint = chiefComplaint;
            HistoryPresentIllness = historyPresentIllness;
            AllergySign = allergySign;
            MedicalHistory = medicalHistory;
            BodyExam = bodyExam;
            PreliminaryDiagnosis = preliminaryDiagnosis;
            HandlingOpinions = handlingOpinions;
            OutpatientSurgery = outpatientSurgery;
            AuxiliaryExamination = auxiliaryExamination;
            Channel = channel;
        }


        /// <summary>
        /// 机构ID,固定：H7110
        /// </summary>
        [Comment("机构ID,固定：H7110")]
        [StringLength(32)]
        public string OrgCode { get; set; } = "H7110";

        /// <summary>
        /// 书写科室,一级科室代码
        /// </summary>
        [Comment("书写科室:一级科室代码")]
        [StringLength(32)]
        public string DeptCode { get; set; }

        /// <summary>
        /// 书写医生,书写医生工号
        /// </summary>
        [Comment("书写医生:书写医生工号")]
        [StringLength(50)] 
        public string DoctorCode { get; set; }

        /// <summary>
        /// 病人ID,His内部识别id
        /// </summary>
        [Comment("病人ID:His内部识别id")]
        [StringLength(32)]
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        [Comment("就诊号")]
        [StringLength(32)]
        public string VisitNo { get; set; }

        /// <summary>
        /// 挂号识别号
        /// <![CDATA[
        /// 8.1 挂号信息回传（正式、hisweb）registerNo
        /// ]]>
        /// </summary>
        [Comment("挂号识别号")]
        [StringLength(32)]
        public string RegisterNo { get; set; }

        /// <summary>
        /// 主诉
        /// </summary>
        [Comment("主诉")]
        [StringLength(2000)]
        public string ChiefComplaint { get; set; }

        /// <summary>
        /// 现病史
        /// </summary>
        [Comment("现病史")]
        [StringLength(2000)]
        public string HistoryPresentIllness { get; set; }

        /// <summary>
        /// 药物过敏史
        /// </summary>
        [Comment("药物过敏史")]
        [StringLength(2000)]
        public string AllergySign { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        [Comment("既往史")]
        [StringLength(2000)]
        public string MedicalHistory { get; set; }

        /// <summary>
        /// 体格体查
        /// </summary>
        [Comment("体格体查")]
        [StringLength(2000)]
        public string BodyExam { get; set; }
       
        /// <summary>
        /// 初步诊断
        /// </summary>
        [Comment("初步诊断")]
        [StringLength(2000)]
        public string PreliminaryDiagnosis { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        [Comment("处理意见")]
        [StringLength(2000)]
        public string HandlingOpinions { get; set; }

        /// <summary>
        /// 门诊手术
        /// </summary>
        [Comment("门诊手术")]
        [StringLength(2000)]
        public string OutpatientSurgery { get; set; }

        /// <summary>
        /// 辅助检查
        /// </summary>
        [Comment("辅助检查")]
        [StringLength(2000)]
        public string AuxiliaryExamination { get; set; }

        /// <summary>
        /// 渠道来源,尚哲：SZKJ
        /// </summary>
        [Comment("渠道来源,尚哲：SZKJ")]
        [StringLength(50)]
        public string Channel { get; set; } = "SZKJ";

        public void Update(string chiefComplaint, 
            string historyPresentIllness, 
            string allergySign, 
            string medicalHistory,
            string bodyExam, 
            string preliminaryDiagnosis, 
            string handlingOpinions, 
            string outpatientSurgery, 
            string auxiliaryExamination)
        {
            if(!chiefComplaint.IsNullOrWhiteSpace()) ChiefComplaint = chiefComplaint;
            if (!historyPresentIllness.IsNullOrWhiteSpace()) HistoryPresentIllness = historyPresentIllness;
            if (!allergySign.IsNullOrWhiteSpace()) AllergySign = allergySign;
            if (!medicalHistory.IsNullOrWhiteSpace()) MedicalHistory = medicalHistory;
            if (!bodyExam.IsNullOrWhiteSpace()) BodyExam = bodyExam;
            if (!preliminaryDiagnosis.IsNullOrWhiteSpace()) PreliminaryDiagnosis = preliminaryDiagnosis;
            if (!handlingOpinions.IsNullOrWhiteSpace()) HandlingOpinions = handlingOpinions;
            if (!outpatientSurgery.IsNullOrWhiteSpace()) OutpatientSurgery = outpatientSurgery;
            if (!auxiliaryExamination.IsNullOrWhiteSpace()) AuxiliaryExamination = auxiliaryExamination;
        }

        public bool CanSubmit()
        {
           var unSubmit = OrgCode.IsNullOrEmpty() 
                || DeptCode.IsNullOrEmpty()
                || DoctorCode.IsNullOrEmpty()
                || PatientId.IsNullOrEmpty()
                || RegisterNo.IsNullOrEmpty()
                || ChiefComplaint.IsNullOrEmpty()
                || HistoryPresentIllness.IsNullOrEmpty()
                || AllergySign.IsNullOrEmpty()
                || MedicalHistory.IsNullOrEmpty()
                || BodyExam.IsNullOrEmpty()
                || PreliminaryDiagnosis.IsNullOrEmpty()
                || HandlingOpinions.IsNullOrEmpty()
                || AuxiliaryExamination.IsNullOrEmpty()
                || Channel.IsNullOrEmpty();

            return !unSubmit; 
        }

        public override string ToString()
        {
            return $"电子病历基础信息采集记录： {JsonConvert.SerializeObject(this)}";
        }

    }
}

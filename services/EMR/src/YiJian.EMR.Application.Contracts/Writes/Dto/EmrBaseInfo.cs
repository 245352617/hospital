using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.EMR.Writes.Dto
{
    /// <summary>
    /// 电子病例采集的基础信息
    /// </summary>
    [Comment("电子病例采集的基础信息")]
    public class EmrBaseInfoDto : EntityDto<Guid?>
    {
        public EmrBaseInfoDto(
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
        [StringLength(32)]
        public string OrgCode { get; set; } = "H7110";

        /// <summary>
        /// 书写科室,一级科室代码
        /// </summary> 
        [StringLength(32)]
        public string DeptCode { get; set; }

        /// <summary>
        /// 书写医生,书写医生工号
        /// </summary> 
        [StringLength(50)]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 病人ID,His内部识别id
        /// </summary> 
        [StringLength(32)]
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary> 
        [StringLength(32)]
        public string VisitNo { get; set; }

        /// <summary>
        /// 挂号识别号
        /// <![CDATA[
        /// 8.1 挂号信息回传（正式、hisweb）registerNo
        /// ]]>
        /// </summary> 
        [StringLength(32)]
        public string RegisterNo { get; set; }

        /// <summary>
        /// 主诉
        /// </summary> 
        [StringLength(2000)]
        public string ChiefComplaint { get; set; }

        /// <summary>
        /// 现病史
        /// </summary> 
        [StringLength(2000)]
        public string HistoryPresentIllness { get; set; }

        /// <summary>
        /// 药物过敏史
        /// </summary> 
        [StringLength(2000)]
        public string AllergySign { get; set; }

        /// <summary>
        /// 既往史
        /// </summary> 
        [StringLength(2000)]
        public string MedicalHistory { get; set; }

        /// <summary>
        /// 体格体查
        /// </summary> 
        [StringLength(2000)]
        public string BodyExam { get; set; }

        /// <summary>
        /// 初步诊断
        /// </summary> 
        [StringLength(2000)]
        public string PreliminaryDiagnosis { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary> 
        [StringLength(2000)]
        public string HandlingOpinions { get; set; }

        /// <summary>
        /// 门诊手术
        /// </summary> 
        [StringLength(2000)]
        public string OutpatientSurgery { get; set; }

        /// <summary>
        /// 辅助检查
        /// </summary> 
        [StringLength(2000)]
        public string AuxiliaryExamination { get; set; }

        /// <summary>
        /// 渠道来源,尚哲：SZKJ
        /// </summary> 
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
            ChiefComplaint = chiefComplaint;
            HistoryPresentIllness = historyPresentIllness;
            AllergySign = allergySign;
            MedicalHistory = medicalHistory;
            BodyExam = bodyExam;
            PreliminaryDiagnosis = preliminaryDiagnosis;
            HandlingOpinions = handlingOpinions;
            OutpatientSurgery = outpatientSurgery;
            AuxiliaryExamination = auxiliaryExamination;
        }

    }
}

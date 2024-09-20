using System;
using System.Collections.Generic;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.DoctorsAdvices.Dto
{
    public class SubmitDoctorsAdviceDto
    {
        public List<Guid> Ids { get; set; }

        /// <summary>
        /// 医生/护士编码
        /// </summary>
        public string OperatorCode { get; set; }

        /// <summary>
        /// 医生/护士名称
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 当前操作医生的科室，如果是多个，需要判断一下是否有急诊科的，没有就默认第一个即可
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊流水号： 4.5.2 就诊记录、诊断信息回传（his提供、需对接集成平台）visSeriaINo
        /// </summary>
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 患者的唯一id
        /// </summary>
        public Guid Piid { get; set; }

        /// <summary>
        /// 系统标识: 0=急诊，1=院前
        /// </summary> 
        public EPlatformType PlatformType { get; set; } = EPlatformType.EmergencyTreatment;

        /// <summary>
        /// 代办人信息
        /// </summary>
        public AgencyPeopleDto AgencyPeople { get; set; }

        /// <summary>
        /// 患者年龄参数，用来检查是否是儿童
        /// </summary>
        public PatientInfoDto PatientInfo { get; set; }

    }

}

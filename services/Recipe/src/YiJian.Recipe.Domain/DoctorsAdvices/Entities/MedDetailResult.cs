using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.DoctorsAdvices.Entities
{
    /// <summary>
    /// 推送医嘱信息给HIS之后返回的结果实体
    /// </summary>
    public class MedDetailResult : CreationAuditedAggregateRoot<Guid>
    {
        private MedDetailResult()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary> 
        public MedDetailResult(
            string visSerialNo,
            string patientId,
            string doctorCode,
            string doctorName,
            string deptCode,
            string deptName,
            string medType,
            string channelNumber,
            string hisNumber,
            string channelNo,
            string medicalNo,
            string lgzxyyPayurl,
            string lgjkzxPayurl,
            string medNature,
            string medFee)
        {
            VisSerialNo = visSerialNo;
            PatientId = patientId;
            DoctorCode = doctorCode;
            DoctorName = doctorName;
            DeptCode = deptCode;
            DeptName = deptName;
            MedType = medType;
            ChannelNumber = channelNumber;
            HisNumber = hisNumber;
            ChannelNo = channelNo;
            MedicalNo = medicalNo;
            LgzxyyPayurl = lgzxyyPayurl;
            LgjkzxPayurl = lgjkzxPayurl;
            MedNature = medNature;
            MedFee = medFee;
        }

        /// <summary>
        /// 就诊流水号
        /// <![CDATA[
        /// 4.5.2 就诊记录、诊断信息回传（his提供、需对接集成平台）visSeriaINo
        /// ]]>
        /// </summary> 
        [Comment("就诊流水号")]
        [StringLength(50)]
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 病人ID
        /// <![CDATA[
        /// 4.5.2 就诊记录、诊断信息回传（his提供、需对接集成平台） patientId
        /// ]]>
        /// </summary> 
        [Comment("病人ID")]
        [StringLength(50)]
        public string PatientId { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary> 
        [Comment("医生编码")]
        [StringLength(50)]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 科室编码
        /// <![CDATA[
        /// 一级科室科室编码:4.4.1 科室字典（his提供） deptCode
        /// ]]>
        /// </summary> 
        [Comment("科室编码")]
        [StringLength(50)]
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称 
        /// <![CDATA[
        /// 一级科室名称:4.4.1 科室字典（his提供） deptName
        /// ]]>
        /// </summary> 
        [Comment("科室名称 ")]
        [StringLength(50)]
        public string DeptName { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary> 
        [Comment("医生姓名")]
        [StringLength(50)]
        public string DoctorName { get; set; }

        /// <summary>
        /// 医嘱类型 处方：CF   非处方:YJ
        /// </summary>
        [Comment("医嘱类型 处方：CF   非处方:YJ")]
        [StringLength(50)]
        public string MedType { get; set; }

        /// <summary>
        /// 渠道识别号  4.5.3医嘱信息回传（his提供、需对接集成平台） chargeDetailNo projectItemNo
        /// </summary>
        [Comment("渠道识别号  4.5.3医嘱信息回传（his提供、需对接集成平台） chargeDetailNo projectItemNo")]
        [StringLength(50)]
        public string ChannelNumber { get; set; }

        /// <summary>
        /// His识别号 对应his处方识别（C）、医技序号（Y）  可用于二维码展示等
        /// </summary>
        [Comment("His识别号 对应his处方识别（C）、医技序号（Y）  可用于二维码展示等")]
        [StringLength(50)]
        public string HisNumber { get; set; }

        /// <summary>
        /// HIS申请单号 处方：处方号码  医技：申请单id（检验、检查返回）
        /// </summary>
        [Comment("HIS申请单号 处方：处方号码  医技：申请单id（检验、检查返回）")]
        [StringLength(50)]
        public string ChannelNo { get; set; }

        /// <summary>
        /// 病历号 患者主索引id、用于条形码展示
        /// </summary>
        [Comment("病历号 患者主索引id、用于条形码展示")]
        [StringLength(50)]
        public string MedicalNo { get; set; }

        /// <summary>
        /// 支付二维码  深圳市龙岗中心医院微信公众
        /// </summary>
        [Comment("支付二维码  深圳市龙岗中心医院微信公众")]
        [StringLength(500)]
        public string LgzxyyPayurl { get; set; }

        /// <summary>
        /// 支付二维码 深圳市龙岗健康在线支付二维码
        /// </summary>
        [Comment("支付二维码 深圳市龙岗健康在线支付二维码")]
        [StringLength(500)]
        public string LgjkzxPayurl { get; set; }

        /// <summary>
        /// 性质 用于申请单性质显示
        /// </summary>
        [Comment("性质 用于申请单性质显示")]
        public string MedNature { get; set; }

        /// <summary>
        /// 费别 用于申请单费别显示
        /// </summary>
        [Comment("费别 用于申请单费别显示")]
        public string MedFee { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.DoctorsAdvices.Entities
{
    /// <summary>
    /// 处方号管理类
    /// </summary>
    [Comment("处方号管理类")]
    public class Prescription : CreationAuditedEntity<Guid>
    {
        public Prescription(
            Guid id,
            Guid doctorsAdviceId,
            string medType,
            string myPrescriptionNo,
            string prescriptionNo,
            string visSerialNo,
            string patientName,
            string deptCode,
            string doctorCode)
        {
            Id = id;
            DoctorsAdviceId = doctorsAdviceId;
            MedType = medType;
            MyPrescriptionNo = myPrescriptionNo;
            PrescriptionNo = prescriptionNo;
            VisSerialNo = visSerialNo;
            PatientName = patientName;
            DeptCode = deptCode;
            DoctorCode = doctorCode;
        }

        /// <summary>
        /// 医嘱Id
        /// </summary>
        [Comment("医嘱Id")]
        public Guid DoctorsAdviceId { get; set; }

        ///// <summary>
        ///// 渠道订单序号
        ///// <![CDATA[
        ///// 4.5.3医嘱信息回传（his提供、需对接集成平台） prescriptionNo, projectItemNo
        ///// = MyPrescriptionNo
        ///// ]]>
        ///// </summary>
        //[Comment("渠道订单序号")]
        //[StringLength(50)]
        //public string ChannelBillId { get; set; }

        /// <summary>
        /// 对应his处方识别（C）、医技序号（Y）
        /// </summary>
        [Comment("对应his处方识别（C）、医技序号（Y）")]
        [StringLength(2)]
        public string MedType { get; set; }

        /// <summary>
        /// 我方映射处方号
        /// </summary>
        [Comment("我方映射处方号")]
        [StringLength(20)]
        public string MyPrescriptionNo { get; set; }

        ///// <summary>
        ///// His订单序号
        ///// <![CDATA[
        ///// 对应his处方、医技唯一识别
        ///// = PrescriptionNo
        ///// ]]>
        ///// </summary>
        //[Comment("His订单序号")]
        //[StringLength(50)]
        //public string HisBillId { get; set; }

        /// <summary>
        /// 处方号 (院方)
        /// </summary>
        [Comment("处方号")]
        [StringLength(20)]
        public string PrescriptionNo { get; set; }

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
        /// 就诊患者姓名
        /// </summary>
        [Comment("就诊患者姓名")]
        [StringLength(50)]
        public string PatientName { get; set; }

        /// <summary>
        /// 就诊科室
        /// </summary>
        [Comment("就诊科室")]
        [StringLength(50)]
        public string DeptCode { get; set; }

        /// <summary>
        /// 就诊医生编号
        /// </summary>
        [Comment("就诊医生编号")]
        [StringLength(50)]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 订单状态
        /// <![CDATA[
        /// 0.未缴费 1.已缴费 -1.已作废 2.已执行
        /// ]]>
        /// </summary>
        [Comment("订单状态, 0.未缴费 1.已缴费 -1.已作废 2.已执行,3.已退费")]
        public int BillState { get; set; }

        public void Update(string prescriptionNo, string visSerialNo)
        {
            PrescriptionNo = prescriptionNo;
            VisSerialNo = visSerialNo;
        }

        public void Update(
            string prescriptionNo,
            string visSerialNo,
            string patientName,
            string deptCode,
            string doctorCode,
            int billState)
        {
            PrescriptionNo = prescriptionNo;
            VisSerialNo = visSerialNo;
            PatientName = patientName;
            DeptCode = deptCode;
            DoctorCode = doctorCode;
            BillState = billState;
        }
    }
}

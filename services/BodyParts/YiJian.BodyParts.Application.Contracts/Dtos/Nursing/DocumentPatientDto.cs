using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 文书表头基本信息
    /// </summary>
    public class DocumentPatientDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        public string bedNo { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string age { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string deptName { get; set; }

        /// <summary>
        /// 病区
        /// </summary>
        public string areaId { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string inpNo { get; set; }

        /// <summary>
        /// 病人ID
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        /// 入院时间
        /// </summary>
        public DateTime? inHospitalTime { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime? inDeptTime { get; set; }

        /// <summary>
        /// 入院诊断
        /// </summary>
        public string diagnosis { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string idNo { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string homeAddress { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string nextOfKin { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string nextOfKinPhone { get; set; }

        /// <summary>
        /// 来源科室编码
        /// </summary>
        public string? InDeptCode { get; set; }

        /// <summary>
        /// 来源科室名称
        /// </summary>
        public string? InDeptName { get; set; }

        /// <summary>
        /// 入科时间之前最近的一条手术
        /// </summary>
        public string? LastOperationInfoBeforInDept { get; set; }

        /// <summary>
        /// 手术信息：当前时间之前最近的一条手术信息
        /// </summary>
        public string? OperationInfo { get; set; }

        /// <summary>
        /// 所有手术信息
        /// </summary>
        public string? AllOperationInfo { get; set; }

        /// <summary>
        /// 在科天数
        /// </summary>
        public int InDeptDays { get; set; }

        /// <summary>
        /// 出入归转
        /// </summary>
        public string? OutTurnoverName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class PatientSepsisDto
    {
        public string PI_ID { get; set; }
        /// <summary>
        /// 是否确诊，true=1：已确诊，false=0：未确诊
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// 确诊时间
        /// </summary>
        public DateTime? ConfirmedTime { get; set; }

        /// <summary>
        /// 医生ID
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 是否显示1h集束化治疗
        /// </summary>
        public bool ShowOneHourClusterTherapy { get; set; }

        /// <summary>
        /// 项目列表
        /// </summary>
        public List<PatientSepsisDetailsDto> patientSepsisDetails { get; set; }
    }

    public class PatientSepsisDetailsDto
    {
        public string Id { get; set; }

        /// <summary>
        /// 患者id(通过业务构造的流水号，每个患者每次入科号码唯一)
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 内容/值
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 确认时间，人工确认和取消人工确认的时间
        /// </summary>
        public DateTime? ConfirmedTime { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public string ExecutionTime { get; set; }

        /// <summary>
        /// 状态，如：已完成、未完成
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 确认类型，2：人工确认，0：未确认，1：取消确认
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 时间类型，0：3h集束化治疗治疗，1：6h集束化治疗治疗，2：1h集束化治疗治疗
        /// </summary>
        public int TimeType { get; set; }
    }
}
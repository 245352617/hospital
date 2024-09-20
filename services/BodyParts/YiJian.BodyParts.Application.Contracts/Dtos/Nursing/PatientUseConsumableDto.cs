using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class PatientUseConsumableDto : ConsumableDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 登记时间
        /// </summary>
        public DateTime RegisterDataTime { get; set; }

        /// <summary>
        /// 耗材分类
        /// </summary>
        public string ConsumableType { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 护士工号
        /// </summary>
        public string NurseCode { get; set; }
    }

    /// <summary>
    /// 耗材统计总表
    /// </summary>
    public class PatientUseConsumableDetailedDto : PatientUseConsumableDto
    {
        /// <summary>
        /// 病人床号
        /// </summary>
        public string PatientBedNum { get; set; }
        /// <summary>
        /// 患者名称
        /// </summary>
        public string PatientName { get; set; }
        /// <summary>
        /// 患者ID
        /// </summary>
        public string PatientId { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string PatientGender { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public string PatientAge { get; set; }
        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime PatientInDeptTime { get; set; }
        /// <summary>
        /// 入科诊断
        /// </summary>
        public string PatientIndiagnosis { get; set; }

        public int SortNum { get; set; }

        /// <summary>
        /// His耗材编码
        /// </summary>
        public string HisCode { get; set; }
    }

    /// <summary>
    /// 耗材统计明细统计Dto
    /// </summary>
    public class ConsumableStatisticalDetailDto : ConsumableDto
    {
        /// <summary>
        /// 病人床号
        /// </summary>
        public string PatientBedNum { get; set; }
        /// <summary>
        /// 患者名称
        /// </summary>
        public string PatientName { get; set; }
        /// <summary>
        /// 患者ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 登记时间
        /// </summary>
        public string RegisterDataTime { get; set; }
        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime PatientInDeptTime { get; set; }
    }


    public class ConsumableDto
    {
        /// <summary>
        /// 耗材名称
        /// </summary>
        public string ConsumableName { get; set; }

        /// <summary>
        /// 使用数量
        /// </summary>
        public int UseCount { get; set; }
    }
}

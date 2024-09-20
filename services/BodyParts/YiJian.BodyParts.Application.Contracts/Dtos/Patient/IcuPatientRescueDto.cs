using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 患者抢救信息
    /// </summary>
    public class IcuPatientRescueDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        //[Required(ErrorMessage ="开始时间不能为空！")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 抢救原因
        /// </summary>
        [StringLength(100)]
        public string RescueReason { get; set; }

        /// <summary>
        /// 0：小抢救；1：大抢救
        /// </summary>
        //[Required(ErrorMessage ="抢救类型不能为空！")]
        public RescueTypeEnum RescueType { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        [StringLength(20)]
        [Required(ErrorMessage = "患者流水号不能为空！")]
        public string PI_ID { get; set; }

        /// <summary>
        /// 是否是第一次获取监测数据
        /// </summary>
        public bool IsFirstGet { get; set; }
    }

    public class RescueDto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        [StringLength(20)]
        [Required(ErrorMessage = "患者流水号不能为空！")]
        public string PI_ID { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required(ErrorMessage = "开始时间不能为空！")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 患者监测时间段列表
    /// </summary>
    public class RescueListDto
    {
        /// <summary>
        /// 表头名称
        /// </summary>
        public List<string> titleNameList { get; set; }

        public List<List<string>> MonitorDetailsListDtos { get; set; }
    }

    /// <summary>
    /// 修改抢救监测数据的措施与药物DTO
    /// </summary>
    public class PutRescueDetailsDto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        [StringLength(20)]
        [Required(ErrorMessage = "患者流水号不能为空！")]
        public string PI_ID { get; set; }

        /// <summary>
        /// 监测时间点
        /// </summary>
        [Required(ErrorMessage = "监测时间点不能为空！")]
        public DateTime MonitorTime { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        [StringLength(100)]
        public string ParaValue { get; set; }

        /// <summary>
        /// 参数名
        /// </summary>
        [StringLength(100)]
        public string ParaName { get; set; }
    }

    /// <summary>
    /// 抢救记录列表
    /// </summary>
    public class IcuPatientRescueRecordListDto
    {
        public string Id { get; set; }

        /// <summary>
        /// 抢救原因
        /// </summary>
        public string RescueReason { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 措施与药物
        /// </summary>
        public string MeasuresAndDrugs { get; set; }
    }
}
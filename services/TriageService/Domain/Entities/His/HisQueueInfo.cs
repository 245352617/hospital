using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// HIS 队列表
    /// </summary>
    public class HisQueueInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public string RDID { get; set; }

        /// <summary>
        /// 入队时间 RDSJ
        /// </summary>
        public DateTime InQueueTime { get; set; }

        /// <summary>
        /// 排队号码 PDHM
        /// </summary>
        public string CallingSn { get; set; }

        /// <summary>
        /// 开始时间 KSSJ
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间 JSSJ
        /// </summary>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 病人名称 PDCY
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 排队状态（2已开始，3已完成） PDZT
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 医生ID YSID
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// 医生姓名 YSXM
        /// </summary>
        public string DoctorName { get; set; }
    }
}

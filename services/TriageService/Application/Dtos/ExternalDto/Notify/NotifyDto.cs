using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class NotifyDto
    {

        /// <summary>
        /// 剩余距离
        /// </summary>
        public string Distance { get; set; }

        /// <summary>
        /// 预计时间
        /// </summary>
        public string ExpectTime { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 患者病历号
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 绿通名称
        /// </summary>
        public string GreenRoadName { get; set; }

        /// <summary>
        /// 分诊人
        /// </summary>
        public string TriageUserName { get; set; }

        /// <summary>
        /// 分诊人科室名称
        /// </summary>
        public string TriageUserDeptName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 任务单Id
        /// </summary>
        public Guid TaskInfoId { get; set; }


        /// <summary>
        /// 开通绿通时间
        /// </summary>
        public DateTime OpenGreenRoadTime { get; set; }

        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 主键Id
        /// </summary>
        public virtual Guid Nid => Id;

        
        /// <summary>
        /// 车牌号
        /// </summary>
        [StringLength(50)]
        public string CarNum { get; set; }
        
        /// <summary>
        /// 任务单号
        /// </summary>
        [StringLength(50)]
        public string TaskNo { get; set; }

        /// <summary>
        /// 患者信息
        /// </summary>
        [StringLength(50)]
        public string PatientInfo { get; set; }
        
        /// <summary>
        /// 任务单位或车辆状态
        /// </summary>
        [StringLength(150)]
        public string Status { get; set; }
        
        /// <summary>
        /// 详情内容
        /// </summary>
        [StringLength(250)]
        public string Content { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        [NotMapped]
        public bool IsRead { get; set; } = false;

        /// <summary>
        /// 是否保存到数据库
        /// </summary>
        public bool IsSaveToDB { get; set; }
        
        /// <summary>
        /// 消息要跳转打开的页面
        /// </summary>
        [StringLength(150)]
        [CanBeNull]
        public string Url { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 撤机评估记录表Dto
    /// </summary>
    public class DocSbtAssessDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        [Required]
        public string PI_ID { get; set; }

        /// <summary>
        /// 护理日期
        /// </summary>
        public string NurseDate { get; set; }

        /// <summary>
        /// SBT状态：试验中 = 1,已停止 = 2
        /// </summary>
        public SbtStateEnum SbtState { get; set; } = SbtStateEnum.试验中;

        /// <summary>
        /// 开始撤机时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// SBT试验
        /// </summary>
        public string SbtTime { get; set; }

        /// <summary>
        /// 撤机拔管
        /// </summary>
        public string ExtubationTime { get; set; }

        /// <summary>
        /// 无创正压通气
        /// </summary>
        public string VentilationTime { get; set; }

        /// <summary>
        /// 复查血气
        /// </summary>
        public string ReviewBloodgasTime { get; set; }

        /// <summary>
        /// 完成
        /// </summary>
        public string FinishTime { get; set; }

        /// <summary>
        /// 撤机评估json信息
        /// </summary>
        public string AccessJson { get; set; }

        /// <summary>
        /// 评估结果：可进行SBT=1，不具备SBT=0
        /// </summary>
        public int? AccessResult { get; set; }

        /// <summary>
        /// 评估医生工号
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 评估医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// SBT试验方法
        /// </summary>
        public string SbtMethod { get; set; }

        /// <summary>
        /// SBT试验Json
        /// </summary>
        public string SbtJson { get; set; }

        /// <summary>
        /// SBT试验是否成功
        /// </summary>
        public bool? SbtResult { get; set; }

        /// <summary>
        /// SBT试验失败原因
        /// </summary>
        public string SbtFailReason { get; set; }

        /// <summary>
        /// 记录人
        /// </summary>
        public string RecordCode { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordTime { get; set; }


    }
}

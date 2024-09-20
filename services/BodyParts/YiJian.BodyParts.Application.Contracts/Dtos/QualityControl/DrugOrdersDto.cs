using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using JetBrains.Annotations;

namespace YiJian.BodyParts.Application.Contracts.Dtos.QualityControl
{

    /// <summary>
    /// 其他医嘱dto
    /// </summary>
    public  class DrugOrdersDto
    {

        /// <summary>
        /// ID
        /// </summary>

        public Guid Id { get; set; }

        /// <summary>
        /// 用药方式（途径）
        /// </summary>
        public string Usage { get; set; }

        /// <summary>
        /// 总剂量
        /// </summary>

        public string TotalDosage { get; set; }



        /// <summary>
        /// 每次剂量单位
        /// </summary>
        public string DosageUnit { get; set; }




        /// <summary>
        /// 医嘱组号
        /// </summary>
        [StringLength(30)]
        public string GroupNo { get; set; }


        /// <summary>
        /// 状态
        /// </summary>
        public string ExcuteFlag { get; set; }

        /// <summary>
        /// 医嘱类型长-1、临-0 
        /// </summary>
        public string OrderType { get; set; }


        /// <summary>
        /// 拆分时间
        /// </summary>

        public DateTime ConversionTime { get; set; }


        /// <summary>
        /// 频率次数
        /// </summary>
        public string FreqCount { get; set; }


      

        /// <summary>
        /// 剂量
        /// </summary>

        public string Dosage { get; set; }


        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime? PlanExcuteTime { get; set; }



        /// <summary>
        /// 执行时间
        /// </summary>

        public DateTime? ExcuteTime { get; set; }


        /// <summary>
        /// 执行护士
        /// </summary>

        public string ExcuteNurseCode { get; set; }

        /// <summary>
        /// 执行护士名称
        /// </summary>

        public string ExcuteNurseName { get; set; }


        /// <summary>
        /// 开 立医生
        /// </summary>
        public string StartDoctorName { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 医嘱内容
        /// </summary>
        public string OrderText { get; set; }

    }
}

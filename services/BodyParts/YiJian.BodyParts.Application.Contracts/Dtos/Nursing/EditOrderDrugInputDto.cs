using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Nursing
{
    public class OrderDrugDto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 医嘱组号
        /// </summary>
        public string GroupNo { get; set; }

        /// <summary>
        /// 药品id-对应HisOrder.Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string OrderText { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string DrugSpec { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        public string Dosage { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        public string DosageUnit { get; set; }

        /// <summary>
        /// 下达时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 途径
        /// </summary>
        public string Usage { get; set; }

        /// <summary>
        /// 途径名称
        /// </summary>
        public string UsageName { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        public string Frequency { get; set; }

        /// <summary>
        /// 医嘱执行日期 格式：yyyy-MM-dd 如果要重新提取医嘱，就只提取传入这天的对应医嘱
        /// </summary>
        public DateTime NurseDate { get; set; }
    }
}

#region 代码备注
//------------------------------------------------------------------------------
// 创建描述:  11/12/2020 06:39:47
//
// 功能描述:
//
// 修改描述:
//------------------------------------------------------------------------------
#endregion 代码备注
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:HIS接口-医嘱信息
    /// </summary>
    public class OrderDto : EntityDto<Guid>
    {
        /// <summary>
        /// 规格
        /// </summary>
        public string DrugSpec { get; set; }


        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        public string PI_ID { get; set; }

        /// <summary>
        /// 医嘱编号
        /// </summary>
        /// <example></example>
        public string OrderCode { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        /// <example></example>
        public string ItemCode { get; set; }

        /// <summary>
        /// 医嘱内容(药品名称)
        /// </summary>
        /// <example></example>
        public string OrderText { get; set; }

        /// <summary>
        /// 组号
        /// </summary>
        /// <example></example>
        public string GroupNo { get; set; }

        /// <summary>
        /// 组内子号
        /// </summary>
        /// <example></example>
        public string GroupSubNo { get; set; }

        /// <summary>
        /// 医嘱分类
        /// </summary>
        /// <example></example>
        public string OrderClass { get; set; }

        /// <summary>
        /// 医嘱类别（类型 1-长，0-临）
        /// </summary>
        /// <example></example>
        public string OrderType { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        public string DeptCode { get; set; }

        /// <summary>
        /// 用法（途径）
        /// </summary>
        /// <example></example>
        public string Usage { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary>
        /// <example></example>
        public string UsageName { get; set; }

        /// <summary>
        /// 用法名全称
        /// </summary>
        /// <example></example>
        public string UsageFullName { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        /// <example></example>
        public string Frequency { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        /// <example></example>
        public string Dosage { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        /// <example></example>
        public string DosageUnit { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        /// <example></example>
        public decimal? Quantity { get; set; }

        /// <summary>
        /// 贴数
        /// </summary>
        /// <example></example>
        public string Copys { get; set; }

        /// <summary>
        /// 开始时间（下嘱时间）
        /// </summary>
        /// <example></example>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 开始医生代码
        /// </summary>
        /// <example></example>
        public string StartDoctorCode { get; set; }

        /// <summary>
        /// 开始医生姓名
        /// </summary>
        /// <example></example>
        public string StartDoctorName { get; set; }

        /// <summary>
        /// 复合时间
        /// </summary>
        /// <example></example>
        public DateTime? VerifyTime { get; set; }

        /// <summary>
        /// 复合护士代码
        /// </summary>
        /// <example></example>
        public string VerifyNurseCode { get; set; }

        /// <summary>
        /// 复合护士姓名
        /// </summary>
        /// <example></example>
        public string VerifyNurseName { get; set; }

        /// <summary>
        /// 停止时间
        /// </summary>
        /// <example></example>
        public DateTime? StopTime { get; set; }

        /// <summary>
        /// 停止医生编号
        /// </summary>
        /// <example></example>
        public string StopDoctorCode { get; set; }

        /// <summary>
        /// 停止医生姓名
        /// </summary>
        /// <example></example>
        public string StopDoctorName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        /// <example></example>
        public string Remark { get; set; }

        /// <summary>
        /// 取消时间
        /// </summary>
        /// <example></example>
        public DateTime? CancelTime { get; set; }

        /// <summary>
        /// 取消医生
        /// </summary>
        /// <example></example>
        public string CancelDoctorCode { get; set; }

        /// <summary>
        /// 取消医生姓名
        /// </summary>
        /// <example></example>
        public string CancelDoctorName { get; set; }

        /// <summary>
        /// 停止标志
        /// </summary>
        /// <example></example>
        public string OrderState { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        /// <example></example>
        public string ClientIp { get; set; }

        /// <summary>
        /// 是否手工录入
        /// </summary>
        /// <example></example>
        public int IfManualInput { get; set; }

        /// <summary>
        /// 有效状态
        /// </summary>
        /// <example></example>
        public int ValidState { get; set; }

        /// <summary>
        /// 提取状态
        /// </summary>
        public string ImportStatus { get; set; }
    }
}

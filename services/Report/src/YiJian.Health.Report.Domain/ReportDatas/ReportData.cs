using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Volo.Abp.Domain.Entities;

namespace YiJian.Health.Report.ReportDatas
{
    /// <summary>
    /// 报表打印数据
    /// </summary>
    [Comment("报表打印数据")]
    public class ReportData : Entity<Guid>
    {
        private ReportData()
        {
        }

        public ReportData(Guid piid, Guid tempId, string dataContent, string prescriptionNo, string operationCode)
        {
            PIID = piid;
            TempId = tempId;
            CreationTime = DateTime.Now;
            PrescriptionNo = prescriptionNo;
            OperationCode = operationCode;
            Modify(dataContent);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dataContent"></param>
        public void Modify(string dataContent)
        {
            DataContent = dataContent;
        }

        /// <summary>
        /// 患者分诊id
        /// </summary>
        [Comment("患者分诊id")]
        public Guid PIID { get; private set; }

        /// <summary>
        /// 模板id
        /// </summary>
        [Comment("模板id")]
        public Guid TempId { get; private set; }

        /// <summary>
        /// 处方号
        /// </summary>
        [Comment("处方号")]
        [StringLength(100)]
        public string PrescriptionNo { get; private set; }

        /// <summary>
        /// 数据
        /// </summary>
        [Comment("数据")]
        [XmlText]
        public string DataContent { get; private set; }

        /// <summary>
        /// 操作人编码
        /// </summary>
        [Comment("操作人编码")]
        [StringLength(20)]
        public string OperationCode { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Comment("创建时间")]
        public DateTime CreationTime { get; private set; }
    }
}
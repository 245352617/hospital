using FreeSql.DataAnnotations;
using System;
using System.ComponentModel;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Domain.Entities;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 报卡
    /// </summary>
    [Table(Name = "Pat_CardReporting")]
    public class CardReporting : Entity<Guid>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cardReportingType"></param>
        /// <param name="isEscalation"></param>
        /// <param name="cardContent"></param>
        /// <param name="piid"></param>
        public CardReporting(Guid id, ECardReportingType cardReportingType, bool isEscalation, string cardContent, Guid piid)
        {
            Id = id;
            CardReportingType = cardReportingType;
            IsEscalation = isEscalation;
            CardContent = cardContent;
            PIID = piid;
        }

        /// <summary>
        /// 患者分诊id
        /// </summary>
        public Guid PIID { get; private set; }

        /// <summary>
        /// 报卡类型
        /// 1	  普通传染病     A
        ///2	  性病           C
        ///3	  结核病		     B
        ///4	  肝炎           C
        ///5	  艾滋病         C
        ///11	  肿瘤           B
        ///12	  脑卒中         C
        ///15	  狂犬病         A
        ///20	  急性心肌梗死    B
        ///27	  食源性急病类    B
        /// </summary>
        [Description("报卡类型：1.普通传染病A;2.性病C;3.结核病B;4.肝炎C;5.艾滋病C;11.肿瘤B;12.脑卒中C;15.狂犬病A;20.急性心肌梗死B;27.食源性急病类B")]
        public ECardReportingType CardReportingType { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; private set; } = DateTime.Now;

        /// <summary>
        /// 是否上报
        /// </summary>
        public bool IsEscalation { get; private set; }

        /// <summary>
        /// 报卡内容
        /// </summary>
        [Column(DbType = "nvarchar(max)")]
        public string CardContent { get; private set; }
    }
}
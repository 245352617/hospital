using System;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Dtos;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class CardReportingDto : EntityDto<Guid>
    {
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
        public ECardReportingType CardReportingType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 是否上报
        /// </summary>
        public bool IsEscalation { get; set; }

        /// <summary>
        /// 报卡内容
        /// </summary>
        public string CardContent { get; set; }
    }
}
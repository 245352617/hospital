using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    public class IcuDocumentRecordDto : EntityDto<Guid>
    {
        /// <summary>
        /// 文书url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 文书编码
        /// </summary>
        public string TemplateCode { get; set; }


        /// <summary>
        /// 文书url
        /// </summary>
        public string Name { get; set; }
    }
}

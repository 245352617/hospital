using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    public class IcuDocumentCategoryRecordDto
    {
        public string Categroy { get; set; }

        public List<IcuDocumentRecordDto> list { get; set; }
    }
}

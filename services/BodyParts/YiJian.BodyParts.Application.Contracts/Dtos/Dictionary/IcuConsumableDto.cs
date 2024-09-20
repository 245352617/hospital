using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtosy
{
    public class IcuConsumableDto
    {
        public string ConsumableTypeName { get; set; }

        public IEnumerable<IcuConsumableInfo> Item { get; set; }
    }

    public class IcuConsumableInfo
    {
        public string ConsumableId { get; set; }

        public string ConsumableName { get; set; }

        public int SortNum { get; set; }
    }


}

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    public class IcuScoreDocDto : EntityDto<Guid>
    {
        /// <summary>
        /// 评分文书实体
        /// </summary>
            
        public List<IcuScoreStandardDto> icuScoreStandardDtos { get; set; }
        public List<IcuPatientScoreDto> icuPatientScoreDtos { get; set; }

    }
}

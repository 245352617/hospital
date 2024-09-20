using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace YiJian.BodyParts.Dtos
{
    public class IcuNursingAdviceExecutePlanDto
    {
        public string CategoryName {get;set;}
        public long CategorySortNum {get;set;}

        public List<IcuNursingAdviceDto> Advices {get;set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling  = NullValueHandling.Include });
        }

    }
}
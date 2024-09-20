using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace YiJian.BodyParts.Dtos
{
    public class CreateNursingAdviceDto
    {
        /// <summary>
        /// 患者患者id
        /// </summary>
        /// <value></value>
        [Required]
        public string PI_ID { get; set; }
        /// <summary>
        /// 护嘱开立人的工号
        /// </summary>
        /// <value></value>
        public string StartNurseCode { get; set; }
        /// <summary>
        /// 护嘱开立人姓名
        /// </summary>
        /// <value></value>
        public string StartNurseName { get; set; }
        /// <summary>
        /// 护嘱计划开始时间
        /// </summary>
        /// <value></value>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 护嘱的详细内容
        /// </summary>
        /// <value></value>
        public List<IcuNursingAdviceContentDto> Advices { get; set;}

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
        }

    }
}
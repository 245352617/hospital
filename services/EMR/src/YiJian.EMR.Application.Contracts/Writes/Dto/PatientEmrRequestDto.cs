using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Writes.Dto
{
    /// <summary>
    /// 患者病历请求参数
    /// </summary>
    public class PatientEmrRequestDto
    {
        /// <summary>
        /// 是否获取当前患者的所有记录
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public bool? GetAllRecord { get;set; } = false;

        /// <summary>
        /// 患者唯一Id (当前患者的当前就诊电子病历)
        /// </summary>
        public Guid Piid { get; set; }

        /// <summary>
        /// 患者编号 (当前患者的所有电子病历)
        /// </summary> 
        public string PatientNo { get; set; }
         
        /// <summary>
        /// 医生编号
        /// </summary> 
        public string DoctorCode { get; set; }

        /// <summary>
        /// 电子文书分类（0=电子病历，1=文书）默认0=EMR
        /// </summary>
        public EClassify Classify { get; set; } = EClassify.EMR;

    }

}

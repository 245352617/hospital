using System;
using System.Collections.Generic;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 特殊护理记录
    /// </summary>
    public class SpecialCareDto
    {
        /// <summary>
        /// 表头,传值过来就查具体的，如果不传动态六项全部查询出来
        /// </summary>
        public List<Guid> Headers { get; set; }

        /// <summary>
        /// 分页动态六项的Id
        /// </summary> 
        public Guid DynamicFieldId { get; set; }

        /// <summary>
        /// 护理记录Id
        /// </summary>
        public Guid NursingRecordId { get; set; }

        /// <summary>
        /// 是否是动态六项 true=动态六项，false=特殊护理记录
        /// </summary>
        public bool IsDynamicSix { get; set; } = true;
    }

}

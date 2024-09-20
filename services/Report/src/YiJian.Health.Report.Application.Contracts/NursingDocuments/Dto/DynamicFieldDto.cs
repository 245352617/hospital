using System.Collections.Generic;
using System.Text;

namespace YiJian.Health.Report.NursingDocuments.Dto
{

    /// <summary>
    /// 动态字段名字描述
    /// </summary>
    public class DynamicFieldDto: DynamicFieldBaseDto
    { 
        /// <summary>
        /// 字段对应的名称
        /// </summary> 
        public FieldNameDto FieldName { get; set; } = new FieldNameDto();
    }

}

using System;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 病人特征记录
    /// </summary>
    public class CharacteristicDto
    {
        /// <summary>
        /// json 结构的数据(前段根据用户选择填写的内容组装成一个json字符串传给后端) 
        /// </summary>
        public string JsonData { get; set; }

        /// <summary>
        /// 护理记录Id
        /// </summary>
        public Guid NursingRecordId { get; set; }

        /// <summary>
        /// 表头Id
        /// </summary>
        public Guid? HeaderId { get; set; }

    }
}

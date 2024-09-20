using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.Health.Report.NursingDocuments.Entities
{
    /// <summary>
    /// 病人特征记录
    /// </summary>
    [Comment("病人特征记录")]
    public class Characteristic : Entity<Guid>
    {
        private Characteristic()
        {

        }

        /// <summary>
        /// 病人特征记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jsonData"></param>
        /// <param name="nursingRecordId"></param>
        /// <param name="headerId"></param>
        public Characteristic(Guid id, string jsonData, Guid nursingRecordId, Guid? headerId)
        {
            Id = id;
            JsonData = jsonData;
            NursingRecordId = nursingRecordId;
            HeaderId = headerId;
        }

        /// <summary>
        /// 表头内容
        /// </summary>
        [Comment("表头内容")]
        public Guid? HeaderId { get; set; }

        /// <summary>
        /// json 结构的数据(前段根据用户选择填写的内容组装成一个json字符串传给后端) 
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Comment("json结构的数据")]
        public string JsonData { get; set; }

        /// <summary>
        /// 护理记录Id
        /// </summary>
        [Comment("护理记录Id")]
        public Guid NursingRecordId { get; set; }

        /// <summary>
        /// 护理记录
        /// </summary>
        public virtual NursingRecord NursingRecord { get; set; }

        public void Update(string jsonData)
        {
            JsonData = jsonData;
        }
    }
}

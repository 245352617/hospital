using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Health.Report.NursingDocuments
{
    /// <summary>
    /// 动态字段名字描述
    /// </summary>
    [Comment("动态字段名字描述")]
    public class DynamicField : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 动态字段名字描述
        /// </summary>
        private DynamicField()
        {

        }

        public DynamicField(Guid id,
            int sheetIndex,
            string sheetName,
            Guid nursingDocumentId)
        {
            Id = id;
            SheetIndex = sheetIndex;
            NursingDocumentId = nursingDocumentId;
            SheetName = sheetName.IsNullOrWhiteSpace() ? $"护理记录单{sheetIndex + 1}" : sheetName;
        }

        /// <summary>
        /// 动态字段名字描述
        /// </summary> 
        public DynamicField(
            Guid id,
            int sheetIndex,
            string sheetName,
            Guid nursingDocumentId,
            Guid? field1,
            Guid? field2,
            Guid? field3,
            Guid? field4,
            Guid? field5,
            Guid? field6,
            Guid? field7,
            Guid? field8,
            Guid? field9
            )
        {
            Id = id;
            SheetIndex = sheetIndex;
            NursingDocumentId = nursingDocumentId;
            SheetName = sheetName.IsNullOrWhiteSpace() ? $"护理记录单{sheetIndex + 1}" : sheetName;
            Field1 = field1;
            Field2 = field2;
            Field3 = field3;
            Field4 = field4;
            Field5 = field5;
            Field6 = field6;
            Field7 = field7;
            Field8 = field8;
            Field9 = field9;
        }

        /// <summary>
        /// 保留字段1
        /// </summary>
        [Comment("保留字段1")]
        public Guid? Field1 { get; set; }

        /// <summary>
        /// 保留字段2
        /// </summary>
        [Comment("保留字段2")]
        public Guid? Field2 { get; set; }

        /// <summary>
        /// 保留字段3
        /// </summary>
        [Comment("保留字段3")]
        public Guid? Field3 { get; set; }

        /// <summary>
        /// 保留字段4
        /// </summary>
        [Comment("保留字段4")]
        public Guid? Field4 { get; set; }

        /// <summary>
        /// 保留字段3
        /// </summary>
        [Comment("保留字段5")]
        public Guid? Field5 { get; set; }

        /// <summary>
        /// 保留字段4
        /// </summary>
        [Comment("保留字段6")]
        public Guid? Field6 { get; set; }

        /// <summary>
        /// 保留字段4
        /// </summary>
        [Comment("保留字段7")]
        public Guid? Field7 { get; set; }

        /// <summary>
        /// 保留字段4
        /// </summary>
        [Comment("保留字段8")]
        public Guid? Field8 { get; set; }

        /// <summary>
        /// 保留字段4
        /// </summary>
        [Comment("保留字段9")]
        public Guid? Field9 { get; set; }

        /// <summary>
        /// 新建页索引
        /// </summary>
        [Comment("护理单记录索引")]
        [Required]
        public int SheetIndex { get; set; }

        /// <summary>
        /// 新建页索引名称
        /// </summary>
        [Comment("护理单记录名称")]
        [StringLength(50, ErrorMessage = "新建页索引名称字符不能超过50字")]
        public string SheetName { get; set; }

        /// <summary>
        /// 护理单Id
        /// </summary>
        [Comment("护理单Id(外键)")]
        [Required]
        public Guid NursingDocumentId { get; set; }

        /// <summary>
        /// 护理单
        /// </summary> 
        public virtual NursingDocument NursingDocument { get; set; }

        public void UpdateSheet(string sheetName)
        {
            if (sheetName.IsNullOrEmpty())
            {
                SheetName = $"护理记录单{SheetIndex + 1}";
            }
            else
            {
                SheetName = sheetName;
            }

        }

        /// <summary>
        /// 更新动态字段名字描述
        /// </summary> 
        public void Update(
            Guid? field1,
            Guid? field2,
            Guid? field3,
            Guid? field4,
            Guid? field5,
            Guid? field6,
            Guid? field7,
            Guid? field8,
            Guid? field9)
        {
            Field1 = field1;
            Field2 = field2;
            Field3 = field3;
            Field4 = field4;
            Field5 = field5;
            Field6 = field6;
            Field7 = field7;
            Field8 = field8;
            Field9 = field9;
        }
    }
}

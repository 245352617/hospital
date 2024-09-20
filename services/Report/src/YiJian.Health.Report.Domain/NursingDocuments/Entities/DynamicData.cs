using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace YiJian.Health.Report.NursingDocuments.Entities
{
    /// <summary>
    /// 动态数据字典集合
    /// </summary>
    [Comment("动态数据字典集合")]
    public class DynamicData : Entity<Guid>
    {
        private DynamicData()
        {

        }

        public DynamicData(
            Guid id,
            Guid header,
            [NotNull] string text,
            int sheetIndex,
            Guid nursingRecordId,
            Guid nursingDocumentId)
        {
            Id = id;
            Header = header;
            Text = Check.NotNullOrEmpty(text, nameof(text), maxLength: 100);
            SheetIndex = sheetIndex;
            NursingRecordId = nursingRecordId;
            NursingDocumentId = nursingDocumentId;
        }

        /// <summary>
        /// 表头[Key]
        /// </summary>
        [Comment("表头[Key]")]
        public Guid Header { get; set; }

        /// <summary>
        /// 文本数据
        /// </summary>
        [Comment("文本数据")]
        [StringLength(100)]
        public string Text { get; set; }

        /// <summary>
        /// 新建页索引
        /// </summary>
        [Comment("新建页索引")]
        public int SheetIndex { get; set; }

        /// <summary>
        /// 护理单记录列Id
        /// </summary>
        [Comment("护理单记录列Id")]
        public Guid NursingRecordId { get; set; }

        /// <summary>
        /// 护理单Id
        /// </summary>
        [Comment("护理单Id")]
        public Guid NursingDocumentId { get; set; }

        public void Update([NotNull] string text)
        {
            Text = text;
        }

    }
}

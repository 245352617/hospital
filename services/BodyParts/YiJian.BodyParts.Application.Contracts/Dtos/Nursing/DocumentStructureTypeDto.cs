using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    public class DocumentStructureDataDto 
    {
        /// <summary>
        /// 护理日期
        /// </summary>
        [Required]
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 班次代码
        /// </summary>
        [Required]
        public string ScheduleCode { get; set; }

        public List<DocumentStructureTypeDto> structureTypeDtos { get; set; }
    }


    /// <summary>
    /// 文书-结构-类别表
    /// </summary>
    public class DocumentStructureTypeDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 该类名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 使用行数数量
        /// </summary>
        public int UseRowsLength { get; set; }

        /// <summary>
        /// 使用列数数量
        /// </summary>
        public int UseColsLength { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        public Guid? ParaValueId { get; set; }

        /// <summary>
        /// 存储数值
        /// </summary>
        public string ParaValue { get; set; }

        public List<DocumentStructureTypeDto> lstTypes { get; set; }
    }
}

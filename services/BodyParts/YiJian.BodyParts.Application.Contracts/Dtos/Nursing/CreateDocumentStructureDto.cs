using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 文书动态结构
    /// </summary>
    public class CreateDocumentStructureDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 所属文书
        /// </summary>
        public string BelongToKey { get; set; }

        /// <summary>
        /// 人员流水号
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 主表Id
        /// </summary>
        public Guid? DocumentId { get; set; }

        /// <summary>
        /// 护理日期
        /// </summary>
        public string NurseTime { get; set; }

        /// <summary>
        /// 班次代码
        /// </summary>
        public string ScheduleCode { get; set; }

        /// <summary>
        /// 类型Id
        /// </summary>
        public Guid? DocumentStructureTypeId { get; set; }

        /// <summary>
        /// 存储数值
        /// </summary>
        public string ParaValue { get; set; }
    }
}

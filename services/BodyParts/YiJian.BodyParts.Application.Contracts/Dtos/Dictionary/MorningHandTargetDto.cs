using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class MorningHandTargetDto
    {

        public Guid? Id { get; set; }

        /// <summary>
        /// 指标编码(拼音码)
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 指标名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 分组编码
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// 统计Sql
        /// </summary>
        public string StaticSql { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsDisplay { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }
    }
}

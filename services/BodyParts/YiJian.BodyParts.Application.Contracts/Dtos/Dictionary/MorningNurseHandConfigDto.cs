using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class MorningNurseHandConfigDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 编码(拼音码)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public string Classify { get; set; }

        /// <summary>
        /// Sql语句(Json串，[{"sql":""}])
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsDisplay { get; set; }
    }
}

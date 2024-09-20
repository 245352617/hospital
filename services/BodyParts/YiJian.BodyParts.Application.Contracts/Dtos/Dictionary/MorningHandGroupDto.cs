using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class MorningHandGroupDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 分组编码(拼音码)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsDisplay { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 图标(base64)
        /// </summary>
        public string Icon { get; set; }
    }
}

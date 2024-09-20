using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.Characters.Dto
{
    /// <summary>
    /// 字符内容
    /// </summary>
    public class UniversalCharacterNodeDto:EntityDto<int?>
    { 
        /// <summary>
        /// 字符
        /// </summary> 
        [StringLength(50)]
        public string Character { get; set; }

        /// <summary>
        /// 排序
        /// </summary> 
        public int Sort { get; set; }

        /// <summary>
        /// 目录对象
        /// </summary>
        public int UniversalCharacterId { get; set; }
    }
}

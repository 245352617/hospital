using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Libs.Dto
{
    /// <summary>
    /// 目录结构上实体
    /// </summary>
    public class CatalogueDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 目录名称
        /// </summary> 
        [Required(ErrorMessage = "目录名称必填"), StringLength(200, ErrorMessage = "目录名称最大长度100字符")]
        public string Title { get; set; }

        /// <summary>
        /// 是否是文件（文件夹=false,文件=true）
        /// </summary> 
        [Required(ErrorMessage = "文件类型必填，文件夹=false,文件=true")]
        public bool IsFile { get; set; } = false;
         
        /// <summary>
        /// 父级Id，根级=不传该参数,如果有父级才传
        /// </summary>  
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 排序权重
        /// </summary> 
        public int Sort { get; set; } = 0;

        /// <summary>
        /// 目录结构层级Level
        /// </summary> 
        public int Lv { get; set; }

        /// <summary>
        /// 电子文书分类(0=电子病历,1=文书)
        /// </summary> 
        public EClassify Classify { get; set; }

    }
}

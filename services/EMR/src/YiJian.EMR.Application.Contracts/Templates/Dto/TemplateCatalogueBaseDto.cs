using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.EMR.Libs.Dto;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.Templates.Dto;
using Volo.Abp.Application.Dtos;
using YiJian.EMR.Enums;
using Microsoft.EntityFrameworkCore;

namespace YiJian.EMR.Templates.Dto
{
    /// <summary>
    /// 基础的内容结构
    /// </summary>
    public class TemplateCatalogueBaseDto : EntityDto<Guid>
    {
        /// <summary>
        /// 目录名称
        /// </summary>  
        public string Title { get; set; }

        /// <summary>
        /// 目录名称编码
        /// </summary>  
        public string Code { get; set; }

        /// <summary>
        /// 是否是文件（文件夹=false,文件=true）
        /// </summary> 
        public bool IsFile { get; set; } = false;

        /// <summary>
        /// 父级Id，根级=0
        /// </summary> 
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 模板类型 模板类型，0=通用，1=科室，2=个人
        /// </summary> 
        public ETemplateType TemplateType { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary> 
        public bool IsEnabled { get; set; } = false;

        /// <summary>
        /// 排序权重
        /// </summary> 
        public int Sort { get; set; }

        /// <summary>
        /// 目录结构层级Level
        /// </summary> 
        public int Lv { get; set; }

        /// <summary>
        /// 病历库引入的Id, 
        /// 只做最初引入的参考，
        /// 不做xml定位的参考，
        /// 因为会在病历模板里面被编辑
        /// </summary> 
        public Guid? CatalogueId { get; set; }

        /// <summary>
        /// 最初引入病历库的名称
        /// </summary> 
        public string CatalogueTitle { get; set; }

        private Guid? originId;

        /// <summary>
        /// 最初引入病历库的Id
        /// </summary>
        public Guid? OriginId
        {
            get
            {
                if (originId == null || originId == Guid.Empty || originId!.ToString() == "00000000-0000-0000-0000-000000000000")
                {
                    return CatalogueId;
                }
                return originId;
            }
            set { originId = value; }
        }

         
    }
}

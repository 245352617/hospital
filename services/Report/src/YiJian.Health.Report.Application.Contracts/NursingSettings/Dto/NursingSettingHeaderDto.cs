using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingSettings.Dto
{
    /// <summary>
    /// 动态六项配置
    /// </summary>
    public class NursingInputOptionsDto
    {
        /// <summary>
        /// 表头名称[名称]
        /// </summary> 
        [Required, StringLength(50, ErrorMessage = "表头名称需在50字内")]
        public string Header { get; set; }

        /// <summary>
        /// 排序编号[序号]
        /// </summary> 
        public int Sort { get; set; } = 1;

        /// <summary>
        /// 护理单配置Id
        /// </summary> 
        public Guid NursingSettingId { get; set; }

        /// <summary>
        /// 护理单配置
        /// </summary>
        public virtual NursingSettingDto NursingSetting { get; set; }

        /// <summary>
        /// 护理单配置项集合
        /// </summary>
        public virtual List<NursingSettingItemDto> Items { get; set; }


    }

    /// <summary>
    /// 护理单表头配置
    /// </summary>
    public class NursingSettingHeaderDto : NursingSettingHeaderBaseDto
    {
        /// <summary>
        /// 护理单配置项集合
        /// </summary>
        public List<NursingSettingItemDto> Items { get; set; } = new List<NursingSettingItemDto>();
    }

    /// <summary>
    /// 护理单表头配置
    /// </summary>
    public class NursingSettingHeaderBaseDto : EntityDto<Guid>
    {
        /// <summary>
        /// 表头名称
        /// </summary>  
        public string Header { get; set; }

        /// <summary>
        /// 表头类型（0=普通文本域,1=单选按钮,2=复选框,3=下拉框,4=数字）
        /// </summary> 
        public EInputType InputType { get; set; }

        /// <summary>
        /// 是否带输入框
        /// </summary> 
        public bool IsCarryInputBox { get; set; } = false;

        /// <summary>
        /// 护理单配置Id
        /// </summary> 
        public Guid NursingSettingId { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }
    }


}

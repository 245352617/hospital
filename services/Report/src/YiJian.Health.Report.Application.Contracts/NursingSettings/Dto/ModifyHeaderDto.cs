using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingSettings.Dto
{
    /// <summary>
    /// 新增或更新表头的信息
    /// </summary>
    public class ModifyHeaderDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 表头类型（0=普通文本域,1=单选按钮,2=复选框,3=下拉框,4=数字）
        /// </summary> 
        [Required]
        public EInputType InputType { get; set; }

        /// <summary>
        /// 是否带输入框
        /// </summary> 
        public bool IsCarryInputBox { get; set; } = false;

        /// <summary>
        /// 表头名称
        /// </summary>  
        [Required, StringLength(50, ErrorMessage = "表头名称需在50字内")]
        public string Header { get; set; }

        /// <summary>
        /// 护理单配置Id
        /// </summary> 
        public Guid NursingSettingId { get; set; }

        /// <summary>
        /// 排序编号[序号]
        /// </summary> 
        public int Sort { get; set; } = 1;



    }
}

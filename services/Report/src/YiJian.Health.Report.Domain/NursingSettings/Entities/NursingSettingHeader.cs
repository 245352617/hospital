using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingSettings.Entities
{
    /// <summary>
    /// 护理单表头配置
    /// </summary>
    [Comment("护理单表头配置")]
    public class NursingSettingHeader : FullAuditedAggregateRoot<Guid>
    {
        private NursingSettingHeader()
        {

        }

        public NursingSettingHeader(Guid id,[NotNull]string header,int sort, Guid nursingSettingId, EInputType inputType, bool isCarryInputBox)
        {
            Id = id; 
            Header = Check.NotNullOrEmpty(header,nameof(header),maxLength:50); 
            Sort = sort;
            NursingSettingId = nursingSettingId;
            InputType = inputType;
            IsCarryInputBox = isCarryInputBox;
        }
         
        /// <summary>
        /// 表头名称[名称]
        /// </summary>
        [Comment("表头名称")]
        [Required,StringLength(50,ErrorMessage = "表头名称需在50字内")]
        public string Header { get; set; }

        /// <summary>
        /// 表头类型（0=普通文本域,1=单选按钮,2=复选框,3=下拉框,4=数字）
        /// </summary> 
        [Comment("表头类型")]
        public EInputType InputType { get; set; }

        /// <summary>
        /// 是否带输入框
        /// </summary> 
        [Comment("是否带输入框")]
        public bool IsCarryInputBox { get; set; } = false;

        /// <summary>
        /// 排序编号[序号]
        /// </summary>
        [Comment("排序编号[序号]")]
        public int Sort { get; set; } = 1;

        /// <summary>
        /// 护理单配置Id
        /// </summary>
        [Comment("护理单配置Id")]
        public Guid NursingSettingId { get; set; }

        /// <summary>
        /// 护理单配置
        /// </summary>
        public virtual NursingSetting NursingSetting { get;set;}

        /// <summary>
        /// 护理单配置项集合
        /// </summary>
        public virtual List<NursingSettingItem> Items { get; set; }

        public void Update([NotNull]string header,int sort ,Guid nursingSettingId, EInputType inputType, bool isCarryInputBox)
        {  
            Header = Check.NotNullOrEmpty(header,nameof(header),maxLength:50); 
            Sort = sort;
            NursingSettingId = nursingSettingId;
            InputType = inputType;
            IsCarryInputBox = isCarryInputBox;
        }

    }
}

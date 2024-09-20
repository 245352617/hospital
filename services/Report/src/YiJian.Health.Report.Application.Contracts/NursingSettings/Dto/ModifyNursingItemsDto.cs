using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingSettings.Dto
{
    /// <summary>
    /// 新增或更新选项内容(单个)
    /// </summary>
    public class ModifyNursingItemsDto
    {
        /// <summary>
        /// 表头编辑
        /// </summary>
        public ModifyHeaderDto Header { get; set; }

        /// <summary>
        /// 表单域列表项内容
        /// </summary>
        public List<ModifyBaseItemDto> Items { get; set; } = new List<ModifyBaseItemDto>();

    }

    /// <summary>
    /// 修改基础项Dto
    /// </summary>
    public class ModifyBaseItemDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 表单域类型（0=普通文本域,1=单选按钮,2=复选框,3=下拉框,4=数字）
        /// </summary> 
        [Required]
        public EInputType InputType { get; set; }

        private string _value;
        /// <summary>
        /// 配置的值
        /// </summary> 
        [StringLength(50, ErrorMessage = "配置的值需在50字内")]
        public string Value
        {
            get { return _value; }
            set
            {
                if (value.IsNullOrEmpty() && !Text.IsNullOrEmpty())
                {
                    _value = Text;
                }
                else
                {
                    _value = value;
                }
            }
        }

        /// <summary>
        /// 水印配置，文本域用
        /// </summary> 
        [StringLength(50, ErrorMessage = "水印需在50字内")]
        public string Watermark { get; set; }

        private string _text;
        /// <summary>
        /// 复选框、单选按钮、下拉框用
        /// </summary> 
        [StringLength(50, ErrorMessage = "Text需在50字内")]
        public string Text
        {
            get { return _text; }
            set
            {
                if (value.IsNullOrEmpty() && !Value.IsNullOrEmpty())
                {
                    _text = Value;
                }
                else
                {
                    _text = value;
                }
            }
        }

        /// <summary>
        /// 是否有提示文本（文本域会有这种左右提示，如：ml,g,% ...）
        /// </summary> 
        private bool hasTextblock;

        /// <summary>
        /// 是否有提示文本（文本域会有这种左右提示，如：ml,g,% ...）
        /// </summary>
        public bool HasTextblock
        {
            get { return hasTextblock; }
            set
            {
                if (!TextblockLeft.IsNullOrEmpty() || !TextblockRight.IsNullOrEmpty())
                {
                    hasTextblock = true;
                }
                else
                {
                    hasTextblock = false;
                }
            }
        }

        /// <summary>
        /// 左边提示文本
        /// </summary> 
        [StringLength(50, ErrorMessage = "左边提示文本需在50字内")]
        public string TextblockLeft { get; set; }

        /// <summary>
        /// 右边提示文本
        /// </summary> 
        [StringLength(50, ErrorMessage = "右边提示文本需在50字内")]
        public string TextblockRight { get; set; }

        /// <summary>
        /// 第一层表单域
        /// </summary> 
        public int Lv { get; set; } = 0;

        /// <summary>
        /// 是否带输入框
        /// </summary> 
        public bool IsCarryInputBox { get; set; } = false;

        /// <summary>
        /// 是否有下一层
        /// </summary>  
        public bool HasNext { get; set; } = false;

        /// <summary>
        /// 上一级的Id
        /// </summary>
        public Guid? NursingSettingItemId { get; set; }

        /// <summary>
        /// 排序
        /// </summary> 
        public int Sort { get; set; }

        /// <summary>
        /// 子集合
        /// </summary>
        public List<ModifyBaseItemDto> Items { get; set; }

    }

}

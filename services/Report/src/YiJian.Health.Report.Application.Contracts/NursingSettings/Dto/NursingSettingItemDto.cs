using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using YiJian.ECIS.ShareModel.Extensions;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingSettings.Dto
{
    /// <summary>
    /// 查询表单域查询条件
    /// </summary>
    public class SearchNursingSettingItemsDto
    {
        /// <summary>
        /// 护理单表头配置Id（第一层用）
        /// </summary>
        public Guid? NursingSettingHeaderId { get; set; }

        /// <summary>
        /// 护理单配置项Id（第二层之后用）
        /// </summary>
        public Guid? NursingSettingItemId { get; set; }
    }

    /// <summary>
    /// 整合表头，表单域信息
    /// </summary>
    public class NursingSettingHeaderItemDto
    {
        /// <summary>
        /// 表单域类型（默认列表，具体的到item内可查）
        /// </summary>
        public List<InputTypeDto> InputTypes { get; set; } = new List<InputTypeDto>();

        /// <summary>
        /// 表头信息
        /// </summary>
        public NursingSettingHeaderBaseDto Header { get; set; }

        /// <summary>
        /// 表单域信息
        /// </summary>
        public List<NursingSettingItemDto> Items { get; set; } = new List<NursingSettingItemDto>();

    }

    /// <summary>
    /// 输入类型数据
    /// </summary>
    public static class InputTypeData
    {
        /// <summary>
        /// 表单域所有选项
        /// </summary>
        /// <returns></returns>
        public static List<InputTypeDto> InpuTypes()
        {
            var inpuTypeList = new List<InputTypeDto>();

            inpuTypeList.Add(new InputTypeDto
            {
                Value = EInputType.Text,
                Text = EInputType.Text.GetDescription()
            });

            inpuTypeList.Add(new InputTypeDto
            {
                Value = EInputType.Radio,
                Text = EInputType.Radio.GetDescription()
            });

            inpuTypeList.Add(new InputTypeDto
            {
                Value = EInputType.Checkbox,
                Text = EInputType.Checkbox.GetDescription()
            });

            inpuTypeList.Add(new InputTypeDto
            {
                Value = EInputType.Select,
                Text = EInputType.Select.GetDescription()
            });

            return inpuTypeList;
        }

    }

    /// <summary>
    /// 表单域类型
    /// </summary>
    public class InputTypeDto
    {
        /// <summary>
        /// 值
        /// </summary>
        public EInputType Value { get; set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text { get; set; }

    }

    /// <summary>
    /// 护理单配置项
    /// </summary>
    public class NursingSettingItemDto : EntityDto<Guid>
    {
        /// <summary>
        /// 表单域类型（0=普通文本域,1=单选按钮,2=复选框,3=下拉框,4=数字）
        /// </summary> 
        public EInputType InputType { get; set; }

        /// <summary>
        /// 配置的值
        /// </summary> 
        public string Value { get; set; }

        /// <summary>
        /// 水印配置，文本域用
        /// </summary> 
        public string Watermark { get; set; }

        /// <summary>
        /// 复选框、单选按钮、下拉框用
        /// </summary> 
        public string Text { get; set; }

        /// <summary>
        /// 是否有提示文本（文本域会有这种左右提示，如：ml,g,% ...）
        /// </summary> 
        public bool HasTextblock { get; set; }

        /// <summary>
        /// 左边提示文本
        /// </summary> 
        public string TextblockLeft { get; set; }

        /// <summary>
        /// 右边提示文本
        /// </summary>  
        public string TextblockRight { get; set; }

        /// <summary>
        /// 是否带输入框
        /// </summary> 
        public bool IsCarryInputBox { get; set; } = false;

        /// <summary>
        /// 护理单配置项Id
        /// </summary>
        public Guid? NursingSettingItemId { get; set; }

        /// <summary>
        /// 护理单配置项 
        /// </summary>
        public virtual List<NursingSettingItemDto> Items { get; set; }

        /// <summary>
        /// 第一层表单域
        /// </summary> 
        public int Lv { get; set; } = 0;

        /// <summary>
        /// 护理单表头配置Id
        /// </summary>
        public Guid? NursingSettingHeaderId { get; set; }

        /// <summary>
        /// 是否有下一层
        /// </summary>  
        public bool HasNext { get; set; } = false;

        /// <summary>
        /// 排序
        /// </summary> 
        public int Sort { get; set; }
    }

}

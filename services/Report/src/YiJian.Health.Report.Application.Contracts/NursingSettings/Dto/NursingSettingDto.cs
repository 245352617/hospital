using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using YiJian.Health.Report.NursingDocuments.Dto;

namespace YiJian.Health.Report.NursingSettings.Dto
{
    /// <summary>
    /// 护理单主题
    /// </summary>
    public class NursingSettingDto : EntityDto<Guid>
    {
        ///// <summary>
        ///// 表头分类
        ///// </summary>  
        //public string Category { get; set; }

        /// <summary>
        /// 配置组Id
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// 配置组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 表头分类
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// 排序顺序
        /// </summary> 
        public int Sort { get; set; }

        /// <summary>
        /// 护理单配置
        /// </summary>
        public List<NursingSettingHeaderDto> Headers { get; set; } = new List<NursingSettingHeaderDto>();
    }

    /// <summary>
    /// 护理单特殊护理记录回填模型
    /// </summary>
    public class BackfillSpecialCareDto
    {
        /// <summary>
        /// 展示的数据
        /// </summary>
        public List<NursingSettingDto> InputSettings { get; set; } = new List<NursingSettingDto>();

        /// <summary>
        /// 回填的病患特征内容
        /// </summary>
        public CharacteristicDto Characteristic { get; set; }

        /// <summary>
        /// 动态六项对应的数据集合
        /// </summary>
        public List<CharacteristicDto> DynamicDataList { get; set; } = new List<CharacteristicDto>();

        /// <summary>
        /// 字典数据
        /// </summary>
        public Dictionary<string, string> Map { get; set; }
    }

}

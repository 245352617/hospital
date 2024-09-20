using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 组长巡查
    /// </summary>
    public class GroupLeaderInspectDto : EntityDto<Guid>
    {

    }

    /// <summary>
    /// 更新设置数据序号
    /// </summary>
    public class UpSiteDataDto
    {
        /// <summary>
        /// 组ID
        /// </summary>
        [Required]
        public Guid Pid { get; set; }

        /// <summary>
        /// 内容ID
        /// </summary>
        public Guid? Cid { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [Required]
        public int SortNum { get; set; }

        /// <summary>
        /// true：组；false：内容
        /// </summary>
        public bool IsGroup { get; set; }

        /// <summary>
        /// 是否上移
        /// </summary>
        public bool IsUp { get; set; }
    }

    /// <summary>
    /// 组长巡查设置
    /// </summary>
    public class GroupLeaderInspectSiteDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 排序序号(组)
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 前端使用
        /// </summary>
        public bool leftMore { get; set; } = false;

        /// <summary>
        /// 前端使用
        /// </summary>
        public bool leftMorePop { get; set; } = false;

        /// <summary>
        /// 内容列表
        /// </summary>
        public List<InspectContentDto> InspectContentDtos { get; set; }
    }

    /// <summary>
    /// 内容
    /// </summary>
    public class InspectContentDto
    {
        public Guid? Pid { get; set; }

        public Guid? Cid { get; set; }

        /// <summary>
        /// 序号(内容)
        /// </summary>
        public int? SortNumByC { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string ContentName { get; set; }

        /// <summary>
        /// 是否多选
        /// </summary>
        public bool? IsMultipleChouce { get; set; }

        /// <summary>
        /// 选项列表
        /// </summary>
        public string[] OptionsName { get; set; }

        /// <summary>
        /// 前端使用
        /// </summary>
        public bool rightMore { get; set; } = false;

        /// <summary>
        /// 前端使用
        /// </summary>
        public bool rightMorePop { get; set; } = false;
    }


    /// <summary>
    /// 组长巡查设置
    /// </summary>
    public class GroupLeaderInspectSiteByAddDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 排序序号(组)
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 前端使用
        /// </summary>
        public bool leftMore { get; set; } = false;

        /// <summary>
        /// 前端使用
        /// </summary>
        public bool leftMorePop { get; set; } = false;

        /// <summary>
        /// 内容列表
        /// </summary>
        public List<InspectContentByAddDto> InspectContentDtos { get; set; }
    }

    /// <summary>
    /// 内容
    /// </summary>
    public class InspectContentByAddDto
    {
        public Guid? Pid { get; set; }

        public Guid? Cid { get; set; }

        /// <summary>
        /// 序号(内容)
        /// </summary>
        public int? SortNumByC { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string ContentName { get; set; }

        /// <summary>
        /// 是否多选
        /// </summary>
        public bool? IsMultipleChouce { get; set; }

        /// <summary>
        /// 选项列表
        /// </summary>
        public string OptionsName { get; set; }

        /// <summary>
        /// 前端使用
        /// </summary>
        public bool rightMore { get; set; } = false;

        /// <summary>
        /// 前端使用
        /// </summary>
        public bool rightMorePop { get; set; } = false;
    }

    /// <summary>
    /// 患者巡查信息
    /// </summary>
    public class PatientInspectDto
    {
        /// <summary>
        /// 内容ID
        /// </summary>
        public Guid Cid { get; set; }

        /// <summary>
        /// 内容名称
        /// </summary>
        public string ContentName { get; set; }

        /// <summary>
        /// 是否多选
        /// </summary>
        public bool? IsMultipleChouce { get; set; }

        /// <summary>
        /// 选项信息
        /// </summary>
        public OptionsDto Options { get; set; }
    }

    /// <summary>
    /// 选项信息
    /// </summary>
    public class OptionsDto
    {
        /// <summary>
        /// 选项
        /// </summary>
        public string[] title { get; set; }

        /// <summary>
        /// 选项值
        /// </summary>
        public string[] value { get; set; }
    }

    /// <summary>
    /// 患者巡查列表
    /// </summary>
    public class PatientInspectListDto
    {
        /// <summary>
        /// 床位号码
        /// </summary>
        public string BedNum { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 患者id(通过业务构造的流水号，每个患者每次入科号码唯一)
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public string InDeptTime { get; set; }

        /// <summary>
        /// 入科天数
        /// </summary>
        public int Indays { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>
        public string ClinicDiagnosis { get; set; }

        /// <summary>
        /// 是否巡查
        /// </summary>
        public bool isInspect { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string InHosId { get; set; }
    }
}

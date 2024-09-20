using System;
using System.Collections.Generic;

namespace YiJian.Nursing.Temperatures.Dtos
{
    /// <summary>
    /// 描述：临床事件Dto
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 10:54:55
    /// </summary>
    public class ClinicalEventDto
    {
        /// <summary>
        /// 入院时间
        /// </summary>
        public DateTime InHospital { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary>
        public Guid PI_Id { get; set; }

        /// <summary>
        /// 护士账号
        /// </summary>
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士名字
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 临床事件
        /// </summary>
        public List<ClinicalEventDetailDto> ClinicalEventDtos { get; set; }
    }

    /// <summary>
    /// 临床事件
    /// </summary>
    public class ClinicalEventDetailDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 患者信息主键
        /// </summary>
        public Guid PI_Id { get; set; }

        /// <summary>
        /// 事件分类编码
        /// </summary>
        public string EventCategoryCode { get; set; }

        /// <summary>
        /// 事件分类
        /// </summary>
        public string EventCategory { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime HappenTime { get; set; }

        /// <summary>
        /// 上下标编码
        /// </summary>
        public string UpDownFlagCode { get; set; }

        /// <summary>
        /// 上下标
        /// </summary>
        public string UpDownFlag { get; set; }

        /// <summary>
        /// 事件描述
        /// </summary>
        public string EventDescription { get; set; }

        /// <summary>
        /// 护士账号
        /// </summary>
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士名字
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}

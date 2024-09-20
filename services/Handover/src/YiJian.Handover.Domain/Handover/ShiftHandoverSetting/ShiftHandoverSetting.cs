using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Handover
{
    /// <summary>
    /// 交接班设置表
    /// </summary>
    public class ShiftHandoverSetting : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 类别编码
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [Description("类别编码")]
        public string CategoryCode { get; private set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [Description("类别名称")]
        public string CategoryName { get; private set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [Description("班次名称")]
        public string ShiftName { get; private set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [Description("开始时间")]
        public string StartTime { get; private set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [Description("结束时间")]
        public string EndTime { get; private set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Description("是否启用")]
        public bool IsEnable { get; private set; } = true;

        /// <summary>
        /// 匹配颜色
        /// </summary>
        [Description("匹配颜色")]
        public string MatchingColor { get; private set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Description("排序")]
        public int Sort { get; private set; }

        /// <summary>
        /// 类型，医生1，护士0
        /// </summary>
        [Description("类型，医生1，护士0")]
        public int Type { get; private set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreationName { get; private set; }

        /// <summary>
        /// 修改人名称
        /// </summary>
        public string ModificationName { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryCode">类别编码</param>
        /// <param name="categoryName">类别名称</param>
        /// <param name="shiftName">班次名称</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="matchingColor">匹配颜色</param>
        /// <param name="sort">排序</param>
        /// <param name="type">类型，医生1，护士0</param>
        /// <param name="creationName">创建人</param>
        public ShiftHandoverSetting(Guid id, string categoryCode, string categoryName, string shiftName,
            string startTime, string endTime, bool isEnable, string matchingColor, int sort, int type,
            string creationName = null)
        {
            Id = id;
            CategoryCode = categoryCode;
            CategoryName = categoryName;
            ShiftName = shiftName;
            StartTime = startTime;
            EndTime = endTime;
            IsEnable = isEnable;
            MatchingColor = matchingColor;
            Sort = sort;
            Type = type;
            CreationName = creationName;
        }

        /// <summary>
        /// 修改函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryCode">类别编码</param>
        /// <param name="categoryName">类别名称</param>
        /// <param name="shiftName">班次名称</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="matchingColor">匹配颜色</param>
        /// <param name="sort">排序</param>
        /// <param name="type">类型，医生1，护士0</param>
        public void Edit(Guid id, string categoryCode, string categoryName, string shiftName,
            string startTime, string endTime, bool isEnable, string matchingColor, string modificationName = null)
        {
            Id = id;
            CategoryCode = categoryCode;
            CategoryName = categoryName;
            ShiftName = shiftName;
            StartTime = startTime;
            EndTime = endTime;
            IsEnable = isEnable;
            MatchingColor = matchingColor;
            ModificationName = modificationName;
        }

        private ShiftHandoverSetting()
        {
        }
    }
}
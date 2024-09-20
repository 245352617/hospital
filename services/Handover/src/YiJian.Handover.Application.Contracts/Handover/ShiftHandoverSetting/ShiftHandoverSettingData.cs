using System;

namespace YiJian.Handover
{
    public class ShiftHandoverSettingData
    {
        /// <summary>
        /// 主键id,有修改，无则新增
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 类别编码
        /// </summary>
        public string CategoryCode { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 班次名称
        /// </summary>
        public string ShiftName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 匹配颜色
        /// </summary>
        public string MatchingColor { get; set; }
        /// <summary>
        /// 类型，医生1，护士0
        /// </summary>
        public int Type { get; set; }
    }
}
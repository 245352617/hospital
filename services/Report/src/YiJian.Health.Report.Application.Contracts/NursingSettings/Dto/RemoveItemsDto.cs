using System;

namespace YiJian.Health.Report.NursingSettings.Dto
{
    /// <summary>
    /// 删除表单域内容
    /// </summary>
    public class RemoveItemsDto
    {
        /// <summary>
        /// 如果有子节点是否需要删除，默认是false 不删除，如果有需要可以填true,将当前节点下的所有子节点一并删除
        /// </summary>
        public bool DeleteChildren { get; set; } = false;

        /// <summary>
        /// 表单域Id
        /// </summary>
        public Guid NursingSettingItemId { get; set; }
    }
}

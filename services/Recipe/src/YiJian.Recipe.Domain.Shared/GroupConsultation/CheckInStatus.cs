using System.ComponentModel;

namespace YiJian.Recipes.GroupConsultation
{
    /// <summary>
    /// 医生报到状态
    /// </summary>
    public enum CheckInStatus
    {
        [Description("已邀请")] 已邀请 = 0,
        [Description("已报到")] 已报到 = 1,
        [Description("已离开")] 已离开 = 2,
        [Description("未报到")] 未报到 = 3,

    }
}
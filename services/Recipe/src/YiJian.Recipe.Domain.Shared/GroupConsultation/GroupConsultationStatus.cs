using System.ComponentModel;

namespace YiJian.Recipes.GroupConsultation
{
    public enum GroupConsultationStatus
    {
        [Description("全部")] 全部 = -1,
        [Description("待开始")] 待开始 = 0,
        [Description("已开始")] 已开始 = 1,
        [Description("已完结")] 已完结 = 2,
        [Description("已取消")] 已取消 = 3
    }
}
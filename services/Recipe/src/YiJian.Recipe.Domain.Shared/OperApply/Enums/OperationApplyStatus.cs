using System.ComponentModel;

namespace YiJian.Recipe
{
    public enum OperationApplyStatus
    {
        [Description("申请中")]
        申请中 = 0,
        [Description("申请通过")]
        申请通过 = 1,
        [Description("已撤回")]
        已撤回 = 2,
        [Description("已驳回")]
        已驳回 = 3
    }
}
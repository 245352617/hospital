using System.ComponentModel;

namespace YiJian.Recipe.Enums
{
    public enum ESpecimenType
    {
        [Description("血液")] Blood,
        [Description("咽拭子")] PharyngealSwab,
        [Description("痰")] Sputum,
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiJian.BodyParts.Domain.Shared.Enum
{
    /// <summary>
    /// 质控15项枚举
    /// </summary>
    public enum IcuQCStatisticsEnum
    {
        [Description("ICU患者收治率")]
        PatientAdmission = 1,
        [Description("ICU患者收治床日率")]
        BedRate = 2,
        [Description("Apachell>=15患者收治率")]
        APACHEIIGT15HZSZL = 3,
        [Description("感染性休克3h集束化治疗（bundle）完成率")]
        GRXXK3HJSHZLWCL = 4,
        [Description("感染性休克 6h集束化治疗（bundle）完成率")]
        GRXXK6HJSHZLWCL = 5,
        [Description("ICU抗菌药物治疗前病原学送检率")]
        ICUKJYWZLQBYXSJL = 6,
        [Description("ICU深静脉血栓（DVT）预防率")]
        ICUDVTYFL = 7,
        [Description("ICU患者预计病死率")]
        ICUHZYJBSL = 8,
        [Description("ICU患者标化病死指数")]
        ICUHZBHBSZS = 9,
        [Description("ICU非计划气管插管拔管率")]
        ICUFJHQGCGBCL = 10,
        [Description("ICU气管插管拔管后48h内再插管率")]
        ICUQGCGBGH48HNZCGL = 11,
        [Description("非计划转入ICU率")]
        FJHZRICUL = 12,
        [Description("转出ICU后48h内重返率")]
        ZCICUH48HNCFL = 13,
        [Description("ICU呼吸机相关性肺炎（VAP）发病率")]
        ICUVAPFBL = 14,
        [Description("ICU血管内导管相关血流感染（CRBSI）发病率")]
        ICUCRBSIFBL = 15,
        [Description("ICU导尿管相关泌尿系感染（CAUTI）发病率")]
        ICUCAUTIFBL = 16,
        [Description("APACHEⅡ评分＜10分患者收治率")]
        TenBelow = 17,
        [Description("10≤APACHEⅡ评分＜15分患者收治率")]
        Ten = 18,
        [Description("15≤APACHEⅡ评分＜20分患者收治率")]
        Fiften = 19,
        [Description("20≤APACHEⅡ评分＜25分患者收治率")]
        Twenty = 20,
        [Description("APACHEⅡ评分≥25分患者收治率")]
        TwentyFive = 21,
        [Description("转出ICU后24h内重返率")]
        TwentyFourReturn = 22
    }
}

namespace YiJian.BodyParts
{
    /// <summary>
    /// hl7病患枚举 
    /// </summary>
    public enum patientEventEnum
    {
        其他或未知 = -1,
        住院登记 = 1,
        转科 = 2,
        住院结算 = 3,
        门诊登记 = 4,
        入科 = 10,
        [DbDescription("取消门诊、住院登记")] 取消门诊住院登记 = 11,
        取消住院结算 = 13,
        出科 = 16,
        取消出科 = 25,
        增加诊断 = 31,
        取消入科 = 32,
        [DbDescription("换床、包床")] 换床包床 = 42,
        [DbDescription("借床")] 借床 = 33,
        更改患者信息 = 99
    }
}
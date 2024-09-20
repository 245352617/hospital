namespace YiJian.ECIS.Call
{
    public static class CallErrorCodes
    {
        //Add your business exception error codes here...

        /// <summary>
        /// 不允许删除
        /// </summary>
        public static string CanNotDelete { get; } = "Call:cant-delete";

        /// <summary>
        /// 诊室固定规则已存在
        /// </summary>
        public static string ConsultingRoomRegularExists { get; } = "Call:consulting-room-regular-exists";

        /// <summary>
        /// 诊室固定规则不存在
        /// </summary>
        public static string ConsultingRoomRegularNotExists { get; } = "Call:consulting-room-regular-not-exists";

        /// <summary>
        /// 诊室不存在
        /// </summary>
        public static string ConsultingRoomNotExists { get; } = "Call:consulting-room-not-exists";

        /// <summary>
        /// 科室不存在
        /// </summary>
        public static string DepartmentNotExists { get; } = "Call:department-not-exists";

        /// <summary>
        /// 医生变动规则已存在
        /// </summary>
        public static string DoctorRegularExists { get; } = "Call:doctor-regular-exists";

        /// <summary>
        /// 医生变动规则不存在
        /// </summary>
        public static string DoctorRegularNotExists { get; } = "Call:doctor-regular-not-exists";

        /// <summary>
        /// 小时（时间）不在0-23之间
        /// </summary>
        public static string HourInvalid { get; } = "Call:hour-invalid";

        /// <summary>
        /// 分钟（时间）不在0-59之间
        /// </summary>
        public static string MinuteInvalid { get; } = "Call:minute-invalid";

        /// <summary>
        /// 基础配置未初始化数据
        /// </summary>
        public static string BaseConfigHasNotInitial { get; } = "Call:base-config-has-not-initial";

        /// <summary>
        /// 当前科室已存在排队号规则
        /// </summary>
        public static string DepartmentSerialNoRuleExists { get; } = "Call:depart-sn-rule-exists";

        /// <summary>
        /// 排队号规则不存在
        /// </summary>
        public static string SerialNoRuleNotExists { get; } = "Call:sn-rule-not-exists";
    }
}

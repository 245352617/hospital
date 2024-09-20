using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Domain.Shared
{
    /// <summary>
    /// 统计表---条件类型
    /// </summary>
    public static class QueryTypeConst
    {
        /// <summary>
        /// 输入框
        /// </summary>
        public const string Input = "input";

        /// <summary>
        /// 日期
        /// </summary>
        public const string DateTime = "dateTime";

        /// <summary>
        /// 时间(范围)
        /// </summary>
        public const string DateTimeArea = "dateTimeArea";

        /// <summary>
        /// 单选框
        /// </summary>
        public const string Radio = "radio";

        /// <summary>
        /// 多选框
        /// </summary>
        public const string Checkbox = "checkbox";

        /// <summary>
        /// 下拉单选
        /// </summary>
        public const string Select = "select";

        /// <summary>
        /// 下拉多选
        /// </summary>
        public const string MultipleSelection = "multipleSelection";

        /// <summary>
        /// 范围
        /// </summary>
        public const string InputArea = "inputArea";
    }
}

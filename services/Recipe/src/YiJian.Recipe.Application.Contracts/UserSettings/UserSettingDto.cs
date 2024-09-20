using System;

namespace YiJian.Recipe
{
    /// <summary>
    /// 个人配置
    /// </summary>
    public class UserSettingDto
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 配置组
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 配置组编码
        /// </summary>
        public string GroupCode { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 配置编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 配置类型:  boolRadio =0 布尔单选,radio=1 多值单选 ，checkBox=2 多选，dropDownList=3 下拉框
        /// </summary>
        public ComponentType Type { get; set; }

        /// <summary>
        /// 配置值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }

    public enum ComponentType
    {
        boolRadio, //布尔单选
        radio, //多值单选
        checkBox, //多选
        dropDownList //下拉框

    }
}
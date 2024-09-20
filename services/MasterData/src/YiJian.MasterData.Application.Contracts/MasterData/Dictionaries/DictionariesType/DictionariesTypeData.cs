namespace YiJian.MasterData.DictionariesType
{
    using System;

    /// <summary>
    /// 字典类型编码 读取输出
    /// </summary>
    [Serializable]
    public class DictionariesTypeData
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 字典类型编码
        /// </summary>
        public string DictionariesTypeCode { get; set; }

        /// <summary>
        /// 字典类型名称
        /// </summary>
        public string DictionariesTypeName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 数据来源，0：急诊添加，1：预检分诊同步
        /// </summary>
        public int DataFrom { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

    }
}
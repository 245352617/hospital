namespace YiJian.ECIS.ShareModel.Etos.NurseExecutes
{
    /// <summary>
    /// 描述：同步执行记录数据到护理记录单Eto
    /// 创建人： yangkai
    /// 创建时间：2023/3/22 14:00:39
    /// </summary>
    public class RecipeExecEto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public Guid PiId { get; set; }

        /// <summary>
        /// 执行单Id
        /// </summary>
        public Guid RecipeExecId { get; set; }

        /// <summary>
        /// 医嘱名称
        /// </summary>
        public string RecipeName { get; set; } = string.Empty;

        /// <summary>
        /// 医嘱类型编码
        /// </summary>
        public string CategoryCode { get; set; } = string.Empty;

        /// <summary>
        /// 用法途径编码
        /// </summary>
        public string UsageCode { get; set; } = string.Empty;

        /// <summary>
        /// 用法途径
        /// </summary>
        public string UsageName { get; set; } = string.Empty;

        /// <summary>
        /// 执行量
        /// </summary>
        public string ExecDosage { get; set; } = string.Empty;

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// 操作人编码
        /// </summary>
        public string OperateCode { get; set; } = string.Empty;

        /// <summary>
        /// 操作人名称
        /// </summary>
        public string OperateName { get; set; } = string.Empty;

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime { get; set; }

        /// <summary>
        /// 签名图片
        /// </summary>
        public string Signature { get; set; } = string.Empty;
    }
}

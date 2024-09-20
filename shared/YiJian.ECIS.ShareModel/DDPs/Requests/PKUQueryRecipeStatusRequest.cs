namespace YiJian.ECIS.ShareModel.DDPs.Requests
{
    /// <summary>
    /// 描    述:查询his医嘱状态参数
    /// 创 建 人:杨凯
    /// 创建时间:2023/11/11 16:25:51
    /// </summary>
    public class PKUQueryRecipeStatusRequest
    {
        /// <summary>
        /// 医嘱类型
        /// </summary>
        public int RecipeType { get; set; }

        /// <summary>
        /// his单号
        /// </summary>
        public string HisOrderNo { get; set; }
    }
}

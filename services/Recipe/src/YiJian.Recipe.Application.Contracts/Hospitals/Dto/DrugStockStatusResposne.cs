namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 药品状态返回
    /// </summary>
    public class DrugStockStatusResposne
    {
        /// <summary>
        /// 药品是否有库存（指定的药品+库存标记唯一）
        /// </summary>
        public bool Exists { get; set; }

        /// <summary>
        /// 查询值
        /// <![CDATA[
        /// queryType非0时需要结合storage使用
        /// 1:药品名称
        /// 2:药品编码
        /// ]]>
        /// </summary>
        public string QueryCode { get; set; }

        /// <summary>
        /// 药房编号 药房唯一编号2.1药房编码码字典（字典、写死）0.查询所有药房 
        /// </summary>
        public int Storage { get; set; }

        /// <summary>
        /// 药房名称 
        /// </summary>
        public string StorageName { get; set; }
    }



}


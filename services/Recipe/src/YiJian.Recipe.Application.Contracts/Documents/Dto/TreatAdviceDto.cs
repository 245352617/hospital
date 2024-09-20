namespace YiJian.Documents.Dto
{
    /// <summary>
    /// 诊疗
    /// </summary>
    public class TreatAdviceDto : BaseAdviceDto
    {
        /// <summary>
        /// 其它价格
        /// </summary> 
        public decimal? OtherPrice { get; set; }

        /// <summary>
        /// 默认频次代码
        /// </summary> 
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 频次
        /// </summary> 
        public string FrequencyName { get; set; }

        /// <summary>
        /// 收费大类代码
        /// </summary> 
        public string FeeTypeMainCode { get; set; }

        /// <summary>
        /// 收费小类代码
        /// </summary>  
        public string FeeTypeSubCode { get; set; }

        /// <summary>
        /// 包装规格
        /// </summary> 
        public string Specification { get; set; }

        /// <summary>
        /// 加收标志	
        /// </summary>
        public bool Additional { get; set; }
    }

}

namespace YiJian.BodyParts.Application.Contracts.Dtos.Dictionary
{

    public class IcuSysParaDictDto
    {
        public string SourceName { get; set; }
        
        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; }
        
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
    }
}
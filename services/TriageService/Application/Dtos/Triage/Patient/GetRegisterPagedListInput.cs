namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 挂号列表分页查询条件
    /// </summary>
    public class GetRegisterPagedListInput
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; } = 15;

        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 模糊检索输入文本
        /// </summary>
        public string SearchText { get; set; }
    }
}

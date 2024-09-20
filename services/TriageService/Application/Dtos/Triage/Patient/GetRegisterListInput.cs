namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 挂号列表查询条件
    /// </summary>
    public class GetRegisterListInput
    {
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

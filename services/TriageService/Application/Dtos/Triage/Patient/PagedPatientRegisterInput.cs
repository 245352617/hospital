using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分页查询患者基本信息及挂号信息输入项
    /// </summary>
    public class PagedPatientRegisterInput
    {
        /// <summary>
        /// 当前页数
        /// </summary>
        /// <example>1</example>
        [Required]
        public int SkipCount { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        /// <example>50</example>
        [Required]
        public int MaxResultCount { get; set; }

        /// <summary>
        /// 挂号医生编码
        /// </summary>
        public string DoctorCode { get; set; }
        
        /// <summary>
        /// 挂号科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 模糊检索输入文本
        /// </summary>
        public string SearchText { get; set; }
    }
}
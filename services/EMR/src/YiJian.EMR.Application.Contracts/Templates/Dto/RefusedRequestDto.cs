using System.Security.Cryptography;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Templates.Dto
{
    /// <summary>
    /// 返回模型
    /// </summary>
    public class RefusedResponseDto
    {
        /// <summary>
        /// 模板类型，0=通用，1=科室，2=个人
        /// </summary>
        public ETemplateType TemplateType { get; set; }

        /// <summary>
        /// 模板类型全称
        /// </summary>
        public string TemplateTypeName { get { return TemplateType.ToString().ToLower(); } }

        /// <summary>
        /// true=拒绝，false=允许
        /// </summary>
        public bool Refused { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }
    }

    public class RefusedRequestDto
    {
        /// <summary>
        /// 医生编码
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 0=电子病历，1=文书
        /// </summary>
        public EClassify Classify { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }
    }

    /// <summary>
    /// 模块权限
    /// </summary>
    public class RefusedResultDto
    {
        /// <summary>
        /// 模块
        /// </summary>
        public string TemplateType { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public string PermissionName { get; set; }
    }
}
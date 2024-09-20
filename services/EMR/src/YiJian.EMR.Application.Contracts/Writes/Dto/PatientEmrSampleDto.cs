using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Writes.Dto
{
    /// <summary>
    /// 患者电子病历归档信息
    /// </summary>
    public class PatientEmrSampleDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 病历名称
        /// </summary>  
        [Required(ErrorMessage = "病历名称不能为空"), StringLength(200, ErrorMessage = "病历名称最大长度100字符")]
        public string Title { get; set; }

        /// <summary>
        /// 患者编号
        /// </summary> 
        [Required(ErrorMessage = "患者编号不能为空"), StringLength(32, ErrorMessage = "患者编号最大长度32字符")]
        public string PatientNo { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>  
        [Required(ErrorMessage = "患者名称不能为空"), StringLength(40, ErrorMessage = "患者名称最大长度20字符")]
        public string PatientName { get; set; }

        /// <summary>
        /// 电子文书分类（0=电子病历，1=文书）
        /// </summary>
        public EClassify Classify { get; set; } = EClassify.EMR;
    }

}

using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace YiJian.Health.Report.Hospitals.Dto
{
    /// <summary>
    /// 护理单配置--医院基础信息
    /// </summary>
    public class HospitalInfoDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 医院的名称
        /// </summary> 
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 医院徽标（用base64字符存储）
        /// </summary> 
        [StringLength(4000)]
        public string Logo { get; set; }
    }

    /// <summary>
    /// 修改医院基础信息
    /// </summary>
    public class ModifyHospitalDto : HospitalInfoDto
    {
        /// <summary>
        /// 医院评级(级别，如：三级甲等)
        /// </summary> 
        [StringLength(50)]
        public string HospitalLevel { get; set; }

        /// <summary>
        /// 医院的地址
        /// </summary> 
        [StringLength(200)]
        public string Address { get; set; }
    }

}

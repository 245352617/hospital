using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.EmrPermissions.Dto
{
    /// <summary>
    /// 授权人
    /// </summary>
    public class OperatingAccountDto:EntityDto<int?>
    {
        /// <summary>
        /// 医生编码
        /// </summary>  
        [StringLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 医生
        /// </summary>  
        [StringLength(50)] 
        public string Name { get; set; } 

    } 

}

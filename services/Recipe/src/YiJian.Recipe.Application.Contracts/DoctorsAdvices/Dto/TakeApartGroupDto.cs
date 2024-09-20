using System;
using System.Collections.Generic;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 拆组模型
    /// </summary>
    public class TakeApartGroupDto
    {
        /// <summary>
        /// 需要拆组的药品
        /// </summary>
        public List<Guid> Ids { get; set; }

        /// <summary>
        /// 需要验证的患者信息，记住考虑三无人员，如果是三无人员需要传年龄
        /// </summary>
        public PatientInfoDto Patientinfo { get; set; }
    }

}


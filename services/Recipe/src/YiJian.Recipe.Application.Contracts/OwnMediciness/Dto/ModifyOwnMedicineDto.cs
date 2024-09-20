using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.OwnMediciness.Dto
{
    /// <summary>
    /// 自备药
    /// </summary>
    public class ModifyOwnMedicineDto
    {
        /// <summary>
        /// 系统标识: 0=急诊，1=院前
        /// </summary> 
        public EPlatformType PlatformType { get; set; }

        /// <summary>
        /// 患者唯一标识
        /// </summary> 
        public Guid PIID { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary> 
        [StringLength(20)]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary> 
        [StringLength(30)]
        public string PatientName { get; set; }

        /// <summary>
        /// 自备药列表
        /// </summary>
        public List<OwnMedicineDto> OwnMedicineDtos { get; set; }
    }
}

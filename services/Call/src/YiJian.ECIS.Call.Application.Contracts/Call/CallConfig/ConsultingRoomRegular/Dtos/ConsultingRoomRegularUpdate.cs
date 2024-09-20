using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.ECIS.Call.CallConfig.Dtos
{
    /// <summary>
    /// 【诊室固定】修改 DTO
    /// direction：input
    /// </summary>
    [Serializable]
    public class ConsultingRoomRegularUpdate
    {
        /// <summary>
        /// 急诊诊室id
        /// </summary>
        [Required(ErrorMessage = "急诊诊室id必填")]
        public Guid? ConsultingRoomId { get; set; }

        /// <summary>
        /// 急诊科室id
        /// </summary>
        [Required(ErrorMessage = "急诊科室id必填")]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsActived { get; set; }
    }
}

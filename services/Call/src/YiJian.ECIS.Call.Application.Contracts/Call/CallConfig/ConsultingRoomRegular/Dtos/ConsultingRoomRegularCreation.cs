using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.ECIS.Call.CallConfig.Dtos
{
    /// <summary>
    /// 【诊室固定】创建 DTO
    /// direction：input
    /// </summary>
    [Serializable]
    public class ConsultingRoomRegularCreation
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
        /// 急诊诊室
        /// </summary>
        public string ConsultingRoomName { get; set; }

        /// <summary>
        /// 急诊诊室编码
        /// </summary>
        public string ConsultingRoomCode { get; set; }

        /// <summary>
        /// 急诊诊室是否使用
        /// </summary>
        public bool ConsultingRoomIsActived { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室是否使用
        /// </summary>
        public bool DeptIsActived { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsActived { get; set; }
    }
}

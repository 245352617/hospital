using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.ECIS.Call.CallConfig.Dtos
{
    /// <summary>
    /// 【诊室固定】查询 DTO
    /// direction：output
    /// </summary>
    [Serializable]
    public class ConsultingRoomRegularData
    {
        /// <summary>
        /// 【诊室固定】记录id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 急诊诊室id
        /// </summary>
        public Guid ConsultingRoomId { get; set; }

        /// <summary>
        /// 急诊诊室代码
        /// </summary>
        public string ConsultingRoomCode { get; set; }

        /// <summary>
        /// 急诊诊室名称
        /// </summary>
        public string ConsultingRoomName { get; set; }

        /// <summary>
        /// 急诊科室id
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 急诊科室代码
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 急诊科室名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 急诊诊室ip
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsActived { get; set; }
    }
}

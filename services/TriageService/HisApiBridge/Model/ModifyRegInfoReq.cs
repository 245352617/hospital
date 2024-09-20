using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 修改挂号信息入参
    /// </summary>
    public class ModifyRegInfoReq
    {
        /// <summary>
        /// 就诊号
        /// </summary>
        public string regSerialNo { get; set; }

        /// <summary>
        /// 挂号流水号
        /// </summary>
        public string visitNo { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string deptName { get; set; }

        /// <summary>
        /// 诊室编码
        /// </summary>
        public string roomCode { get; set; }

        /// <summary>
        /// 诊室名称
        /// </summary>
        public string roomName { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary>
        public string doctorCode { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string doctorName { get; set; }
    }
}

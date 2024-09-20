using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace YiJian.EMR.Writes.Dto
{
    /// <summary>
    /// 打印合并电子病历
    /// </summary>
    public class PrintMergeEmrDto
    { 
        /// <summary>
        /// 病历名称
        /// </summary> 
        public string EmrTitle { get; set; }
         
        /// <summary>
        /// 患者名称
        /// </summary> 
        public string PatientName { get; set; }

        /// <summary>
        /// 患者唯一id
        /// </summary>
        public Guid Piid { get; set; }

        /// <summary>
        /// 电子病历源路径
        /// </summary>
        public Guid OriginId { get; set; }

        /// <summary>
        /// 合并的PDF路径
        /// </summary>
        public string Pdf { get; set; } 

    }

}

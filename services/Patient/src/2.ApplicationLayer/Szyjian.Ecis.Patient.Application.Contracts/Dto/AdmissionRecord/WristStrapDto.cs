using System.Collections.Generic;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 描    述 ：腕带打印用
    /// 创 建 人 ：杨凯
    /// 创建时间 ：2023/8/24 15:49:32
    /// </summary>
    public class WristStrapDto
    {
        /// <summary>
        /// 患者信息
        /// </summary>
        public List<AdmissionRecordDto> AdmissionRecords { get; set; }
    }
}

using System.Collections.Generic;
using YiJian.Documents.Dto;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 医嘱单打印Dto
    /// </summary>
    public class RecipesPrintDto
    {
        /// <summary>
        /// 患者记录信息
        /// </summary>
        public List<AdmissionRecordDto> AdmissionRecords { get; set; } = new List<AdmissionRecordDto>();

        /// <summary>
        /// 医嘱
        /// </summary>
        public List<DoctorsAdvicesDto> DoctorsAdvicesDtos { get; set; } = new List<DoctorsAdvicesDto> { };
    }
}

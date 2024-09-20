using System.Collections.Generic;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 添加整个套装的医嘱记录，包括药方，检查，检验，诊疗
    /// </summary>
    public class AddFullAdviceDto
    {
        /// <summary>
        /// 添加医嘱开方
        /// </summary>
        public List<AddPrescribeDto> AddPrescribeses { get; set; } = new List<AddPrescribeDto>();

        /// <summary>
        /// 添加医嘱检查
        /// </summary>
        public List<AddPrePacsDto> AddPacsListes { get; set; } = new List<AddPrePacsDto>();

        /// <summary>
        /// 添加医嘱检验
        /// </summary>
        public List<AddPreLisDto> AddLises { get; set; } = new List<AddPreLisDto>();

        /// <summary>
        /// 添加医嘱诊疗
        /// </summary>
        public List<AddTreatDto> AddTreats { get; set; } = new List<AddTreatDto>();

    }

}

using System.Collections.Generic;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 新增一组医嘱药品
    /// </summary>
    public class AddGroupPrescribesDto
    {
        /// <summary>
        /// 一组医嘱
        /// </summary>
        public List<AddPrescribeDto> PrescribeItems { get; set; }
    }

}

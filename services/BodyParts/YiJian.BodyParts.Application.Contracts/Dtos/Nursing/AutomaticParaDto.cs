using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 自动计算参数Dto
    /// </summary>
    public class AutomaticParaDto
    {
        public string ParaCode { get; set; }

        public string NuringViewCode { get; set; }

        public string ParaName { get; set; }

        public string ParaValue { get; set; }

        public DateTime NurseTime { get; set; }
    }

    public class DoctorScoreRequestDto
    {
        public string ModuleCode { get; set; }

        public string ModuleName { get; set; }

        public string ParaCode { get; set; }

        public string ParaName { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Application.Contracts.Dtos.CanulaSkin {
    public class CanulaModuleDto {
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public int SortNum { get; set; }
        public bool IsEnable { get; set; }
        public bool IsDeleted { get; set; }

    }
}

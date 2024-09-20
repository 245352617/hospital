using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    public class IcuSysParaDto : EntityDto<Guid>
    {
        /// <summary>
        /// 参数类型(S-系统参数，D-科室参数)
        /// </summary>
        public string ParaType { get; set; }

        /// <summary>
        /// 科室代码(系统：0)
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        public string ParaCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string ParaName { get; set; }

        /// <summary>
        /// 选项
        /// </summary>
        public string ParaValue { get; set; }

        /// <summary>
        /// 选项列表
        /// </summary>
        public List<object> ParaValueList { get; set; }
    }
}

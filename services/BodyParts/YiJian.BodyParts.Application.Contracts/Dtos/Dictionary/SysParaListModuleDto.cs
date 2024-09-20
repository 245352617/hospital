using System.Collections.Generic;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Dictionary
{

    public class SysParaListModuleDto
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }
     
        /// <summary>
        /// 
        /// </summary>
        public List<SysParaItemDto> Items { get; set; }
    }
}
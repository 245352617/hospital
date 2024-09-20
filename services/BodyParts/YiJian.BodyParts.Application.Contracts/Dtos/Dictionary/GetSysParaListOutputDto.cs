using System.Collections.Generic;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Dictionary
{

    public class GetSysParaListOutputDto
    {
        /// <summary>
        /// 科室编号
        /// </summary>
        public string DeptCode { get; set; }
        
        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        public List<SysParaListModuleDto> Paras { get; set; } = new List<SysParaListModuleDto>();

        /// <summary>
        /// 字典
        /// </summary>
        public Dictionary<string, List<IcuSysParaDictDto>> DictSource { get; set; } = new Dictionary<string, List<IcuSysParaDictDto>>();
    }
}
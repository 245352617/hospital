using System.Collections.Generic;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Dictionary
{
    public class SaveConfigInputDto
    {
        /// <summary>
        /// 参数代码 
        /// </summary> 
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数类型 S 系统参数  D科室参数
        /// </summary>
        public string ParaType { get; set; }

        /// <summary>
        /// 配置分类 1：系统配置  2：特护单配置
        /// </summary>
        public SysTypeEnum Type { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 常规格式-结果
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 表格样式-行号，对应查询配置集合接口的Value.Row
        /// </summary>
        public string TbRow { get; set; }

        /// <summary>
        /// 表格样式-操作类型  ADD：新增  UPD：修改  DEL：删除
        /// </summary>
        public string TbRowStatus { get; set; }

        /// <summary>
        /// 表格样式-一行的结果
        /// </summary>
        public Dictionary<string,string>? TbRowValue { get; set; }
    }
}

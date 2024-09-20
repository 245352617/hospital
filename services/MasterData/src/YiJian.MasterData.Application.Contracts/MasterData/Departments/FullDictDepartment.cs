using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EventBus;

namespace YiJian.MasterData.MasterData.Departments;

[EventName("EICSDeptEvents")]
public class FullDictDepartment
{
    /// <summary>
    /// 数据
    /// </summary>
    public List<Dictionary<string, Object>> DicDatas { get; set; }

    /// <summary>
    /// 字典类型   字典类型:1-检验;2-检查;3-科室;4-员工;5-费别;6-诊断;7-组套指引;8-诊疗材料;9-手术;10-药品用法;11-药品频次;12-药品信息
    /// </summary>
    public int DicType { get; set; }
}

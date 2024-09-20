using System;
using System.Collections.Generic;
using Volo.Abp.EventBus;

namespace YiJian.MasterData.MasterData.HospitalClient;

/// <summary>
/// 订阅模型
/// </summary>
[EventName("FullDicEvents")]
public class FullHandlerEto
{
    /// <summary>
    /// 数据
    /// </summary>
    public List<Dictionary<string, Object>> DicDatas { get; set; }

    /// <summary>
    /// 字典类型:1-检验;2-检查;3-科室;4-员工;5-费别;6-诊断;7-组套指引;8-诊疗材料;9-手术;10-药品用法;11-药品频次;12-药品信息;13-检验标本;14-检查分类;21-检验单信息;22-检查附带药品
    /// </summary>
    public int DicType { get; set; }
}
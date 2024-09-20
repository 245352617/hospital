using System;
using System.Collections.Generic;

namespace YiJian.MasterData.AllItems;


/// <summary>
/// 诊疗检查检验药品项目合集 读取输出
/// </summary>
[Serializable]
public class AllItemData
{        
    public int Id { get; set; }
    
    /// <summary>
    /// 分类编码
    /// </summary>
    public string  CategoryCode { get; set; }
    
    /// <summary>
    /// 分类名称
    /// </summary>
    public string  CategoryName { get; set; }
    
    /// <summary>
    /// 编码
    /// </summary>
    public string  Code { get; set; }
    
    /// <summary>
    /// 名称
    /// </summary>
    public string  Name { get; set; }
    
    /// <summary>
    /// 单位
    /// </summary>
    public string  Unit { get; set; }
    
    /// <summary>
    /// 价格
    /// </summary>
    public decimal  Price { get; set; }
    
    /// <summary>
    /// 排序
    /// </summary>
    public int  IndexNo { get; set; }
    
    /// <summary>
    /// 类型编码
    /// </summary>
    public string  TypeCode { get; set; }
    
    /// <summary>
    /// 类型名称
    /// </summary>
    public string  TypeName { get; set; }
    
    /// <summary>
    /// 收费分类编码
    /// </summary>
    public string ChargeCode { get; set; }

    /// <summary>
    /// 收费分类名称
    /// </summary>
    public string ChargeName { get; set; }
    
    /// <summary>
    /// 拓展字段，这个字段AdditionalRemark有内容时，弹窗提示
    /// </summary>
    public Dictionary<string, object> ExtraProperties { get; set; }
}
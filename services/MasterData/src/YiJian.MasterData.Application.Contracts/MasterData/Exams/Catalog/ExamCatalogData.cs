using System;
using System.Collections.Generic;
using YiJian.MasterData.Exams;

namespace YiJian.MasterData;

/// <summary>
/// 检查目录 读取输出
/// </summary>
[Serializable]
public class ExamCatalogData
{        
    public int Id { get; set; }


    /// <summary>
    /// 一级目录编码
    /// </summary>
    public string FirstNodeCode { get; set; }

    /// <summary>
    ///一级目录 名称
    /// </summary> 

    public string FirstNodeName { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string  CatalogCode { get; set; }
    
    /// <summary>
    /// 名称
    /// </summary>
    public string  CatalogName { get; set; }
    
    /// <summary>
    /// 显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单
    /// </summary>
    public string  DisplayName { get; set; }
    
    /// <summary>
    /// 科室编码
    /// </summary>
    public string  DeptCode { get; set; }
    
    /// <summary>
    /// 科室名称
    /// </summary>
    public string  DeptName { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int  Sort { get; set; }
    
    /// <summary>
    /// 拼音码
    /// </summary>
    public string  PyCode { get; set; }
    
    /// <summary>
    /// 五笔
    /// </summary>
    public string  WbCode { get; set; }
    
    /// <summary>
    /// 执行机房编码
    /// </summary>
    public string  RoomCode { get; set; }
    
    /// <summary>
    /// 执行机房
    /// </summary>
    public string  RoomName { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool  IsActive { get; set; }

    public List<ExamProjectData> ExamProject { get; set; }

}
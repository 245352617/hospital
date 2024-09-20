using System.ComponentModel;

namespace YiJian.ECIS.ShareModel.HisDto
{
    /// <summary>
    /// 检查项目重复校验 Response
    /// </summary>
    [Serializable]
    public class CheckPacsXmcfResponseDto
    {
        /// <summary>
        /// 返回代码 1成功；-1失败； 90未启用功能
        /// </summary>
        public float Code { get; set; }
        /// <summary>
        ///  接口返回信息 eg:校验成功！
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Mess { get; set; }
        /// <summary>
        /// 返回data
        /// </summary>
        public Data Data { get; set; }
    }

    /// <summary>
    /// 返回的Data
    /// </summary>
    [Serializable]
    public class Data
    {
        /// <summary>
        /// 1：门急诊 2住院
        /// </summary>
        public float mzzy { get; set; }
        /// <summary>
        /// 患者ID/住院流水号 门诊传患者档案的病人ID ，  住院传住院流水号
        /// </summary>
        public float brid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float GZXH { get; set; }
        /// <summary>
        /// 项目列表集合
        /// </summary>
        public List<XmlistItem> xmlist { get; set; }
    }

    /// <summary>
    /// 列表
    /// </summary>
    [Serializable]
    public class XmlistItem
    {
        /// <summary>
        /// 记录唯一值
        /// </summary>
        public string jlwyz { get; set; }
        /// <summary>
        /// 组套序号 ProjectCode
        /// </summary>
        public float ztxh { get; set; }
        /// <summary>
        /// 费用序号  TargetCode
        /// </summary>
        public float fyxh { get; set; }
        /// <summary>
        /// 费用名称 eg: 磁共振平扫(1.5T以上）TargetName
        /// </summary>
        public string fymc { get; set; }
        /// <summary>
        /// 费用数量 totalNum  
        /// </summary>
        public int fysl { get; set; }
        /// <summary>
        /// 费用单价  price 
        /// </summary>
        public decimal fydj { get; set; }
        /// <summary>
        /// 触发规则类型
        /// </summary>
        public string gzlx { get; set; }
        /// <summary>
        /// 规则上限阀值
        /// </summary>
        public float gzsxfz { get; set; }
        /// <summary>
        /// 超过阀值数量
        /// </summary>
        public float cgfzsl { get; set; }
        /// <summary>
        /// 处置方法 eg：允许开单
        /// </summary>
        public string czff { get; set; }
        /// <summary>
        /// 新费用序号
        /// </summary>
        public float newfyxh { get; set; }
        /// <summary>
        /// 新费用名称
        /// </summary>
        public string newfymc { get; set; }
        /// <summary>
        /// 新费用数量
        /// </summary>
        public decimal newfysl { get; set; }
        /// <summary>
        /// 新费用单价
        /// </summary>
        public decimal newfydj { get; set; }
        /// <summary>
        /// 预留字段 （未启用）
        /// </summary>
        public float jssl { get; set; }
        /// <summary>
        /// 预留字段（未启用）
        /// </summary>
        public float jsfyxh { get; set; }
    }

    /// <summary>
    /// 处置方法
    /// </summary>
    public enum CzffEnum
    {
        /// <summary>
        /// 允许开单
        /// </summary>
        [Description("允许开单")]
        Allow = 0,
        /// <summary>
        /// 不允许开单
        /// </summary>
        [Description("不允许开单")]
        NotAllow = 1,
        /// <summary>
        /// 0元开单
        /// </summary>
        [Description("0元开单")]
        ZeroAllow = 2,
        /// <summary>
        /// 项目置换
        /// </summary>
        [Description("项目置换")]
        Replacement = 3
    }

}

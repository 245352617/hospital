namespace YiJian.ECIS.ShareModel.HisDto
{
    /// <summary>
    /// 检查项目重复校验 Response
    /// </summary>
    [Serializable]
    public class CheckLisXmcfResponseDto
    {
        /// <summary>
        /// 返回代码 
        /// -1 当前提交的项目中，存在相同组套; 
        /// -2 当前提交的组套细项有重复，且该细项一天只能开一次。
        /// -3 当前提交的组套细项与该病人当天开单细项存在重复，且该项目限制一天只能开一次。
        /// -4 存在交叉项目，不允许继续开单(组套包含不同方法学检测的相同项目，禁止同时提交申请单。)
        /// </summary>
        public string retCode { get; set; }
        /// <summary>
        ///  接口返回信息 eg:校验成功！
        /// </summary>
        public string retInfo { get; set; }
        /// <summary>
        /// 返回data
        /// </summary>
        public LisData result { get; set; }
    }

    /// <summary>
    /// 返回的Data
    /// </summary>
    [Serializable]
    public class LisData
    {
        /// <summary>
        /// 项目列表集合
        /// </summary>
        public List<LisXmlistItem> Cdata { get; set; }
    }

    /// <summary>
    /// 列表
    /// </summary>
    [Serializable]
    public class LisXmlistItem
    {
        /// <summary>
        /// 重复细项目对应的组套名称  eg 乙肝五项定量
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 重复细项目对应的组套序号 10335
        /// </summary>
        public float code { get; set; }
        /// <summary>
        /// 细项序号   302665
        /// </summary>
        public float xmxh { get; set; }
        /// <summary>
        /// 细项名称 eg: 乙型肝炎表面抗原测定(HBsAg:化学发光法)/项 ProjectName
        /// </summary>
        public string xmmc { get; set; }


        /// <summary>
        /// 参数识别，分组
        /// </summary>
        public int Fgroup { get; set; }
        /// <summary>
        /// 开单科室
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        ///细项序号
        /// </summary>
        public float fyxh { get; set; }
        /// <summary>
        /// 细项名称
        /// </summary>
        public string fymc { get; set; }
        /// <summary>
        /// 细项物价编码
        /// </summary>
        public string wjxmbm { get; set; }
        /// <summary>
        /// 推荐方案序号
        /// </summary>
        public string tjxh { get; set; }
        /// <summary>
        /// 推荐方案
        /// </summary>
        public string tjfa { get; set; }

        /// <summary>
        /// 重复开单时间
        /// </summary>
        public string KDRQ { get; set; }
        /// <summary>
        /// 申请单号
        /// </summary>
        public float yjxh { get; set; }

        /// <summary>
        /// 开单医生
        /// </summary>
        public string ygxm { get; set; }
        /// <summary>
        /// 重复细项序号
        /// </summary>
        public float ylxh { get; set; }
        /// <summary>
        ///重复细项名称
        /// </summary>
        public string ylmc { get; set; }
    }



}

using YiJian.ECIS.ShareModel.Models;

namespace YiJian.ECIS.Call.CallCenter.Dtos
{
    public class GetCallInfoInput : PageBase
    {
        /// <summary>
        /// 分诊科室编码
        /// </summary>
        public string TriageDept { get; set; }

        /// <summary>
        /// 分诊级别
        /// </summary>
        public string ActTriageLevel { get; set; }

        /// <summary> 
        /// 叫号状态（0: 未叫号; 1: 叫号中; 2: 暂停中  3: 已叫号 已就诊 4: 已经过号 5：作废，失效 ） 
        /// </summary>
        public CallStatus CallingStatus { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Filter { get; set; }
    }


    public class GetCallInfoInput2 
    {
        /// <summary>
        /// 分诊科室编码
        /// </summary>
        public string TriageDept { get; set; }

        /// <summary>
        /// 分诊级别
        /// </summary>
        public string ActTriageLevel { get; set; }

        /// <summary> 
        /// 叫号状态（0: 未叫号; 1: 叫号中; 2: 暂停中  3: 已叫号 已就诊 4: 已经过号 5：作废，失效 ） 
        /// </summary>
        public CallStatus CallingStatus { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Filter { get; set; }
    }
}

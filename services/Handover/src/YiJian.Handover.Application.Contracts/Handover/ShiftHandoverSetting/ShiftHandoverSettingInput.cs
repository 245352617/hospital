using YiJian.ECIS.ShareModel.Models;

namespace YiJian.Handover
{
    public class ShiftHandoverSettingInput : PageBase
    {
        /// <summary>
        /// 类型，1：医生，0：护士
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnable { get; set; } = -1;


    }
}
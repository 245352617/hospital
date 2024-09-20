namespace YiJian.Recipes.InviteDoctor
{
    using System;
    using YiJian.ECIS.ShareModel.Models;

    /// <summary>
    /// 会诊邀请医生 分页排序输入
    /// </summary>
    [Serializable]
    public class GetInviteDoctorPagedInput : PageBase
    {
        /// <summary>
        /// 过滤条件.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 排序字段.
        /// </summary>
        public string Sorting { get; set; }
    }
}

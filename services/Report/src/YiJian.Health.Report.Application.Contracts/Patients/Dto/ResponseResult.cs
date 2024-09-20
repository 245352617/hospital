using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Health.Report.Patients.Dto
{
    /// <summary>
    /// 描述：患者接口返回数据
    /// 创建人： yangkai
    /// 创建时间：2022/11/30 14:40:18
    /// </summary>
    public class ResponseResult<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public EHttpStatusCodeEnum Code
        {
            get;
            set;
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg
        {
            get;
            set;
        }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data
        {
            get;
            set;
        }

        /// <summary>
        /// 额外信息
        /// </summary>
        public dynamic Extra
        {
            get;
            set;
        }
    }
}

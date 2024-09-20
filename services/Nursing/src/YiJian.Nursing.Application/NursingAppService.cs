using Volo.Abp.Application.Services;

namespace YiJian.Nursing
{
    /// <summary>
    /// 护理服务基类
    /// </summary>
    public abstract class NursingAppService : ApplicationService
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        protected NursingAppService()
        {
            ObjectMapperContext = typeof(NursingApplicationModule);
        }
    }
}

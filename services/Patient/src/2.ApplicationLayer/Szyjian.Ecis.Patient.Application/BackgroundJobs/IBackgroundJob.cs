using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Szyjian.Ecis.Patient.BackgroundJob
{
    /// <summary>
    /// 后台任务接口
    /// </summary>
    public interface IBackgroundJob : ITransientDependency
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <returns></returns>
        Task ExecuteAsync();
    }
}
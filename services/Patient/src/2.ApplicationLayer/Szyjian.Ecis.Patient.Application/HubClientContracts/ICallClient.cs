using System.Threading.Tasks;

namespace Szyjian.Ecis.Patient
{
    /// <summary>
    /// 叫号中心强类型集线器接口
    /// </summary>
    public interface ICallClient
    {
        /// <summary>
        /// 刷新叫号列表
        /// </summary>
        /// <returns></returns>
        Task CallingQueueChanged();
    }
}

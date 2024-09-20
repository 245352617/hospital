using System.Threading;
using System.Threading.Tasks;

namespace YiJian.ECIS.Core.Internals;

/// <summary>
/// RPC 任务帮助类
/// </summary>
public static class TaskHelper
{
    /// <summary>
    /// 等待任务
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task WaitAsync(this CancellationToken cancellationToken)
    {
        TaskCompletionSource<bool> cancelationTaskCompletionSource = new TaskCompletionSource<bool>();
        cancellationToken.Register(CancellationTokenCallback, cancelationTaskCompletionSource);

        return cancellationToken.IsCancellationRequested ? Task.CompletedTask : cancelationTaskCompletionSource.Task;
    }

    private static void CancellationTokenCallback(object taskCompletionSource)
    {
        ((TaskCompletionSource<bool>)taskCompletionSource).SetResult(true);
    }
}

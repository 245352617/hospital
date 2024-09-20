using System.Diagnostics;

namespace TriageService.Extensions
{
    public static class StopwatchExtension
    {
        /// <summary>
        /// 通知stopwatch停止并返回经过的毫秒数，然后重新启动stopwatch
        /// </summary>
        /// <param name="stopwatch"></param>
        /// <returns></returns>
        public static long StopThenGetElapsedMillisecondsThenRestart(this Stopwatch stopwatch)
        {
            stopwatch.Stop();
            long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();
            return elapsedMilliseconds;
        }

        /// <summary>
        /// 通知stopwatch停止并返回经过的毫秒数
        /// </summary>
        /// <param name="stopwatch"></param>
        /// <returns></returns>
        public static long StopThenGetElapsedMilliseconds(this Stopwatch stopwatch)
        {
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}

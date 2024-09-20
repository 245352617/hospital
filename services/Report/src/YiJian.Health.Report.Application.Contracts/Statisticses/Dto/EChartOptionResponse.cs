using System.Collections.Generic;

namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// EChart 关键数据结构
    /// </summary>
    public class EChartOptionResponse
    {
        /// <summary>
        /// EChart 关键数据结构
        /// </summary>
        /// <param name="xAxis"></param>
        /// <param name="series"></param>
        public EChartOptionResponse(List<string> xAxis, List<SeriesResponse> series)
        {
            XAxis = xAxis;
            Series = series;
        }

        /// <summary>
        /// X轴坐标标记，比如 2021-01 2021-02 ...
        /// </summary>
        public List<string> XAxis { get; set; }

        /// <summary>
        /// 系列数据
        /// </summary>
        public List<SeriesResponse> Series { get; set; }

    }

}

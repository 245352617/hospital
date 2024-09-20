using System.Collections.Generic;

namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 系列数据
    /// </summary>
    public class SeriesResponse
    {
        /// <summary>
        /// 系列数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <param name="stack"></param>
        public SeriesResponse(string name, List<dynamic> data, string stack = null)
        {
            Name = name;
            Data = data;
            Stack = stack ?? string.Empty;
        }

        /// <summary>
        /// 数据描述
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 具体数据
        /// </summary>
        public List<dynamic> Data { get; set; }

        /// <summary>
        /// 堆叠名称，如果不是空字符串，那就是堆叠
        /// </summary>
        public string Stack { get; set; } = string.Empty;

    }

}

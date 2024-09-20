using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 配置
/// </summary>
namespace YiJian.CardReader.Settings
{
    public class Setting
    {
        /// <summary>
        /// 读卡器厂家
        /// </summary>
        public string? Vendor { get; set; }
        /// <summary>
        /// WebSocket的监听地址
        /// </summary>
        public string? WebSocketIp { get; set; }
        /// <summary>
        /// WebSocket的监听端口
        /// </summary>
        public string? WebSocketPort { get; set; }
    }
}

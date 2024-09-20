using System.Collections.Generic;

namespace YiJian.ECIS.Core
{
    /// <summary>
    /// DDP请求
    /// </summary>
    public class DDPModel
    {
        public string path { get; set; }
        public Dictionary<string, object> req { get; set; } = new Dictionary<string, object>();

    }
}

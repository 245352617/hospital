using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tray.Install.Models
{
    public class UpdateVersionInfo
    {
        /// <summary>
        /// 版本 eg:v2.2.6.0
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 更新内容备注
        /// </summary>
        public string Remarks { get;set; }

        /// <summary>
        /// 发现版本时间
        /// </summary>
        public DateTime ReleaseTime { get; set; }

        /// <summary>
        /// 需要更新的文件，压缩成Zip包
        /// </summary>
        public string UpdateFile { get; set; }

    }
}

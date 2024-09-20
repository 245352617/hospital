using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Tray.Install.Utils
{
    /// <summary>
    /// httpclient 工具
    /// </summary>
    public static class HttpClientUtils
    {

        /// <summary>
        /// Get获取文件内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url)
        {
            try
            { 
                using var client = new HttpClient();
                var content = await client.GetStringAsync(url);
                return content;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            } 
        }

        /// <summary>
        /// 下载待更新的更新包到临时文件目录
        /// </summary>
        /// <param name="url"></param>
        /// <param name="saveAsFilename"></param>
        /// <returns></returns>
        public static async Task<string> DownloadAsync(string url,string saveAsFilename)
        {
            try
            {
                var download = Path.Combine(Path.GetTempPath(), "download"); 
                if (!Directory.Exists(download)) Directory.CreateDirectory(download); 
                var file = Path.Combine(download, saveAsFilename);
                using var client = new HttpClient();
                byte[] fileBytes = await client.GetByteArrayAsync(url);
                File.WriteAllBytes(file, fileBytes);
                return file;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

    }
}

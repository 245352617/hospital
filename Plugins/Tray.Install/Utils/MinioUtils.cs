using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minio;
using Tray.Install.Models;

namespace Tray.Install.Utils;

/// <summary>
/// 对象存储工具
/// </summary>
public static class MinioUtils
{   
    /// <summary>
    /// 获取预览地址
    /// </summary>
    /// <param name="setting">配置</param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static async Task<string> GetUrlAsync(MinioSetting setting,string fileName)
    {
        try
        {
            var minio = new MinioClient()
                .WithEndpoint(setting.Endpoint)
                .WithCredentials(setting.AccessKey, setting.SecretKey)
                .Build();
            var objectName = fileName; 
            var presigned = new PresignedGetObjectArgs()
                .WithBucket(setting.Bucket)
                .WithObject(objectName)
                .WithExpiry(1200);
            return await minio.PresignedGetObjectAsync(presigned);  
        }
        catch (Exception e)
        {
            Console.WriteLine($"获取URL地址错误{e.Message}");
            return "";
        }
    }


}

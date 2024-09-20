using BeetleX.Http.Clients;
using DCSoft.Writer.Dom;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Models;
using YiJian.EMR.DCWriter.Models;

namespace YiJian.EMR.DCWriter.Services;

/// <summary>
/// 电子病历，护理文书归档服务
/// </summary>
public class EmrArchiveService: IEmrArchiveService
{
    private readonly IOptionsMonitor<MinioSetting> _minio;
    private readonly IOptionsMonitor<RemoteServices> _remoteServices;
    private readonly ILogger<EmrArchiveService> _logger;

    public EmrArchiveService(
        IOptionsMonitor<MinioSetting> minio,
         IOptionsMonitor<RemoteServices> remoteServices,
         ILogger<EmrArchiveService> logger)
    {
        _minio = minio;
        _remoteServices = remoteServices;
        _logger = logger;
    }
    
    public void SavePDF()
    {
        var data = GetPatientEmrs().Result;
        Console.WriteLine($"我是测试调度SavePDF{DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss")}");
    }


    public async Task<List<PatientEmrSampleDto>> GetPatientEmrs()
    {
        var remoteServices = _remoteServices.CurrentValue; 
        HttpCluster httpCluster = new HttpCluster();
        httpCluster.TimeOut = 10 * 60 * 1000;
        httpCluster.DefaultNode.Add(remoteServices.Emr);
        var service = httpCluster.Create<EmrRemoteService>();
        var data = await service.GetPatientEmrsAsync();

        foreach (var item in data)
        {
            try
            {
                var xml = await service.ArchiveAsync(item.Id);
                SavePDF(item, xml);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"归档异常，回滚归档操作"); 
                await service.RollbackArchiveAsync(item.Id);
            }
        }

        return data;
    }

    /// <summary>
    /// 保存PDF文件
    /// </summary>
    /// <param name="item"></param>
    /// <param name="xml"></param>
    private void SavePDF(PatientEmrSampleDto item, string xml)
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pdf");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        var saveFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pdf", $"{item.Id}.pdf");
        XTextDocument doc = new XTextDocument();//获去到前端的文档
        doc.LoadFromString(xml, "xml"); //doc加载XML文档
        doc.SaveToFile(saveFile, "pdf");

        PutMinioAsync(item, saveFile).GetAwaiter(); 
    }

    /// <summary>
    /// 推送到Minion服务器示例
    /// </summary>
    /// <returns></returns> 
    public async Task PutMinioAsync(PatientEmrSampleDto item,string file)
    {
        var setting = _minio.CurrentValue;
        try
        {
            var minio = new MinioClient()
                .WithEndpoint(setting.Endpoint)
                .WithCredentials(setting.AccessKey, setting.SecretKey)
                .Build();

            var objectName = $"{item.Id}.pdf"; 
            var contentType = "	application/pdf"; 
            var bucketExists = new BucketExistsArgs()
                .WithBucket(setting.Bucket.Emr);
            bool found = await minio.BucketExistsAsync(bucketExists);
            var bucket = item.Classify == 1? setting.Bucket.Writer: setting.Bucket.Emr; 
            var make = new MakeBucketArgs()
                .WithBucket(bucket)
                .WithLocation("us-east-1");
            if (!found) await minio.MakeBucketAsync(make);

            var putObject = new PutObjectArgs()
                .WithBucket(bucket)
                .WithObject(objectName)
                .WithFileName(file)
                .WithContentType(contentType);
            await minio.PutObjectAsync(putObject);

            _logger.LogInformation($"成功上传了{file}文件 " + objectName); 
        }
        catch (Exception e)
        {
            Console.WriteLine($"文件上传异常 :{e.Message}");
            throw;
        }

    }

}


/// <summary>
/// 调用字典服务
/// </summary>
public class MasterRemoteervice : IMasterRemoteervice
{ 
    private readonly IOptionsMonitor<RemoteServices> _remoteServices;
    private readonly ILogger<MasterRemoteervice> _logger;

    public MasterRemoteervice( 
         IOptionsMonitor<RemoteServices> remoteServices,
         ILogger<MasterRemoteervice> logger)
    { 
        _remoteServices = remoteServices;
        _logger = logger;
    }
     

    /// <summary>
    /// 获取电子病历水印配置信息
    /// </summary>
    /// <returns></returns>
    public async Task<EmrWatermarkDto> GetEmrWatermarkingAsync()
    {
        var remoteServices = _remoteServices.CurrentValue;
        HttpCluster httpCluster = new HttpCluster();
        httpCluster.TimeOut = 10 * 60 * 1000;
        httpCluster.DefaultNode.Add(remoteServices.MasterData);
        var service = httpCluster.Create<MasterRemoteService>();
        var ret  = await service.GetEmrmarkingAsync();  
        return ret.Data;
    }
      
}

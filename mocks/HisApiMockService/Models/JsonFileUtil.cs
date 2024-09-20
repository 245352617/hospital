using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HisApiMockService.Models;

/// <summary>
/// 处理JSON文件工具
/// </summary>
public class JsonFileUtil
{
    private readonly string _path;

    public JsonFileUtil()
    {
        _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonDatas");
    }

    /// <summary>
    /// 读取JSON文件的内容并且返回映射的对象
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task<T> ReadAsync<T>(string fileName)
    {
        try
        {
            var file = Path.Combine(_path, fileName);
            using (StreamReader sr = new StreamReader(file))
            {
                var json = await sr.ReadToEndAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return await Task.FromResult(default(T));
        }
    }

    /// <summary>
    /// 写整个文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="context">内容</param>
    /// <returns></returns>
    public async Task<bool> WriteFileAsync(string fileName, string context)
    {
        try
        {
            var datadir = Path.Combine(_path, "data");
            if (!Directory.Exists(datadir)) Directory.CreateDirectory(datadir);
            var file = Path.Combine(datadir, fileName);
            if (File.Exists(file)) File.Delete(file);
            await File.WriteAllTextAsync(file, context);
            return await Task.FromResult(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return await Task.FromResult(false);
        }
    }

    /// <summary>
    /// 最佳数据
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<bool> AppendFileAsync(string fileName, string context)
    { 
        try
        {
            var datadir = Path.Combine(_path, "data");
            if (!Directory.Exists(datadir)) Directory.CreateDirectory(datadir);
            var file = Path.Combine(datadir, fileName); 
            await File.AppendAllTextAsync(file, context);  
            await File.AppendAllLinesAsync(file, new List<string>{""}); 
            return await Task.FromResult(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return await Task.FromResult(false);
        }
    }

}



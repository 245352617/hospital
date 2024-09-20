using System;
using System.Linq;

namespace YiJian.ECIS.Core.Utils;

/// <summary>
/// Redis配置解析工具
/// <![CDATA[
/// 192.168.241.101:6379,password=123456,DefaultDatabase=1
/// 拆分为 host=192.168.241.101  port=6379 password=123456 DefaultDatabase=1
/// ]]>
/// </summary>
public class RedisSetting
{
    public string Host { get; set; }

    public int Port { get; set; }

    public string Password { get; set; }

    public int DefaultDatabase { get; set; }

    /// <summary>
    /// 字符串格式： 192.168.241.101:6379,password=123456,DefaultDatabase=1
    /// </summary>
    /// <param name="conf"></param>
    public RedisSetting(string conf)
    {
        try
        {
            //192.168.241.101:6379,password=123456,DefaultDatabase=1
            var arr = conf.Split(',');
            var domain = arr[0].Split(':');
            Host = domain[0];
            Port = domain.Count() > 1 ? int.Parse(domain[1]) : 6379;
            Password = arr.Count() > 1 ? arr[1].Split('=')[1] : "";
            DefaultDatabase = arr.Count() > 2 ? int.Parse(arr[2].Split('=')[1]) : 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("解析Redis配置异常：" + ex);
            throw;
        }
    }

}

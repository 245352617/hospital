using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tray.Install.Models;

/// <summary>
/// Minio配置
/// </summary>
public class MinioSetting
{
    /// <summary>
    /// Endpoint
    /// </summary>
    public string Endpoint { get; set; }

    /// <summary>
    /// AccessKey
    /// </summary>
    public string AccessKey { get; set; }

    /// <summary>
    /// SecretKey
    /// </summary>
    public string SecretKey { get; set; }

    /// <summary>
    /// Bucket
    /// </summary>
    public string Bucket { get; set; }
}

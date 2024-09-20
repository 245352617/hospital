using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tray.Install.Utils;

/// <summary>
/// 解压缩工具
/// </summary>
public static class ZipUtils
{
    /// <summary>
    /// 解压Zip文件到指定目录
    /// </summary> 
    /// <param name="zip">压缩文件</param>
    /// <param name="dir">解压缩目录</param>
    public static void UnZip(string zip, string dir)
    { 
        DirectoryInfo directoryInfo = new(dir);
        if (!directoryInfo.Exists) directoryInfo.Create(); 
        var parentDir = Directory.GetParent(zip);
        if (parentDir == null) return;
        var tmpDir = Path.Combine(parentDir.FullName, "temp");
        DeleteDir(tmpDir);
        ZipFile.ExtractToDirectory(zip, tmpDir);
        string root = "";
        CopyTo(tmpDir,dir,ref root); 
    }

    /// <summary>
    /// 压缩备份文件
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="zip"></param>
    public static string Zip(string dir, string zip)
    { 
        var templatePath = Path.Combine(Path.GetTempPath(),"traybak");
        if (!Directory.Exists(templatePath)) Directory.CreateDirectory(templatePath);
        var zipFile = Path.Combine(templatePath, zip); 
        if(File.Exists(zipFile)) File.Delete(zipFile);
        ZipFile.CreateFromDirectory(dir,zipFile);
        return zipFile;
    }

    /// <summary>
    /// 删除指定目录的所有文件和文件夹
    /// </summary>
    /// <param name="dir"></param>
    public static void DeleteDir(string dir)
    {
        try
        {
            //去除文件夹和子文件的只读属性
            //去除文件夹的只读属性
            DirectoryInfo fileInfo = new DirectoryInfo(dir);
            fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

            //去除文件的只读属性
            File.SetAttributes(dir, FileAttributes.Normal);

            //判断文件夹是否还存在
            if (Directory.Exists(dir))
            {
                foreach (string f in Directory.GetFileSystemEntries(dir))
                {

                    if (File.Exists(f)) //如果有子文件删除文件
                        File.Delete(f);
                    else//循环递归删除子文件夹 
                        DeleteDir(f); 
                } 
                //删除空文件夹
                Directory.Delete(dir); 
            }

        }
        catch (Exception ex) // 异常处理
        {
            Console.WriteLine(ex.Message.ToString());// 异常信息
        }
    }

    public static void CopyTo(string fromPath, string toPath,ref string dir)
    {
        if (!Directory.Exists(fromPath)) return;
        if (!Directory.Exists(toPath)) Directory.CreateDirectory(toPath);
        var folder = new DirectoryInfo(fromPath);
        //遍历文件
        var tagPath = Path.Combine(toPath,dir);
        foreach (var file in folder.GetFiles())
        {
            var destFile = Path.Combine(tagPath, file.Name);
            try
            { 
                File.Copy(file.FullName, destFile,true); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"拷贝文件异常{ex.Message}");  
            }
        }
        //遍历文件夹
        foreach (var fromDir in folder.GetDirectories())
        {
            if (fromDir == null) continue;
            var subDir = Path.Combine(dir, fromDir.Name);
            if (!Directory.Exists(subDir)) Directory.CreateDirectory(subDir); 
            CopyTo(fromDir.FullName, toPath, ref subDir);
        }


    }


}

using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

//string url = "http://172.16.175.225:8080/BBMSWeb/welcome1.do?departName1=急诊科&departCode1=1902&departName2=&departCode2=&employeeNo=admin&employeeName=管理员&patientID=65200020052&departType=O&doctorType=D2&time=&bedNo=&diagnosis=B99.x01%2CR50.800x002&operation=&Token=";
//var textBytes = System.Text.Encoding.UTF8.GetBytes(url);
//string base64String = System.Convert.ToBase64String(textBytes);

if (args == null || args.Length < 1)
{
    return;
}
var arguments = args[0].Replace("szyjian://", "").Split("&");
if (arguments.Length < 1)
{
    throw new ArgumentException($"参数错误, {args[0]}");
}

switch (arguments[0])
{
    case "ie":
        OpenWithIE(arguments[1]);
        break;
    default:
        break;
}

// 使用IE浏览器打开链接
void OpenWithIE(string base64Url)
{
    var base64Bytes = Convert.FromBase64String(base64Url);
    string url = System.Text.Encoding.UTF8.GetString(base64Bytes);

    // 区分64位系统与32位系统，取不同的目录
    var programPath = Environment.Is64BitOperatingSystem
        ? "C:\\Program Files (x86)\\Internet Explorer\\iexplore.exe"
        : "C:\\Program Files\\Internet Explorer\\iexplore.exe";

    ProcessStartInfo startInfo = new ProcessStartInfo();
    startInfo.WorkingDirectory = Path.GetDirectoryName(programPath);
    startInfo.FileName = programPath;
    startInfo.Arguments = url;
    startInfo.UseShellExecute = false;
    startInfo.RedirectStandardOutput = true;
    startInfo.CreateNoWindow = true;

    Process process = new Process() { StartInfo = startInfo };
    bool isProcess = process.Start();
}

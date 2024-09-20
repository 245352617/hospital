using DCSoft;
using DCSoft.Writer;
using DCSoft.Writer.Controls;
using DCSoft.Writer.Dom;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DCWriterCoreMVCDemo.Controllers
{
    /// <summary>
    /// 都昌组件
    /// </summary>
    [RequestFormLimits(ValueCountLimit = 5000, ValueLengthLimit = 2097152000)]
    public class DCWriterController : Controller
    {
        private IHttpContextAccessor _accessor;
        private readonly IHostingEnvironment _hostingEnvironment;
        private HttpContext httpcontext;
        private readonly IConfiguration _configration;
        private readonly string _dcRegisterCode; // 都昌电子病历注册码

        public DCWriterController(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment,
            IConfiguration configuration)
        {
            _accessor = accessor;
            _hostingEnvironment = hostingEnvironment;
            _configration = configuration;
            _dcRegisterCode = _configration["DcRegistrationCode:Code"];
        }

        [HttpGet]
        public IActionResult Index()
        {
            httpcontext = _accessor.HttpContext;
            return View(httpcontext);
        }

        private static string GetMacAddressFromRegistry()
        {
            string macAddress = null;
            var p = Environment.OSVersion.Platform;
            bool IsWindowsPlatform =
                p == PlatformID.Win32NT
                || p == PlatformID.Win32S
                || p == PlatformID.Win32Windows
                || p == PlatformID.WinCE;
            if (IsWindowsPlatform)
            {
                var regRoot = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                    @"SYSTEM\CurrentControlSet\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}", false);
                if (regRoot != null)
                {
                    var names = regRoot.GetSubKeyNames();
                    foreach (var name in names)
                    {
                        //try
                        {
                            var regItem = regRoot.OpenSubKey(name, false);
                            if (regItem != null)
                            {
                                var deviceID = Convert.ToString(regItem.GetValue("DeviceInstanceID"));
                                if (deviceID != null && deviceID.StartsWith("PCI\\"))
                                {
                                    macAddress = Convert.ToString(regItem.GetValue("NetworkAddress"));
                                    if (macAddress == null || macAddress.Length == 0)
                                    {
                                        var BIMacAddress_h = regItem.GetValue("BIMacAddress_h");
                                        var BIMacAddress_l = regItem.GetValue("BIMacAddress_l");
                                        if (BIMacAddress_h != null && BIMacAddress_l != null)
                                        {
                                            var h = Convert.ToInt32(BIMacAddress_h);
                                            var l = Convert.ToInt32(BIMacAddress_l);
                                            if (h != 0 && l != 0)
                                            {
                                                macAddress = h.ToString("X") + l.ToString("X");
                                                //System.Diagnostics.Debug.WriteLine(macAddress);
                                            }
                                        }
                                    }
                                }

                                regItem.Close();
                                if (macAddress != null)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    regRoot.Close();
                }
            }


            return macAddress;
        }

        //private DCSoft.Writer.Controls.WebWriterControlEngine eng;

        #region 创建WEB控件引擎

        //创建WEB控件引擎
        private WebWriterControlEngine GetControlEngine()
        {
            DCSoft.Writer.Controls.WebWriterControlEngine engine =
                new DCSoft.Writer.Controls.WebWriterControlEngine(_accessor.HttpContext);
            engine.RegisterCode = _dcRegisterCode;
            // engine.SetRegisterCode("05AE077EAECA61BD6B4D1431E1CE5B67CD6202A61C56477B3C152F8CB7D6AEF5739A65E4E4E3BED8C7F6F6AF0D790E5AEEC97742D3037C36F566C4C27353BD1B1770A5B39782753F54FCA9E77AE5743D8068BDF8F573A6B5D5CC9BA07DDEB820849F201F547E97B56A6DDDECD49707D79338B56364AC114CE6E3DBAADD973F5CC3E8735014BE9D912F");//注册码
            engine.Options.ControlName = "myWriterControl";
            //engine.Options.AttachedAJAXHeader = "{'token':'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa'}";
            engine.Options.HeaderFooterSelect = true;
            engine.Options.ContentRenderMode = DCSoft.Writer.Controls.WebWriterControlRenderMode.NormalHtmlEditable;
            //engine.CrossDomainSettings.Enabled = true;
            engine.Options.ImageDataEmbedInHtml = true;
            engine.Options.UseClassAttribute = false;
            engine.Options.UseDCWriterMiniJS = DCBooleanValueHasDefault.False;
            engine.Options.ServicePageURL = "/DCWriterDemo/MoreHandleDCWriterServicePage"; //第二种服务路径

            return engine;
        }

        public void SetJqueryVersion()
        {
            string jqueryFile = _configration["JQueryVersion"];
            string rootDir = _hostingEnvironment.WebRootPath;
            string dir_jq = Path.Combine(rootDir, "js/" + jqueryFile);
            string jq_str = System.IO.File.ReadAllText(dir_jq);
            WebWriterControlEngine.SetSourceCodeForJQuery(jq_str);
        }

        //第二种服务
        [ResponseCache(Duration = 7200, Location = ResponseCacheLocation.Any)]
        public ActionResult MoreHandleDCWriterServicePage()
        {
            // IO操作影响加载速度，需要优化IO到缓存
            // SetJqueryVersion();
            return new MoreActionResult(_dcRegisterCode);
        }

        private class MoreActionResult : ActionResult
        {
            private readonly string _dcRegisterCode; // 都昌电子病历注册码

            public MoreActionResult(string dcRegisterCode)
            {
                _dcRegisterCode = dcRegisterCode;
            }

            public override void ExecuteResult(ActionContext context)
            {
                DCSoft.Writer.Controls.WebWriterControlServicePageExecuter executer =
                    new DCSoft.Writer.Controls.WebWriterControlServicePageExecuter();
                executer.RegisterCode = _dcRegisterCode;
                executer.CrossDomainSettings.Enabled = true;

                if (executer.HandleService(context.HttpContext))
                {
                    return;
                }
            }
        }

        #endregion
    }
}
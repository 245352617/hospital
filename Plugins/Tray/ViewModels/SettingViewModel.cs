using System.Collections.ObjectModel;
using System.Drawing.Printing;
using YiJian.CardReader.Settings;
using YiJian.Tray.Settings;
using static System.Drawing.Printing.PrinterSettings;

namespace YiJian.CardReader.ViewModels
{
    public class SettingViewModel : NotificationObject
    {
        /// <summary>
        /// 系统支持的所有厂家列表
        /// </summary>
        public ObservableCollection<Vendor> Vendors => YiJian.CardReader.Vendors.GetList();

        /// <summary>
        /// 当前的配置
        /// </summary>
        private Setting Setting = new();

        /// <summary>
        /// 打印中心设置
        /// </summary>
        private ReportSetting ReportSetting = new();

        /// <summary>
        /// 运行程序配置
        /// </summary>
        private ProcessSetting processSetting = new();

        /// <summary>
        /// 当前配置的读卡器厂家
        /// </summary>
        public string Vendor
        {
            get
            {
                return Setting.Vendor ?? string.Empty;
            }
            set
            {
                Setting.Vendor = value;
                RaisePropertyChanged(nameof(Vendor));
                RaisePropertyChanged(nameof(VendorDescription));
            }
        }

        /// <summary>
        /// 厂家描述
        /// </summary>
        public string VendorDescription
        {
            get
            {
                var vendor = YiJian.CardReader.Vendors.Find(Vendor);
                return vendor?.Description ?? "请先选择读卡器类型";
            }
        }

        public string WebSocketIp
        {
            get
            {
                return Setting.WebSocketIp ?? string.Empty;
            }
            set
            {
                Setting.WebSocketIp = value;
                RaisePropertyChanged(nameof(WebSocketIp));
            }
        }

        public string WebSocketPort
        {
            get
            {
                return Setting.WebSocketPort ?? string.Empty;
            }
            set
            {
                Setting.WebSocketPort = value;
                RaisePropertyChanged(nameof(WebSocketPort));
            }
        }

        /// <summary>
        /// 打印中心地址
        /// </summary>
        public string PrintCenterUrl
        {
            get
            {
                return ReportSetting.PrintCenterUrl ?? string.Empty;
            }
            set
            {
                ReportSetting.PrintCenterUrl = value;
                RaisePropertyChanged(nameof(PrintCenterUrl));
            }
        }

        #region 打印配置

        private ObservableCollection<string> _printers;
        /// <summary>
        /// 打印机列表
        /// </summary>
        public ObservableCollection<string> Printers
        {
            get { return _printers; }
            set { _printers = value; RaisePropertyChanged(nameof(Printers)); }
        }

        /// <summary>
        /// 默认打印机
        /// </summary>
        public string DefaultPrinter
        {
            get { return ReportSetting.DefaultPrinter ?? string.Empty; }
            set
            {
                ReportSetting.DefaultPrinter = value; RaisePropertyChanged(nameof(DefaultPrinter));
                PeperNames = GetPageSizes(DefaultPrinter);
            }
        }

        private ObservableCollection<string> _peperNames;
        /// <summary>
        /// 打印机列表
        /// </summary>
        public ObservableCollection<string> PeperNames
        {
            get { return _peperNames; }
            set { _peperNames = value; RaisePropertyChanged(nameof(PeperNames)); }
        }

        /// <summary>
        /// 默认打印机
        /// </summary>
        public string DefaultPaperName
        {
            get { return ReportSetting.DefaultPaperName ?? string.Empty; }
            set
            {
                ReportSetting.DefaultPaperName = value; RaisePropertyChanged(nameof(DefaultPaperName));
            }
        }


        #endregion

        #region 医嘱打印配置

        private ObservableCollection<string> _doctorsAdvicePrinters;
        /// <summary>
        /// 打印机列表
        /// </summary>
        public ObservableCollection<string> DoctorsAdvicePrinters
        {
            get { return _doctorsAdvicePrinters; }
            set { _doctorsAdvicePrinters = value; RaisePropertyChanged(nameof(DoctorsAdvicePrinters)); }
        }
        /// <summary>
        /// 默认打印机
        /// </summary>
        public string DefaultDoctorsAdvicePrinter
        {
            get { return ReportSetting.DefaultDoctorsAdvicePrinter ?? string.Empty; }
            set
            {
                ReportSetting.DefaultDoctorsAdvicePrinter = value; RaisePropertyChanged(nameof(DefaultDoctorsAdvicePrinter));
                DoctorsAdvicePaperNames = GetPageSizes(DefaultDoctorsAdvicePrinter);
            }
        }


        private ObservableCollection<string> _doctorsAdvicePaperNames;
        /// <summary>
        /// 医嘱打印配置列表
        /// </summary>
        public ObservableCollection<string> DoctorsAdvicePaperNames
        {
            get { return _doctorsAdvicePaperNames; }
            set { _doctorsAdvicePaperNames = value; RaisePropertyChanged(nameof(DoctorsAdvicePaperNames)); }
        }
        /// <summary>
        /// 医嘱打印配置
        /// </summary>
        public string DefaultDoctorsAdvicePaperName
        {
            get { return ReportSetting.DefaultDoctorsAdvicePaperName ?? string.Empty; }
            set { ReportSetting.DefaultDoctorsAdvicePaperName = value; RaisePropertyChanged(nameof(DefaultDoctorsAdvicePaperName)); }
        }

        #endregion

        #region 电子病历打印配置

        private ObservableCollection<string> _emrPrinters;
        /// <summary>
        /// 打印机列表
        /// </summary>
        public ObservableCollection<string> EmrPrinters
        {
            get { return _emrPrinters; }
            set { _emrPrinters = value; RaisePropertyChanged(nameof(EmrPrinters)); }
        }
        /// <summary>
        /// 打印机列表
        /// </summary>
        public string DefaultEmrPrinter
        {
            get { return ReportSetting.DefaultEmrPrinter ?? string.Empty; }
            set
            {
                ReportSetting.DefaultEmrPrinter = value; RaisePropertyChanged(nameof(DefaultEmrPrinter));
                EmrPaperNames = GetPageSizes(DefaultEmrPrinter);
            }
        }

        private ObservableCollection<string> _emrPaperNames;
        /// <summary>
        /// 打印机列表
        /// </summary>
        public ObservableCollection<string> EmrPaperNames
        {
            get { return _emrPaperNames; }
            set { _emrPaperNames = value; RaisePropertyChanged(nameof(EmrPaperNames)); }
        }
        /// <summary>
        /// 打印机列表
        /// </summary>
        public string DefaultEmrPaperName
        {
            get { return ReportSetting.DefaultEmrPaperName ?? string.Empty; }
            set { ReportSetting.DefaultEmrPaperName = value; RaisePropertyChanged(nameof(DefaultEmrPaperName)); }
        }

        #endregion

        #region 贴瓶打印配置

        private ObservableCollection<string> _pastingBottlesPrinters;
        /// <summary>
        /// 打印机列表
        /// </summary>
        public ObservableCollection<string> PastingBottlesPrinters
        {
            get { return _pastingBottlesPrinters; }
            set { _pastingBottlesPrinters = value; RaisePropertyChanged(nameof(PastingBottlesPrinters)); }
        }
        /// <summary>
        /// 打印机列表
        /// </summary>
        public string PastingBottlesPrinter
        {
            get { return ReportSetting.PastingBottlesPrinter ?? string.Empty; }
            set
            {
                ReportSetting.PastingBottlesPrinter = value; RaisePropertyChanged(nameof(PastingBottlesPrinter));
                PastingBottlesPaperNames = GetPageSizes(PastingBottlesPrinter);
            }
        }

        private ObservableCollection<string> _pastingBottlesPaperNames;
        /// <summary>
        /// 打印机列表
        /// </summary>
        public ObservableCollection<string> PastingBottlesPaperNames
        {
            get { return _pastingBottlesPaperNames; }
            set { _pastingBottlesPaperNames = value; RaisePropertyChanged(nameof(PastingBottlesPaperNames)); }
        }
        /// <summary>
        /// 打印机列表
        /// </summary>
        public string PastingBottlesPaperName
        {
            get { return ReportSetting.PastingBottlesPaperName ?? string.Empty; }
            set { ReportSetting.PastingBottlesPaperName = value; RaisePropertyChanged(nameof(PastingBottlesPaperName)); }
        }

        #endregion


        #region 腕带配置

        private ObservableCollection<string> _wristStrapPrinters;
        /// <summary>
        /// 打印机列表
        /// </summary>
        public ObservableCollection<string> WristStrapPrinters
        {
            get { return _wristStrapPrinters; }
            set { _wristStrapPrinters = value; RaisePropertyChanged(nameof(WristStrapPrinters)); }
        }
        /// <summary>
        /// 打印机列表
        /// </summary>
        public string WristStrapPrinter
        {
            get { return ReportSetting.WristStrapPrinter ?? string.Empty; }
            set
            {
                ReportSetting.WristStrapPrinter = value; RaisePropertyChanged(nameof(WristStrapPrinter));
                WristStrapPaperNames = GetPageSizes(WristStrapPrinter);
            }
        }

        private ObservableCollection<string> _wristStrapPaperNames;
        /// <summary>
        /// 打印机列表
        /// </summary>
        public ObservableCollection<string> WristStrapPaperNames
        {
            get { return _wristStrapPaperNames; }
            set { _wristStrapPaperNames = value; RaisePropertyChanged(nameof(WristStrapPaperNames)); }
        }
        /// <summary>
        /// 打印机列表
        /// </summary>
        public string WristStrapPaperName
        {
            get { return ReportSetting.WristStrapPaperName ?? string.Empty; }
            set { ReportSetting.WristStrapPaperName = value; RaisePropertyChanged(nameof(WristStrapPaperName)); }
        }

        #endregion

        #region ProcessSetting
        public string IePath
        {
            get
            {
                return processSetting.IePath ?? string.Empty;
            }
            set
            {
                processSetting.IePath = value;
                RaisePropertyChanged(nameof(IePath));
            }
        }

        public string ProcessPath
        {
            get
            {
                return processSetting.ProcessPath ?? string.Empty;
            }
            set
            {
                processSetting.ProcessPath = value;
                RaisePropertyChanged(nameof(ProcessPath));
            }
        }

        public string ProcessArgs
        {
            get
            {
                return processSetting.ProcessArgs ?? string.Empty;
            }
            set
            {
                processSetting.ProcessArgs = value;
                RaisePropertyChanged(nameof(ProcessArgs));
            }
        }
        #endregion


        /// <summary>
        /// 获取对应打印机对应pageSize
        /// </summary>
        /// <param name="printName"></param>
        /// <returns></returns>
        public ObservableCollection<string> GetPageSizes(string printName)
        {
            var paperNameList = new ObservableCollection<string>();
            if (string.IsNullOrEmpty(printName))
            {
                PrintDocument printDoc = new PrintDocument();
                foreach (PaperSize ps in printDoc.PrinterSettings.PaperSizes)
                {
                    paperNameList.Add(ps.PaperName);
                }
            }
            else
            {
                // 使用PrinterSettings获取打印机设置
                PrinterSettings printerSettings = new PrinterSettings();
                printerSettings.PrinterName = printName;

                // 获取指定打印机的PaperSize集合
                PaperSizeCollection paperSizes = printerSettings.PaperSizes;
                foreach (PaperSize paperSize in paperSizes)
                {
                    paperNameList.Add(paperSize.PaperName);
                }
            }
            return paperNameList;
        }
    }
}

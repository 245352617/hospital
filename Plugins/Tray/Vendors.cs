using System.Linq;
using System.Collections.ObjectModel;

namespace YiJian.CardReader
{
    public class Vendor
    {
        /// <summary>
        /// 厂家名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 配置描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 驱动文件存放的相对路径
        /// </summary>
        public string DriverPath { get; set; }

        public Vendor(string name, string description, string driverPath)
        {
            Name = name;
            Description = description;
            DriverPath = driverPath;
        }
    }

    public static class Vendors
    {
        public static Vendor Wante = new("万特", "无需特别配置", "Drivers/Wante");
        public static Vendor Donsee = new("东信", "无需特别配置", "Drivers/Donsee");
        public static Vendor DeCard = new("德卡", @"修改系统文件：C:\Windows\System32\drivers\etc\hosts, 在hosts文件中增加映射设置：10.97.240.206 igb.hsa.gdgov.cn", "Drivers/DeCard");

        private static readonly ObservableCollection<Vendor> _vendors = new() { Wante, DeCard, Donsee };

        public static ObservableCollection<Vendor> GetList() => _vendors;

        public static Vendor? Find(string name) => _vendors.FirstOrDefault(x => x.Name == name);
    }
}

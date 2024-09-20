using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.CardReader.CardReaders
{
    public abstract class AbstractCardReader : ICardReader
    {
        public AbstractCardReader(string driverRelativePath)
        {
            // 初始化环境变量，保证DLL能被搜索到
            var current = AppDomain.CurrentDomain.BaseDirectory;
            var driverPath = new[] { Path.Combine(current, driverRelativePath) };
            var path = new[] { Environment.GetEnvironmentVariable("PATH") ?? string.Empty };
            var newPath = string.Join(Path.PathSeparator.ToString(), path.Concat(driverPath));
            Environment.SetEnvironmentVariable("PATH", newPath);
        }

        public abstract void CloseDevice();

        public abstract bool OpenDevice();

        public abstract IDCardInfo? ReadIdCard();

        public virtual void ReadMagneticCard()
        {
            throw new NotImplementedException("本设备不支持读取词条卡");
        } 

        public abstract MedicareCardInfo? ReadMedicareCard();

        public abstract RFIDCardInfo? ReadRfCard();
    }
}

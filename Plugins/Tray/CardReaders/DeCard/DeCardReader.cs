using NLog;
using System;
using System.Text;

namespace YiJian.CardReader.CardReaders.DeCard
{
    /// <summary>
    /// 德卡 读卡器
    /// </summary>
    public class DeCardReader : AbstractCardReader
    {
        private static readonly ILogger _log = LogManager.GetCurrentClassLogger();
        private readonly int bufferSize = 1024;
        private bool hasInitialized = false;

        public DeCardReader(string driverRelativePath) : base(driverRelativePath) { }

        public override void CloseDevice()
        {
            // Nothing to do
        }

        public override bool OpenDevice()
        {
            if (hasInitialized) return hasInitialized;
            
            var url = @"http://igb.hsa.gdgov.cn/gdyb_api/prd/api/card/initDll";
            var user = "440300";
            try
            {
                var ret = DeCardDriver.Init(url, user);
                _log.Info("初始化读卡器：{Ret}", ret);
                hasInitialized = ret == 0;
                return hasInitialized;
            }
            catch(Exception ex)
            {
                _log.Error("初始化读卡器失败： {Message}", ex.Message);
                return false;
            }
        }

        public override IDCardInfo? ReadIdCard()
        {
            var outBuffer = new StringBuilder(bufferSize);
            var signBuffer = new StringBuilder(bufferSize);
            if (0 == DeCardDriver.ReadSFZ(outBuffer, bufferSize, signBuffer, bufferSize))
            {
                var cardInfo = outBuffer.ToString();
                if (!string.IsNullOrWhiteSpace(cardInfo))
                {
                    var cardFields = cardInfo.Split("^");
                    var validDates = cardFields[8].Split("-");
                    return new IDCardInfo(cardFields[1], cardFields[3], cardFields[4], 
                        Utils.SetDateTime(cardFields[5]), cardFields[6], cardFields[0],
                        cardFields[7], Utils.SetDateTime(validDates[0]), Utils.SetDateTime(validDates[1]), null);
                }
            }
            else
            {
                throw new InvalidOperationException(outBuffer.ToString());
            }
            return null;
        }

        public override MedicareCardInfo? ReadMedicareCard()
        {
            var outBuffer = new StringBuilder(bufferSize);
            var signBuffer = new StringBuilder(bufferSize);
            if (0 == DeCardDriver.ReadCardBas(outBuffer, bufferSize, signBuffer, bufferSize))
            {
                var cardInfo = outBuffer.ToString();
                if (!string.IsNullOrWhiteSpace(cardInfo))
                {
                    var cardFields = cardInfo.Split("|");
                    return new MedicareCardInfo(
                        cardFields[1],
                        cardFields[1],
                        cardFields[4],
                        String.Empty,
                        String.Empty,
                        null,
                        cardFields[0]);
                }
            }
            else
            {
                throw new InvalidOperationException(outBuffer.ToString());
            }
            return null;
        }

        public override RFIDCardInfo? ReadRfCard()
        {
            throw new NotImplementedException("德卡读卡器，不支持读取射频卡");
        }
    }
}

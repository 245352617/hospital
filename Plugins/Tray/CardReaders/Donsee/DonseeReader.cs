using NLog;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace YiJian.CardReader.CardReaders.Donsee
{
    /// <summary>
    /// 东信 读卡器
    /// </summary>
    public class DonseeReader : AbstractCardReader
    {
        private static readonly ILogger _log = LogManager.GetCurrentClassLogger();
        private byte keyMode = 0x60;
        private readonly StringBuilder driverMessage = new(100);
        private readonly StringBuilder cardInfo = new(300);
        private readonly StringBuilder b64Info = new(6500);

        public DonseeReader(string driverRelativePath) : base(driverRelativePath)
        {
        }

        public override void CloseDevice()
        {
            if (DonseeDriver.iClosePort() != 0)
            {
                // TODO: 关闭端口失败
            }
        }

        public override bool OpenDevice()
        {
            if (DonseeDriver.iOpenPort(driverMessage) != 0)
            {
                _log.Error("东信读卡器 打开设备端口失败：{Error}", driverMessage.ToString());
                return false;
            }
            DonseeDriver.iPosBeep();
            return true;
        }

        /// <summary>
        /// 读取身份证
        /// </summary>
        /// <returns></returns>
        public override IDCardInfo? ReadIdCard()
        {
            var pBmpFile = new StringBuilder(100);
            var str = System.Environment.CurrentDirectory;
            pBmpFile.Append(str);
            pBmpFile.Append(@"\zp.bmp");

            cardInfo.Clear();
            DonseeDriver.iPosBeep();
            var ret = DonseeDriver.iReaderIDCard_CS(pBmpFile, cardInfo, b64Info, driverMessage);
            if (ret != 0)
            {
                _log.Error("东信读卡器读取卡片信息错误，返回结果：{Ret}  错误原因：{Msg}", ret, driverMessage);
                throw new InvalidOperationException(driverMessage.ToString());
            }
            else
            {
                _log.Info("身份证数据:{CardInfo}", cardInfo.ToString());
                if (!string.IsNullOrWhiteSpace(cardInfo.ToString()))
                {
                    var cardInfoSplit = cardInfo.ToString()
                        .Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (cardInfoSplit.Length == 0) return null;
                    var idCardInfo = new IDCardInfo(
                        cardInfoSplit[0],
                        cardInfoSplit[1],
                        cardInfoSplit[2],
                        Utils.SetDateTime(cardInfoSplit[3]),
                        cardInfoSplit[4],
                        cardInfoSplit[5],
                        cardInfoSplit[6],
                        Utils.SetDateTime(cardInfoSplit[7]),
                        Utils.SetDateTime(cardInfoSplit[8]),
                        null);
                    return idCardInfo;
                }
                _log.Error("东信读卡器 读卡失败, 内容为空");
            }
            return null;
        }

        /// <summary>
        /// 读取医保卡信息
        /// </summary>
        /// <returns></returns>
        public override MedicareCardInfo? ReadMedicareCard()
        {
            cardInfo.Clear();
            DonseeDriver.iPosBeep();
            var ret = DonseeDriver.iReadSicard_CS(0x11, cardInfo, driverMessage);
            if (ret != 0)
            {
                _log.Error("东信读卡器读取卡片信息错误，返回结果：{Ret}  错误原因：{Msg}", ret, driverMessage);
                throw new InvalidOperationException(driverMessage.ToString());
            }
            else
            {
                _log.Info("医保卡数据：{CardInfo}", cardInfo.ToString());
                if (!string.IsNullOrWhiteSpace(cardInfo.ToString()))
                {
                    var cardInfoSplit = cardInfo.ToString()
                        .Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (cardInfoSplit.Length == 0) return null;
                    var medCardInfo = new MedicareCardInfo(
                        //cardInfoSplit[0],
                        cardInfoSplit[1],
                        cardInfoSplit[2],
                        cardInfoSplit[3],
                        cardInfoSplit[4],
                        cardInfoSplit[5],
                        Utils.SetDateTime(cardInfoSplit[6]));
                    return medCardInfo;
                }
                _log.Error("东信读卡器 读卡失败, 内容为空");
            }
            return null;
        }

        public override RFIDCardInfo? ReadRfCard()
        {
            var secNr = 0;
            var addr = secNr * 4 + 0;

            StringBuilder key = new StringBuilder(30);
            key.Clear();
            key.Append("FFFFFFFFFFFF");

            DonseeDriver.iPosBeep();
            var ret = DonseeDriver.MifareOnCardReadHEX_CS(addr, keyMode, key, cardInfo, driverMessage);
            if (ret != 0)
            {
                _log.Error("东信读卡器读取卡片信息错误，返回结果：{Ret}  错误原因：{Msg}", ret, driverMessage);
                throw new InvalidOperationException(driverMessage.ToString());
            }
            else
            {
                _log.Info("射频卡数据：{CardInfo}", cardInfo.ToString());
                //窃取前8位，2个字符为一组并倒叙拼接，在转换成10进制
                if (!string.IsNullOrWhiteSpace(cardInfo.ToString()))
                {
                    var newString = cardInfo.ToString().Substring(0, 8);
                    StringBuilder sb = new StringBuilder();
                    sb.Append(newString.Substring(6, 2));
                    sb.Append(newString.Substring(4, 2));
                    sb.Append(newString.Substring(2, 2));
                    sb.Append(newString.Substring(0, 2));
                    //16进制转10进制RFID设备ID
                    var id = Convert.ToInt32(sb.ToString(), 16);
                    var rfidCardInfo = new RFIDCardInfo(id.ToString());
                    return rfidCardInfo;
                }
                _log.Error("东信读卡器 读卡失败, 内容为空");
            }
            return null;
        }

        public override void ReadMagneticCard()
        {
            DonseeDriver.iPosBeep();
            var ret = DonseeDriver.iReadMagCard(10, 3, cardInfo, driverMessage);
            if (ret != 0)
            {
                _log.Error("东信读卡器读取卡片信息错误，返回结果：{Ret}  错误原因：{Msg}", ret, driverMessage);
                throw new InvalidOperationException(driverMessage.ToString());
            }
            else
            {
                //窃取前8位，2个字符为一组并倒叙拼接，在转换成10进制
                _log.Info("磁条卡内容：{Message}", cardInfo.ToString());
                _log.Error("东信读卡器 读卡失败, 内容为空");
            }
        }
    }
}

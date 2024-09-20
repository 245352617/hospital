using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.CardReader.CardReaders.Wante
{
    /// <summary>
    /// 万特 读卡器 Native 接口
    /// </summary>
    public class WanteDriver
    {
        [DllImport("SSCARD_SW.dll", EntryPoint = "iOpenPort")]
        public static extern int iOpenPort(StringBuilder errMsg);

        [DllImport("SSCARD_SW.dll", EntryPoint = "iClosePort")]
        public static extern int iClosePort();

        [DllImport("SSCARD_SW.dll", EntryPoint = "iPosBeep")]
        public static extern int iPosBeep();

        [DllImport("SSCARD_SW.dll", EntryPoint = "iReadSicard_CS")]
        public static extern int iReadSicard_CS(int slot, StringBuilder cardInfo, StringBuilder errMsg);

        [DllImport("SSCARD_SW.dll", EntryPoint = "iReadSicardEx")]
        public static extern int iReadSicardEx(int slot, StringBuilder SBKH, StringBuilder XM, StringBuilder XB, StringBuilder MZ, StringBuilder CSRQ, StringBuilder SFZHM, StringBuilder FKRQ, StringBuilder KYXQ, StringBuilder errMsg);

        [DllImport("SSCARD_SW.dll", EntryPoint = "iReadCertID_CS")]
        public static extern int iReadCertID_CS(StringBuilder cardInfo, StringBuilder errMsg);

        [DllImport("SSCARD_SW.dll", EntryPoint = "iReaderIDCard_CS")]
        public static extern int iReaderIDCard_CS(StringBuilder bmpFilePath, StringBuilder cardInfo, StringBuilder base64Info, StringBuilder errMsg);

        [DllImport("SSCARD_SW.dll", EntryPoint = "MifareOnCardRead_CS")]
        public static extern int MifareOnCardRead_CS(int addr, int keyType, StringBuilder key, byte[] cardInfo, StringBuilder errMsg);

        [DllImport("SSCARD_SW.dll", EntryPoint = "MifareOnCardReadHEX_CS")]
        public static extern int MifareOnCardReadHEX_CS(int addr, int keyType, StringBuilder key, StringBuilder cardInfo, StringBuilder errMsg);

        [DllImport("SSCARD_SW.dll", EntryPoint = "MifareOnCardWrite_CS")]
        public static extern int MifareOnCardWrite_CS(int addr, int keyType, StringBuilder key, byte[] cardInfo, StringBuilder errMsg);

        [DllImport("SSCARD_SW.dll", EntryPoint = "MifareOnCardWriteHEX_CS")]
        public static extern int MifareOnCardWriteHEX_CS(int addr, int keyType, StringBuilder key, StringBuilder cardInfo, StringBuilder errMsg);

        [DllImport("SSCARD_SW.dll", EntryPoint = "iReadBankNo_CS")]
        public static extern int iReadBankNo_CS(int iType, StringBuilder cardInfo, StringBuilder errMsg);

        [DllImport("SSCARD_SW.dll", EntryPoint = "iReadMagCard")]
        public static extern int iReadMagCard(int timeOut, int nTrack, StringBuilder cardInfo, StringBuilder errMsg);

        [DllImport("SSCARD_SW.dll", EntryPoint = "ScanQRcode")]
        public static extern int ScanQRcode(int timeOut, StringBuilder cardInfo, StringBuilder errMsg);

        [DllImport("SSCARD_SW.dll", EntryPoint = "SetAutoQRcode")]
        public static extern int SetAutoQRcode(StringBuilder errMsg);

        [DllImport("SSCARD_SW.dll", EntryPoint = "GetPassWord")]
        public static extern int GetPassWord(int iType, StringBuilder pin, StringBuilder errMsg);
    }
}

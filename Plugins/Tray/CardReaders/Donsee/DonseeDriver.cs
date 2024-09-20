using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.CardReader.CardReaders.Donsee
{
    /// <summary>
    /// 东信 驱动
    /// </summary>
    public class DonseeDriver
    {
        /// <summary>
        /// 打开设备
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [DllImport("SSCARDInterface.dll", EntryPoint = "iOpenPort")]
        public static extern int iOpenPort(StringBuilder errMsg);

        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        [DllImport("SSCARDInterface.dll", EntryPoint = "iClosePort")]
        public static extern int iClosePort();

        /// <summary>
        /// 蜂鸣器
        /// </summary>
        /// <returns></returns>
        [DllImport("SSCARDInterface.dll", EntryPoint = "iPosBeep")]
        public static extern int iPosBeep();

        /// <summary>
        /// 读取社保卡信息
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="cardInfo"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [DllImport("SSCARDInterface.dll", EntryPoint = "iReadSicard_CS")]
        public static extern int iReadSicard_CS(int slot, StringBuilder cardInfo, StringBuilder errMsg);

        /// <summary>
        /// 读取身份证UID
        /// </summary>
        /// <param name="cardInfo"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [DllImport("SSCARDInterface.dll", EntryPoint = "iReadCertID_CS")]
        public static extern int iReadCertID_CS(StringBuilder cardInfo, StringBuilder errMsg);

        /// <summary>
        /// 读取身份证/港澳台居住证信息
        /// </summary>
        /// <param name="bmpFilePath"></param>
        /// <param name="cardInfo"></param>
        /// <param name="base64Info"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [DllImport("SSCARDInterface.dll", EntryPoint = "iReaderIDCard_CS")]
        public static extern int iReaderIDCard_CS(StringBuilder bmpFilePath, StringBuilder cardInfo, StringBuilder base64Info, StringBuilder errMsg);

        /// <summary>
        /// M1卡读操作
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="keyType"></param>
        /// <param name="key"></param>
        /// <param name="cardInfo"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [DllImport("SSCARDInterface.dll", EntryPoint = "MifareOnCardRead_CS")]
        public static extern int MifareOnCardRead_CS(int addr, int keyType, StringBuilder key, byte[] cardInfo, StringBuilder errMsg);

        /// <summary>
        /// M1卡读操作（十六进制）
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="keyType"></param>
        /// <param name="key"></param>
        /// <param name="cardInfo"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [DllImport("SSCARDInterface.dll", EntryPoint = "MifareOnCardReadHEX_CS")]
        public static extern int MifareOnCardReadHEX_CS(int addr, int keyType, StringBuilder key, StringBuilder cardInfo, StringBuilder errMsg);

        /// <summary>
        /// M1卡写操作
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="keyType"></param>
        /// <param name="key"></param>
        /// <param name="cardInfo"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [DllImport("SSCARDInterface.dll", EntryPoint = "MifareOnCardWrite_CS")]
        public static extern int MifareOnCardWrite_CS(int addr, int keyType, StringBuilder key, byte[] cardInfo, StringBuilder errMsg);

        /// <summary>
        /// M1卡写操作（十六进制）
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="keyType"></param>
        /// <param name="key"></param>
        /// <param name="cardInfo"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [DllImport("SSCARDInterface.dll", EntryPoint = "MifareOnCardWriteHEX_CS")]
        public static extern int MifareOnCardWriteHEX_CS(int addr, int keyType, StringBuilder key, StringBuilder cardInfo, StringBuilder errMsg);

        /// <summary>
        /// 读取磁条卡信息
        /// </summary>
        /// <param name="timeOut"></param>
        /// <param name="nTrack"></param>
        /// <param name="cardInfo"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [DllImport("SSCARDInterface.dll", EntryPoint = "iReadMagCard")]
        public static extern int iReadMagCard(int timeOut, int nTrack, StringBuilder cardInfo, StringBuilder errMsg);

        /// <summary>
        /// 扫码二维码信息
        /// </summary>
        /// <param name="timeOut"></param>
        /// <param name="cardInfo"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [DllImport("SSCARDInterface.dll", EntryPoint = "ScanQRcode")]
        public static extern int ScanQRcode(int timeOut, StringBuilder cardInfo, StringBuilder errMsg);

        /// <summary>
        /// 扫码二维码信息（设置主动扫码）
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [DllImport("SSCARDInterface.dll", EntryPoint = "SetAutoQRcode")]
        public static extern int SetAutoQRcode(StringBuilder errMsg);
    }
}

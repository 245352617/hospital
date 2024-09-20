using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.CardReader.CardReaders.DeCard
{
    /// <summary>
    /// 德卡 驱动
    /// </summary>
    public class DeCardDriver
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [DllImport("SSCard")]
        public static extern int Init(string url, string user);
        /// <summary>
        /// 读取社保卡信息
        /// </summary>
        /// <param name="outBuffer"></param>
        /// <param name="outBufferSize"></param>
        /// <param name="signBuffer"></param>
        /// <param name="signBuffSize"></param>
        /// <returns></returns>
        [DllImport("SSCard")]
        public static extern int ReadCardBas(StringBuilder outBuffer, int outBufferSize, StringBuilder signBuffer, int signBuffSize);

        /// <summary>
        /// 校验PIN码
        /// </summary>
        /// <param name="outBuffer"></param>
        /// <param name="outBufferSize"></param>
        /// <returns></returns>
        [DllImport("SSCard")]
        public static extern int VerifyPIN(StringBuilder outBuffer, int outBufferSize);

        /// <summary>
        /// 修改PIN码
        /// </summary>
        /// <param name="outBuffer"></param>
        /// <param name="outBufferSize"></param>
        /// <returns></returns>
        [DllImport("SSCard")]
        public static extern int ChangePIN(StringBuilder outBuffer, int outBufferSize);

        /// <summary>
        /// 读取身份证
        /// </summary>
        /// <param name="outBuffer"></param>
        /// <param name="outBufferSize"></param>
        /// <param name="signBuffer"></param>
        /// <param name="signBufferSize"></param>
        /// <returns></returns>
        [DllImport("SSCard")]
        public static extern int ReadSFZ(StringBuilder outBuffer, int outBufferSize, StringBuilder signBuffer, int signBufferSize);

        /// <summary>
        /// 读取二维码
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="outBuffer"></param>
        /// <param name="outBufferSize"></param>
        /// <param name="signBuffer"></param>
        /// <param name="signBufferSize"></param>
        /// <returns></returns>
        [DllImport("SSCard")]
        public static extern int GetQRBase(int timeout, StringBuilder outBuffer, int outBufferSize, StringBuilder signBuffer, int signBufferSize);
    }
}

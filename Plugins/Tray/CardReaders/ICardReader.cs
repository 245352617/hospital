using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.CardReader.CardReaders
{
    /// <summary>
    /// 读卡器通用接口
    /// </summary>
    public interface ICardReader
    {
        /// <summary>
        /// 打开读卡设备
        /// </summary>
        /// <returns></returns>
        bool OpenDevice();

        /// <summary>
        /// 关闭读卡设备
        /// </summary>
        void CloseDevice();

        /// <summary>
        /// 读取身份证
        /// </summary>
        /// <returns></returns>
        IDCardInfo? ReadIdCard();

        /// <summary>
        /// 读取射频卡
        /// </summary>
        /// <returns></returns>
        RFIDCardInfo? ReadRfCard();

        /// <summary>
        /// 读取医保卡
        /// </summary>
        /// <returns></returns>
        MedicareCardInfo? ReadMedicareCard();

        /// <summary>
        /// 读取磁条卡
        /// </summary>
        void ReadMagneticCard();
    }
}

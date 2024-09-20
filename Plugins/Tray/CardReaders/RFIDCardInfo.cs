using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.CardReader.CardReaders
{
    /// <summary>
    /// RFID信息
    /// </summary>
    public class RFIDCardInfo
    {
        /// <summary>
        /// Id值
        /// </summary>
        public string CardId { get; set; }

        public RFIDCardInfo(string cardId)
        {
            CardId = cardId;
        }
    }
}

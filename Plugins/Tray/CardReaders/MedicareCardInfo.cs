using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.CardReader.CardReaders
{
    /// <summary>
    /// 医保卡信息
    /// </summary>
    public class MedicareCardInfo
    {
        
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNum { get; set; }
        
        /// <summary>
        /// 参保地编码
        /// </summary>
        public string InsuPlace { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }

        public string PatientName { get; set; }

        public string Sex { get; set; }

        public string Nation { get; set; }

        public DateTime? Birthday { get; set; }

        public MedicareCardInfo(string cardNum, string idCard, string name, string sex, string nation, DateTime? birthday = null, string insuPlace = "")
        {
            CardNum = cardNum;
            IdCard = idCard;
            PatientName = name;
            Sex = sex;
            Nation = nation;
            Birthday = birthday;
            InsuPlace = insuPlace;
        }
    }
}

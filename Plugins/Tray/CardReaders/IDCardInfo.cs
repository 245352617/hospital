using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.CardReader.CardReaders
{
    /// <summary>
    /// 身份证信息
    /// </summary>
    public class IDCardInfo
    {
        public string PatientName { get; set; }

        public string Sex { get; set; }

        public string Nation { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }


        public string IdCard { get; set; }

        /// <summary>
        /// 签发机关
        /// </summary>
        public string IssuingAuthority { get; set; }

        /// <summary>
        /// 有效期 起
        /// </summary>
        public DateTime? StartValid { get; set; }
        /// <summary>
        /// 有效期 止
        /// </summary>
        public DateTime? EndValid { get; set; }
        /// <summary>
        ///  照片
        /// </summary>
        public string? Photo { get; set; }

        public IDCardInfo(string patientName, string sex, string nation, DateTime? birthday,
            string address, string idCard, string authority, DateTime? startValid, DateTime? endValid, string? photo)
        {
            PatientName = patientName;
            Sex = sex;
            Nation = nation;
            Birthday = birthday;
            Address = address;
            IdCard = idCard;
            IssuingAuthority = authority;
            StartValid = startValid;
            EndValid = endValid;
            Photo = photo;
        }
    }
}

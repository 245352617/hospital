using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 修改档案
    /// </summary>
    public class PatientModifyMqDto
    {
        /// <summary>
        /// PatientInfo表Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 患者 ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 患者性别名称
        /// </summary>
        public string SexName { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string BirthDay { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        /// <example>18381847987</example>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 地址信息
        /// </summary>
        /// <example>广东-深圳-坪山区-马峦街道碧玲新榕路桂花巷5号</example>
        public string Address { get; set; }

        /// <summary>
        /// 证件类型编码
        /// </summary>
        public string IdTypeCode { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string IdTypeName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityNo { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        // /// <summary>
        // /// 叫号排队号
        // /// </summary>
        // public string CallingSn { get; set; }

        /// <summary>
        /// 获取身份证中的年龄
        /// </summary>
        /// <returns></returns>
        public int GetAgeByIdCard(string idCard)
        {
            int age = 0;
            if (!string.IsNullOrWhiteSpace(idCard))
            {
                var subStr = string.Empty;
                if (idCard.Length == 18)
                {
                    subStr = idCard.Substring(6, 8).Insert(4, "-").Insert(7, "-");
                }
                else if (idCard.Length == 15)
                {
                    subStr = ("19" + idCard.Substring(6, 6)).Insert(4, "-").Insert(7, "-");
                }

                try
                {
                    TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(subStr));
                    age = ts.Days / 365;
                }
                catch
                {
                    age = 1;
                }
            }
            return age;
        }
    }
}
using FreeSql.DataAnnotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 代办人信息
    /// </summary>
    [Table(Name = "Pat_AgencyPeople")]
    public class AgencyPeople : Entity<Guid>
    {
        /// <summary>
        /// 构造代办人信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="piId"></param>
        /// <param name="agencyPeopleName"></param>
        /// <param name="agencyPeopleIdCard"></param>
        /// <param name="agencyPeopleMobile"></param>
        public AgencyPeople(Guid id, Guid piId, [Required] string agencyPeopleName, [Required] string agencyPeopleIdCard,
            string agencyPeopleMobile)
        {
            Id = id;
            Update(agencyPeopleName, agencyPeopleIdCard, agencyPeopleMobile);
            PiId = piId;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="piId"></param>
        /// <param name="agencyPeopleName"></param>
        /// <param name="agencyPeopleIdCard"></param>
        /// <param name="agencyPeopleMobile"></param>
        /// <param name="sex"></param>
        /// <param name="age"></param>
        public AgencyPeople(Guid id, Guid piId, [Required] string agencyPeopleName, [Required] string agencyPeopleIdCard,
          string agencyPeopleMobile, string sex, string age)
        {
            Id = id;
            Update(agencyPeopleName, agencyPeopleIdCard, agencyPeopleMobile, sex, age);
            PiId = piId;
        }
        /// <summary>
        /// 修改患者信息
        /// </summary>
        /// <param name="agencyPeopleName"></param>
        /// <param name="agencyPeopleIdCard"></param>
        /// <param name="agencyPeopleMobile"></param>
        /// <param name="sex"></param>
        /// <param name="age"></param>
        public void Update([Required] string agencyPeopleName, [Required] string agencyPeopleIdCard,
            string agencyPeopleMobile, string sex, string age)
        {

            AgencyPeopleName = agencyPeopleName;
            AgencyPeopleCard = agencyPeopleIdCard;
            AgencyPeopleMobile = agencyPeopleMobile;
            AgencyPeopleSex = GetSexByIdCard(agencyPeopleIdCard, sex);
            AgencyPeopleAge = GetAgeByIdCard(agencyPeopleIdCard, age);
        }

        /// <summary>
        /// 修改患者信息
        /// </summary>
        /// <param name="agencyPeopleName"></param>
        /// <param name="agencyPeopleIdCard"></param>
        /// <param name="agencyPeopleMobile"></param>
        public void Update([Required] string agencyPeopleName, [Required] string agencyPeopleIdCard,
            string agencyPeopleMobile)
        {

            AgencyPeopleName = agencyPeopleName;
            AgencyPeopleCard = agencyPeopleIdCard;
            AgencyPeopleMobile = agencyPeopleMobile;
            AgencyPeopleSex = string.IsNullOrWhiteSpace(agencyPeopleIdCard) ? "0" : int.Parse(agencyPeopleIdCard.Substring(16, 1)) % 2 == 0 ? "2" : "1";
            AgencyPeopleAge = GetAgeByIdCard(agencyPeopleIdCard, null);
        }
        /// <summary>
        /// 设置id
        /// </summary>
        /// <param name="id"></param>
        public void SetId(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 分诊库患者基本信息表主键ID
        /// </summary>
        public Guid PiId { get; private set; }

        /// <summary>
        /// 代办人名称
        /// </summary>
        [Required, StringLength(50)]
        [Description("代办人名称")]
        public string AgencyPeopleName { get; private set; }

        /// <summary>
        /// 代办人证件号码
        /// </summary>
        [Required, StringLength(20)]
        [Description("代办人证件号码")]
        public string AgencyPeopleCard { get; private set; }

        /// <summary>
        /// 代办人联系电话
        /// </summary>
        [StringLength(20)]
        [Description("代办人联系电话")]
        public string AgencyPeopleMobile { get; private set; }

        /// <summary>
        /// 代办人性别
        /// </summary>
        [StringLength(5)]
        [Description("代办人性别")]
        public string AgencyPeopleSex { get; private set; }

        /// <summary>
        /// 代办人年龄
        /// </summary>
        [Description("代办人年龄")]
        public int AgencyPeopleAge { get; private set; }

        /// <summary>
        /// 获取身份证中的年龄
        /// </summary>
        /// <returns></returns>
        private static int GetAgeByIdCard(string idCard, string age)
        {
            int agencyPeopleAge = 0;
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
                else if (!string.IsNullOrWhiteSpace(age) && age.Contains("岁"))
                {
                    int.TryParse(age.Split('岁')[0], out agencyPeopleAge);
                    return agencyPeopleAge;
                }
                else
                {
                    int.TryParse(age, out agencyPeopleAge);
                    return agencyPeopleAge;
                }

                TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(subStr));
                agencyPeopleAge = ts.Days / 365;
            }

            return agencyPeopleAge;
        }

        private static string GetSexByIdCard(string idCard, string sex)
        {
            string agencyPeopleSex = "0";
            if (!string.IsNullOrWhiteSpace(idCard))
            {
                var subStr = string.Empty;
                if (idCard.Length == 18)
                {
                    agencyPeopleSex = string.IsNullOrWhiteSpace(idCard) ? "0" : int.Parse(idCard.Substring(16, 1)) % 2 == 0 ? "2" : "1";
                }
                else
                {
                    agencyPeopleSex = sex == "Sex_Man" ? (sex == "Sex_Woman" ? "2" : "0") : "1";
                }
            }
            return agencyPeopleSex;
        }
    }
}
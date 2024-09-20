using IDCard;
using System;
using System.Collections.Generic;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.Hospitals.Enums;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 医嘱信息回传
    /// </summary>
    public class SendMedicalInfoEto
    {
        /// <summary>
        /// 医嘱信息(根据分方分组的医嘱信息)
        /// </summary>
        public Dictionary<string, List<SendAdviceInfoEto>> AdviceGroup { get; set; }

        /// <summary>
        /// 操作基础信息
        /// </summary>
        public SubmitDoctorsAdviceDto BaseInfo { get; set; }

    }

    /// <summary>
    /// 医嘱信息列表
    /// </summary>
    public class SendAdviceInfoEto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 系统标识: 0=急诊，1=院前
        /// </summary> 
        public EPlatformType PlatformType { get; set; }

        /// <summary>
        /// 医嘱各项分类: 0=药方项,1=检查项,2=检验项,3=诊疗项
        /// </summary> 
        public EDoctorsAdviceItemType ItemType { get; set; }

    }


    /// <summary>
    /// 代办人信息
    /// </summary>
    public class AgencyPeopleDto
    {
        /// <summary>
        /// 代办人姓名
        /// <![CDATA[
        /// prescriptionType = 3（麻醉处方）、代办人姓名必填 
        /// ]]>
        /// </summary>
        public string AgencyPeopleName { get; set; }

        /// <summary>
        /// 代办人证件
        /// <![CDATA[
        /// prescriptionType = 3（麻醉处方）、代办人证件必填
        /// ]]>
        /// </summary>
        public string AgencyPeopleCard { get; set; }


        /// <summary>
        /// 代办人联系电话 prescriptionType = 3（麻醉处方）、联系电话
        /// </summary>
        public string AgencyPeopleMobile { get; set; }

        /// <summary>
        /// 身份证是否正确
        /// </summary>
        public bool IsVerify { get; private set; }

        /// <summary>
        /// 代办人性别 prescriptionType = 3（麻醉处方）、1.男  2.女
        /// </summary>
        public EAgencyPeopleSex AgencyPeopleSex
        {
            get
            {
                if (!AgencyPeopleCard.IsNullOrWhiteSpace())
                {
                    var idcard = AgencyPeopleCard.Verify();
                    IsVerify = idcard.IsVerifyPass;
                    return idcard.Gender == "男" ? EAgencyPeopleSex.Male : EAgencyPeopleSex.Female;
                }
                return EAgencyPeopleSex.Male;
            }
        }

        /// <summary>
        /// 代办人年龄 prescriptionType = 3（麻醉处方）、阿拉伯数字、不满一岁按一岁计算
        /// </summary>
        public int AgencyPeopleAge
        {
            get
            {
                if (!AgencyPeopleCard.IsNullOrWhiteSpace())
                {
                    var idcard = AgencyPeopleCard.Verify();
                    IsVerify = idcard.IsVerifyPass;
                    var ts = DateTime.Now - idcard.DayOfBirth;
                    int age = (int)Math.Floor(ts.TotalDays / 365);
                    age = age >= 1 ? age : 1;
                    return age;
                }
                return 0;
            }
        }

    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Mapster;
using SamJan.MicroService.PreHospital.Core;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class PatientInfoFromHis
    {
        /// <summary>
        /// 任务单Id
        /// </summary>
        public Guid TaskInfoId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNum { get; set; }

        /// <summary>
        /// 就诊次数
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisNo { get; set; }

        /// <summary>
        /// 患者类型
        /// 1：成人
        /// 2：儿童
        /// 3：三无人员（必填：姓名、性别、出生年月、费别）
        /// </summary>
        public string patientType { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string Py { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// 患者出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 患者住址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string ContactsPerson { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 来院方式
        /// </summary>
        public string ToHospitalWay { get; set; }

        /// <summary>
        /// 患者身份
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        /// 费别
        /// </summary>
        public string ChargeType { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityNo { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 国籍
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 国籍代码
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// RFID
        /// </summary>
        public string RFID { get; set; }

        /// <summary>
        /// 医保卡号
        /// </summary>
        public string MedicalNo { get; set; }


        /// <summary>
        /// 电子医保凭证
        /// </summary>
        public string ElectronCertNo { get; set; }


        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 开始分诊时间
        /// </summary>
        public DateTime? StartTriageTime { get; set; }

        /// <summary>
        /// 是否三无病人
        /// </summary>
        public bool IsNoThree { get; set; }

        /// <summary>
        /// 证件类型编码
        /// </summary>
        public string IdTypeCode { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string IdTypeName { get; set; }

        /// <summary>
        /// 监护人身份证号码
        /// </summary>
        public string GuardianIdCardNo { get; set; }

        /// <summary>
        /// 监护人/联系人电话
        /// </summary>
        public string GuardianPhone { get; set; }

        /// <summary>
        /// 监护人证件类型（默认居民身份证）
        /// </summary>
        public string GuardianIdTypeCode { get; set; } = "IdType_01";

        /// <summary>
        /// 监护人证件类型（默认居民身份证）
        /// </summary>
        public string GuardianIdTypeName { get; set; }

        /// <summary>
        /// 与联系人关系
        /// </summary>
        public string SocietyRelationCode { get; set; }

        /// <summary>
        /// 预约流水号
        /// </summary>
        public string SeqNumber { get; set; }

        /// <summary>
        /// 参保地代码
        /// </summary>
        public string InsuplcAdmdvCode { get; set; }


        /// <summary>
        /// 孕周
        /// </summary>
        public int? GestationalWeeks { get; set; }

        /// <summary>
        /// 人群编码
        /// </summary>
        public string CrowdCode { get; set; }

        public string CrowdName { get; set; }

        /// <summary>
        /// 挂号类型 1：普通号；2专家号；3名专家号
        /// </summary>
        public string RegType { get; set; }
        /// <summary>
        /// 就诊类型 1:门诊，2:住院，3:体检
        /// </summary>
        public string PatientKind { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 午别/0：上午；1：下午；2：晚上
        /// </summary>
        public string TimeInterval { get; set; }
        /// <summary>
        /// 诊结状态  1:未诊，2：诊结 
        /// </summary>
        public string Diagnosis { get; set; }
        /// <summary>
        /// 是否退费  1:退费{ get; set; }0:未退费
        /// </summary>
        public string IsRefund { get; set; }
        /// <summary>
        /// 是否已添加此患者到任务单，0：未添加，1：已添加
        /// </summary>
        public int IsAdd { get; set; }
        /// <summary>
        /// 既往史
        /// </summary>
        public string PastMedicalHistory { get; set; }

        /// <summary>
        /// 过敏史
        /// </summary>
        public string AllergyHistory { get; set; }
        /// <summary>
        /// 设定年龄与出生日期
        /// </summary>
        /// <param name="identityNo"></param>
        /// <returns></returns>
        public PatientInfoFromHis SetGenderAndBirthday(string identityNo)
        {
            if (!string.IsNullOrEmpty(identityNo) && identityNo.Length == 18)
            {
                if (Sex.IsNullOrWhiteSpace())
                {
                    var sexFlag = Convert.ToInt32(identityNo.Substring(16, 1));
                    Sex = Convert.ToBoolean(sexFlag & 1)
                        ? Gender.Male.GetDescriptionByEnum()
                        : Gender.Female.GetDescriptionByEnum();
                }

                if (Birthday == null || Birthday.Value == DateTime.MinValue)
                {
                    Birthday = DateTime.ParseExact(identityNo.Substring(6, 8), "yyyyMMdd",
                        CultureInfo.CurrentCulture);
                }

                if (Age.IsNullOrWhiteSpace())
                {
                    this.GetAge();
                }
            }

            return this;
        }

        /// <summary>
        /// 通过正则匹配消息体中患者信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public PatientInfoFromHis GetPatientInfoByRegexMsg(string msg)
        {
            var match = new Regex(@"[0]{1,3}\d+").Match(msg);
            if (match.Success)
            {
                PatientId = match.Value;
                return this;
            }

            return this;
        }


        /// <summary>
        /// 计算年龄
        /// </summary>
        /// <returns></returns>
        public PatientInfoFromHis GetAge()
        {
            DateTime? birthday = Birthday;
            DateTime? nullable = DateTime.Now;
            if (birthday == null || nullable.Value < birthday.Value)
                return this;
            try
            {
                string str;
                if (nullable.Value.Year - birthday.Value.Year > 3 || (nullable.Value.Year - birthday.Value.Year == 3 &&
                                                                      birthday.Value.Month <= nullable.Value.Month))
                {
                    if (birthday.Value.Month > nullable.Value.Month)
                    {
                        str = (nullable.Value.Year - birthday.Value.Year - 1) + "岁";
                    }
                    else if (birthday.Value.Month == nullable.Value.Month)
                    {
                        if (birthday.Value.Day > nullable.Value.Day)
                        {
                            str = (nullable.Value.Year - birthday.Value.Year - 1) + "岁";
                        }
                        else
                        {
                            str = (nullable.Value.Year - birthday.Value.Year) + "岁";
                        }
                    }
                    else
                    {
                        str = (nullable.Value.Year - birthday.Value.Year) + "岁";
                    }
                }
                else if ((nullable.Value.Year - birthday.Value.Year) * 12 +
                         (nullable.Value.Month - birthday.Value.Month) > 3 ||
                         (nullable.Value.Year - birthday.Value.Year) * 12 +
                         (nullable.Value.Month - birthday.Value.Month) ==
                         3 && birthday.Value.Day <= nullable.Value.Day)
                {
                    str = birthday.Value.Day > nullable.Value.Day
                        ? ((nullable.Value.Year - birthday.Value.Year) * 12 +
                            (nullable.Value.Month - birthday.Value.Month) - 1) + "个月"
                        : ((nullable.Value.Year - birthday.Value.Year) * 12 +
                           (nullable.Value.Month - birthday.Value.Month)) + "个月";
                }
                else
                {
                    TimeSpan timeSpan = nullable.Value - birthday.Value;
                    if (timeSpan.TotalDays >= 3.0)
                    {
                        timeSpan = nullable.Value - birthday.Value;
                        str = ((int)timeSpan.TotalDays) + "天";
                    }
                    else
                    {
                        timeSpan = nullable.Value - birthday.Value;
                        if (timeSpan.TotalHours >= 3.0)
                        {
                            timeSpan = nullable.Value - birthday.Value;
                            str = ((int)timeSpan.TotalHours) + "小时";
                        }
                        else
                        {
                            timeSpan = nullable.Value - birthday.Value;
                            str = ((int)timeSpan.TotalMinutes) + "分钟";
                        }
                    }
                }

                Age = str;
                return this;
            }
            catch (Exception)
            {
                Age = "";
                return this;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// CS版急诊分诊患者信息Dto
    /// </summary>
    public class PatientVisitDto
    {
        /// <summary>
        /// 患者唯一标识
        /// </summary>
        [Description("患者唯一标识")]
        public string PatientID { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [Description("患者姓名")]
        public string PatientName { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        [Description("患者性别")]
        public string Sex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [Description("出生日期")]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>

        [Description("身份证号")]
        public string IndentityNo { get; set; }

        /// <summary>
        /// 姓名拼音
        /// </summary>
        [Description("姓名拼音")]
        public string NamePhonetic { get; set; }

        /// <summary>
        /// 出生地（通用字典）
        /// </summary>
        [Description("出生地（通用字典）")]
        public string BirthPlace { get; set; }

        /// <summary>
        /// 出生地代码
        /// </summary>
        [Description("出生地代码")]
        public string BirthPlaceCode { get; set; }

        /// <summary>
        /// 费别
        /// </summary>
        [Description("费别")]
        public string ChargeType { get; set; }

        /// <summary>
        /// 身份
        /// </summary>
        [Description("身份")]
        public string Identity { get; set; }

        /// <summary>
        /// 住址（通讯地址）
        /// </summary>
        [Description("住址（通讯地址）")]
        public string Address { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [Description("邮编")]
        public string ZipCode { get; set; }

        /// <summary>
        /// 家庭电话
        /// </summary>
        [Description("家庭电话")]
        public string PhoneNumberHome { get; set; }

        /// <summary>
        /// 合同单位
        /// </summary>
        [Description("合同单位")]
        public string UnitInContract { get; set; }

        /// <summary>
        /// 合同单位代码
        /// </summary>
        [Description("合同单位代码")]
        public string UnitInContractCode { get; set; }


        [Description("单位电话号码")] public string PhoneNumberBusiness { get; set; }


        [Description("紧急联系人")] public string ContactPerson { get; set; }


        [Description("联系电话")] public string ContactPhone { get; set; }


        [Description("与联系人关系")] public string RelationShip { get; set; }


        [Description("联系人地址")] public string ContactPersonAdd { get; set; }


        [Description("联系人邮编")] public string ContactPersonZipCode { get; set; }


        [Description("民族")] public string Nation { get; set; }


        [Description("民族代码")] public string NationCode { get; set; }


        [Description("国家")] public string Country { get; set; }


        [Description("国家代码")] public string CountryCode { get; set; }


        [Description("照片")] public Byte[] Photo { get; set; }


        [Description("门诊卡（就诊卡）卡号")] public string ClinicCardNo { get; set; }


        [Description("医保卡卡号")] public string MedicalCardNo { get; set; }


        [Description("备用字段1")] public string Addtional1 { get; set; }


        [Description("备用字段2")] public string Addtional2 { get; set; }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid PVID { get; set; }

        /// <summary>
        /// 就诊标识
        /// </summary>
        public string VisitID { get; set; }

        /// <summary>
        /// 周岁年龄
        /// </summary>
        public int AgeYears
        {
            get { return GetAgeYears(BirthDate, VisitDate); }
        }

        /// <summary>
        /// 年龄（精确到天）
        /// </summary>
        public string Age
        {
            get { return GetAge(BirthDate, VisitDate); }
        }

        /// <summary>
        /// 年龄
        /// </summary>
        public string NewAge { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string NewBirthDate { get; set; }


        /// <summary>
        /// 挂号时间
        /// </summary>
        public DateTime? RegisterDT { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 来院方式
        /// </summary>
        public string RegisterFrom { get; set; }

        /// <summary>
        /// 就诊时间
        /// </summary>
        public DateTime? VisitDate { get; set; }


        /// <summary>
        /// 更新标志
        /// </summary>
        public int UpdateSign { get; set; }

        /// <summary>
        /// 挂号编号
        /// </summary>
        public string RegisterNo { get; set; }


        public string Organization { get; set; }

        /// <summary>
        /// 绿色通道
        /// </summary>
        public string GreenRoad { get; set; }


        /// <summary>
        /// 重点病种
        /// </summary>
        public string ImportantDisease { get; set; }

        /// <summary>
        /// 其他
        /// </summary>
        public string SpecialSign { get; set; }

        /// <summary>
        /// 群伤标记
        /// </summary>
        public Guid BulkinjuryID { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        public string BedNo { get; set; }


        public DateTime? HappenDate { get; set; }

        /// <summary>
        /// 是否退号
        /// </summary>
        public bool IsBackNum { get; set; }

        /// <summary>
        /// 拟诊
        /// </summary>
        public string Examination { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public string Balance { get; set; }

        /// <summary>
        /// 体征
        /// </summary>
        public string VitalSign { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        public string Temp { get; set; }

        /// <summary>
        /// 心率  
        /// </summary>
        public string HeartRate { get; set; }

        /// <summary>
        /// 呼吸
        /// </summary>
        public string BreathRate { get; set; }

        /// <summary>
        /// 收缩压
        /// </summary>
        public string SDP { get; set; }

        /// <summary>
        /// 舒张压
        /// </summary>
        public string SBP { get; set; }

        /// <summary>
        /// 血氧
        /// </summary>
        public string SPO2 { get; set; }

        /// <summary>
        /// 体征备注
        /// </summary>
        public string VitalSignMemo { get; set; }

        /// <summary>
        /// 发热筛查
        /// </summary>
        public string FeverScreening { get; set; }

        /// <summary>
        /// 分诊正误
        /// </summary>
        public string TriageError { get; set; }

        /// <summary>
        /// 接诊医师
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 接诊科室
        /// </summary>
        public string ToDeptName { get; set; }

        /// <summary>
        /// 意识
        /// </summary>
        public string Mind { get; set; }

        public int GetAgeYears(DateTime? birth, DateTime? visit)
        {
            if (birth == null || !birth.HasValue ||
                visit == null || !visit.HasValue ||
                visit.Value < birth.Value) return 0;

            TimeSpan ts = visit.Value.Subtract(birth.Value);
            if (ts.Days > 365)
                return ts.Days / 365;
            return 0;
        }

        public string GetAge(DateTime? birth, DateTime? visit)
        {
            if (birth == null || !birth.HasValue || visit == null || !visit.HasValue || visit.Value < birth.Value)
            {
                return "";
            }

            string age = string.Empty;

            try
            {
                #region 以3为界限(已注释，采用和主程序以6为界限标准)

                //*<3小时         显示分钟  如：125分钟
                //3小时<*<3天     显示小时  如：48小时
                //3天<*<3个月     显示天    如：60天
                //3个月<*<3岁     显示月    如：24个月
                //*>3岁           显示岁    如：5岁，87岁等。

                //if ((visit.Value.Year - birth.Value.Year) > 3 || ((visit.Value.Year - birth.Value.Year) == 3 && (birth.Value.Month <= visit.Value.Month)))//大于等于三岁，显示岁
                //{
                //    if (birth.Value.Month <= visit.Value.Month) //足月
                //    {
                //        age = (visit.Value.Year - birth.Value.Year).ToString() + "岁";
                //    }
                //    else
                //    {
                //        age = (visit.Value.Year - birth.Value.Year - 1).ToString() + "岁";
                //    }
                //}
                //else if (((visit.Value.Year - birth.Value.Year) * 12 + (visit.Value.Month - birth.Value.Month)) > 3
                //    || ((((visit.Value.Year - birth.Value.Year) * 12 + (visit.Value.Month - birth.Value.Month)) == 3) 
                //    && (birth.Value.Day <= visit.Value.Day)) ) //大于等于三个月，显示月
                //{
                //    if (birth.Value.Day <= visit.Value.Day) //足日
                //    {
                //        age = ((visit.Value.Year - birth.Value.Year) * 12 + (visit.Value.Month - birth.Value.Month)).ToString() + "个月";
                //    }
                //    else
                //    {
                //        age = ((visit.Value.Year - birth.Value.Year) * 12 + (visit.Value.Month - birth.Value.Month) - 1).ToString() + "个月";
                //    }
                //}
                //else if ((visit.Value - birth.Value).TotalDays >= 3) //大于等于三天，显示天
                //{
                //    age = ((int)(visit.Value - birth.Value).TotalDays).ToString() + "天";
                //}
                //else if ((visit.Value - birth.Value).TotalHours >= 3) //大于等于三小时，显示小时
                //{
                //    age = ((int)(visit.Value - birth.Value).TotalHours).ToString() + "小时";
                //}
                //else //小于三小时，显示分钟
                //{
                //    age = ((int)(visit.Value - birth.Value).TotalMinutes).ToString() + "分钟";
                //}

                #endregion

                //大于6岁的只显示xx岁
                //1至6岁之间的显示x岁x月
                //6至12个月之间的显示x月
                //1至6个月之间的显示x月x天
                //1天至一个月之间的显示x天
                //0至1天之间得显示x小时

                int months = (visit.Value.Year - birth.Value.Year) * 12 + (visit.Value.Month - birth.Value.Month);
                int hour = visit.Value.Hour - birth.Value.Hour;
                int day = visit.Value.Day - birth.Value.Day;

                int year = months / 12;
                int month = months % 12;

                if (year >= 6) // 大于6岁
                {
                    age = year + "岁";
                }
                else if (year >= 1) //1岁到6岁之间
                {
                    age = year + "岁" + month + "个月";

                    if (month == 0) // 刚好在生日月
                    {
                        age = year + "岁";
                    }
                }
                else if (month >= 6) // 6个月到12个月之间
                {
                    age = month + "个月";
                }
                else if (month >= 1) // 1个月到6个月之间
                {
                    if (day < 0) // 不足月
                    {
                        int count;

                        switch (birth.Value.Month)
                        {
                            case 1:
                            case 3:
                            case 5:
                            case 7:
                            case 8:
                            case 10:
                            case 12:
                                count = 31;
                                break;

                            case 2:

                                if (birth.Value.Year % 4 == 0 && birth.Value.Year % 100 != 0)
                                {
                                    count = 29;
                                }
                                else
                                {
                                    count = 28;
                                }

                                break;

                            default:
                                count = 30;
                                break;
                        }

                        day = count - birth.Value.Day + visit.Value.Day;

                        age = day + "天";
                    }
                    else if (day > 0)
                    {
                        age = month + "个月" + day + "天";
                    }
                    else
                    {
                        age = month + "个月";
                    }
                }
                else if (day >= 1) // 1天到1个月之间
                {
                    age = day + "天";
                }
                else
                {
                    age = hour + "小时";
                }
            }
            catch
            {
            }

            return age;
        }

        /// <summary>
        /// RFID
        /// </summary>

        public string RFID { get; set; }

        /// <summary>
        /// 体重
        /// </summary>

        public string Weight { get; set; }


        /// <summary>
        /// 急诊分诊判定依据Dto
        /// </summary>
        public TriageAccordingRecordDto AccordingRecord { get; set; }

        /// <summary>
        /// 生命体征
        /// </summary>
        public TriageVitalSignRecordDto VitalSignRecord { get; set; }

        /// <summary>
        /// 分诊记录
        /// </summary>
        public TriageRecordDto TriageRecord { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        public List<ScoreRecordDto> ScoreRecords { get; set; }


        /// <summary>
        /// 删除标志：0：已删除；1：未删除
        /// </summary>
        public int IsDelete { get; set; } = 1;
    }
}
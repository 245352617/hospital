using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 根据输入项获取或创建患者病历号Dto
    /// </summary>
    public class CreateOrGetPatientIdInput
    {
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityNo { get; set; }

        /// <summary>
        /// 证件类型 
        /// 1: 就诊卡
        /// 2: 居民身份证
        /// 当类型为2时，配合证件类型编码判断证件
        /// </summary>
        public int CardType { get; set; } = 2;

        /// <summary>
        /// 证件类型编码
        /// </summary>
        public string IdTypeCode { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }
        
        /// <summary>
        /// 医保卡号
        /// </summary>
        public string MedicalNo { get; set; }

        /// <summary>
        /// 挂号流水号
        /// </summary>
        public string RegisterNo { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// 患者出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 患者年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactsPerson { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 联系人地址
        /// </summary>
        public string ContactsAddress { get; set; }

        /// <summary>
        /// 患者身份
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        /// 是否为三无患者
        /// </summary>
        public bool IsNoThree { get; set; } = false;

        /// <summary>
        /// 是否婴幼儿
        /// </summary>
        public bool IsInfant { get; set; } = false;

        /// <summary>
        /// 任务单Id
        /// </summary>
        public Guid TaskInfoId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNum { get; set; }

        /// <summary>
        /// 病历服务患者病历基本信息主键Id
        /// </summary>
        public Guid EmrPatientInfoId { get; set; }

        /// <summary>
        /// 发病时间
        /// </summary>
        public DateTime? OnsetTime { get; set; }

        /// <summary>
        /// RFID
        /// </summary>
        public string RFID { get; set; }

        /// <summary>
        /// 开始分诊时间
        /// </summary>
        public DateTime? StartTriageTime { get; set; }

        /// <summary>
        /// 费别
        /// </summary>
        public string ChargeType { get; set; }

        /// <summary>
        /// 是否需要同步到急救病历
        /// </summary>
        public bool IsSyncToEmrService { get; set; } = true;

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
        public string GuardianIdTypeCode { get; set; }

        /// <summary>
        /// 与联系人关系
        /// </summary>
        public string SocietyRelationCode { get; set; }

        /// <summary>
        /// 人群编码
        /// </summary>
        public string CrowdCode { get; set; }

        public string CrowdName { get; set; }
        
        /// <summary>
        /// 孕周
        /// </summary>
        public int? GestationalWeeks { get; set; }


        /// <summary>
        /// 检查名族是否完整
        /// </summary>
        /// <returns></returns>
        public CreateOrGetPatientIdInput CheckNationIsFull()
        {
            if (Nation.IsNullOrWhiteSpace()) return this;
            if (!Nation.EndsWith("族"))
            {
                Nation += "族";
            }

            return this;
        }
        
    }
}
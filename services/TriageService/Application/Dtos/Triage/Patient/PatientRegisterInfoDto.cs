using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class PatientRegisterInfoDto
    {
        /// <summary>
        /// Id 不需要前端传值
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 任务单号
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
        /// 患者唯一标识(HIS)
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        public string Sex { get; set; }

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
        /// 发病时间
        /// </summary>
        public DateTime? OnsetTime { get; set; }

        /// <summary>
        /// 绿色通道
        /// </summary>
        public string GreenRoad { get; set; }

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
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 患者姓名拼音码
        /// </summary>
        public string Py { get; set; }

        /// <summary>
        /// 重点病种
        /// </summary>
        /// <returns></returns>
        public string DiseaseCode { get; set; }

        /// <summary>
        /// 开始分诊时间
        /// </summary>
        public DateTime? StartTriageTime { get; set; }

        /// <summary>
        /// 分诊时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 患者基本信息是否不可编辑
        /// 患者信息需与 HIS 同步，所以当患者信息与HIS同步后基本信息不再允许修改
        /// 即挂号之后信息便不允许再次修改（目前没有HIS修改患者的接口）
        /// by: ywlin 2021-11-29
        /// </summary>
        public bool IsBasicInfoReadOnly { get; set; }

        /// <summary>
        /// 新冠问卷是否从外部获取
        /// 当从外部获取新冠问卷时，不在ECIS中录入个人史信息
        /// </summary>
        public bool IsCovidExamFromOuterSystem { get; set; }

        /// <summary>
        /// 挂号信息
        /// </summary>
        public RegisterInfoDto RegisterInfo { get; set; }
    }
}
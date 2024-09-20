using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 消息体
    /// </summary>
    public class TransBodyEntity
    {
        /// <summary>
        /// 门诊号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string IdCardNo { get; set; }

        /// <summary>
        /// 卡类型
        /// </summary>
        public string IdCardType { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// 结算类别编码
        /// </summary>
        public string BalanceType { get; set; }

        /// <summary>
        /// 结算类别名称
        /// </summary>
        public string BalanceTypeName { get; set; }

        /// <summary>
        /// 医疗证号
        /// </summary>
        public string MedicalNo { get; set; }

        /// <summary>
        /// 支付类型
        /// </summary>
        public string PayMode { get; set; }

        /// <summary>
        /// EMPI
        /// </summary>
        public string Empi { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string TelPhone { get; set; }

        /// <summary>
        /// 挂号级别代码
        /// </summary>
        public string RegLevelCode { get; set; }

        /// <summary>
        /// 挂号级别名称
        /// </summary>
        public string RegLevelName { get; set; }

        /// <summary>
        /// 挂号医生编码
        /// </summary>
        public string RegDoctCode { get; set; }


        /// <summary>
        /// 挂号医生名称
        /// </summary>
        public string RegDoctName { get; set; }

        /// <summary>
        /// 挂号科室代码
        /// </summary>
        public string RegDeptCode { get; set; }

        /// <summary>
        /// 挂号科室名称
        /// </summary>
        public string RegDeptName { get; set; }

        /// <summary>
        /// 预约流水号
        /// </summary>
        public string BookNo { get; set; }

        /// <summary>
        /// 预约开始时间
        /// </summary>
        public string BookBeginTime { get; set; }

        /// <summary>
        /// 预约结束时间
        /// </summary>
        public string BookEndTime { get; set; }

        /// <summary>
        /// 预约时段ID
        /// </summary>
        public string SchemaNo { get; set; }


        /// <summary>
        /// 是否境外入深
        /// </summary>
        public string IsOtherIn { get; set; }

        /// <summary>
        /// 异地参保地
        /// </summary>
        public string OtherSi { get; set; }

        /// <summary>
        /// 当前地址
        /// </summary>
        public string CurrentAddress { get; set; }

        /// <summary>
        /// 业务来源 1.门诊 2.体检 3.绿通 4.急诊 5.免费 6.义诊
        /// </summary>
        public string WorkType { get; set; }
    }
}
using System;

namespace Szyjian.Ecis.Patient
{
    /// <summary>
    /// 患者视图
    /// </summary>
    public class V_JHJK_HZLB
    {
        /// <summary>
        /// 患者PatientID
        /// </summary>
        public decimal? PatientID { get; set; }

        /// <summary> 
        /// 患者挂号序号
        /// </summary>
        public decimal? GHXH { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        public decimal? Sex { get; set; }

        /// <summary>
        /// 患者出生日期
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// 是否核酸检测
        /// </summary>
        public string HasCovid19Exam { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        public DateTime? GHSJ { get; set; }

        /// <summary>
        /// 队列ID，1909是急诊
        /// </summary>
        public decimal? DLID { get; set; }

        /// <summary>
        /// 挂号科室
        /// </summary>
        public string GHKSDM { get; set; }

        /// <summary>
        /// 科室实际编号
        /// </summary>
        public decimal? KSDM { get; set; }

        /// <summary>
        /// 医生编号
        /// </summary>
        public string YSDM { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string YGXM { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime? KSSJ { get; set; }

        /// <summary>
        /// 出科时间
        /// </summary>
        public DateTime? JSSJ { get; set; }

        /// <summary>
        /// 就诊状态  8就诊，9出科
        /// </summary>
        public decimal? JZZT { get; set; }

        /// <summary>
        /// THBZ
        /// </summary>
        public decimal? THBZ { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string SFZH { get; set; }

        /// <summary>
        /// 收费类型编号
        /// </summary>
        public decimal? brxz { get; set; }

        /// <summary>
        /// 收费类型
        /// </summary>
        public string xzmc { get; set; }

        /// <summary>
        /// 户口地址
        /// </summary>
        public string HKDZ { get; set; }

        /// <summary>
        /// 家庭电话
        /// </summary>
        public string JTDH { get; set; }

        /// <summary>
        /// 门诊卡号
        /// </summary>
        public string mzhm { get; set; }

        /// <summary>
        /// fzjb
        /// </summary>
        public decimal? fzjb { get; set; }

        /// <summary>
        /// 绿通挂号
        /// </summary>
        public string ltbz { get; set; }

        /// <summary>
        /// 就诊号码
        /// </summary>
        public string JZHM { get; set; }

        /// <summary>
        /// 病人床号
        /// </summary>
        public string BRCH { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public string WZJB { get; set; }

        /// <summary>
        /// 出科去向
        /// </summary>
        public string CKQX { get; set; }
    }
}

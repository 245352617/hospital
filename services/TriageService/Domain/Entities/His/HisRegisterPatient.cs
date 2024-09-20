using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// HIS 挂号患者列表
    /// JZZT：就诊状态（0 新病人，1 就诊中，2 挂起，8 退号，9 结束就诊）
    /// JZZT = null，THBZ = 1 的也是退号
    /// </summary>
    public class HisRegisterPatient
    {
        /// <summary>
        /// 患者 ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 患者性别（0未知；1男；2女）
        /// </summary>
        public int? Sex { get; set; }

        /// <summary>
        /// 挂号流水号/序号，同一个患者不同时间挂号，其值都不一致
        /// PKU的HIS字段GHXH
        /// </summary>
        public string RegisterNo { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// 挂号时间
        /// PKU的HIS字段GHSJ
        /// </summary>
        public DateTime RegisterTime { get; set; }

        /// <summary>
        /// 队列ID
        /// 对接北大 HIS 系统时，该ID与诊室关联，使用该ID分诊患者到指定诊室
        /// </summary>
        public string QueueId { get; set; }

        /// <summary>
        /// 科室代码（北大急诊科代码：280）
        /// PKU的HIS字段KSDM
        /// </summary>
        public string DepartCode { get; set; }

        /// <summary>
        /// 医生工号
        /// PKU的HIS字段YSDM
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 开始就诊时间
        /// PKU的HIS字段KSSJ
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 结束就诊时间
        /// PKU的HIS字段JSSJ
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 门诊号
        /// PKU的HIS字段MZHM
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// HIS 就诊状态（0 新病人，1 就诊中，2 挂起，8 退号，9 结束就诊）
        /// </summary>
        public int? JZZT { get; set; }

        /// <summary>
        /// 户口地址
        /// PKU的HIS字段HKZD
        /// </summary>
        public string HKDZ { get; set; }

        /// <summary>
        /// 退号状态（0未退号；1已退号）
        /// PKU的HIS字段THBZ
        /// </summary>
        public int RefundStatus { get; set; }

        /// <summary>
        /// 身份证号
        /// PKU的HIS字段SFZH
        /// </summary>
        public string IdentityNo { get; set; }

        /// <summary>
        /// 费别Code
        /// PKU的HIS字段BRXZ
        /// </summary>
        public string ChargeType { get; set; }

        /// <summary>
        /// 费别名称
        /// PKU的HIS字段XZMC
        /// </summary>
        public string ChargeTypeName { get; set; }

        /// <summary>
        /// 手机号码
        /// PKU的HIS字段JTDH
        /// </summary>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 分诊级别
        /// PKU的HIS字段FZJB
        /// </summary>
        public int? TriageLevel { get; set; }

        /// <summary>
        /// 就诊医生名称
        /// PKU的HIS字段YGXM
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 发票号
        /// PKU的HIS字段JZHM
        /// </summary>
        public string InvoiceNum { get; set; }

        /// <summary>
        /// 扩展字段（诊断）
        /// PKU的HIS字段ZDMC
        /// </summary>
        public string ExtendField1 { get; set; }

        #region 医保控费相关
        /// <summary>
        /// 参保人标识
        /// </summary>
        public string PatnId { get; set; }

        /// <summary>
        /// 当前就诊标识
        /// </summary>
        public string CurrMDTRTId { get; set;}

        /// <summary>
        /// 统筹区编码
        /// </summary>
        public string PoolArea { get; set;}

        /// <summary>
        /// 险种类型
        /// 310职工基本医疗保险；390城乡居民基本医疗保险；320公务员医疗补助；392城乡居民大病医疗保险；330大额医疗费用补助；510生育保险；340离休人员医疗保障；
        /// </summary>
        public string InsureType { get; set; }

        /// <summary>
        /// 异地结算标志 0否;1是
        /// </summary>
        public string OutSetlFlag { get; set; }
        #endregion
    }
}
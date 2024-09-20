using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 病人卡片总计
    /// </summary>
    public class PatientCardSumDto
    {
        /// <summary>
        /// 总床位
        /// </summary>
        public int BedSum { get; set; }

        /// <summary>
        /// 当前在科
        /// </summary>
        public int SectionSum { get; set; }

        /// <summary>
        /// 今日转入
        /// </summary>
        public int IndeptSum { get; set; }

        /// <summary>
        /// 今日转出
        /// </summary>
        public int OutdeptSum { get; set; }

        /// <summary>
        /// 死亡
        /// </summary>
        public int DeathSum { get; set; }

        /// <summary>
        /// 病危
        /// </summary>
        public int Criticallyill { get; set; }

        /// <summary>
        /// 病重
        /// </summary>
        public int Seriouslyill { get; set; }

        /// <summary>
        /// 一级护理
        /// </summary>
        public int GradeOneNursing { get; set; }

        /// <summary>
        /// 特级护理
        /// </summary>
        public int GradeZeroNursing { get; set; }

        /// <summary>
        /// 出科未确认人员总数
        /// </summary>
        public int Outdepts { get; set; }

        public List<PatientCardDto> PatientCard { get; set; }
    }
    /// <summary>
    /// 病人卡片信息
    /// </summary>
    public class PatientCardDto
    {
        /// <summary>
        /// 危重程度(0：其他，1：病危，2：病重)
        /// </summary>
        public int? CriticaDegree { get; set; }

        /// <summary>
        /// 床位号
        /// </summary>
        public string BedNum { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string InHosId { get; set; }

        /// <summary>
        /// 病人ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNum { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime? InDeptTime { get; set; }

        /// <summary>
        /// 入科天数
        /// </summary>
        public int Indays { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>
        public string ClinicDiagnosis { get; set; }

        /// <summary>
        /// 主管医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 责任护士名称
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 有危急值(同步检验接口，当天班次内有危急值)
        /// </summary>
        public bool CriticalValue { get; set; }

        /// <summary>
        /// 新入科
        /// </summary>
        public bool NewIndept { get; set; }

        /// <summary>
        /// 是否医生确认
        /// </summary>
        public bool IsDoctorConfirm { get; set; }

        /// <summary>
        /// 是否入科评估
        /// </summary>
        public bool IsIndeptAssess { get; set; }

        /// <summary>
        /// 跌倒
        /// </summary>
        public bool Tumble { get; set; }

        /// <summary>
        /// 压疮
        /// </summary>
        public bool PressureSores { get; set; }

        /// <summary>
        /// 脱管
        /// </summary>
        public bool Detached { get; set; }

        /// <summary>
        /// 误吸
        /// </summary>
        public bool Aspiration { get; set; }

        /// <summary>
        /// 过敏
        /// </summary>
        public bool Allergy { get; set; }

        /// <summary>
        /// 今术（今日班次有手术）
        /// </summary>
        public bool Surgery { get; set; }

        /// <summary>
        /// 监护仪
        /// </summary>
        public bool Monitor { get; set; }

        /// <summary>
        /// 呼吸机
        /// </summary>
        public bool BreathMachine { get; set; }

        /// <summary>
        /// 输液(微量泵)
        /// </summary>
        public bool Infusion { get; set; }

        /// <summary>
        /// APACHE II评分
        /// </summary>
        public decimal Apache { get; set; }

        /// <summary>
        /// GCS评分
        /// </summary>
        public decimal Gcs { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string IdentityType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string IdentityCard { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
    }
}

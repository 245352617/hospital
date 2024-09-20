using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 医生交班
    /// </summary>
    public class IcuDoctorHandOverDto
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 新人数
        /// </summary>
        public int News { get; set; }

        /// <summary>
        /// 转科数
        /// </summary>
        public int Transfers { get; set; }

        /// <summary>
        /// 出院数
        /// </summary>
        public int Dischargeds { get; set; }

        /// <summary>
        /// 死亡数
        /// </summary>
        public int Deaths { get; set; }

        /// <summary>
        /// 危重人数
        /// </summary>
        public int Criticallyill { get; set; }

        /// <summary>
        /// 手术人数
        /// </summary>
        public int Surgery { get; set; }

        /// <summary>
        /// 患者信息列表
        /// </summary>
        public List<IcuPatientsDto> icuPatientsDtos { get; set; }
    }

    /// <summary>
    /// 交班患者信息列表
    /// </summary>
    public class IcuPatientsDto
    {
        /// <summary>
        /// 患者流水号
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string InHosId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNum { get; set; }

        /// <summary>
        /// 病人ID
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        public string BedNum { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 是否交班，0：待交班，1：已交班
        /// </summary>
        public bool IsHandOver { get; set; }

        /// <summary>
        /// /// <summary>
        /// 临床诊断
        /// </summary>
        /// </summary>
        public string ClinicDiagnosis { get; set; }

        /// <summary>
        /// 状态,0：其他（不展示），1：出院，2：转出，3：死亡，4：新入
        /// </summary>
        public List<int?> Status { get; set; }

        /// <summary>
        /// 交班信息列表
        /// </summary>
        public List<IcuPatientHandOverDto> icuPatientHandOverDtos { get; set; }
    }

    /// <summary>
    ///  交班信息列表
    /// </summary>
    public class IcuPatientHandOverDto
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 患者流水号
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string InHosId { get; set; }

        /// <summary>
        /// 采集器(绑定床位)的编号
        /// </summary>

        public string BedNum { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>

        public string PatientName { get; set; }

        /// <summary>
        /// 主管医生
        /// </summary>

        public string DoctorCode { get; set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        public string ScheduleName { get; set; }

        /// <summary>
        /// 交班内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 交接时间，未交接则为空
        /// </summary>
        public string HandOverTime { get; set; }

        /// <summary>
        /// 交接时间，未交接则为空
        /// </summary>
        public string ScheduleTime { get; set; }

        /// <summary>
        /// 交班医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 交班签名
        /// </summary>
        public string SignImage { get; set; }

        /// <summary>
        /// 接班签名
        /// </summary>
        public string SuccessorSignImage { get; set; }
    }
}
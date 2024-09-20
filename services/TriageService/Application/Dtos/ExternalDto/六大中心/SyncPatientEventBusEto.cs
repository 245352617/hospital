using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.EventBus;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 六大中心同步患者事务总线Dto
    /// </summary>
    [EventName("SynchPatientEvents")]
    public class SyncPatientEventBusEto
    {
        /// <summary>
        /// 病患主键Id
        /// </summary>
        public Guid Pid { get; set; }

        /// <summary>
        /// 病患号
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string RegisterNo { get; set; }

        /// <summary>
        /// 病患类型，绿道 未知 = -1,卒中 = 0,胸痛 = 1,创伤 = 2,中毒 = 3,孕产妇 = 4,危重新生儿 = 5,
        /// </summary>
        public string WebSite { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 性别 未知=-1,男=0,女=1
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 性别，男，女，未知
        /// </summary>
        public string SexVal { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 手环id
        /// </summary>
        public string RFID { get; set; }

        /// <summary>
        /// 发病时间
        /// </summary>
        public DateTime? OnsetTime { get; set; }

        /// <summary>
        /// 到院时间
        /// </summary>
        public DateTime? ArrivalTime { get; set; }
        
        /// <summary>
        /// 病患所属科室Id
        /// </summary>
        public string DepId { get; set; } = "2000";

        /// <summary>
        /// 病患所属科室名称
        /// </summary>
        public string DepName { get; set; } = "急诊科";

        /// <summary>
        /// 来院方式. 其他 = -2,未知 = -1,本院120 = 0,外院120 = 1,自行来院 = 2,院内发病 = 3,外院转入 = 4
        /// </summary>
        public string ToHospitalWayCode { get; set; }

        /// <summary>
        /// 分诊等级 未分级 = -1,I级 = 0,II级 = 1,III级 = 2,IVa级 = 3,IVb级 = 4
        /// </summary>
        public string ActTriageLevel { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 病患手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 初步诊断
        /// </summary>
        public string PreDiagnose { get; set; }

        /// <summary>
        /// 治疗类型
        /// </summary>
        public string CureType { get; set; }

        /// <summary>
        /// 就诊次数
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        public string Temperature { get; set; }

        /// <summary>
        /// 生命体征-心率(次/min)
        /// </summary>
        public string HeartRate { get; set; }
        
        /// <summary>
        /// 生命体征-呼吸
        /// </summary>
        public string Breathing { get; set; }
        
        /// <summary>
        /// 生命体征-左上支最低血压收缩压，mmHg
        /// </summary>
        public string LSBloodPressureMax { get; set; }
        
        /// <summary>
        /// 生命体征-右上支最低血压收缩压，mmHg
        /// </summary>
        public string RSBloodPressureMax { get; set; }
        
        /// <summary>
        /// 生命体征-左上支最低血压舒张压，mmHg
        /// </summary>
        public string LSBloodPressureMin { get; set; }
        
        /// <summary>
        /// 生命体征-右上支最低血压舒张压，mmHg
        /// </summary>
        public string RSBloodPressureMin { get; set; }
        
        /// <summary>
        /// 生命体征-血氧饱和度
        /// </summary>
        public string SaO2 { get; set; }

        /// <summary>
        /// 患者来源平台 院前，急诊
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 任务单Id（院前患者）
        /// </summary>
        public Guid TaskId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNum { get; set; }

        /// <summary>
        /// 群伤事件Id
        /// </summary>
        public Guid GroupInjuryInfoId { get; set; }

        /// <summary>
        /// 患者Tag
        /// </summary>
        public List<string> PatientTags { get; set; } = new List<string>();

        /// <summary>
        /// 添加患者群伤Tag
        /// </summary>
        /// <returns></returns>
        public SyncPatientEventBusEto SetGroupInjury()
        {
            if (GroupInjuryInfoId != Guid.Empty)
            {
                PatientTags.Add("群伤");
            }
            
            return  this;
        }
    }
}
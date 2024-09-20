using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 实现门诊患者生命体征数据的保存功能
    /// 龙岗中心设备对接生命体征接口
    /// </summary>
    public class MZPatientVitalSignInfo
    {
        /// <summary>
        /// 门诊号码
        /// </summary>
        public string OPID { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public string Jlsj { get; set; }

        /// <summary>
        /// 体温,（float类型）单位：摄氏度
        /// </summary>
        public float Tw { get; set; }

        /// <summary>
        /// 呼吸,（int类型）单位：BPM
        /// </summary>
        public int Hx { get; set; }

        /// <summary>
        /// 脉搏,（int类型）单位：BPM
        /// </summary>
        public int Mb { get; set; }

        /// <summary>
        /// 血氧,（int类型）单位：%
        /// </summary>
        public int Spo2 { get; set; }

        /// <summary>
        /// 收缩压,（int类型）单位：mmHg
        /// </summary>
        public int Ssy { get; set; }

        /// <summary>
        /// 舒张压,（int类型）单位：mmHg
        /// </summary>
        public int Szy { get; set; }

        /// <summary>
        /// 平均压,（int类型）单位：mmHg
        /// </summary>
        public int Pjy { get; set; }

        /// <summary>
        /// 血糖,（float类型）单位：mmol/L
        /// </summary>
        public float Xt { get; set; }

        /// <summary>
        /// 血糖类型，（int类型）（包括0：随机；1：空腹；2：早餐前；3：早餐后2h；4、午餐前；5、午餐后2h；6、晚餐前；7、晚餐后2h；8、睡前；-1：无）
        /// </summary>
        public int XtType { get; set; }
    }
}

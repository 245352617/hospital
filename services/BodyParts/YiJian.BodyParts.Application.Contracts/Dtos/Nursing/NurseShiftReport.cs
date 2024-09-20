using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos {
    /// <summary>
    /// 护士交班报告返回参数
    /// </summary>
    public class NurseShiftReport {
        public Guid Id { get; set; }

        /// <summary>
        /// 签名图片
        /// </summary>
        public string SignImage { get; set; }

        /// <summary>
        /// 二级签名图片
        /// </summary>
        public string SignImage2 { get; set; }

        /// <summary>
        /// 三级签名图片
        /// </summary>
        public string SignImage3 { get; set; }

        /// <summary>
        /// 交接签名Id
        /// </summary>
        public Guid HandOverId { get; set; }

        /// <summary>
        /// a1->p1签名图片
        /// </summary>
        public string ASignImage { get; set; }

        /// <summary>
        /// n1->p1签名图片
        /// </summary>
        public string PSignImage { get; set; }

        /// <summary>
        /// p1->a1签名图片
        /// </summary>
        public string NSignImage { get; set; }

        /// <summary>
        /// 签名控件按钮数
        /// </summary>
        public int ControlsNumber { get; set; }

        /// <summary>
        /// 表头内容
        /// </summary>
        public List<TableHeader> tableHeaders { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public Dictionary<string, string> title { get; set; }

        /// <summary>
        /// 患者信息
        /// </summary>
        public List<PatientType> patientTypes { get; set; }

        /// <summary>
        /// 护理内容
        /// </summary>
        public List<NursingContent> NursingContents { get; set; }
    }
    public class TableHeader {
        public string Number { get; set; }
        public string OutHospital { get; set; }
        public string Critically { get; set; }
    }
    public class PatientType {
        /// <summary>
        /// 类别
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 床位号
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
        /// 临床诊断
        /// </summary>
        public string ClinicDiagnosis { get; set; }

        /// <summary>
        /// 交班内容
        /// </summary>
        public Dictionary<string, string> texts { get; set; }
    }

    public class NursingContent {
        /// <summary>
        /// 未完成输液的患者
        /// </summary>
        public string NotCompleteInfusion { get; set; }
        /// <summary>
        /// 机械通气患者
        /// </summary>
        public string MechanicallyVentilated { get; set; }
        /// <summary>
        /// 动脉置管
        /// </summary>
        public string ArteryPlacePipe { get; set; }
        /// <summary>
        /// 深静脉置管
        /// </summary>
        public string DeepVeinCatheterization { get; set; }
        /// <summary>
        /// 鼻饲护理
        /// </summary>
        public string Nasogastric { get; set; }
        /// <summary>
        /// 高流速氧疗
        /// </summary>
        public string HighFlowRateOxygen { get; set; }
        /// <summary>
        /// 约束护理
        /// </summary>
        public string ConstraintNursing { get; set; }
        /// <summary>
        /// 压疮护理
        /// </summary>
        public string PressureUlcer { get; set; }
        /// <summary>
        /// 吸氧护理
        /// </summary>
        public string Oxygen { get; set; }
        /// <summary>
        /// 留置引流管护理
        /// </summary>
        public string IndwellingDrainage { get; set; }
        /// <summary>
        /// 气管插管
        /// </summary>
        public string TracheaCannula { get; set; }
        /// <summary>
        /// 气管切开
        /// </summary>
        public string Tracheotomy { get; set; }
        /// <summary>
        /// 鼻肠管
        /// </summary>
        public string NasointestinalTube { get; set; }
        /// <summary>
        /// 接触隔离
        /// </summary>
        public string ContactIsolation { get; set; }
        /// <summary>
        /// 飞沫隔离
        /// </summary>
        public string DropletIsolation { get; set; }
        /// <summary>
        /// 空气隔离
        /// </summary>
        public string AirIsolation { get; set; }
        /// <summary>
        /// 保护性隔离
        /// </summary>
        public string ProtectiveIsolation { get; set; }
    }
}

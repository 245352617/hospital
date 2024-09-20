using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital
{
    /// <summary>
    /// 生命体征登记
    /// 目前使用：龙岗中心
    /// </summary>
    public class PatientVitalSign
    {
        /// <summary>
        /// 就诊类型
        /// 1门急诊；2住院； 3体检
        /// </summary>
        public string patientType { get; set; } = "1";

        /// <summary>
        /// 就诊号
        /// </summary>
        public string visitNo { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string visitSerialNo { get; set; }

        /// <summary>
        /// 挂号流水号
        /// </summary>
        public string regSerialNo { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// 患者性别
        /// 1男；2女；3未知；9不明；
        /// </summary>
        public int patientGender { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string deptName { get; set; }

        /// <summary>
        /// 记录人代码
        /// </summary>
        public string recorderCode { get; set; }

        /// <summary>
        /// 记录人姓名
        /// </summary>
        public string recorderName { get; set; }

        /// <summary>
        /// 体征时间
        /// 格式:yyyy-mm-dd hh24:mi:ss
        /// </summary>
        public string regDatetime { get; set; }

        /// <summary>
        /// 生命体征列表
        /// </summary>
        public List<SignDetailReq> signDetailReqs { get; set; }
    }

    public class SignDetailReq
    {
        /// <summary>
        /// 项目代码
        /// 1.体温：数字，可带1位小数。
        /// 2.体重：数字，可带2位小数。
        /// 3.收缩压：数字。
        /// 4.舒张压：数字。
        /// 5.心率：数字。
        /// 6.脉搏：数字。
        /// 7.呼吸：数字。
        /// 8.血氧：数字，可带1位小数。
        /// 9.血糖：数字。
        /// 10.意识：清醒、烦躁、嗜睡、昏睡、浅昏迷、深昏迷
        /// 11.病情等级：整数。
        /// 12.分区：整数。
        /// 13.血氧饱和度:数字 
        /// 14.是否已做心电图:是、否、拒测
        /// </summary>
        public string itemCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string itemName { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// 体征值
        /// </summary>
        public string itemValue { get; set; }

        /// <summary>
        /// 体征值名称
        /// </summary>
        public string itemValueName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 体征唯一码
        /// </summary>
        public string signId { get; set; }
    }
}

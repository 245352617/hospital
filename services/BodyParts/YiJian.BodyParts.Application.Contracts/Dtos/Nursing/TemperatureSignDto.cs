using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 海泰生命体征数据
    /// </summary>
    public class TemperatureSignDto
    {
        /// <summary>
        /// 异常标志（-2：低  -1：偏低  0：正常  1：偏高  2：高）
        /// </summary>
        public string abnomalType { set; get; }

        /// <summary>
        /// 病人床号
        /// </summary>
        public string bedNum { set; get; }

        /// <summary>
        /// 采集时间
        /// </summary>
        public string collectDate { set; get; }

        /// <summary>
        /// 采集组号
        /// </summary>
        public string collectGroupId { set; get; }

        /// <summary>
        /// 采集号
        /// </summary>
        public string collectId { set; get; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public string createtime { set; get; }

        /// <summary>
        /// 记录人员
        /// </summary>
        public string creator { set; get; }

        /// <summary>
        /// 病人科室
        /// </summary>
        public string departmentId { set; get; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string inpatientId { set; get; }

        /// <summary>
        /// 作废标志（0：有效 1：已作废）
        /// </summary>
        public string isEnable { set; get; }

        /// <summary>
        /// 复测标志（0：首测  1：复测）
        /// </summary>
        public string isRetest { set; get; }

        /// <summary>
        /// 体温单显示（0：不显示 1：显示，显示在体温单的‘空白栏’或‘其他’栏内）
        /// </summary>
        public string isTemperatureItem { set; get; }

        /// <summary>
        /// 体征采集项目编号
        /// </summary>
        public string itemCode { set; get; }

        /// <summary>
        /// 体征内容
        /// </summary>
        public string itemContent { set; get; }

        /// <summary>
        /// 项目号
        /// </summary>
        public string itemId { set; get; }

        /// <summary>
        /// 体征采集项目名称
        /// </summary>
        public string itemName { set; get; }

        /// <summary>
        /// 项目下标（体温类型等）PART E：耳温 G：肛温 K：口温（默认）  Y：腋温
        /// </summary>
        public string part { set; get; }

        /// <summary>
        /// 计划标志（0：临时；1：计划）
        /// </summary>
        public string planType { set; get; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string remark { set; get; }

        /// <summary>
        /// 复测关联
        /// </summary>
        public string retestId { set; get; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string sjly { set; get; }

        /// <summary>
        /// 病人病区
        /// </summary>
        public string wardId { set; get; }
    }

    public class request_Date<T>
    {
        public T date { get; set; }
    }

    /// <summary>
    /// 汕大医惠生命体征数据
    /// </summary>
    public class TemperatureSign_SD_Dto
    {
        //示例

        //      "isValid": 1,						----数据有效 1：有效，0：无效
        //"mrn": "00908920",				----住院号
        //"patientId": "5053964",			----病人id
        //"patientName": "张三",			----病人姓名
        //"planTime": "2017-2-5 14:00:00",	----测量点
        //"recordNurseId": "1234",			----录入护士的工号
        //"recordNurseName": "张斯",		----录入护士的姓名
        //"recordTime": "2017-2-5 14:00:00",----数据的录入时间
        //"remark": "",					----备注
        //"series": "1",						----住院次数
        //"unit": "℃",						----体征的单位
        //"vitalsignNVal1": "36.7",			----体征数字值1
        //"vitalsignNVal2": "",				----体征数字值2
        //"vitalsignName": "体温",			----体征名称
        //"vitalsignSVal1": "耳温",			----体征字符串值1
        //"vitalsignSVal2": "",				----体征字符串值2
        //"vitalsignType": "1001",			----体征编码,详见码表部分
        //"wardCode": "0101001"			----病区代码


    //      体征编码 体征说明    单位
    //          1001	体温	℃
    //          1002	脉搏 次/分
    //          1003	心率 次/分  
    //          1004	呼吸 次/分
    //      1005	血压 mmHg

        /// <summary>
        /// 数据有效 1：有效，0：无效
        /// </summary>
        public int isValid { set; get; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string mrn { set; get; }
        /// <summary>
        /// 病人id
        /// </summary>
        public string patientId { set; get; }
        /// <summary>
        /// 病人姓名
        /// </summary>
        public string patientName { set; get; }
        /// <summary>
        /// 测量时间点
        /// </summary>
        public string planTime { set; get; }
        /// <summary>
        /// 录入护士的工号
        /// </summary>
        public string recordNurseId { set; get; }
        /// <summary>
        /// 录入护士的姓名
        /// </summary>
        public string recordNurseName { set; get; }
        /// <summary>
        /// 数据的录入时间
        /// </summary>
        public string recordTime { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { set; get; }
        /// <summary>
        /// 住院次数
        /// </summary>
        public string series { set; get; }
        /// <summary>
        /// 体征的单位
        /// </summary>
        public string unit { set; get; }
        /// <summary>
        /// 体征数字值1
        /// </summary>
        public string vitalsignNVal1 { set; get; }
        /// <summary>
        /// 体征数字值2
        /// </summary>
        public string vitalsignNVal2 { set; get; }
        /// <summary>
        /// 体征名称
        /// </summary>
        public string vitalsignName { set; get; }
        /// <summary>
        /// 体征字符串值1
        /// </summary>
        public string vitalsignSVal1 { set; get; }
        /// <summary>
        /// 体征字符串值2
        /// </summary>
        public string vitalsignSVal2 { set; get; }
        /// <summary>
        /// 体征编码,详见码表部分
        /// </summary>
        public string vitalsignType { set; get; }
        /// <summary>
        /// 病区代码
        /// </summary>
        public string wardCode { set; get; }

    }
}

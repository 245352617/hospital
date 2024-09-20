using YiJian.BodyParts.Domain.Shared.Enum;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.BodyParts.Dtos
{


    public class PatientCaseResDto
    {
        public string Lable { get; set; }

        public string PI_ID { get; set; }

    }
    
    /// <summary>
    /// 病案检索
    /// </summary>
    public class PatientCaseDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary> 
        /// 床位号
        /// </summary>
        public string BedNum { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string InHosId { get; set; }

        /// <summary>
        /// 病人Id
        /// </summary>
        public string PatientId { get; set; }
        
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
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 入科来源（0：其他，1：急诊科，2：手术，3：外院转入）
        /// </summary>
        public string InSource { get; set; }

        /// <summary>
        /// 转入科室、转入医院名称
        /// </summary>
        public string InDeptName { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime? InDeptTime { get; set; }

        /// <summary>
        /// 出科时间
        /// </summary>
        public DateTime? OutDeptTime { get; set; }

        /// <summary>
        /// 出科转归(1：出院，2：转出，3：死亡）
        /// </summary>
        public string OutTurnover { get; set; }

        /// <summary>
        /// 转出科室名称
        /// </summary>
        public string OutDeptName { get; set; }

        /// <summary>
        /// 在科天数
        /// </summary>
        public int Indays { get; set; }

        /// <summary>
        /// 入科诊断
        /// </summary>
        public string Indiagnosis { get; set; }

        /// <summary>
        /// 出科诊断
        /// </summary>
        public string Outdiagnosis { get; set; }

        /// <summary>
        /// 是否已出科
        /// </summary>
        public bool HistoryPatient { get; set; }

        /// <summary>
        /// 在科小时数
        /// </summary>
        public int DepHours { get; set; } = 0;

        /// <summary>
        /// 20210816 添加呼吸机
        /// 呼吸机使用时长
        /// </summary>
        public int? HDeviceHours { get; set; } = 0;

        /// <summary>
        /// 20210816 添加监护仪
        /// 监护仪使用时长
        /// </summary>

        public int? JDeviceHours { get; set; } = 0;
        
        /// <summary>
        /// 标签
        /// </summary>
        public string Lable { get; set; }
        
        /// <summary>
        /// 诊断
        /// </summary>
        public string ClinicDiagnosis { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string VisitNum { get; set; }
    }



    /// <summary>
    /// 异常体征 查询条件
    /// </summary>
    public class PatientCaseAbnormalMultiple
    {

        [Required]
        /// <summary>
        /// 科室号
        /// </summary>
        public string DeptCode { get; set; }


        /// <summary>
        /// 指标
        /// </summary>
        public List<PatientCaseAbnormalEnum> PatientCaseAbnormal { get; set; } = new List<PatientCaseAbnormalEnum>();


        /// <summary>
        /// 状态(1：在科，0：出科,不填 同时查询)
        /// </summary>
        public int? InDeptState { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string PI_ID { get; set; }


        /// <summary>
        /// 患者名称
        /// </summary>
        public string PatientName { get; set; }



        /// <summary>
        /// 入科开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }


        /// <summary>
        /// 入科结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

       

        /// <summary>
        /// 当前页码(默认第一页)
        /// </summary>
        public int PageIndex { get; set; }=1;


        /// <summary>
        /// 当前页码(默认第一页)
        /// </summary>
        public int PageSize { get; set; } = 10;


    }


    /// <summary>
    /// 指标类
    /// </summary>
    public class PatientCaseAbnormalEnum
    {

        /// <summary>
        /// 类型
        /// </summary>
        public ParaItemNameEnum ParaItemNameEnum { get; set; }


        /// <summary>
        /// 范围前
        /// </summary>
        public string ARang { get; set; }

        /// <summary>
        /// 范围后
        /// </summary>
        public string URang { get; set; }

    }


    /// <summary>
    /// 
    /// </summary>

    public class UintPartDto
    {



        /// <summary>
        /// 参数代号
        /// </summary>
        public string ParaCode { get; set; }



        /// <summary>
        /// 单位
        /// </summary>
        public string UnitName { get; set; }

      
    }



    /// <summary>
    /// 异常体征Dto 返回
    /// </summary>
    public class PatientCaseAbnormalDto
    {
        /// <summary> 
        /// 床位号
        /// </summary>
        public string BedNum { get; set; }



        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }


        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }



        /// <summary>
        /// 参数代号
        /// </summary>
        public string ParaCode { get; set; }


        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime? InDeptTime { get; set; }


        /// <summary>
        /// 出科时间
        /// </summary>
        public DateTime? OutDeptTime { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string InHosId { get; set; }


        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? RecordTime { get; set; }



        /// <summary>
        /// 单位
        /// </summary>
        public string UnitName { get; set; }


        /// <summary>
        /// 输入的值
        /// </summary>
        public string  ParaValue { get; set; }



        /// <summary>
        /// 项目名称
        /// </summary>

        public string Name { get; set; }


        /// <summary>
        /// 状态
        /// </summary>
        public int InDeptState { get; set; }


    }


}

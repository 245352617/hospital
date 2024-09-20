using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YiJian.EMR.DataBinds.Dto;

namespace YiJian.EMR.Writes.Dto
{
    /// <summary>
    /// 患者的电子病历
    /// </summary>
    public class ModifyPatientEmrDto: PatientEmrBaseDto
    { 
        /// <summary>
        /// 电子病历Xml文档
        /// </summary> 
        public string EmrXml { get; set; }
           
        /// <summary>
        /// 数据绑定
        /// </summary>
        public ModifyDataBindDto DataBind { get; set; }

        /// <summary>
        /// 诊断记录引用情况
        /// </summary>
        public DiagnoseRecordUsed DiagnoseRecordUsed { get; set; }

    }

    /// <summary>
    /// 诊断记录引用数据
    /// </summary>
    public class DiagnoseRecordUsed
    {
        /// <summary>
        /// 诊断记录Id
        /// </summary>
        public  List<int> Pdid { get; set; }  = new List<int>();
    }
}

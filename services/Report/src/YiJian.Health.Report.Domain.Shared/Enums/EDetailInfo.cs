using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiJian.Health.Report.Enums
{
    /// <summary>
    /// 查询的详细信息 0=默认详细， 1=医师详细， 2=护士详细， 3=患者详细
    /// </summary>
    public enum EDetailInfo:int
    { 
        /// <summary>
        /// 默认详细
        /// </summary>
        DefaultDetail = 0,

        /// <summary>
        /// 医师详细
        /// </summary>
        [Description("医师")]
        DoctorDetail = 1,

        /// <summary>
        /// 护士详细
        /// </summary> 
        [Description("护士")]
        NurseDetail = 2,

        /// <summary>
        /// 患者详细
        /// </summary> 
        [Description("患者")]
        PatientDetail = 3,

    }
}

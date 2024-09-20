using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.ECIS.Call.Patient
{
    /// <summary>
    /// 患者信息
    /// </summary>
    public class AdmissionRecordDto
    {

        /// <summary>
        /// PVID 分诊库患者基本信息表主键ID
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 挂号流水号
        /// </summary>
        public string RegisterNo { get; set; }
    }
 }

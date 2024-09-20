using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace YiJian.Health.Report.Statisticses.Entities
{

    /// <summary>
    /// 抢救室滞留详情视图
    /// </summary>
    [Comment("抢救室滞留详情视图")]
    public class ViewEmergencyroomAndPatientDetail
    {
        /// <summary>
        /// 就诊流水号
        /// </summary>
        [Comment("就诊流水号")]
        public string VisitNo { get; set; }

        /// <summary>
        /// 分诊等级
        /// </summary>
        [Comment("分诊等级")]
        public string Level { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [Comment("患者姓名")]
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Comment("性别")]
        public string Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [Comment("年龄")]
        public string Age { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [Comment("出生日期")]
        public string Dayofbirth { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        [Comment("证件号码")]
        public string IDCard { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        [Comment("联系方式")]
        public string Contact { get; set; }

        /// <summary>
        /// 接诊医生
        /// </summary>
        [Comment("接诊医生")]
        public string ReceptionDoctor { get; set; }

        /// <summary>
        /// 接诊护士
        /// </summary>
        [Comment("接诊护士")]
        public string ReceptionNurse { get; set; }

        /// <summary>
        /// 接诊时间
        /// </summary>
        [Comment("接诊时间")]
        public DateTime ReceptionTime { get; set; }

        /// <summary>
        /// 滞留时间(分钟)
        /// </summary>
        [Comment("滞留时间(分钟)")]
        public int RetentionTime { get; set; }

    }
}

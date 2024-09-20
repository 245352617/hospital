using SamJan.MicroService.PreHospital.Core.BaseEntities;
using SamJan.MicroService.PreHospital.Core.Help;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.Core;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class TriageConfig : BaseEntity<Guid>
    {
        public TriageConfig SetId(Guid id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 分诊设置代码
        /// </summary>
        [StringLength(50)]
        [Description("分诊设置代码")]
        public string TriageConfigCode { get; set; }

        /// <summary>
        /// 分诊设置名称
        /// </summary>
        [StringLength(50)]
        [Description("分诊设置名称")]
        public string TriageConfigName { get; set; }

        /// <summary>
        /// 是否启用 0：不启用 1：启用
        /// </summary>
        [Description("是否启用 0：不启用 1：启用")]
        public int IsDisable { get; set; }

        /// <summary>
        /// 分诊设置类型  1001:绿色通道 1002:群伤事件 1003:费别 1004:来院方式 1005:科室配置 1006:院前分诊去向 1013:院前分诊评分类型 具体以TriageDict数据为准
        /// </summary>
        [Description("分诊设置类型  1001:绿色通道 1002:群伤事件 1003:费别 1004:来院方式 1005:科室配置 1006:院前分诊去向 1013:院前分诊评分类型 具体以TriageDict数据为准")]
        public int TriageConfigType { get; set; }

        /// <summary>
        /// 对应 HIS 系统中的代码
        /// </summary>
        [StringLength(50)]
        [Description("对应 HIS 系统中的代码")]
        public string HisConfigCode { get; set; }

        /// <summary>
        /// 额外代码（费别对接省医保缴费档次代码）
        /// </summary>
        [StringLength(50)]
        [Description("额外代码（费别对接省医保缴费档次代码）")]
        public string ExtraCode { get; set; }

        /// <summary>
        /// 平台标识，0:不标识，1:院前急救，2：预检分诊，3：急诊管理,报表使用
        /// </summary>
        [Description("平台标识，0:不标识,1:院前急救，2：预检分诊，3：急诊管理,报表使用")]
        public int Platform { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        [Description("拼音码")]
        [StringLength(50)]
        public string Py { get; set; }

        /// <summary>
        /// 是否不允许删除
        /// </summary>
        [Description("是否不允许删除")]
        public bool UnDeletable { get; set; }

        /// <summary>
        /// 六大中心值
        /// </summary>
        [Description("六大中心值")]
        public string SixCenterValue { get; set; }

        public TriageConfig GetNamePy()
        {
            Py = PyHelper.GetFirstPy(TriageConfigName);
            return this;
        }
    }
}

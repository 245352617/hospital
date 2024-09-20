using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class TriageConfigDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 分诊设置代码
        /// </summary>
        public string TriageConfigCode { get; set; }

        /// <summary>
        /// 对应 HIS 系统中的代码
        /// </summary>
        public string HisConfigCode { get; set; }

        /// <summary>
        /// 额外代码（费别对接省医保缴费档次代码）
        /// </summary>
        public string ExtraCode { get; set; }

        /// <summary>
        /// 分诊设置名称
        /// </summary>
        public string TriageConfigName { get; set; }
        
        /// <summary>
        /// 是否启用 0：不启用 1：启用
        /// </summary>
        public int IsDisable { get; set; }

        /// <summary>
        /// 分诊设置类型  1001:绿色通道 1002:群伤事件 1003:费别 1004:来院方式 1005:科室配置 1006:院前分诊去向 1013:院前分诊评分类型 具体以TriageDict数据为准
        /// </summary>
        public int TriageConfigType { get; set; }

        /// <summary>
        /// 平台标识，1:院前急救，2：预检分诊，3：急诊管理,报表使用
        /// </summary>
        public int Platform { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        public string Py { get; set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }
        
        /// <summary>
        /// 添加人
        /// </summary>
        public string AddUser { get; set; }


        /// <summary>
        /// 修改人
        /// </summary>
        public string ModUser { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        

        /// <summary>
        /// 医院编码
        /// </summary>
        public string HospitalCode { get; set; }

        /// <summary>
        /// 医院名称
        /// </summary>
        public string HospitalName { get; set; }

        /// <summary>
        /// 扩展字段1
        /// </summary>
        public string ExtensionField1 { get; set; }

        /// <summary>
        /// 扩展字段2
        /// </summary>
        public string ExtensionField2 { get; set; }

        /// <summary>
        /// 扩展字段3，ToHospitalWay字典默认选中值，1为默认值
        /// </summary>
        public string ExtensionField3 { get; set; }

        /// <summary>
        /// 扩展字段4
        /// </summary>
        public string ExtensionField4 { get; set; }

        /// <summary>
        /// 扩展字段5
        /// </summary>
        public string ExtensionField5 { get; set; }

        /// <summary>
        /// 六大中心值
        /// </summary>
        public string SixCenterValue { get; set; }
    }
}

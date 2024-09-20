using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class IcuDiseasesWarningDto
    {
        /// <summary>
        /// 配置id
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 病种
        /// </summary>
        public string Diseases { get; set; }

        /// <summary>
        /// 病种拼音
        /// </summary>
        public string DiseasesPY { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 预警配置
        /// </summary>
        public List<Config> Configs { get; set; } = new List<Config> ();
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 病种预警配置Dto
        /// </summary>
        public class Config
        {
            /// <summary>
            /// 项目code
            /// </summary>
            public string ParaCode { get; set; }
            /// <summary>
            /// 单位名称
            /// </summary>
            public string UnitName { get; set; }
            /// <summary>
            /// 项目名称
            /// </summary>
            public string DisplayName { get; set; }
            /// <summary>
            /// 高值
            /// </summary>
            public string HighValue { get; set; }
            /// <summary>
            /// 低值
            /// </summary>
            public string LowValue { get; set; }
            /// <summary>
            /// 值类型
            /// </summary>
            public string ValueType { get; set; }
            /// <summary>
            /// 默认值
            /// </summary>
            public string NormalValue { get; set; }
        }
    }
    public class IcuDiseasesWarningSearchDto
    { 
        public string SearchKey { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}

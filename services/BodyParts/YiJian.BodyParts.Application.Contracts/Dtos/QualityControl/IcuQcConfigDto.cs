using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class IcuQcConfigDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 统计项目
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 统计指标Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 统计指标名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 总计Sql
        /// </summary>
        public string TotalSql { get; set; }

        /// <summary>
        /// 计算公式
        /// </summary>
        public string TotalFunction { get; set; }

        /// <summary>
        /// 统计类型
        /// </summary>
        public StatisticalWayEnum StatType { get; set; }

        /// <summary>
        /// 总计详情sql
        /// </summary>
        public string TotalDetailsSql { get; set; }

        /// <summary>
        /// 是否有总计详情sql
        /// </summary>
        public bool HasTotalDetailsSql { get; set; } 

        /// <summary>
        /// 分子名称
        /// </summary>
        public string MoleculeName { get; set; }

        /// <summary>
        /// 分子统计sql
        /// </summary>
        public string MoleculeSql { get; set; }

        /// <summary>
        /// 分母名称
        /// </summary>
        public string DenominatorName { get; set; }

        /// <summary>
        /// 分母统计sql
        /// </summary>
        public string DenominatorSql { get; set; }

        /// <summary>
        /// 是否有分母详情sql
        /// </summary>
        public bool HasDenominatorDetailsSql { get; set; }

        /// <summary>
        /// 分子详情配置
        /// </summary>
        public string MoleculeDetailsSql { get; set; }

        /// <summary>
        /// 是否有分子详情sql
        /// </summary>
        public bool HasMoleculeDetailsSql { get; set; }

        /// <summary>
        /// 分子详情配置
        /// </summary>
        public bool HasMoleculeSql { get; set; }

        /// <summary>
        ///  是否有分子详情sql
        /// </summary>
        public bool HasDenominatorSql { get; set; }

        /// <summary>
        /// 分母详情配置
        /// </summary>
        public string DenominatorDetailsSql { get; set; }


        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}

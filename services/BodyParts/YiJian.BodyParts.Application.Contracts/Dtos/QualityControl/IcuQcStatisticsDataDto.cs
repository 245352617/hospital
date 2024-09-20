using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class IcuQcStatisticsDataDto
    {

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分子名称
        /// </summary>
        public string MoleculeName { get; set; }

        /// <summary>
        /// 分母名称
        /// </summary>
        public string DenominatorName { get; set; }

        /// <summary>
        /// 计算公式
        /// </summary>
        public string TotalFunction { get; set; }

        /// <summary>
        /// 是否有分母详情sql
        /// </summary>
        public bool HasDenominatorDetail { get; set; }

        /// <summary>
        /// 是否有分子详情sql
        /// </summary>
        public bool HasMoleculeDetail { get; set; }

        /// <summary>
        /// 是否有分子统计sql
        /// </summary>
        public bool HasMolecule { get; set; }

        /// <summary>
        ///  是否有分母统计sql
        /// </summary>
        public bool HasDenominator { get; set; }

        /// <summary>
        /// 分子
        /// </summary>
        public Dictionary<string,string> Molecule { get; set; }

        /// <summary>
        /// 分母
        /// </summary>
        public Dictionary<string, string> Denominator { get; set; }
    }
}

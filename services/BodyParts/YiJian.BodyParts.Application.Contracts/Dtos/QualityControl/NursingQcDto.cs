using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class NursingQcDto
    {
        /// <summary>
        /// 固定序号
        /// </summary>
        public List<string> Index { get; set; }

        /// <summary>
        /// 指标名称（月份）
        /// </summary>
        public List<string> Name { get; set; }

        /// <summary>
        /// CATUI预防率
        /// </summary>
        public List<string> Catui { get; set; }

        /// <summary>
        /// 分子
        /// </summary>
        public List<string> Molecular1 { get; set; }

        /// <summary>
        /// 分母
        /// </summary>
        public List<string> Denominator1 { get; set; }

        /// <summary>
        /// CRBSI预防率
        /// </summary>
        public List<string> Crbsi { get; set; }

        /// <summary>
        /// 分子
        /// </summary>
        public List<string> Molecular2 { get; set; }

        /// <summary>
        /// 分母
        /// </summary>
        public List<string> Denominator2 { get; set; }

        /// <summary>
        /// VAP预防率
        /// </summary>
        public List<string> Vap { get; set; }

        /// <summary>
        /// 分子
        /// </summary>
        public List<string> Molecular3 { get; set; }

        /// <summary>
        /// 分母
        /// </summary>
        public List<string> Denominator3 { get; set; }

    }
}
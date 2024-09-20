using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 管道统计
    /// </summary>
    public class CanulaStatisticalDto : PatientBasicInformationDto
    {
        /// <summary>
        /// 管道名称
        /// </summary>
        /// <example></example>
        public string CanulaName { get; set; }

        /// <summary>
        /// 插管时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 插管时间
        /// </summary>
        public string StartTime2
        {
            get
            {
                if (StartTime != null)
                {
                    return StartTime.Value.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    return "不详";
                }
            }
        }

        /// <summary>
        /// 拔管时间
        /// </summary>
        /// <example></example>
        public DateTime? StopTime { get; set; }

        /// <summary>
        /// 拔管时间
        /// </summary>
        /// <example></example>
        public string StopTime2
        {
            get
            {
                if (StopTime != null)
                {
                    return StopTime.Value.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 留置天数
        /// </summary>
        /// <example></example>
        public double Days { get; set; }

        /// <summary>
        /// 拔管原因
        /// </summary>
        /// <example></example>
        public string DrawReason { get; set; }

        /// <summary>
        /// 使用标志：（Y在用，N已拔管）
        /// </summary>
        /// <example></example>
        public string UseFlag { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string InHosId { get; set; }

        /// <summary>
        /// 转入科室编号
        /// </summary>
        public string InDeptCode { get; set; }

        /// <summary>
        /// 转入科室
        /// </summary>
        public string InDeptName { get; set; }
        
        /// <summary>
        /// 转出科室编号
        /// </summary>
        public string OutDeptCode { get; set; }
        
        /// <summary>
        /// 转出科室
        /// </summary>
        public string OutDeptName { get; set; }
        
        /// <summary>
        /// 置管地点
        /// </summary>
        public string CanulaPosition { get; set; }
                
        /// <summary>
        /// 插管次数
        /// </summary>
        public int? CanulaNumber { get; set; }
        
        /// <summary>
        /// 更换次数
        /// </summary>
        public int? ChangeTimes { get; set; }

        /// <summary>
        /// 导管ID
        /// </summary>
        public Guid CanulaId { get; set; }
    }
}

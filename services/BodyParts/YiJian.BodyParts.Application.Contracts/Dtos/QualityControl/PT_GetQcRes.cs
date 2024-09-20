using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Serilog;

namespace YiJian.BodyParts.Application.Contracts.Dtos.QualityControl
{

    public class PT_GetQcRes
    {
        /// <summary>
        /// 同期医院收治患者总数
        /// </summary>
        public List<PT_QcInHosPatInfo> InHosPatTotal { get; set; }
        
        /// <summary>
        /// 同期医院收治患者总床日数
        /// </summary>
        public List<PT_QcInHosPatBedInfo> InHosPatBedTotal { get; set; }
        
        /// <summary>
        /// 抗菌药物数据
        /// </summary>
        public List<PT_QcAntibacterialInfo> AntibacterialData { get; set; }


        /// <summary>
        /// 获取同期医院患者收治患者总数
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool GetInHosPatTotal(int year, int month,out decimal count)
        {
            count = 0;
            if (InHosPatTotal != null && InHosPatTotal.Count > 0)
            {
                var inHosPatTotal = InHosPatTotal.FirstOrDefault(f => f.Year == year.ToString() && f.Month == month.ToString());
                if (inHosPatTotal != null && decimal.TryParse(inHosPatTotal.Count,out count))
                {
                    Log.Information($"同期收治患者总数获取成功[InHosPatTotal]，{year}年{month}月 数量：{count}");
                    return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// 获取同期医院患者收治患者总床日数
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool GetInHosPatBedTotal(int year, int month,out decimal count)
        {
            count = 0;
            if (InHosPatBedTotal != null && InHosPatBedTotal.Count > 0)
            {
                var inHosPatBedTotal = InHosPatBedTotal.FirstOrDefault(f => f.Year == year.ToString() && f.Month == month.ToString());
                if (inHosPatBedTotal != null && decimal.TryParse(inHosPatBedTotal.Count,out count))
                {
                    Log.Information($"同期收治患者总床日数获取成功[InHosPatBedTotal]，{year}年{month}月 数量：{count}");
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 获取抗菌药物已送检例数
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool GetAntibacterialSentCount(Expression<Func<PT_QcAntibacterialInfo,bool>> where,out int count)
        {
            count = 0;
            if (AntibacterialData != null && AntibacterialData.Count > 0)
            {
                where = where.And(p => p.IsInspect == "1");
                count = AntibacterialData.Where(where.Compile()).Count();
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取抗菌药物总例数
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool GetAntibacterialCount(Expression<Func<PT_QcAntibacterialInfo,bool>> where,out int count)
        {
            count = 0;
            if (AntibacterialData != null && AntibacterialData.Count > 0)
            {
                count = AntibacterialData.Where(where.Compile()).Count();
                return true;
            }

            return false;
        }
    }
}
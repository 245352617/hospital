using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 护理工作量统计
    /// </summary>

    public class NursingWorkStaisticsDto
    {
        /// <summary>
        /// 数据表头
        /// </summary>
        public List<string> HaedList { get; set; }

        /// <summary>
        /// 列的患者列表Title
        /// </summary>
        // public List<string> PatientListTitle { get; set; }

        /// <summary>
        /// 统计数据列表
        /// </summary>
        public List<NursingWOrkStaisticsResultDto> TableList { get; set; }
    }

    public class NursingWOrkStaisticsResultDto
    {
        /// <summary>
        /// 数据源配置项的Id，请求详情接口时会带上该参数
        /// </summary>
        public Guid DateSourceId { get; set; }
        
        /// <summary>
        /// 数据源编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 指标名称
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 合计
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 统计结果，key为日期，value为对应的结果
        /// </summary>
        public Dictionary<string, int> keyValuePairs;
        
        ///// <summary>
        ///// 指标列表
        ///// </summary>
        //public List<IndexData> RowListByNew { get; set; }

        // public List<IndexData> RowList { get; set; }

        ///// <summary>
        ///// 子集列表
        ///// </summary>
        //public PatientDetail PatientDetail { get; set; }
    }

    public class IndexData
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 指标名称
        /// </summary>
        public string Res { get; set; }

        /// <summary>
        /// 时间-列-列表
        /// </summary>
        public dynamic keyValuePairs { get; set; }

        /// <summary>
        /// 合计
        /// </summary>
        public string Total { get; set; }
    }

    /// <summary>
    /// 患者详情
    /// </summary>
    public class PatientDetail
    {
        public List<List<string>> TableList { get; set; }
    }
}
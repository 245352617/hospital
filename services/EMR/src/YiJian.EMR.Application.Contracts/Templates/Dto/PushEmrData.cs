using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.EMR.Templates.Dto
{ 
    /// <summary>
    /// 推送到医嘱的数据配置
    /// </summary>
    public class PushEmrDataSetting
    { 
        /// <summary>
        /// 数据源
        /// </summary>
        public string DataSource { get;set; }

       /// <summary>
       /// 数据源路径
       /// </summary>
        public PathInfo Path { get;set;}
    }

    /// <summary>
    /// 数据源路径
    /// </summary>
    public class PathInfo
    {
        /*
          "Pastmedicalhistory": "pastmedicalhistory", //既往史
          "Presentmedicalhistory": "presentmedicalhistory", //现病史
          "Physicalexamination": "physicalexamination", //体格检查
          "Narrationname": "narrationname", //主诉
          "Aidpacs": "aidpacs", //辅助检查结果
          "Treatopinion": "treatopinion", //处理意见
          "Diagnosename": "diagnosename" //初步诊断 
         */

        /// <summary>
        /// 既往史
        /// </summary>
        public string Pastmedicalhistory { get; set; }

        /// <summary>
        /// 现病史
        /// </summary>
        public string Presentmedicalhistory { get; set; }

        /// <summary>
        /// 体格检查
        /// </summary>
        public string Physicalexamination { get; set; }

        /// <summary>
        /// 主诉
        /// </summary>
        public string Narrationname { get; set; }

        /// <summary>
        /// 辅助检查结果
        /// </summary>
        public string Aidpacs { get;set;}

        /// <summary>
        /// 处理意见
        /// </summary>
        public string Treatopinion { get; set; }

        /// <summary>
        /// 初步诊断
        /// </summary>
        public string Diagnosename { get; set; }

    }

}

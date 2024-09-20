namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 创伤评分
    /// </summary>
    public class WoundScoreDto
    {
        /// <summary>
        /// 呼吸频率
        /// </summary>
        public int RespiratoryRate { get; set; }

        /// <summary>
        /// 呼吸幅度
        /// </summary>
        public int RespiratoryRange { get; set; }

        /// <summary>
        /// 收缩压
        /// </summary>
        public int Sbp { get; set; }

        /// <summary>
        /// 毛细血管充酸度
        /// </summary>
        public int CapillaryFillingAcidity { get; set; }

        /// <summary>
        /// 眼睛
        /// </summary>
        public int OpenEye { get; set; } 

        /// <summary>
        /// 语言交流
        /// </summary>
        public int Communication { get; set; }
        
        /// <summary>
        /// 运动
        /// </summary>
        public int Sport { get; set; }
    }
}

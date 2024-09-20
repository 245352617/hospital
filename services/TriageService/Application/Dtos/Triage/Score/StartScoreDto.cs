namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// Start评分
    /// </summary>
    public class StartScoreDto
    {
        /// <summary>
        /// 能否行走
        /// </summary>
        public bool IsWalk { get; set; } 

        /// <summary>
        /// 能否呼吸
        /// </summary>
        public bool IsBreath { get; set; }

        /// <summary>
        /// 打开气管是否有呼吸
        /// </summary>
        public bool Trachea { get; set; }

        /// <summary>
        /// 呼吸频率
        /// </summary>
        public bool R { get; set; }  

        /// <summary>
        /// 是否触及脉搏
        /// </summary>
        public bool IsPulse { get; set; } 

        /// <summary>
        /// 是否有意识做简单动作
        /// </summary>
        public bool Awareness { get; set; }
    }
}

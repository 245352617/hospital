namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// ESI 评分
    /// </summary>
    public class EsiScoreDto
    {
        /// <summary>
        /// 病人是否是濒危？
        /// </summary>
        public bool IsEndangered { get; set; }

        /// <summary>
        /// 病人是否不能等？
        /// </summary>
        public bool IsCantWait { get; set; }

        /// <summary>
        /// 急症病人？
        /// </summary>
        public bool IsEmergency { get; set; }

        /// <summary>
        /// 需要多少资源处置？（放射/实验室检查/专家会诊/心电图等）
        /// 0-1 = true
        /// >=2 = false
        /// </summary>
        public bool ResourceNum { get; set; }

        /// <summary>
        /// 生命体征是否异常
        /// </summary>
        public bool IsVitalSignsAbnormal { get; set; }
    }
}

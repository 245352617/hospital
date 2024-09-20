using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class ScoreManageInput
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        public bool IsEnable { get; set; }
    }
}
namespace SamJan.MicroService.PreHospital.TriageService
{
    public class RegisterUpdateDto
    {
        /// <summary>
        /// 挂号模式名称
        /// </summary>
        public string TriageConfigName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsDisable { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}

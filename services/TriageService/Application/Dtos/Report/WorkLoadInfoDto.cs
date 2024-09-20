namespace SamJan.MicroService.PreHospital.TriageService
{
    public class WorkLoadInfoDto
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int RowNum { get; set; }

        /// <summary>
        /// 分诊人员
        /// </summary>
        public string TriageUserName { get; set; }

        /// <summary>
        /// 急诊科室名称
        /// </summary>
        public string TriageDeptName { get; set; }

        /// <summary>
        /// 人次
        /// </summary>
        public int PersonCount { get; set; }
    }
}
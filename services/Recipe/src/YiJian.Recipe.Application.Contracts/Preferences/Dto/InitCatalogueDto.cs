using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Preferences.Dto
{
    /// <summary>
    /// 初始化快速开嘱目录
    /// </summary>
    public class InitCatalogueDto
    {
        /// <summary>
        /// 医生id
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 系统标识， 0= 急诊， 1=院前急救
        /// </summary>
        public EPlatformType PlatformType { get; set; }
    }

}

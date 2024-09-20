namespace YiJian.EMR.Templates.Dto
{
    /// <summary>
    /// 个人模板目录集合
    /// </summary>
    public class PersonalCatalogueListDto : TemplateCatalogueBaseDto
    {
        /// <summary>
        /// 医生编码
        /// </summary> 
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生
        /// </summary> 
        public string DoctorName { get; set; }
    }


}

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 动态六项的修改的模型
    /// </summary>
    public class DynamicSixFieldDto : DynamicFieldBaseDto
    {
        /// <summary>
        /// 当动态动态六项选项修改之后，内容是否清除，默认清除=true,不先清除填false
        /// </summary>
        public bool IsChangeDeleteFieldData { get; set; }
    }

}

namespace YiJian.BodyParts.Application.Contracts.Dtos.Patient
{

    public class GetPatInfoOutputDto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }
        
        /// <summary>
        /// 患者id
        /// </summary>
        public string ArchiveId { get; set; }
        
        /// <summary>
        /// 姓名
        /// </summary>
        public string PatName { get; set; }
        
        /// <summary>
        /// 诊断
        /// </summary>
        public string Diagnose { get; set; }
        
        /// <summary>
        /// 科室编号
        /// </summary>
        public string DeptCode { get; set; }
        
        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }
        
        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }
        
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
    }
}
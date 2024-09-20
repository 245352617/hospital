namespace YiJian.ECIS.ShareModel.Etos.EMRs
{
    /// <summary>
    /// 存档的xml对象
    /// </summary>
    public class XmlModelEto
    {
        /// <summary>
        /// 存档的xml对象
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="xml"></param> 
        /// <param name="doctorName"></param>
        /// <param name="patientName"></param>
        public XmlModelEto(string fileName, string xml, string doctorName, string patientName)
        {
            FileName = fileName;
            Xml = xml;
            DoctorName = doctorName;
            PatientName = patientName;
        }

        /// <summary>
        /// pdf的文件名称 eg:出诊病历_2022_09_05_001.pdf 
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// xml 文档-字符串的方式传递过来
        /// </summary>
        public string Xml { get; set; }

        /// <summary>
        /// 书写电子病历的医生
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>
        public string PatientName { get; set; }
    }
}
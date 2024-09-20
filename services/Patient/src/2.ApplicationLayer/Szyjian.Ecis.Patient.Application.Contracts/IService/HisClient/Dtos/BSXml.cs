using System.Collections.Generic;
using System.Xml.Serialization;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class BSXml
    {
        [XmlElement("success")]
        public Success success { get; set; }

        [XmlElement("code")]
        public Code code { get; set; }

        [XmlElement("message")]
        public Message message { get; set; }

        [XmlElement("data")]
        public Data data { get; set; }

    }

    public class Success
    {
        public string success { get; set; }
    }

    public class Code
    {
        public string code { get; set; }
    }

    public class Message
    {
        public string message { get; set; }
    }

    public class Data
    {
        [XmlElement("NewDataSet")]
        public NewDataSet newDataSet { get; set; }
    }

    public class NewDataSet
    {
        [XmlElement(ElementName = "HIS_YS_MZ_JBZD")]
        public List<GetDiagnoseRecordBySocketDto> diagnoseRecordList { get; set; }
    }

}

using System.Xml;

namespace YiJian.ECIS.ShareModel.Utils
{
    /// <summary>
    /// Xml文档处理工具
    /// </summary>
    public static class XmlUtil
    {

        /// <summary>
        /// 擦除xml水印内容(只支持都昌的病历)
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>第一个返回的是是否有水印，第二个返回的是xml的内容</returns>
        public static Tuple<bool, string> CleanWatermark(string xml)
        {
            if (string.IsNullOrEmpty(xml)) return new Tuple<bool, string>(false, xml);

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                var pageSettings = doc.SelectSingleNode("XTextDocument/PageSettings");
                var ele = doc.SelectSingleNode("XTextDocument/PageSettings/Watermark");
                if (pageSettings is not null && ele is not null)
                {
                    pageSettings.RemoveChild(ele);
                    return new Tuple<bool, string>(true, ConvertXmlToString(doc));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return new Tuple<bool, string>(false, xml);
        }

        /// <summary>
        /// 将XmlDocument转化为string
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        private static string ConvertXmlToString(XmlDocument xmlDoc)
        {
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented;
            xmlDoc.Save(writer);
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string xmlString = sr.ReadToEnd();
            sr.Close();
            writer.Close();
            stream.Close();
            return xmlString;
        }

    }
}

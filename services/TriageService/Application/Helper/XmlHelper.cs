using Newtonsoft.Json;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class XmlHelper
    {
        /// <summary>
        /// Json转xml
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string JsonToXml(string json)
        {
            json = "{\"root\":" + json + "}";
            var doc = JsonConvert.DeserializeXmlNode(json.Replace("\\", ""));
            return doc == null ? "" : doc.SelectSingleNode("root")?.InnerXml;
        }
    }
}
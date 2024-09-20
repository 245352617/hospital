using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Newtonsoft.Json;

namespace YiJian.BodyParts.Domain.Shared.Extension
{

    //如果好用，请收藏地址，帮忙分享。
    public class RowVet
    {
        /// <summary>
        /// 
        /// </summary>
        public string TotalScore { get; set; }
    }

    public class RowsVet
    {
        /// <summary>
        /// 
        /// </summary>
        public RowVet Row { get; set; }
    }

    public class ResponseVet
    {
        /// <summary>
        /// 
        /// </summary>
        public RowsVet Rows { get; set; }
    }



    /// <summary>
    /// 质控帮助类
    /// </summary>
    public static class QcHelpExtension
    {



        /// <summary>
        /// 根据出生日期获取年龄
        /// </summary>
        /// <param name="Birthday"></param>
        /// <param name="paraValue"></param>
        /// <param name="scheduleDate"></param>
        /// <returns></returns>
        public static string GetAge(this string  init, DateTime ? Birthday, string paraValue, DateTime scheduleDate,string Age)
        {
            try
            {

                if (Birthday == null)
                {
                    return Age;
                }
                //年龄界限
                //string paraValue = DbContext.IcuSysPara.Where(x => x.ParaType == "S" && x.ParaCode == "Age_Limit").Select(x => x.ParaValue).FirstOrDefault();

                //获取入院日期，根据入院日期和出生日期计算年龄

                DateTime now = scheduleDate == null || scheduleDate == DateTime.MinValue ? DateTime.Now : scheduleDate;

                #region
                string dateDiff = null;

                TimeSpan ts1 = new TimeSpan(now.Ticks);
                TimeSpan ts2 = new TimeSpan(Birthday.Value.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();

                //计算岁
                int age = now.Year - Birthday.Value.Year;
                int month = 0;
                int day = 0;
                if (now.Month < Birthday.Value.Month || (now.Month == Birthday.Value.Month && now.Day < Birthday.Value.Day))
                {
                    age--;
                    month = now.Month - Birthday.Value.Month + 12;
                }
                else
                {
                    month = now.Month - Birthday.Value.Month;
                }


                int lastDays = 30;
                if (DateTime.Now.Month == 1)
                {
                    lastDays = DateTime.DaysInMonth(DateTime.Now.Year - 1, 12);
                }
                else
                {
                    lastDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month - 1);
                }

                if (now.Day < Birthday.Value.Day)
                {
                    month--;
                    day = now.Day - Birthday.Value.Day + lastDays;
                }
                else
                {
                    day = now.Day - Birthday.Value.Day;
                }

                //超过设置的年龄显示岁
                if (age >= int.Parse(paraValue))
                {
                    return age + "岁";
                }

                //不足24小时
                if (ts.Days == 0)
                {
                    dateDiff = ts.Minutes == 0 ? ts.Hours + "小时" : ts.Hours + "小时" + ts.Minutes + "分";
                    return dateDiff;
                }

                //不足1月
                if (age == 0 && month == 0)
                {
                    dateDiff = ts.Days + "天";
                    return dateDiff;
                }

                //不足1年
                if (age == 0)
                {
                    dateDiff = day == 0 ? month + "月" : month + "月" + day + "天";
                    return dateDiff;
                }
                else
                {
                    dateDiff = month == 0 ? age + "岁" : age + "岁" + month + "月";
                    return dateDiff;
                }
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }



        /// <summary>
        /// 入科计划（0：非计划转入，1：计划转入）转
        /// </summary>
        /// <param name="InPlan"></param>
        /// <returns></returns>
        public static string GetInPlan(this int? InPlan)
        {
            if (InPlan == null)
            {
                return string.Empty;
            }

            return InPlan.Value == 0 ? "非计划转入" : "计划转入";
        }

        /// <summary>
        /// 原入科来源（0：其他， 1:产房，2：产科，3：门诊，4：急诊科，5：手术室，6：外院转入）
        /// 现入科来源取值 7-入院、5-手术、6-外院转入、8-转入
        /// </summary>
        /// <param name="InSource"></param>
        /// <returns></returns>
        public static string GetInSource(this int? InSource)
        {
            if (InSource==null)
            {
                return "";  
            }
            switch (InSource.Value)
            {
                case 0: return "其他";

                case 1: return "产房";

                case 2: return "产科";

                case 3: return "门诊";

                case 4: return "急诊科";

                case 6: return "外院转入";

                case 7: return "入院";

                case 8: return "转入";

                case 5: return "手术";

                case 9: return "医联体医院转入";

                default: return "其他";
            };
        }


        /// <summary>
        /// 字符串自定义分组
        /// </summary>
        /// <param name="Mumber"></param>
        /// <param name="GroupLength">分割字符</param>
        /// <returns></returns>
        public static string[] Pro(this String Mumber, int GroupLength)
        {
            string[] res = new string[Mumber.Length / GroupLength];

            for (int i = 0; i < Mumber.Length / 2; i++)
            {
                res[i] = Mumber.Substring(i * 2, 2);
            }

            return res;
        }



        /// <summary>
        /// 判断是否数字
        /// </summary>
        /// <returns></returns>
        public static string Setdecimal(this string Data)
        {
            if (decimal.TryParse(Data, out decimal temp))
            {
                return Data;
            }
            return string.Empty;
        }



        #region 静脉血栓栓塞症（VTE）风险护理单评分查询接口 reqVteSoreByIds

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="PressureHighRisk_URL"></param>
        /// <param name="PatientIds"></param>
        /// <returns></returns>
        public static JsonResult<T> GetreqVteSoreByIds<T>(this string action,string PressureHighRisk_URL,string PatientIds)where T :class
        {
            if ((PressureHighRisk_URL ?? "") == "")
            {
                if (string.IsNullOrWhiteSpace(PressureHighRisk_URL))
                {
                    return JsonResult<T>.Ok(data: null);
                }
            }

            if ((action??"") == "")
            {
                action = "reqVteSoreByIds";
            }

            Console.WriteLine("开始调用webservice");

            var results = VtesoapPost_Bed<T>(PressureHighRisk_URL, action, PatientIds);

            return JsonResult<T>.Ok(data: results);

        }



        /// <summary>
        /// 发送请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <param name="dicBody"></param>
        /// <returns></returns>
        private static T VtesoapPost_Bed<T>(string url, string action, string dicBody) where T : class
        {
            try
            {
                T descJsonStu = default;

                Uri uri = new Uri(url);

                string Body = String.Format("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ws=\"http://ws.nis.ewell/\"> <soapenv:Header/> <soapenv:Body> <ws:reqVteSoreByIds> <inputXml> <![CDATA[<?xml version=\"1.0\" encoding=\"UTF-8\" ?> <root> <Request><PatientID>{0}</PatientID></Request> </root>]]></inputXml> </ws:reqVteSoreByIds> </soapenv:Body> </soapenv:Envelope>", dicBody);

                byte[] bs = Encoding.UTF8.GetBytes(Body.ToString());

                Console.WriteLine($"WebServic连接地址:{url}");

                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(uri);

                myRequest.Method = "POST";

                myRequest.Timeout = 10000;

                myRequest.ContentType = "text/xml; charset=utf-8";

                //mediate为调用方法
                myRequest.Headers.Add("SOAPAction", "reqVteSoreByIds");

                myRequest.ContentLength = bs.Length;

                Console.WriteLine("完成准备工作");

                using (Stream reqStream = myRequest.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                }

                Console.WriteLine("开始调用");

                using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
                {
                    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

                    Console.WriteLine("WebService响应成功");

                    string JsonValue = sr.ReadToEnd();

                    Console.WriteLine("反馈结果" + JsonValue);

                    Console.WriteLine("开始解析xml");

                    XmlDocument xmldoc = new XmlDocument();

                    xmldoc.LoadXml(JsonValue);

                    string result = xmldoc.InnerText;

                    if (result.Contains("没有相关入参病人VTE分数信息"))
                    {
                        return null;
                    }

                    Console.WriteLine($"WebServic返回的数据:{result}");

                    Console.WriteLine($"开始解析数据血气:{result}");

                    XmlDocument xmldocChild1 = new XmlDocument();

                    xmldocChild1.LoadXml(result);

                    string json = JsonConvert.SerializeXmlNode(xmldocChild1.SelectSingleNode("/Response/Rows"));

                    Console.WriteLine($"WebServic返回的数据字符串:{json}");

                    descJsonStu = JsonConvert.DeserializeObject<T>(json);

                    Console.WriteLine($"json序列化成功");

                    Console.WriteLine($"WebServic序列表的数据结构:{Newtonsoft.Json.JsonConvert.SerializeObject(descJsonStu)}");
                }

                Console.WriteLine("完成调用接口");

                return descJsonStu;
            }
            catch
            {
                Console.WriteLine("连接超时了");

                return null;
            }

        }

        #endregion


    }
}

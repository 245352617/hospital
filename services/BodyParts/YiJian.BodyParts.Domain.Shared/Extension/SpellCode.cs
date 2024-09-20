using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.BodyParts
{
    /// <summary>
    /// 转换字符串
    /// 1.汉字转拼音
    /// 2.根据小数位数返回值
    /// </summary>
    public static class SpellCode
    {
        /// <summary>
        /// 汉字转拼音
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string GetSpellCode(string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    return string.Empty;
                }

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding = Encoding.GetEncoding("GB2312");

                var cResult = Task<string>.Run(() =>
                {
                    string tempStr = string.Empty;
                    foreach (char c in query)
                    {
                        if ((int)c >= 33 && (int)c <= 126)
                        {//字母和符号原样保留 
                            tempStr += c.ToString();
                        }
                        else
                        {
                            //累加拼音声母 
                            byte[] array = new byte[2];
                            array = encoding.GetBytes(c.ToString());
                            if (array == null || array.Length < 2)
                            {
                                //tempStr += string.Empty;
                                continue;
                            }
                            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
                            if ((i >= 0xB0A1) && (i <= 0xB0C4))
                            {
                                tempStr += "a";
                            }
                            else if ((i >= 0xB0C5) && (i <= 0xB2C0))
                            {
                                tempStr += "b";
                            }
                            else if ((i >= 0xB2C1) && (i <= 0xB4ED))
                            {
                                tempStr += "c";
                            }
                            else if ((i >= 0xB4EE) && (i <= 0xB6E9))
                            {
                                tempStr += "d";
                            }
                            else if ((i >= 0xB6EA) && (i <= 0xB7A1))
                            {
                                tempStr += "e";
                            }
                            else if ((i >= 0xB7A2) && (i <= 0xB8C0))
                            {
                                tempStr += "f";
                            }
                            else if ((i >= 0xB8C1) && (i <= 0xB9FD))
                            {
                                tempStr += "g";
                            }
                            else if ((i >= 0xB9FE) && (i <= 0xBBF6))
                            {
                                tempStr += "h";
                            }
                            else if ((i >= 0xBBF7) && (i <= 0xBFA5))
                            {
                                tempStr += "j";
                            }
                            else if ((i >= 0xBFA6) && (i <= 0xC0AB))
                            {
                                tempStr += "k";
                            }
                            else if ((i >= 0xC0AC) && (i <= 0xC2E7))
                            {
                                tempStr += "l";
                            }
                            else if ((i >= 0xC2E8) && (i <= 0xC4C2))
                            {
                                tempStr += "m";
                            }
                            else if ((i >= 0xC4C3) && (i <= 0xC5B5))
                            {
                                tempStr += "n";
                            }
                            else if ((i >= 0xC5B6) && (i <= 0xC5BD))
                            {
                                tempStr += "o";
                            }
                            else if ((i >= 0xC5BE) && (i <= 0xC6D9))
                            {
                                tempStr += "p";
                            }
                            else if ((i >= 0xC6DA) && (i <= 0xC8BA))
                            {
                                tempStr += "q";
                            }
                            else if ((i >= 0xC8BB) && (i <= 0xC8F5))
                            {
                                tempStr += "r";
                            }
                            else if ((i >= 0xC8F6) && (i <= 0xCBF9))
                            {
                                tempStr += "s";
                            }
                            else if ((i >= 0xCBFA) && (i <= 0xCDD9))
                            {
                                tempStr += "t";
                            }
                            else if ((i >= 0xCDDA) && (i <= 0xCEF3))
                            {
                                tempStr += "w";
                            }
                            else if ((i >= 0xCEF4) && (i <= 0xD1B8))
                            {
                                tempStr += "x";
                            }
                            else if ((i >= 0xD1B9) && (i <= 0xD4D0))
                            {
                                tempStr += "y";
                            }
                            else if ((i >= 0xD4D1) && (i <= 0xD7F9))
                            {
                                tempStr += "z";
                            }
                            else
                            {
                                tempStr += string.Empty;
                            }
                        }
                    }
                    return tempStr;
                }).Result.ToString().ToUpper();

                return cResult;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 根据小数位数返回值
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="DecimalDigits">小数位数</param>
        /// <returns></returns>
        public static string SetStringOfDecimal(string txt, string DecimalDigits)
        {
            string temp = string.Empty;
            int digit = 0;
            if (!string.IsNullOrWhiteSpace(DecimalDigits))
            {
                digit = int.Parse(DecimalDigits);
            }
            if (!string.IsNullOrEmpty(txt) && Decimal.TryParse(txt, out decimal ftemp))
            {
                switch (digit)
                {
                    case 0:
                        temp = Convert.ToInt32(ftemp).ToString();
                        break;
                    case 1:
                        temp = ftemp.ToString("0.0");
                        break;
                    case 2:
                        temp = ftemp.ToString("0.00");
                        break;
                    case 3:
                        temp = ftemp.ToString("0.000");
                        break;
                }
            }
            return temp;
        }
    }
}

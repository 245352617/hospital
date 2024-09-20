using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace YiJian.Nursing.RecipeExecutes
{
    /// <summary>
    /// 药品频次信息里面的频次单位分析处理
    /// </summary>
    internal class FrequencyUnitAnalysis
    {
        /// <summary>
        /// Unit类型判断
        /// </summary>
        private static readonly Regex regexNO = new Regex(@"(?<n>\d+)(?<t>[hdw])", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// <![CDATA[/q(?<n>0[\.]5|[1-9])(?:h)/gi]]>
        /// </summary>
        private static readonly Regex regexNH = new Regex(@"q(?<n>0[\.]5|[1-9])(?:h)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// <![CDATA[/(qd[0-9a-z\-_]+)|(bid[0-9a-z\-_]*)|(tid[0-9a-z\-_]*)|(qid[0-9a-z\-_]*)|(qd\d*)|(q\d*h\d*)/gi]]>
        /// </summary>
        private static readonly Regex regex1D = new Regex(@"(qd[0-9a-z\-_]+)|(bid[0-9a-z\-_]*)|(tid[0-9a-z\-_]*)|(qid[0-9a-z\-_]*)|(qd\d*)|(q\d*h\d*)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// <![CDATA[/(q(?:(?<d>[2-9o]|[1-9][0-9]+))d)/gi]]>
        /// </summary>
        private static readonly Regex regexND = new Regex(@"(q(?:(?<d>[2-9o]|[1-9][0-9]+))d)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// <![CDATA[/(biw[0-9a-z\-_]*)|(qw[0-9a-z\-_]*)|(tiw[0-9a-z\-_]*)/gi]]>
        /// </summary>
        private static readonly Regex regex1W = new Regex(@"(biw[0-9a-z\-_]*)|(qw[0-9a-z\-_]*)|(tiw[0-9a-z\-_]*)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// <![CDATA[/([qbt]?(?<d>[2-9])w[0-9a-z\-_]*)/gi]]>
        /// </summary>
        private static readonly Regex regexNW = new Regex(@"([qbt]?(?<d>[2-9])w[0-9a-z\-_]*)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// 分析频次编码，规范频次规则
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public static FrequencyRule UnitAnalysis(Frequency frequency)
        {
            //频次单位已经填写的情况下
            if (!string.IsNullOrWhiteSpace(frequency.Unit))
            {
                return UnitExists(frequency);
            }

            var code = frequency.FrequencyCode.ToLower();

            //1H,NH的情况
            var matchNH = regexNH.Match(code);
            if (matchNH.Success)
            {
                return UnitNH(frequency, matchNH);
            }

            //常见的已知标准1D编码
            var m1dArray = new[] { "hs", "qm", "qn", "om", "on", "qd", "q1d" };
            if (m1dArray.Contains(code))
            {
                frequency.Unit = "1D";
                return new FrequencyRule { Frequency = frequency, UnitType = FrequencyUnitType.Daily, UnitOffset = 1 };
            }

            //常见的已知标准1W编码
            var m1wArray = new[] { "qw", "bw", "tw", "biw", "tiw" };
            if (m1wArray.Contains(code))
            {
                frequency.Unit = "1W";
                return new FrequencyRule { Frequency = frequency, UnitType = FrequencyUnitType.Weekly, UnitOffset = 1 };
            }

            //常见的已知标准2W编码
            var m2wArray = new[] { "qiw", "qow", "bow", "tow" };
            if (m2wArray.Contains(code))
            {
                frequency.Unit = "2W";
                return new FrequencyRule { Frequency = frequency, UnitType = FrequencyUnitType.NWeek, UnitOffset = 2 };
            }

            if (regex1D.IsMatch(code))
            {
                frequency.Unit = "1D";
                return new FrequencyRule { Frequency = frequency, UnitType = FrequencyUnitType.Daily, UnitOffset = 1 };
            }

            var matchND = regexND.Match(code);
            if (matchND.Success)
            {
                //var v = matchND.Groups.ToArray()[1..];
                var d = matchND.Groups["d"].Value;
                if (d == "o" || d == "O")
                {
                    d = "2";
                }
                frequency.Unit = $"{d}D";
                return new FrequencyRule { Frequency = frequency, UnitType = FrequencyUnitType.NDay, UnitOffset = int.Parse(d) };
            }

            if (regex1W.IsMatch(code))
            {
                frequency.Unit = "1W";
                return new FrequencyRule { Frequency = frequency, UnitType = FrequencyUnitType.Weekly, UnitOffset = 1 };
            }

            var matchNW = regexNW.Match(code);
            if (matchNW.Success)
            {
                var d = matchNW.Groups["d"].Value;
                frequency.Unit = $"{d}W";
                return new FrequencyRule { Frequency = frequency, UnitType = FrequencyUnitType.NWeek, UnitOffset = int.Parse(d) };
            }

            frequency.Unit = "ST";
            return new FrequencyRule { Frequency = frequency, UnitType = FrequencyUnitType.ST, UnitOffset = 0 };

            static FrequencyRule UnitExists(Frequency frequency)
            {
                //检查频次单位填写的格式
                var matchNO = regexNO.Match(frequency.Unit);
                if (matchNO.Success)
                {
                    var n = matchNO.Groups["n"].Value;
                    var t = matchNO.Groups["t"].Value;
                    var nt = n == "1" ? "1" : "N";
                    var nct = $"{nt}{t.ToUpper()}";
                    var unitType = nct switch
                    {
                        "1H" => FrequencyUnitType.Hour,     //1小时及以下
                        "NH" => FrequencyUnitType.NHour,    //2小时及以上
                        "1D" => FrequencyUnitType.Daily,    //每天
                        "ND" => FrequencyUnitType.NDay,     //2天及以上周期
                        "1W" => FrequencyUnitType.Weekly,   //每周
                        "NW" => FrequencyUnitType.NWeek,    //2周及以上周期
                        _ => FrequencyUnitType.ST,
                    };
                    var offset = int.Parse(n);
                    return new FrequencyRule { Frequency = frequency, UnitType = unitType, UnitOffset = offset };
                }
                return new FrequencyRule { Frequency = frequency, UnitType = FrequencyUnitType.ST, UnitOffset = 0 };
            }

            static FrequencyRule UnitNH(Frequency frequency, Match matchNH)
            {
                var n = matchNH.Groups["n"].Value;
                var d = Convert.ToSingle(n);
                if (d * (frequency.Times ?? 0) == 24)   //频率单位前的数字乘以频次次数等于24表示以1D为单位
                {
                    frequency.Unit = $"1D";
                    return new FrequencyRule { Frequency = frequency, UnitType = FrequencyUnitType.Daily, UnitOffset = 1 };
                }
                else
                {
                    if (n == "0.25" || n == "0.5" || n == "1" || d <= 1)
                    {
                        frequency.Unit = "1H";
                        return new FrequencyRule { Frequency = frequency, UnitType = FrequencyUnitType.Hour, UnitOffset = 1 };
                    }
                    else
                    {
                        frequency.Unit = $"{n}H";
                        return new FrequencyRule { Frequency = frequency, UnitType = FrequencyUnitType.NHour, UnitOffset = int.Parse(n) };

                    }
                }
            }
        }


    }
}

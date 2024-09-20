namespace YiJian.BodyParts.Domain.Shared.Extension
{
    public static class DecimalExtend
    {
        /// <summary>
        /// decimal转字符串，去掉小数位后面的0，且最多保留两位小数
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string ToStrWithoutZero(this decimal d)
        {
            var ss = d.ToString().Split('.');
            var strPoing = string.Empty;
            if (ss.Length == 2)
            {
                strPoing = ss[1].Substring(0, ss[1].Length > 2 ? 2 : ss[1].Length);
            }
            var dStr = $"{ss[0]}.{strPoing}";
            return decimal.Parse(dStr).ToString("0.##");
        }
    }
}

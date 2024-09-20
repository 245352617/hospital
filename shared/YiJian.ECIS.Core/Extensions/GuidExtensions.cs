using System;
using DbPrimaryType = System.String;

namespace YiJian.ECIS.Core
{
    public static class GuidExtensions
    {
        public static DbPrimaryType Create(string text = null)
        {
            return string.IsNullOrEmpty(text) ? Guid.NewGuid().ToString() : new Guid(text).ToString();
        }
        public static DbPrimaryType Empty = Guid.Empty.ToString();


    }
}

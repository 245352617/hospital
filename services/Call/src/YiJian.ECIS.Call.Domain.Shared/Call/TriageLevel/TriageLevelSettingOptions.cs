using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.ECIS.Call
{
    /// <summary>
    /// 分诊等级设置
    /// Author: ywlin
    /// Date: 2021-11-23
    /// </summary>
    public class TriageLevelSettingOptions : List<TriageLevelSetting>
    {
        public TriageLevelSetting this[string key]
        {
            get
            {
                if (key == null) throw new ArgumentNullException("key");
                if (!this.Exists(x => x.Key == key)) return null;
                return this.Find(x => x.Key == key);
            }
        }
    }
}

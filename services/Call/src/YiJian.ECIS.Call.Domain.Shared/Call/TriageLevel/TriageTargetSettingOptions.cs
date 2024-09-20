using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.ECIS.Call
{
    /// <summary>
    /// 分诊去向设置
    /// Author: ywlin
    /// Date: 2021-11-23
    /// </summary>
    public class TriageTargetSettingOptions : List<TriageTartgetSetting>
    {
        public TriageTartgetSetting this[string code]
        {
            get
            {
                if (code == null) throw new ArgumentNullException("code");
                if (!this.Exists(x => x.Code == code)) return null;
                return this.Find(x => x.Code == code);
            }
        }
    }
}

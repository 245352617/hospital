using System.Collections.Generic;
using System.Text;

namespace YiJian.EMR.Writes.Dto
{
    /// <summary>
    /// 电子病历分类汇总记录
    /// </summary>
    public class EmrCategoryDto
    {
        /// <summary>
        /// 抢救
        /// </summary>
        public EmrCategoryCountDto QiangJiu { get;set; }

        /// <summary>
        /// 心肺复苏
        /// </summary>
        public EmrCategoryCountDto XinFeiFuSu { get; set; }
    }

}

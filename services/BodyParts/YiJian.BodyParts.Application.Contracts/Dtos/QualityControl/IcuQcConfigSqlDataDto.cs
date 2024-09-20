using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class IcuQcConfigSqlDataDto
    {
        /// <summary>
        /// 数据
        /// </summary>
        public List<Dictionary<string,string>> Data { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public List<string> ColumnName { get; set; }
    }
}

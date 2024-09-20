using System;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class ExcelAttribute : System.Attribute
    {
        /// <summary>
        /// 表头列名
        /// </summary>
        public string ColumnName { get; set; } = default!;

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        public ExcelAttribute(string columnName, int sort)
        {
            ColumnName = columnName;
            Sort = sort;
        }
    }
}

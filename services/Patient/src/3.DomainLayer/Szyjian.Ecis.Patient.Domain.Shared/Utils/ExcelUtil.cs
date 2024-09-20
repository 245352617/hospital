using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    public class ExcelUtil
    {
        public static byte[] ExportExcel<T>(IEnumerable<T> list)
        {
            //创建工作薄
            HSSFWorkbook workbook = new HSSFWorkbook();
            //创建表
            ISheet table = workbook.CreateSheet("Sheet1");
            Type type = typeof(T);

            PropertyInfo[] propertyInfos = type.GetProperties();
            List<ColumnInfo> columnInfoList = new List<ColumnInfo>();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                ExcelAttribute excelAttribute = propertyInfo.GetCustomAttribute<ExcelAttribute>();
                if (excelAttribute == null)
                {
                    continue;
                }

                ColumnInfo columnInfo = new ColumnInfo()
                {
                    ColumnName = excelAttribute.ColumnName,
                    Sort = excelAttribute.Sort,
                    PropertyInfo = propertyInfo
                };

                columnInfoList.Add(columnInfo);
            }

            columnInfoList.Sort(new ColumnInfo());
            IRow rowTitle = table.CreateRow(0);
            for (int i = 0; i < columnInfoList.Count; i++)
            {
                ICell cell = rowTitle.CreateCell(i);
                cell.SetCellValue(columnInfoList[i].ColumnName);
            }

            int count = 1;
            foreach (T item in list)
            {
                IRow row = table.CreateRow(count);
                for (int i = 0; i < columnInfoList.Count; i++)
                {
                    ICell cell = row.CreateCell(i);
                    object objValue = columnInfoList[i].PropertyInfo.GetValue(item);
                    //更多的转换细节自己去调咯
                    if (columnInfoList[i].PropertyInfo.PropertyType.IsEnum && objValue != null)
                    {
                        string description = EnumUtils.GetDescription(objValue);
                        cell.SetCellValue(description);
                    }
                    else if (columnInfoList[i].PropertyInfo.PropertyType == typeof(DateTime) && objValue != null)
                    {
                        cell.SetCellValue(((DateTime)objValue).ToString("yyyy/MM/dd HH:mm:ss"));
                    }
                    else if (IsNullableDateTime(columnInfoList[i].PropertyInfo))
                    {
                        if (objValue is null)
                        {
                            cell.SetCellValue(objValue?.ToString());
                        }
                        else
                        {
                            cell.SetCellValue(((DateTime)objValue).ToString("yyyy/MM/dd HH:mm:ss"));

                        }
                    }
                    else
                    {
                        cell.SetCellValue(objValue?.ToString());
                    }
                }

                count++;
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                workbook.Write(memoryStream);
                memoryStream.Position = 0;
                return memoryStream.GetBuffer();
            }
        }
        public static bool IsNullableDateTime(PropertyInfo propertyInfo)
        {
            Type propertyType = propertyInfo.PropertyType;
            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return propertyType.GetGenericArguments()[0] == typeof(DateTime);
            }
            return false;
        }



        class ColumnInfo : IComparer<ColumnInfo>
        {
            /// <summary>
            /// 列名
            /// </summary>
            public string ColumnName { get; set; } = default!;

            /// <summary>
            /// 排序
            /// </summary>
            public int Sort { get; set; }

            /// <summary>
            /// 属性信息
            /// </summary>
            public PropertyInfo PropertyInfo { get; set; } = default!;

            public int Compare(ColumnInfo x, ColumnInfo y)
            {
                if (x?.Sort > y?.Sort)
                {
                    return -1;
                }

                return 1;
            }
        }
    }
}

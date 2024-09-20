using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// List导出到Excel文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelHelper<T> where T : new()
    {
        private static readonly ConcurrentDictionary<string, object> _dictCache =
            new ConcurrentDictionary<string, object>();

        #region 得到类里面的属性集合

        /// <summary>
        /// 得到类里面的属性集合
        /// </summary>
        /// <param name="type"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private static PropertyInfo[] GetProperties(Type type, string[] columns = null)
        {
            PropertyInfo[] properties;
            if (_dictCache.ContainsKey(type.FullName))
            {
                properties = _dictCache[type.FullName] as PropertyInfo[];
            }
            else
            {
                properties = type.GetProperties();
                _dictCache.TryAdd(type.FullName, properties);
            }

            if (columns != null && columns.Length > 0)
            {
                //  按columns顺序返回属性
                var result = columns
                    .Select(column => properties.FirstOrDefault(p => p.Name.ToLower() == column.ToLower()))
                    .Where(columnProperty => columnProperty != null);
                return result.ToArray();
            }
            else
            {
                return properties;
            }
        }

        #endregion

        #region List导出到Excel文件

        /// <summary>
        /// List导出到Excel文件
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        /// <param name="sHeaderText"></param>
        /// <param name="list"></param>
        /// <param name="columns"></param>
        /// <param name="columnNames"></param>
        public byte[] ExportToExcel(IWebHostEnvironment hostingEnvironment, string sHeaderText, List<T> list,
            string[] columns, string[] columnNames)
        {
            var sRoot = hostingEnvironment.ContentRootPath;
            var partDirectory = string.Format("Resource{0}Export{0}Excel", Path.DirectorySeparatorChar);
            var sDirectory = Path.Combine(sRoot, partDirectory);
            if (!Directory.Exists(sDirectory))
            {
                Directory.CreateDirectory(sDirectory);
            }

            using var ms = CreateExportMemoryStream(list, sHeaderText, columns, columnNames);
            var buffer = ms.GetBuffer();
            ms.Close();
            return buffer;
        }

        /// <summary>  
        /// List导出到Excel的MemoryStream  
        /// </summary>  
        /// <param name="list">数据源</param>  
        /// <param name="sHeaderText">表头文本</param>  
        /// <param name="columns">需要导出的属性</param>
        /// <param name="columnNames"></param>  
        private MemoryStream CreateExportMemoryStream(List<T> list, string sHeaderText, string[] columns,
            string[] columnNames)
        {
            try
            {
                var workbook = new HSSFWorkbook();
                var sheet = workbook.CreateSheet();

                var type = typeof(T);
                var properties = GetProperties(type, columns);

                var dateStyle = workbook.CreateCellStyle();
                var format = workbook.CreateDataFormat();
                dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd");
                //单元格填充循环外设定单元格格式，避免4000行异常
                var contentStyle = workbook.CreateCellStyle();
                contentStyle.Alignment = HorizontalAlignment.Left;

                #region 取得每列的列宽（最大宽度）

                var arrColWidth = new int[properties.Length];
                for (var columnIndex = 0; columnIndex < properties.Length; columnIndex++)
                {
                    //GBK对应的code page是CP936
                    arrColWidth[columnIndex] = properties[columnIndex].Name.Length;
                }

                #endregion

                for (var rowIndex = 0; rowIndex < list.Count; rowIndex++)
                {
                    #region 新建表，填充表头，填充列头，样式

                    if (rowIndex == 65535 || rowIndex == 0)
                    {
                        if (rowIndex != 0)
                        {
                            sheet = workbook.CreateSheet();
                        }

                        #region 表头及样式

                        {
                            var headerRow = sheet.CreateRow(0);
                            headerRow.HeightInPoints = 25;
                            headerRow.CreateCell(0).SetCellValue(sHeaderText);

                            var headStyle = workbook.CreateCellStyle();
                            headStyle.Alignment = HorizontalAlignment.Center;
                            var font = workbook.CreateFont();
                            font.FontHeightInPoints = 20;
                            headStyle.SetFont(font);

                            headerRow.GetCell(0).CellStyle = headStyle;

                            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, properties.Length - 1));
                        }

                        #endregion

                        #region 列头及样式

                        {
                            var headerRow = sheet.CreateRow(1);
                            var headStyle = workbook.CreateCellStyle();
                            headStyle.Alignment = HorizontalAlignment.Center;
                            var font = workbook.CreateFont();
                            font.FontHeightInPoints = 10;
                            headStyle.SetFont(font);

                            for (var columnIndex = 0; columnIndex < columnNames.Length; columnIndex++)
                            {
                                var description = columnNames[columnIndex];
                                headerRow.CreateCell(columnIndex).SetCellValue(description);
                                headerRow.GetCell(columnIndex).CellStyle = headStyle;
                                //根据表头设置列宽  
                                sheet.SetColumnWidth(columnIndex, (arrColWidth[columnIndex] + 1) * 256);
                            }
                        }

                        #endregion
                    }

                    #endregion

                    #region 填充内容

                    var dataRow = sheet.CreateRow(rowIndex + 2); // 前面2行已被占用
                    for (var columnIndex = 0; columnIndex < properties.Length; columnIndex++)
                    {
                        var newCell = dataRow.CreateCell(columnIndex);
                        newCell.CellStyle = contentStyle;
                        var drValue = properties[columnIndex].GetValue(list[rowIndex], null).ParseToString();
                        //根据单元格内容设定列宽
                        var length = (Encoding.UTF8.GetBytes(drValue).Length + 1) * 256;
                        if (sheet.GetColumnWidth(columnIndex) < length && drValue != "")
                        {
                            sheet.SetColumnWidth(columnIndex, length);
                        }

                        switch (properties[columnIndex].PropertyType.ToString())
                        {
                            case "System.String":
                                newCell.SetCellValue(drValue);
                                break;

                            case "System.DateTime":
                                newCell.SetCellValue(Convert.ToDateTime(drValue).ToString("yyyy-MM-dd HH:mm:ss"));
                                newCell.CellStyle = dateStyle; //格式化显示  
                                break;
                            case "System.Nullable`1[System.DateTime]":

                                break;

                            case "System.Boolean":
                            case "System.Nullable`1[System.Boolean]":
                                newCell.SetCellValue(drValue.ParseToBool());
                                break;

                            case "System.Byte":
                            case "System.Nullable`1[System.Byte]":
                            case "System.Int16":
                            case "System.Nullable`1[System.Int16]":
                            case "System.Int32":
                            case "System.Nullable`1[System.Int32]":
                                newCell.SetCellValue(drValue.ParseToInt());
                                break;

                            case "System.Int64":
                            case "System.Nullable`1[System.Int64]":
                                newCell.SetCellValue(drValue.ParseToString());
                                break;

                            case "System.Double":
                            case "System.Nullable`1[System.Double]":
                                newCell.SetCellValue(drValue.ParseToDouble());
                                break;

                            case "System.Single":
                            case "System.Nullable`1[System.Single]":
                                newCell.SetCellValue(drValue.ParseToDouble());
                                break;

                            case "System.Decimal":
                            case "System.Nullable`1[System.Decimal]":
                                newCell.SetCellValue(drValue.ParseToDouble());
                                break;

                            case "System.DBNull":
                                newCell.SetCellValue(string.Empty);
                                break;

                            default:
                                newCell.SetCellValue(string.Empty);
                                break;
                        }
                    }

                    #endregion
                }

                var ms = new MemoryStream();
                workbook.Write(ms);
                workbook.Close();
                ms.Flush();
                ms.Position = 0;
                return ms;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion


        /// <summary>
        /// 查找Excel列名对应的实体属性
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private PropertyInfo MapPropertyInfo(string columnName)
        {
            var propertyList = GetProperties(typeof(T));
            var propertyInfo = propertyList.FirstOrDefault(p => p.Name == columnName);
            return propertyInfo != null
                ? propertyInfo
                : (from tempPropertyInfo in propertyList
                    let attributes =
                        (DescriptionAttribute[])tempPropertyInfo.GetCustomAttributes(typeof(DescriptionAttribute),
                            false)
                    where attributes.Length > 0
                    where attributes[0].Description == columnName
                    select tempPropertyInfo).FirstOrDefault();
        }
    }
}
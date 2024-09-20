using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using Volo.Abp.DependencyInjection;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 将DataTable导出到Excel
    /// </summary>
    public class DataTableExtensions : ITransientDependency
    {
        private readonly ILogger<DataTableExtensions> _log;
        private readonly Dictionary<int, int> _dictMaxLength = new Dictionary<int, int>();
        private readonly List<ReportHeaderDto> _hasDataHeaderList = new List<ReportHeaderDto>();

        public DataTableExtensions(ILogger<DataTableExtensions> log)
        {
            _log = log;
        }

        /// <summary>
        /// 导出为Excel
        /// </summary>
        /// <param name="headerDtos">表头Dto</param>
        /// <param name="dt">数据源</param>
        /// <returns></returns>
        public async Task<byte[]> DataTableToExcelAsync(List<ReportHeaderDto> headerDtos, DataTable dt)
        {
            try
            {
                var workBook = new HSSFWorkbook();
                var sheet = workBook.CreateSheet();
                //单元格填充循环外设定单元格格式，避免4000行异常
                var rowIndex = 0;
                var colIndex = 0;
                var cellStyle = workBook.CreateCellStyle();
                cellStyle.Alignment = HorizontalAlignment.Center;
                cellStyle.VerticalAlignment = VerticalAlignment.Center;
                // 生成表头
                CreateExcelHeader(sheet, rowIndex, colIndex, cellStyle, headerDtos);
                MergeCell(sheet, rowIndex, colIndex, headerDtos);
                if (_hasDataHeaderList.Count <= 0) return null;
                rowIndex = sheet.LastRowNum;
                var contentCellStyle = workBook.CreateCellStyle();
                var font = workBook.CreateFont();
                font.FontHeightInPoints = 10;
                contentCellStyle.SetFont(font);
                contentCellStyle.Alignment = HorizontalAlignment.Center;
                contentCellStyle.VerticalAlignment = VerticalAlignment.Center;
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var row = sheet.CreateRow(i + rowIndex + 1);
                    row.RowStyle = contentCellStyle;
                    row.HeightInPoints = 15;
                    var columnIndex = 0;
                    foreach (var header in _hasDataHeaderList)
                    {
                        if (!string.IsNullOrWhiteSpace(dt.Rows[i][header.HeaderField.ToLowerInvariant()].ToString()))
                        {
                            if (dt.Rows[i][header.HeaderField.ToLowerInvariant()].ToString().Length >
                                _dictMaxLength[columnIndex])
                            {
                                _dictMaxLength[columnIndex] =
                                    dt.Rows[i][header.HeaderField.ToLowerInvariant()].ToString().Length;
                            }
                        }

                        var cell = row.CreateCell(columnIndex);
                        cell.CellStyle = contentCellStyle;
                        cell.SetCellValue(dt.Rows[i][header.HeaderField.ToLowerInvariant()].ToString());
                        columnIndex++;
                    }
                }

                foreach (var (key, value) in _dictMaxLength)
                {
                    sheet.SetColumnWidth(key, (value + 10) * 256);
                }

                await using var ms = new MemoryStream();
                workBook.Write(ms);
                workBook.Close();
                await ms.FlushAsync();
                ms.Position = 0;
                return ms.GetBuffer();
            }
            catch (Exception e)
            {
                _log.LogWarning($"【DataTableExtensions】【DataTableToExcel】【导出为Excel错误】【Msg：{e}】");
                return null;
            }
        }



        /// <summary>
        /// 递归生成Excel表头
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="colIndex">列索引</param>
        /// <param name="cellStyle">表格样式</param>
        /// <param name="dtos">表头Dto</param>
        /// <returns></returns>
        private void CreateExcelHeader(ISheet sheet, int rowIndex, int colIndex, ICellStyle cellStyle,
            IReadOnlyCollection<ReportHeaderDto> dtos)
        {
            if (dtos == null)
            {
                return;
            }

            var row = sheet.GetRow(rowIndex);
            if (row == null)
            {
                row = sheet.CreateRow(rowIndex);
            }

            var font = row.Sheet.Workbook.CreateFont();
            font.FontHeightInPoints = 12;
            font.IsBold = true;
            cellStyle.SetFont(font);
            row.HeightInPoints = 25;
            foreach (var item in dtos)
            {
                var cell = row.CreateCell(colIndex);
                colIndex++;
                cell.SetCellValue(item.HeaderName);
                cell.CellStyle = cellStyle;
                // 增加子集表头
                if (item.Children != null && item.Children.Count > 0)
                {
                    CreateExcelHeader(sheet, rowIndex + 1, colIndex - 1, cellStyle, item.Children.ToList());
                    /*// 有子级父级表头向右合并列
                    sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, colIndex - 1,
                        colIndex - 1 + item.Children.Count - 1));*/
                    colIndex += item.Children.Count - 1;
                }
                else
                {
                    var columnIndex = _dictMaxLength.Count;
                    _dictMaxLength.Add(columnIndex, item.HeaderName.Length);
                    _hasDataHeaderList.Add(item);
                }
            }
        }

        /// <summary>
        /// 合并区域
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="colIndex"></param>
        /// <param name="dtos"></param>
        /// <param name="rowIndex"></param>
        private void MergeCell(ISheet sheet, int rowIndex, int colIndex, List<ReportHeaderDto> dtos)
        {
            for (var i = 0; i < dtos.Count; i++)
            {
                if (dtos[i].Children == null || dtos[i].Children.Count <= 0)
                {
                    if (sheet.LastRowNum > rowIndex)
                    {
                        sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex + 1, colIndex, colIndex));
                    }
                }
                else
                {
                    sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, colIndex, colIndex + 1));
                    MergeCell(sheet, rowIndex + 1, colIndex, dtos[i].Children.ToList());
                    colIndex++;
                }

                colIndex++;
            }
        }
    }
}
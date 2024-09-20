using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Dtos;

namespace Szyjian.Ecis.Patient.Application
{
    [Authorize]
    public class TraumaPatientsStatistics : EcisPatientAppService
    {
        private readonly IFreeSql _freeSql;

        public TraumaPatientsStatistics(IFreeSql freeSql)
        {
            _freeSql = freeSql;
        }

        public ResponseResult<PagedResultDto<TraumaPatientsResponseDto>> GetTraumaPatientsStatistics(TraumaPatientsRequestDto TraumaPatientsRequestDto)
        {
            if (TraumaPatientsRequestDto == null)
            {
                return RespUtil.Error(data: new PagedResultDto<TraumaPatientsResponseDto>(), msg: "请求参数为空");
            }

            PagedResultDto<TraumaPatientsResponseDto> crisisDtos = QueryTraumaPatientsStatistics(TraumaPatientsRequestDto);
            return RespUtil.Ok(data: crisisDtos);
        }


        public FileResult ExportTraumaPatientsStatistics(TraumaPatientsRequestDto rescueRequestDto)
        {
            rescueRequestDto.Index = 1;
            rescueRequestDto.PageCount = int.MaxValue;
            try
            {
                PagedResultDto<TraumaPatientsResponseDto> crisisDtos = QueryTraumaPatientsStatistics(rescueRequestDto);

                byte[] bytes = ExcelUtil.ExportExcel(crisisDtos.Items);
                FileContentResult fileContentResult = new FileContentResult(bytes, "application/vnd.ms-excel");
                fileContentResult.FileDownloadName = string.Format("{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmss"));
                return fileContentResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private PagedResultDto<TraumaPatientsResponseDto> QueryTraumaPatientsStatistics(TraumaPatientsRequestDto TraumaPatientsRequestDto)
        {
            string startTime = null;
            if (TraumaPatientsRequestDto.StartTime.HasValue)
            {
                startTime = TraumaPatientsRequestDto.StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }

            string endTime = null;
            if (TraumaPatientsRequestDto.EndTime.HasValue)
            {
                endTime = TraumaPatientsRequestDto.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }

            DbParameter outParameter = null;
            DataTable dataTable = _freeSql.Ado.CommandFluent("sp_traumapatients_statistics")
                 .CommandType(CommandType.StoredProcedure)
                 .CommandTimeout(60)
                 .WithParameter("pageNumber", TraumaPatientsRequestDto.Index, x =>
                 {
                     x.DbType = DbType.Int32;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("pageSize", TraumaPatientsRequestDto.PageCount, x =>
                 {
                     x.DbType = DbType.Int32;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("rowTotal", null, x =>
                 {
                     outParameter = x;
                     x.DbType = DbType.Int32;
                     x.Direction = ParameterDirection.Output;
                 })
                 .WithParameter("beginDate", startTime, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("endDate", endTime, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                 .ExecuteDataTable();

            int rowTotal = (int)outParameter.Value;
            List<TraumaPatientsResponseDto> TraumaPatientsResponseDtos = DataTableToList<TraumaPatientsResponseDto>(dataTable);

            PagedResultDto<TraumaPatientsResponseDto> res = new PagedResultDto<TraumaPatientsResponseDto>
            {
                TotalCount = rowTotal,
                Items = TraumaPatientsResponseDtos
            };

            return res;
        }

        private List<T> DataTableToList<T>(DataTable dataTable) where T : new()
        {
            List<T> list = new List<T>();

            foreach (DataRow row in dataTable.Rows)
            {
                T obj = new T();
                foreach (DataColumn column in dataTable.Columns)
                {
                    PropertyInfo propertyInfo = typeof(T).GetProperty(column.ColumnName);
                    if (propertyInfo != null && row[column] != DBNull.Value)
                    {
                        propertyInfo.SetValue(obj, row[column]);
                    }
                }
                list.Add(obj);
            }

            return list;
        }
    }
}

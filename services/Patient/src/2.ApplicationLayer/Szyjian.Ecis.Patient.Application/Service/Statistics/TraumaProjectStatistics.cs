using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain.Shared;
using Volo.Abp.Application.Dtos;

namespace Szyjian.Ecis.Patient.Application
{
    [Authorize]
    public class TraumaProjectStatistics : EcisPatientAppService
    {
        private readonly IFreeSql _freeSql;

        public TraumaProjectStatistics(IFreeSql freeSql)
        {
            _freeSql = freeSql;
        }

        public ResponseResult<PagedResultDto<TraumaProjectResponseDto>> GetTraumaProjectStatistics(TraumaProjectRequestDto traumaProjectRequestDto)
        {
            if (traumaProjectRequestDto == null)
            {
                return RespUtil.Error(data: new PagedResultDto<TraumaProjectResponseDto>(), msg: "请求参数为空");
            }

            List<TraumaProjectResponseDto> traumaPatientsResponseDtos = QueryTraumaProjectStatistics(traumaProjectRequestDto);

            int rowTotal = traumaPatientsResponseDtos.Count;
            traumaPatientsResponseDtos = traumaPatientsResponseDtos.Skip((traumaProjectRequestDto.Index - 1) * traumaProjectRequestDto.PageCount).Take(traumaProjectRequestDto.PageCount).ToList();

            PagedResultDto<TraumaProjectResponseDto> res = new PagedResultDto<TraumaProjectResponseDto>
            {
                TotalCount = rowTotal,
                Items = traumaPatientsResponseDtos
            };

            return RespUtil.Ok(data: res);
        }

        public FileResult ExportTraumaPatientsStatistics(TraumaProjectRequestDto traumaProjectRequestDto)
        {
            try
            {
                List<TraumaProjectResponseDto> traumaProjectResponseDtos = QueryTraumaProjectStatistics(traumaProjectRequestDto);

                byte[] bytes = ExcelUtil.ExportExcel(traumaProjectResponseDtos);
                FileContentResult fileContentResult = new FileContentResult(bytes, "application/vnd.ms-excel");
                fileContentResult.FileDownloadName = string.Format("{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmss"));
                return fileContentResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private List<TraumaProjectResponseDto> QueryTraumaProjectStatistics(TraumaProjectRequestDto traumaProjectRequestDto)
        {
            DataTable dataTable = _freeSql.Ado.CommandFluent("sp_trauma_project")
                  .CommandType(CommandType.StoredProcedure)
                  .CommandTimeout(30 * 60 * 60)
                  .WithParameter("beginDate", traumaProjectRequestDto.StartTime, x =>
                  {
                      x.DbType = DbType.DateTime2;
                      x.Direction = ParameterDirection.Input;
                  })
                 .WithParameter("endDate", traumaProjectRequestDto.EndTime, x =>
                 {
                     x.DbType = DbType.DateTime2;
                     x.Direction = ParameterDirection.Input;
                 })
                 .WithParameter("doctorCode", traumaProjectRequestDto.DoctorCode, x =>
                 {
                     x.DbType = DbType.String;
                     x.Direction = ParameterDirection.Input;
                 })
                .WithParameter("searchText", traumaProjectRequestDto.SearchText, x =>
                                  {
                                      x.DbType = DbType.String;
                                      x.Direction = ParameterDirection.Input;
                                  })
                 .ExecuteDataTable();

            List<TraumaProjectResponseDto> TraumaPatientsResponseDtos = DataTableToList<TraumaProjectResponseDto>(dataTable);

            if (!string.IsNullOrEmpty(traumaProjectRequestDto.ProjectName))
            {
                TraumaPatientsResponseDtos = TraumaPatientsResponseDtos.Where(x => x.Name == traumaProjectRequestDto.ProjectName).ToList();
            }
            return TraumaPatientsResponseDtos;
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

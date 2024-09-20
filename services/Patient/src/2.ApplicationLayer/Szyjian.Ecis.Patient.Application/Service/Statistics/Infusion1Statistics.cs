using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application
{
    [Authorize]
    public class Infusion1Statistics : EcisPatientAppService
    {
        private readonly IFreeSql _freeSql;

        public Infusion1Statistics(IFreeSql freeSql)
        {
            _freeSql = freeSql;
        }

        public ResponseResult<List<Infusion1ResponseDto>> GetInfusion1Statistics(Infusion1RequestDto infusion1RequestDto)
        {
            if (infusion1RequestDto == null)
            {
                return RespUtil.Error(data: new List<Infusion1ResponseDto>(), msg: "请求参数为空");
            }

            string startTime = null;
            if (infusion1RequestDto.StartTime.HasValue)
            {
                startTime = infusion1RequestDto.StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }

            string endTime = null;
            if (infusion1RequestDto.EndTime.HasValue)
            {
                endTime = infusion1RequestDto.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }

            DataTable dataTable = _freeSql.Ado.CommandFluent("sp_infusion_statistics1")
                        .CommandType(CommandType.StoredProcedure)
                        .CommandTimeout(30 * 60 * 60)
                        .WithParameter("startTime", startTime, x =>
                        {
                            x.DbType = DbType.String;
                            x.Direction = ParameterDirection.Input;
                        })
                        .WithParameter("endTime", endTime, x =>
                        {
                            x.DbType = DbType.String;
                            x.Direction = ParameterDirection.Input;
                        })
                        .ExecuteDataTable();

            List<Infusion1ResponseDto> rescueResponseDtos = DataTableToList<Infusion1ResponseDto>(dataTable);
            if (!string.IsNullOrEmpty(infusion1RequestDto.DoctorName))
            {
                rescueResponseDtos = rescueResponseDtos.Where(x => x.FirstDoctorCode.Contains(infusion1RequestDto.DoctorName) || x.FirstDoctorName.Contains(infusion1RequestDto.DoctorName)).ToList();
            }

            rescueResponseDtos = rescueResponseDtos.OrderByDescending(x => x.Percentage).ToList();

            return RespUtil.Ok(data: rescueResponseDtos);
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
                    if (propertyInfo != null && propertyInfo.CanWrite && row[column] != DBNull.Value)
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

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using Dapper;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.DependencyInjection;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class DapperRepository : IDapperRepository, ISingletonDependency
    {
        private ConcurrentDictionary<string, string> _connStrDit = new ConcurrentDictionary<string, string>();
        private readonly IConfiguration _configuration;
        private readonly NLogHelper _log;

        public DapperRepository(IConfiguration configuration, NLogHelper log)
        {
            _configuration = configuration;
            _log = log;
        }

        /// <summary>
        ///  将IDataReader转换为DataTable
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private DataTable DataTableToIDataReader(IDataReader reader)
        {
            var objDataTable = new DataTable("Table");
            try
            {
                var intFieldCount = reader.FieldCount;
                for (var intCounter = 0; intCounter < intFieldCount; ++intCounter)
                {
                    objDataTable.Columns.Add(reader.GetName(intCounter).ToUpper(), reader.GetFieldType(intCounter));
                }

                objDataTable.BeginLoadData();
                var objValues = new object[intFieldCount];
                while (reader.Read())
                {
                    reader.GetValues(objValues);
                    objDataTable.LoadDataRow(objValues, true);
                }

                reader.Close();
                objDataTable.EndLoadData();
                return objDataTable;
            }
            catch (Exception e)
            {
                Console.WriteLine($"【DapperHelper】【DataTableToIDataReader】" +
                                  $"【将IDataReader转换为DataTable错误】【Msg：{e}】");
                return objDataTable;
            }

        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="dbKey"></param>
        /// <param name="connectionStringKey"></param>
        /// <returns></returns>
        private string GetConnectionString(string dbKey, string connectionStringKey)
        {
            string connString;
            if (_connStrDit.ContainsKey(dbKey))
            {
                connString = _connStrDit[dbKey];
            }
            else
            {
                connString = connectionStringKey;//_configuration[$"ConnectionStrings:{connectionStringKey}"];
                _connStrDit.TryAdd(dbKey, connString);
            }

            return connString;
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbKey"></param>
        /// <param name="connectionStringKey"></param>
        /// <param name="dbTypeEnum"></param>
        /// <returns></returns>
        public IDbConnection GetConnection(string dbKey, string connectionStringKey, DapperDbTypeEnum dbTypeEnum)
        {
            IDbConnection connObj = null;
            try
            {
                switch (dbTypeEnum)
                {
                    case DapperDbTypeEnum.SqlServer:
                        var connectionString = GetConnectionString(dbKey, connectionStringKey);
                        connObj = new SqlConnection(connectionString);
                        break;
                    case DapperDbTypeEnum.MySql:
                        break;
                }

                if (connObj == null) return null;
                if (connObj.State != ConnectionState.Open)
                {
                    connObj.Open();
                }

                return connObj;
            }
            catch (Exception e)
            {
                _log.Warning($"【DapperRepository】【GetConnection】【获取数据库连接错误】【Msg：{e}】");
                return connObj;
            }
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public IDbTransaction GetDbTransaction(IDbConnection conn)
        {
            return conn.BeginTransaction();
        }

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql查询参数</param>
        /// <param name="dbKey">数据库键（默认TriageService数据库）</param>
        /// <param name="connectionStringKey"></param>
        /// <param name="buffered">是否缓冲</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="dapperDbTypeEnum">数据库类型（默认SqlServer）</param>
        /// <returns></returns>
        public async Task<T> QueryAsync<T>(string sql, object param = null, string dbKey = "TriageService", string connectionStringKey = "DefaultConnection",
            bool buffered = true,
            int? commandTimeout = default, CommandType? commandType = default,
            DapperDbTypeEnum dapperDbTypeEnum = DapperDbTypeEnum.SqlServer)
        {
            using var conn = GetConnection(dbKey, connectionStringKey, dapperDbTypeEnum);
            var res = await conn.QueryFirstAsync<T>(sql, param, null, commandTimeout, commandType);
            return res;
        }

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql查询参数</param>
        /// <param name="dbKey">数据库键（默认TriageService数据库）</param>
        /// <param name="connectionStringKey"></param>
        /// <param name="buffered">是否缓冲</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="dapperDbTypeEnum">数据库类型（默认SqlServer）</param>
        /// <returns></returns>
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, string dbKey = "TriageService", string connectionStringKey = "DefaultConnection",
            bool buffered = true,
            int? commandTimeout = default, CommandType? commandType = default,
            DapperDbTypeEnum dapperDbTypeEnum = DapperDbTypeEnum.SqlServer)
        {
            using var conn = GetConnection(dbKey, connectionStringKey, dapperDbTypeEnum);
            var res = await conn.QueryFirstOrDefaultAsync<T>(sql, param, null, commandTimeout, commandType);
            return res;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql查询参数</param>
        /// <param name="dbKey">数据库键（默认TriageService数据库）</param>
        /// <param name="connectionStringKey"></param>
        /// <param name="buffered">是否缓冲</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="dapperDbTypeEnum">数据库类型（默认SqlServer）</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryListAsync<T>(string sql, object param = null, string dbKey = "TriageService", string connectionStringKey = "DefaultConnection", bool buffered = true,
            int? commandTimeout = default, CommandType? commandType = default,
            DapperDbTypeEnum dapperDbTypeEnum = DapperDbTypeEnum.SqlServer)
        {
            using var conn = GetConnection(dbKey, connectionStringKey, dapperDbTypeEnum);
            var res = await conn.QueryAsync<T>(sql, param, null, commandTimeout, commandType);
            return res;
        }

        /// <summary>
        /// 执行增删改sql
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="param">sql查询参数</param>
        /// <param name="dbKey">数据库键（默认TriageService数据库）</param>
        /// <param name="connectionStringKey"></param>
        /// <param name="commandTimeout">超时时间</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="dapperDbTypeEnum">数据库类型（默认SqlServer）</param>
        /// <returns></returns>
        public async Task<int> ExecuteSqlAsync(string sql, object param = null, string dbKey = "TriageService", string connectionStringKey = "DefaultConnection", int? commandTimeout = default,
            CommandType? commandType = default, DapperDbTypeEnum dapperDbTypeEnum = DapperDbTypeEnum.SqlServer)
        {
            
            try
            {
                using var conn = GetConnection(dbKey, connectionStringKey, dapperDbTypeEnum);
                using var tran = conn.BeginTransaction();
                var count = await conn.ExecuteAsync(sql, param, tran, commandTimeout, commandType);
                tran.Commit();
                return count;
            }
            catch (Exception e)
            {
                _log.Warning($"【DapperRepository】【ExecuteSqlAsync】【执行增删改sql错误】【Msg：{e}】");
                return 0;
            }
        }

        /// <summary>
        /// 查询返回 IDataReader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql查询参数</param>
        /// <param name="dbKey">数据库键（默认TriageService数据库）</param>
        /// <param name="connectionStringKey"></param>
        /// <param name="commandTimeout">超时时间</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="dapperDbTypeEnum">数据库类型（默认SqlServer）</param>
        /// <returns></returns>
        public async Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, string dbKey = "TriageService", string connectionStringKey = "DefaultConnection", int? commandTimeout = default,
            CommandType? commandType = default, DapperDbTypeEnum dapperDbTypeEnum = DapperDbTypeEnum.SqlServer)
        {
            try
            {
                using var conn = GetConnection(dbKey, connectionStringKey, dapperDbTypeEnum);
                return await conn.ExecuteReaderAsync(sql, param, null, commandTimeout, commandType);
            }
            catch (Exception e)
            {
                _log.Warning($"【DapperRepository】【ExecuteReader】【查询返回 IDataReader】【Msg：{e}】");
                return null;
            }
        }

        /// <summary>
        /// 查询返回 DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql查询参数</param>
        /// <param name="dbKey">数据库键（默认TriageService数据库）</param>
        /// <param name="connectionStringKey"></param>
        /// <param name="commandTimeout">超时时间</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="dapperDbTypeEnum">数据库类型（默认SqlServer）</param>
        /// <returns></returns>
        public async Task<DataTable> GetDataTableExecuteReaderAsync(string sql, object param = null, string dbKey = "TriageService", string connectionStringKey = "DefaultConnection", int? commandTimeout = default,
            CommandType? commandType = default, DapperDbTypeEnum dapperDbTypeEnum = DapperDbTypeEnum.SqlServer)
        {
            var conn = GetConnection(dbKey, connectionStringKey, dapperDbTypeEnum);
            try
            {
                var reader = await conn.ExecuteReaderAsync(sql, param, null, commandTimeout, commandType);
                return DataTableToIDataReader(reader);
            }
            catch (Exception e)
            {
                _log.Warning($"【DapperRepository】【ExecuteReader】【查询返回 IDataReader】【Msg：{e}】");
                return null;
            }
        }

        /// <summary>
        /// 查询单个返回值 
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql查询参数</param>
        /// <param name="dbKey">数据库键（默认TriageService数据库）</param>
        /// <param name="connectionStringKey"></param>
        /// <param name="commandTimeout">超时时间</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="dapperDbTypeEnum">数据库类型（默认SqlServer）</param>
        /// <returns></returns>
        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, string dbKey = "TriageService", string connectionStringKey = "DefaultConnection", int? commandTimeout = default,
            CommandType? commandType = default, DapperDbTypeEnum dapperDbTypeEnum = DapperDbTypeEnum.SqlServer)
        {
            var conn = GetConnection(dbKey, connectionStringKey, dapperDbTypeEnum);
            var res = await conn.ExecuteScalarAsync<T>(sql, param, null, commandTimeout, commandType);
            return res;
        }
    }
}
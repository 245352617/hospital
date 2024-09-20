using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IDapperRepository
    {
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
        Task<T> QueryAsync<T>(string sql, object param = null, string dbKey = "TriageService", string connectionStringKey = "DefaultConnection",
            bool buffered = true, int? commandTimeout = default,
            CommandType? commandType = default, DapperDbTypeEnum dapperDbTypeEnum = DapperDbTypeEnum.SqlServer);

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
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, string dbKey = "TriageService", string connectionStringKey = "DefaultConnection",
            bool buffered = true, int? commandTimeout = default,
            CommandType? commandType = default, DapperDbTypeEnum dapperDbTypeEnum = DapperDbTypeEnum.SqlServer);

        /// <summary>
        /// 查询
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
        Task<IEnumerable<T>> QueryListAsync<T>(string sql, object param = null, string dbKey = "TriageService", string connectionStringKey = "DefaultConnection", bool buffered = true,
            int? commandTimeout = default, CommandType? commandType = default,
            DapperDbTypeEnum dapperDbTypeEnum = DapperDbTypeEnum.SqlServer);

        /// <summary>
        /// 执行增删改sql(异步版本)
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="param">sql查询参数</param>
        /// <param name="dbKey">数据库键（默认TriageService数据库）</param>
        /// <param name="connectionStringKey"></param>
        /// <param name="commandTimeout">超时时间</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="dapperDbTypeEnum">数据库类型（默认SqlServer）</param>
        /// <returns></returns>
        Task<int> ExecuteSqlAsync(string sql, object param = null, string dbKey = "TriageService", string connectionStringKey = "DefaultConnection", int? commandTimeout = default,
            CommandType? commandType = default, DapperDbTypeEnum dapperDbTypeEnum = DapperDbTypeEnum.SqlServer);

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
        Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, string dbKey = "TriageService", string connectionStringKey = "DefaultConnection", int? commandTimeout = default,
            CommandType? commandType = default, DapperDbTypeEnum dapperDbTypeEnum = DapperDbTypeEnum.SqlServer);

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
        Task<DataTable> GetDataTableExecuteReaderAsync(string sql, object param = null, string dbKey = "TriageService", string connectionStringKey = "DefaultConnection", int? commandTimeout = default,
            CommandType? commandType = default, DapperDbTypeEnum dapperDbTypeEnum = DapperDbTypeEnum.SqlServer);


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
        Task<T> ExecuteScalarAsync<T>(string sql, object param = null, string dbKey = "TriageService", string connectionStringKey = "DefaultConnection", int? commandTimeout = default,
            CommandType? commandType = default, DapperDbTypeEnum dapperDbTypeEnum = DapperDbTypeEnum.SqlServer);
    }
}
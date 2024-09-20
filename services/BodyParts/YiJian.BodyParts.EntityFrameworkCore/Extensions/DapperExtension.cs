using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.BodyParts.EntityFrameworkCore
{
    public static class DapperExtension
    {
        /// <summary>
        /// 创建数据库连接对象并打开链接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection OpenCurrentDbConnection(string dbConnectionString)
        {
            IDbConnection dbConnection = new SqlConnection(dbConnectionString);
            dbConnection.Open();
            return dbConnection;
        }


        public static IEnumerable<T> Query<T>(string dbConnectionString, string sql)
        {
            using (IDbConnection _dbConnection = new SqlConnection(dbConnectionString))
            {
                var result = _dbConnection.Query<T>(sql);
                return result;
            }
        }


        public static async Task<IEnumerable<T>> QueryAsync<T>(string dbConnectionString, string sql)
        {
            using (IDbConnection _dbConnection = new SqlConnection(dbConnectionString))
            {
                var result = await _dbConnection.QueryAsync<T>(sql);
                return result;
            }
        }

        public static DataTable Query(string dbConnectionString, string sql)
        {
            using (IDbConnection _dbConnection = new SqlConnection(dbConnectionString))
            {
                DataTable table = new DataTable("MyTable");
                var result = _dbConnection.ExecuteReader(sql);
                table.Load(result);
                return table;
            }
        }
    }
}

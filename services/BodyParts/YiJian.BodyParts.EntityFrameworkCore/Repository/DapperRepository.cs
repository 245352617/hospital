using Dapper;
using YiJian.BodyParts.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.BodyParts.Repository
{
    /// <summary>
    /// Dapper仓储
    /// </summary>
    public class DapperRepository : DapperRepository<DbContext>, ITransientDependency
    {
        public DapperRepository(IDbContextProvider<DbContext> dbContextProvider) : base(dbContextProvider)
        {

        }


        public List<dynamic> Query(string sql)
        {
            var result = new List<dynamic>();
            if (string.IsNullOrEmpty(sql))
            {
                return null;
            }

            result = DbConnection.Query(sql).AsList();
            return result;
        }

        public async Task<List<dynamic>> QueryAsync(string sql)
        {
            var result = new List<dynamic>();
            if (string.IsNullOrEmpty(sql))
            {
                return null;
            }

            var data = await DbConnection.QueryAsync(sql);
            result = data.AsList();
            return result;
        }


        public async Task<DataTable> ExecuteReaderAsync(string sql)
        {
            var result = new DataTable("MyTable");
            try
            {
                if (string.IsNullOrEmpty(sql))
                {
                    return null;
                }
                var data = await DbConnection.ExecuteReaderAsync(sql,transaction:DbTransaction);
                result.Load(data);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return result;
            }
          
           
        }
    }
}

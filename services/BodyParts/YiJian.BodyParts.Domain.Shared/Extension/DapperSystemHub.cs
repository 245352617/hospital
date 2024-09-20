using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SqlSugar;

namespace YiJian.BodyParts.Domain.Shared.Extension
{

    /// <summary>
    /// 
    /// </summary>

    public class ViewMoodel
    {
        /// <summary>
        /// 视图
        /// </summary>
        public string 入院人数 { get; set; }

        public string 日期 { get; set; }

        public string 开放床位数 { get; set; }
    }

    /// <summary>
    /// Dapper查询类库 单例
    /// </summary>
    public static class DapperSystemHub
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public static async Task<List<ViewMoodel>> GetVW_ICU_CRRS(string ConnectionString, string StartTime, string EndTime) 
        {
            try
            {
                //创建数据库对象 SqlSugarClient   
                SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = ConnectionString,//连接符字串

                    DbType = SqlSugar.DbType.SqlServer, //数据库类型

                    IsAutoCloseConnection = true,//不设成true要手动close,
                });

                Console.WriteLine("sqlsugar连接成功");

                Console.WriteLine($"StartTime:{StartTime}");

                Console.WriteLine($"EndTime:{EndTime}");

                var dt = await db.Ado.SqlQueryAsync<ViewMoodel>("Select * From VW_ICU_CRRS WHERE 日期>=@StartTime And   日期<=@EndTime", new List<SugarParameter>(){
                
                    new SugarParameter("@StartTime",StartTime),
               
                    new SugarParameter("@EndTime",EndTime) //执行sql语句
                });

                Console.WriteLine($"VW_ICU_CRRS:{Newtonsoft.Json.JsonConvert.SerializeObject(dt)}");

                db.Close();

                db.Dispose();

                return dt;
            }
            catch (Exception)
            {
                return new List<ViewMoodel>(); 
            }
        }
    }

}

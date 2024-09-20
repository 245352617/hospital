using Nancy.Json;
using Oracle.ManagedDataAccess.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Xml;

namespace YiJian.BodyParts
{
    public class OracleConnectionHelper
    {
        private static string _host;  //地址
        private static string _port;  //端口
        private static string _serviceName;  //服务名
        private static string _username;  //用户名
        private static string _password;  //密码
        Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private static string connectionString;
        private readonly OracleConnection Singletonconn = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public OracleConnectionHelper(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {

            _configuration = configuration;
            _host = _configuration["OracleDb:Host"];
            _port = _configuration["OracleDb:Port"];
            _serviceName = _configuration["OracleDb:ServiceName"];
            _username = _configuration["OracleDb:Username"];
            _password = _configuration["OracleDb:Password"];

            //绑定连接字符串
            if (!string.IsNullOrWhiteSpace(_host) && !string.IsNullOrWhiteSpace(_port) && !string.IsNullOrWhiteSpace(_serviceName) && !string.IsNullOrWhiteSpace(_username) && !string.IsNullOrWhiteSpace(_password))
            {
                connectionString = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={_host})(PORT={_port}))(CONNECT_DATA=(SERVICE_NAME={_serviceName})));Persist Security Info=True;User ID={_username};Password={_password};";
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public OracleConnectionHelper(Microsoft.Extensions.Configuration.IConfiguration configuration,bool Singleton)
        {

            _configuration = configuration;
            _host = _configuration["OracleDb:Host"];
            _port = _configuration["OracleDb:Port"];
            _serviceName = _configuration["OracleDb:ServiceName"];
            _username = _configuration["OracleDb:Username"];
            _password = _configuration["OracleDb:Password"];

            //绑定连接字符串
            if (!string.IsNullOrWhiteSpace(_host) && !string.IsNullOrWhiteSpace(_port) && !string.IsNullOrWhiteSpace(_serviceName) && !string.IsNullOrWhiteSpace(_username) && !string.IsNullOrWhiteSpace(_password))
            {
                connectionString = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={_host})(PORT={_port}))(CONNECT_DATA=(SERVICE_NAME={_serviceName})));Persist Security Info=True;User ID={_username};Password={_password};";
                try
                {
                    // 创建一个OracleConnection
                    OracleConnection conn = new OracleConnection(connectionString);

                    //如果连接未打开，先打开连接
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    conn.Close();//测试是否可以通信
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }





        /// <summary>
        /// 查询返回一个结果集
        /// </summary>
        /// <param name="cmdType">命令类型（sql或者存储过程）</param>
        /// <param name="cmdText">sql语句或者存储过程名称</param>
        /// <param name="commandParameters">命令所需参数数组</param>
        /// <returns></returns>
        public OracleDataReader ExecuteReader(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return null;
            }
            // 创建一个OracleCommand
            OracleCommand cmd = new OracleCommand();
            // 创建一个OracleConnection
            OracleConnection conn = new OracleConnection(connectionString);
            try
            {
                //调用静态方法PrepareCommand完成赋值操作
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //执行查询
                OracleDataReader odr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //清空参数
                cmd.Parameters.Clear();
                return odr;
            }
            catch (Exception ex)
            {
                //如果发生异常，关闭连接，并且返回null
                conn.Close();
                Log.Error($"Oracle连接错误消息：{ex.Message};{ex.StackTrace}");
                return null;
            }
        }

        /// <summary>
        /// 查询返回一个结果集
        /// </summary>
        /// <param name="cmdType">命令类型（sql或者存储过程）</param>
        /// <param name="cmdText">sql语句或者存储过程名称</param>
        /// <param name="commandParameters">命令所需参数数组</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {

            // 创建一个OracleCommand
            OracleCommand cmd = new OracleCommand();
            // 创建一个OracleConnection
            OracleConnection conn = new OracleConnection(connectionString);
            DataSet dataSet = null;
            try
            {
                //调用静态方法PrepareCommand完成赋值操作
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);

                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = cmd;
                dataSet = new DataSet();
                adapter.Fill(dataSet);

                //清空参数
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                // 如果发生异常，关闭连接，并且返回null
                conn.Close();
                Log.Error($"Oracle连接错误消息：{ex.Message};{ex.StackTrace}");
                return null;
            }
            return dataSet;
        }




        /// <summary>
        /// 一个静态的预处理函数
        /// </summary>
        /// <param name="cmd">存在的OracleCommand对象</param>
        /// <param name="conn">存在的OracleConnection对象</param>
        /// <param name="trans">存在的OracleTransaction对象</param>
        /// <param name="cmdType">命令类型（sql或者存在过程）</param>
        /// <param name="cmdText">sql语句或者存储过程名称</param>
        /// <param name="commandParameters">Parameters for the command</param>
        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] commandParameters)
        {

            //如果连接未打开，先打开连接
            if (conn.State != ConnectionState.Open)
                conn.Open();

            //未要执行的命令设置参数
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            //如果传入了事务，需要将命令绑定到指定的事务上去
            if (trans != null)
                cmd.Transaction = trans;

            //将传入的参数信息赋值给命令参数
            if (commandParameters != null)
            {
                cmd.Parameters.AddRange(commandParameters);
            }
        }

        /// <summary>
        /// 利用反射将DataTable转换为List对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<T> DataTableToList<T>(DataTable dt) where T : class, new()
        {
            // 定义集合 
            List<T> ts = new List<T>();
            //定义一个临时变量 
            string tempName = string.Empty;
            //遍历DataTable中所有的数据行 
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性 
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性 
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;//将属性名称赋值给临时变量 
                                       //检查DataTable是否包含此列（列名==对象的属性名）  
                    if (dt.Columns.Contains(tempName))
                    {
                        //取值 
                        object value = dr[tempName];
                        //如果非空，则赋给对象的属性 
                        if (value != DBNull.Value)
                        {
                            pi.SetValue(t, value, null);
                        }
                    }
                }
                //对象添加到泛型集合中 
                ts.Add(t);
            }
            return ts;
        }

        /// <summary>
        /// 将datatable转换为json  
        /// </summary>
        /// <param name="dtb">Dt</param>
        /// <returns>JSON字符串</returns>
        public string Dtb2Json(DataTable dtb)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            System.Collections.ArrayList dic = new System.Collections.ArrayList();
            foreach (DataRow dr in dtb.Rows)
            {
                System.Collections.Generic.Dictionary<string, object> drow = new System.Collections.Generic.Dictionary<string, object>();
                foreach (DataColumn dc in dtb.Columns)
                {
                    //drow.Add(dc.ColumnName, dr[dc.ColumnName]);
                    object value = dr[dc.ColumnName];
                    if (value != DBNull.Value)
                    {
                        drow.Add(dc.ColumnName, value);
                    }                   
                }
                dic.Add(drow);

            }
            //序列化  
            return jss.Serialize(dic);
        }
    }
}

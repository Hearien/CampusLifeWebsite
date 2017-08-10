using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common
{
    public class SQLDBHelper
    {
        private static string connStr;
        internal static string ConnStr
        {
            get
            {
                if (connStr == null)
                {
                    //connStr = @"Data Source=.;Initial Catalog=ShowCook;User ID=sa;Password=sa";
                    connStr = System.Configuration.ConfigurationSettings.AppSettings["strConn"].ToString();
                }
                return connStr;
            }
        }

        //数据库连接对象
        private static volatile SqlConnection conn = null;
        static object obj = new object();

        public static SqlConnection Conn
        {
            get
            {
                if (conn == null)
                {
                    lock (obj)
                    {
                        if (conn == null)
                        {
                            conn = new SqlConnection(ConnStr);
                        }
                    }
                }
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                return conn;
            }
            set { conn = value; }
        }

        //数据库命令对象
        static SqlCommand cmd = null;
        public static SqlCommand CMD
        {
            get
            {
                if (cmd == null)
                {
                    cmd = new SqlCommand("", Conn);
                }
                return cmd;
            }
            set { cmd = value; }
        }


        private static void PrepareCommand(string cmdText)
        {
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            CMD.CommandText = cmdText;
            CMD.CommandType = CommandType.Text;
            CMD.Parameters.Clear();
        }


        private static void PrepareCommand(string cmdText, SqlParameter[] cmdParms, CommandType cmdType)
        {
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            CMD.CommandText = cmdText;
            CMD.CommandType = cmdType;
            CMD.Parameters.Clear();
            if (cmdParms != null)
            {
                for (int i = 0; i < cmdParms.Length; i++)
                {
                    cmd.Parameters.Add(cmdParms[i]);
                }
            }
        }



        public static SqlDataReader ExecuteReader(string strSQL)
        {
            SqlConnection connection = new SqlConnection(ConnStr);
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 返回首行首列
        /// </summary>
        /// <param Name="cmdText"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdText)
        {
            object obj = null;
            try
            {
                PrepareCommand(cmdText);
                obj = CMD.ExecuteScalar();
            }
            catch (Exception exp)
            {
                return exp.Message;
                //return null;
            }
            return obj;
        }

        /// <summary>
        /// 查询数据是否存在
        /// </summary>
        /// <param Name="SQLString"></param>
        /// <returns></returns>
        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;

                    }
                }
            }
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {


                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }



        /// <summary>
        /// 执行sql命令,添加,删除,修改
        /// </summary>
        /// <param Name="strSQL">sql命令语句</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string strSQL)
        {
            //申请一个数据库连接
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                //申明数据库命令，且把要执行的sql语句加入命令对象中
                using (SqlCommand cmd = new SqlCommand(strSQL, connection))
                {
                    try
                    {
                        //打开数据库连接
                        connection.Open();
                        //执行sql命令，得到受影响的行数
                        int r = cmd.ExecuteNonQuery();
                        return r;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 通过SQL语句得到数据集
        /// </summary>
        /// <param Name="strSQL">查询语句</param>
        /// <returns>以DataSet形式数据集</returns>
        public static DataSet GetDataSetBySQL(string strSQL)
        {
            //SqlConnection申明一个数据库连接对象
            SqlConnection connection = new SqlConnection(ConnStr);
            //SqlCommand申明一个数据库命令对象。
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try
            {
                //打开数据库连接
                connection.Open();
                //数据适配器
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                //将数据填充到dataset
                da.Fill(ds);
                return ds;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }

        }

        public static bool Exists(string strSql)
        {
            object obj = GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }


        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }
    }
}

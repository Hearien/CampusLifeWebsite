using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Data;
using Common;

namespace DAL
{
    public class DAL_admin
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(Admin model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from admin");
            strSql.Append(" where uname=@uname and upwd=@upwd and status=@status");
            SqlParameter[] parameters = {
					new SqlParameter("@uname", SqlDbType.VarChar,50),
                    new SqlParameter("@upwd", SqlDbType.VarChar,50),
                    new SqlParameter("@status", SqlDbType.Int,4)
			};
            parameters[0].Value = model.uname;
            parameters[1].Value = model.upwd;
            parameters[2].Value = model.status;

            return SQLDBHelper.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(Admin model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into admin(");
            strSql.Append("uname,upwd,status)");
            strSql.Append(" values (");
            strSql.Append("@uname,@upwd,@status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@uname", SqlDbType.VarChar,50),
					new SqlParameter("@upwd", SqlDbType.VarChar,50),
					new SqlParameter("@status", SqlDbType.Int,4)};
            parameters[0].Value = model.uname;
            parameters[1].Value = model.upwd;
            parameters[2].Value = model.status;

            object obj = SQLDBHelper.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Admin model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update admin set ");
            strSql.Append("uname=@uname,");
            strSql.Append("upwd=@upwd,");
            strSql.Append("status=@status");
            strSql.Append(" where uid=@uid");
            SqlParameter[] parameters = {
					new SqlParameter("@uname", SqlDbType.VarChar,50),
					new SqlParameter("@upwd", SqlDbType.VarChar,50),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@uid", SqlDbType.Int,4)};
            parameters[0].Value = model.uname;
            parameters[1].Value = model.upwd;
            parameters[2].Value = model.status;
            parameters[3].Value = model.uid;

            int rows = SQLDBHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(int uid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from admin ");
            strSql.Append(" where uid=@uid");
            SqlParameter[] parameters = {
					new SqlParameter("@uid", SqlDbType.Int,4)
			};
            parameters[0].Value = uid;

            int rows = SQLDBHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public static bool DeleteList(string uidlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from admin ");
            strSql.Append(" where uid in (" + uidlist + ")  ");
            int rows = SQLDBHelper.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Admin GetModel(int uid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 uid,uname,upwd,status from admin ");
            strSql.Append(" where uid=@uid");
            SqlParameter[] parameters = {
					new SqlParameter("@uid", SqlDbType.Int,4)
			};
            parameters[0].Value = uid;

            Admin model = new Admin();
            DataSet ds = SQLDBHelper.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Admin DataRowToModel(DataRow row)
        {
            Admin model = new Admin();
            if (row != null)
            {
                if (row["uid"] != null && row["uid"].ToString() != "")
                {
                    model.uid = int.Parse(row["uid"].ToString());
                }
                if (row["uname"] != null)
                {
                    model.uname = row["uname"].ToString();
                }
                if (row["upwd"] != null)
                {
                    model.upwd = row["upwd"].ToString();
                }
                if (row["status"] != null && row["status"].ToString() != "")
                {
                    model.status = int.Parse(row["status"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select uid,uname,upwd,status ");
            strSql.Append(" FROM admin ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SQLDBHelper.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public static DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" uid,uname,upwd,status ");
            strSql.Append(" FROM admin ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SQLDBHelper.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public static int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM admin ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = SQLDBHelper.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public static DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.uid desc");
            }
            strSql.Append(")AS Row, T.*  from admin T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return SQLDBHelper.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public static DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "admin";
            parameters[1].Value = "uid";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return SQLDBHelper.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/
    }
}

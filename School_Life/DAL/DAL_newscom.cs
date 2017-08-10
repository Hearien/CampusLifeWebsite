using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Model;
using System.Data.SqlClient;
using Common;
using System.Data;

namespace DAL
{
    public class DAL_newscom
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int comid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from newscom");
            strSql.Append(" where comid=@comid ");
            SqlParameter[] parameters = {
					new SqlParameter("@comid", SqlDbType.VarChar,50)			};
            parameters[0].Value = comid;

            return SQLDBHelper.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(NewsCom model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into newscom(");
            strSql.Append("newsid,comdesc,comtime,comip,uerid)");
            strSql.Append(" values (");
            strSql.Append("@newsid,@comdesc,@comtime,@comip,@uerid)");
            SqlParameter[] parameters = {
					new SqlParameter("@newsid", SqlDbType.VarChar,50),
					new SqlParameter("@comdesc", SqlDbType.VarChar,-1),
					new SqlParameter("@comtime", SqlDbType.VarChar,50),
					new SqlParameter("@comip", SqlDbType.VarChar,20),
					new SqlParameter("@uerid", SqlDbType.VarChar,50)};
            parameters[0].Value = model.newsid.newsid;
            parameters[1].Value = model.comdesc;
            parameters[2].Value = model.comtime;
            parameters[3].Value = model.comip;
            parameters[4].Value = model.uerid.sno;

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
        /// 更新一条数据
        /// </summary>
        public static bool Update(NewsCom model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update newscom set ");
            strSql.Append("newsid=@newsid,");
            strSql.Append("comdesc=@comdesc,");
            strSql.Append("comtime=@comtime,");
            strSql.Append("comip=@comip,");
            strSql.Append("uerid=@uerid");
            strSql.Append(" where comid=@comid ");
            SqlParameter[] parameters = {
					new SqlParameter("@newsid", SqlDbType.VarChar,50),
					new SqlParameter("@comdesc", SqlDbType.VarChar,-1),
					new SqlParameter("@comtime", SqlDbType.VarChar,50),
					new SqlParameter("@comip", SqlDbType.VarChar,20),
					new SqlParameter("@uerid", SqlDbType.VarChar,50),
					new SqlParameter("@comid", SqlDbType.VarChar,50)};
            parameters[0].Value = model.newsid;
            parameters[1].Value = model.comdesc;
            parameters[2].Value = model.comtime;
            parameters[3].Value = model.comip;
            parameters[4].Value = model.uerid;
            parameters[5].Value = model.comid;

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
        public static bool Delete(int comid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from newscom ");
            strSql.Append(" where comid=@comid ");
            SqlParameter[] parameters = {
					new SqlParameter("@comid", SqlDbType.VarChar,50)			};
            parameters[0].Value = comid;

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
        public static bool DeleteList(string comidlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from newscom ");
            strSql.Append(" where comid in (" + comidlist + ")  ");
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
        public static NewsCom GetModel(int comid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 comid,newsid,comdesc,comtime,comip,uerid from newscom ");
            strSql.Append(" where comid=@comid ");
            SqlParameter[] parameters = {
					new SqlParameter("@comid", SqlDbType.VarChar,50)			};
            parameters[0].Value = comid;

            NewsCom model = new NewsCom();
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
        public static NewsCom DataRowToModel(DataRow row)
        {
            NewsCom model = new NewsCom();
            if (row != null)
            {
                if (row["comid"] != null)
                {
                    model.comid = Int32.Parse(row["comid"].ToString());
                }
                if (row["newsid"] != null)
                {
                    model.newsid.newsid = Int32.Parse(row["newsid"].ToString());
                }
                if (row["comdesc"] != null)
                {
                    model.comdesc = row["comdesc"].ToString();
                }
                if (row["comtime"] != null && row["comtime"].ToString() != "")
                {
                    model.comtime = row["comtime"].ToString();
                }
                if (row["comip"] != null)
                {
                    model.comip = row["comip"].ToString();
                }
                if (row["uerid"] != null)
                {
                    model.uerid.sno = row["uerid"].ToString();
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
            strSql.Append("select comid,newsid,comdesc,comtime,comip,uerid ");
            strSql.Append(" FROM newscom ");
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
            strSql.Append(" comid,newsid,comdesc,comtime,comip,uerid ");
            strSql.Append(" FROM newscom ");
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
            strSql.Append("select count(1) FROM newscom ");
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
                strSql.Append("order by T.comid desc");
            }
            strSql.Append(")AS Row, T.*  from newscom T ");
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
            parameters[0].Value = "newscom";
            parameters[1].Value = "comid";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return SQLDBHelper.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/
    }
}

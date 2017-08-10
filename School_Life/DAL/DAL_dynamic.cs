using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data;
using Model;
using System.Data.SqlClient;
using System.Collections;

namespace DAL
{
    public class DAL_dynamic
    {
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public static int GetMaxId()
        {
            return SQLDBHelper.GetMaxID("dynamicid", "dynamic");
        }

        /// <summary>
        /// 根据条件查询商品并进行分页
        /// </summary>
        /// <param name="paraMap"></param>
        /// <returns></returns>
        public static Pager<Hashtable> queryByCondition(Hashtable paraMap)
        {
            Pager<Hashtable> pager = new Pager<Hashtable>();
            if (!Util.isNull(paraMap["pageCode"].ToString()))
            {
                pager.setCurrentPage(Convert.ToInt32(paraMap["pageCode"].ToString()));
            }
            else
            {
                pager.setCurrentPage(1);
            }
            pager.setTotalRecord(queryCount(paraMap));
            pager.setPageSize(3);
            paraMap.Add("startIndex", pager.getStartIndex());
            paraMap.Add("endIndex", pager.getEndIndex());
            StringBuilder sqlSb = new StringBuilder("select t2.head,t2.dynamicid,t2.context,t2.createtime,t2.sname from(" +
                        "select ROW_NUMBER() over(order by t1.dynamicid) rn,t1.* from" +
                            "(select s.head,d.dynamicid,d.context,d.createtime,s.sname from dynamic d inner join student s on d.userid=s.sno where 1=1"
                                );
            
            sqlSb.Append(")t1 )t2 where t2.rn>" + paraMap["startIndex"] +
                       " and t2.rn<=" + paraMap["endIndex"]
                       );
            DataSet rstSet = SQLDBHelper.GetDataSetBySQL(sqlSb.ToString());
            DataTable rstTable = rstSet.Tables[0];
            List<Hashtable> list = new List<Hashtable>();
            for (int i = 0; i < rstTable.Rows.Count; i++)
            {
                DataRow row = rstTable.Rows[i];
                Hashtable model = new Hashtable();
                foreach (DataColumn column in rstTable.Columns)
                {
                    string colNm = column.ColumnName;//得到列名
                    if (!Util.isNull(row[colNm].ToString()))
                    {
                        model.Add(colNm, row[colNm]);
                    }
                    else
                    {
                        model.Add(colNm, "");
                    }
                }
                list.Add(model);
            }
            pager.setList(list);
            return pager;
        }

        /// <summary>
        /// 根据条件查询商品总记录数
        /// </summary>
        /// <param name="paraMap"></param>
        /// <returns></returns>
        public static int queryCount(Hashtable paraMap)
        {
            StringBuilder sqlSb = new StringBuilder("select count(*) from dynamic d inner join student s on d.userid=s.sno");
            DataSet rstSet = SQLDBHelper.GetDataSetBySQL(sqlSb.ToString());
            return Convert.ToInt32(rstSet.Tables[0].Rows[0][0]);
        }
        
        /// <summary>
        /// 获取动态列表
        /// </summary>
        /// <returns></returns>
        public static List<Hashtable> getDynamicList()
        {
            string sql = "select d.dynamicid,d.userid,s.sname,s.head,d.context,d.createtime from dynamic d inner join student s on s.sno=d.userid order by d.createtime desc";
            DataSet rstSet = SQLDBHelper.GetDataSetBySQL(sql);
            DataTable rstTable = rstSet.Tables[0];
            List<Hashtable> list = new List<Hashtable>();
            for (int i = 0; i < rstTable.Rows.Count; i++)
            {
                DataRow row = rstTable.Rows[i];
                Hashtable model = new Hashtable();
                foreach (DataColumn column in rstTable.Columns)
                {
                    string colNm = column.ColumnName;//得到列名
                    if (!Util.isNull(row[colNm].ToString()))
                    {
                        model.Add(colNm, row[colNm]);
                    }
                    else
                    {
                        model.Add(colNm, "");
                    }
                }
                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int dynamicid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dynamic");
            strSql.Append(" where dynamicid=@dynamicid ");
            SqlParameter[] parameters = {
					new SqlParameter("@dynamicid", SqlDbType.Int,4)			};
            parameters[0].Value = dynamicid;

            return SQLDBHelper.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(Dynamic model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dynamic(");
            strSql.Append("userid,createtime,context)");
            strSql.Append(" values (");
            strSql.Append("@userid,@createtime,@context)");
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Char,20),
					new SqlParameter("@createtime", SqlDbType.VarChar,50),
					new SqlParameter("@context", SqlDbType.Text)};
            parameters[0].Value = model.userid.sno;
            parameters[1].Value = model.createtime;
            parameters[2].Value = model.context;

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
        public static bool Update(Dynamic model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dynamic set ");
            strSql.Append("userid=@userid,");
            strSql.Append("createtime=@createtime,");
            strSql.Append("context=@context");
            strSql.Append(" where dynamicid=@dynamicid ");
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Char,20),
					new SqlParameter("@createtime", SqlDbType.VarChar,50),
					new SqlParameter("@context", SqlDbType.Text),
					new SqlParameter("@dynamicid", SqlDbType.Int,4)};
            parameters[0].Value = model.userid.sno;
            parameters[1].Value = model.createtime;
            parameters[2].Value = model.context;
            parameters[3].Value = model.dynamicid;

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
        public static bool Delete(int dynamicid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dynamic ");
            strSql.Append(" where dynamicid=@dynamicid ");
            SqlParameter[] parameters = {
					new SqlParameter("@dynamicid", SqlDbType.Int,4)			};
            parameters[0].Value = dynamicid;

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
        public static bool DeleteList(string dynamicidlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dynamic ");
            strSql.Append(" where dynamicid in (" + dynamicidlist + ")  ");
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
        public static Dynamic GetModel(int dynamicid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 dynamicid,userid,createtime,context from dynamic ");
            strSql.Append(" where dynamicid=@dynamicid ");
            SqlParameter[] parameters = {
					new SqlParameter("@dynamicid", SqlDbType.Int,4)			};
            parameters[0].Value = dynamicid;

            Dynamic model = new Dynamic();
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
        public static Dynamic DataRowToModel(DataRow row)
        {
            Dynamic model = new Dynamic();
            Student s = new Student();
            if (row != null)
            {
                if (row["dynamicid"] != null && row["dynamicid"].ToString() != "")
                {
                    model.dynamicid = int.Parse(row["dynamicid"].ToString());
                }
                if (row["userid"] != null)
                {
                    s.sno = row["userid"].ToString();
                    model.userid = s;
                }
                if (row["createtime"] != null)
                {
                    model.createtime = row["createtime"].ToString();
                }
                if (row["context"] != null)
                {
                    model.context = row["context"].ToString();
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
            strSql.Append("select dynamicid,userid,createtime,context ");
            strSql.Append(" FROM dynamic ");
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
            strSql.Append(" dynamicid,userid,createtime,context ");
            strSql.Append(" FROM dynamic ");
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
            strSql.Append("select count(1) FROM dynamic ");
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
                strSql.Append("order by T.dynamicid desc");
            }
            strSql.Append(")AS Row, T.*  from dynamic T ");
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
            parameters[0].Value = "dynamic";
            parameters[1].Value = "dynamicid";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return SQLDBHelper.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using System.Data.SqlClient;
using Common;
using System.Collections;

namespace DAL
{
    public class DAL_news
    {
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int newsid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from news");
            strSql.Append(" where newsid=@newsid ");
            SqlParameter[] parameters = {
					new SqlParameter("@newsid", SqlDbType.Int)			};
            parameters[0].Value = newsid;

            return SQLDBHelper.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取各分类的头条新闻
        /// </summary>
        /// <returns></returns>
        public static Hashtable getTopNews()
        {
            Hashtable newsTb = new Hashtable();
            DataSet ds = null;
            string sqlGov = "select top 1 * from news where ishead='1' and newscatno = '1' order by createtime desc";
            string sqlEco = "select top 1 * from news where ishead='1' and newscatno = '2' order by createtime desc";
            string sqlEdu = "select top 1 * from news where ishead='1' and newscatno = '3' order by createtime desc";
            string sqlSpo = "select top 1 * from news where ishead='1' and newscatno = '4' order by createtime desc";
            string sqlSoc = "select top 1 * from news where ishead='1' and newscatno = '5' order by createtime desc";
            string sqlSci = "select top 1 * from news where ishead='1' and newscatno = '9' order by createtime desc";
            
            ds = SQLDBHelper.GetDataSetBySQL(sqlGov);
            News newsGov = DataRowToModel(ds.Tables[0].Rows[0]);
            newsTb.Add("gov", newsGov);

            ds = SQLDBHelper.GetDataSetBySQL(sqlEco);
            News newsEco = DataRowToModel(ds.Tables[0].Rows[0]);
            newsTb.Add("eco", newsEco);

            ds = SQLDBHelper.GetDataSetBySQL(sqlEdu);
            News newsEdu = DataRowToModel(ds.Tables[0].Rows[0]);
            newsTb.Add("edu", newsEdu);

            ds = SQLDBHelper.GetDataSetBySQL(sqlSpo);
            News newsSpo = DataRowToModel(ds.Tables[0].Rows[0]);
            newsTb.Add("spo", newsSpo);

            ds = SQLDBHelper.GetDataSetBySQL(sqlSoc);
            News newsSoc = DataRowToModel(ds.Tables[0].Rows[0]);
            newsTb.Add("soc", newsSoc);

            ds = SQLDBHelper.GetDataSetBySQL(sqlSci);
            News newsSci = DataRowToModel(ds.Tables[0].Rows[0]);
            newsTb.Add("sci", newsSci);

            return newsTb;
        }

        /// <summary>
        /// 根据条件查询新闻并进行分页
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
            pager.setPageSize(Int32.Parse(paraMap["pageSize"].ToString()));
            pager.setTotalRecord(queryCount(paraMap));
            paraMap.Add("startIndex", pager.getStartIndex());
            paraMap.Add("endIndex", pager.getEndIndex());
            StringBuilder sqlSb = new StringBuilder("select t2.newsid,t2.newstitle,t2.newscatnm,t2.source,t2.createtime,t2.newsdetsc from(" +
	                    "select ROW_NUMBER() over(order by t1.newsid) rn,t1.* from"+
		                    "(select n.newsid,n.newstitle,nc.newscatnm,n.source,n.createtime,n.newsdetsc from news n "+
		                    "inner join newscat nc on n.newscatno=nc.newscatid where 1=1"
                                );
            if (!Util.isNull(paraMap["title"].ToString()))
            {
                sqlSb.Append(" and n.newstitle like '%" + paraMap["title"].ToString() + "%'");
            }
            if (!Util.isNull(paraMap["fDate"].ToString()))
            {
                sqlSb.Append(" and CONVERT(datetime,n.createtime) >= CONVERT(datetime,'" + paraMap["fDate"].ToString().Trim() + "')");
            }
            if (!Util.isNull(paraMap["tDate"].ToString()))
            {
                sqlSb.Append(" and CONVERT(datetime,n.createtime) <= CONVERT(datetime,'" + paraMap["tDate"].ToString().Trim() + "')");
            }
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
        /// 根据条件查询新闻总记录数
        /// </summary>
        /// <param name="paraMap"></param>
        /// <returns></returns>
        public static int queryCount(Hashtable paraMap)
        {
            StringBuilder sqlSb = new StringBuilder("select count(*) from news n " +
                                    "inner join newscat nc on n.newscatno=nc.newscatid where 1=1"
                                );
            if (!Util.isNull(paraMap["title"].ToString()))
            {
                sqlSb.Append(" and n.newstitle like '%" + paraMap["title"].ToString() + "%'");
            }
            if (!Util.isNull(paraMap["fDate"].ToString()))
            {
                sqlSb.Append(" and CONVERT(datetime,n.createtime) >= CONVERT(datetime,'" + paraMap["fDate"].ToString().Trim() + "')");
            }
            if (!Util.isNull(paraMap["tDate"].ToString()))
            {
                sqlSb.Append(" and CONVERT(datetime,n.createtime) <= CONVERT(datetime,'" + paraMap["tDate"].ToString().Trim() + "')");
            }
            DataSet rstSet = SQLDBHelper.GetDataSetBySQL(sqlSb.ToString());
            return Convert.ToInt32(rstSet.Tables[0].Rows[0][0]);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(News model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into news(");
            strSql.Append("newscatno,newstitle,createtime,source,newsdetsc,authorid,clickcount,ishead,ishot,lastcomid,lastcomtime,comcount)");
            strSql.Append(" values (");
            strSql.Append("@newscatno,@newstitle,@createtime,@source,@newsdetsc,@authorid,@clickcount,@ishead,@ishot,@lastcomid,@lastcomtime,@comcount)");
            SqlParameter[] parameters = {
					new SqlParameter("@newscatno", SqlDbType.VarChar,10),
					new SqlParameter("@newstitle", SqlDbType.VarChar,100),
					new SqlParameter("@createtime", SqlDbType.VarChar,50),
					new SqlParameter("@source", SqlDbType.VarChar,50),
					new SqlParameter("@newsdetsc", SqlDbType.Text),
					new SqlParameter("@authorid", SqlDbType.VarChar,20),
					new SqlParameter("@clickcount", SqlDbType.Decimal,5),
					new SqlParameter("@ishead", SqlDbType.Decimal,5),
					new SqlParameter("@ishot", SqlDbType.Decimal,5),
					new SqlParameter("@lastcomid", SqlDbType.VarChar,20),
					new SqlParameter("@lastcomtime", SqlDbType.VarChar,50),
					new SqlParameter("@comcount", SqlDbType.Decimal,5)};
            parameters[0].Value = model.newscatno.newscatid;
            parameters[1].Value = model.newstitle;
            parameters[2].Value = model.createtime;
            parameters[3].Value = model.source;
            parameters[4].Value = model.newsdetsc;
            parameters[5].Value = model.authorid.sno;
            parameters[6].Value = model.clickcount;
            parameters[7].Value = model.ishead;
            parameters[8].Value = model.ishot;
            parameters[9].Value = model.lastcomid;
            parameters[10].Value = model.lastcomtime;
            parameters[11].Value = model.comcount;

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
        public static bool Update(News model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update news set ");
            strSql.Append("newscatno=@newscatno,");
            strSql.Append("newstitle=@newstitle,");
            strSql.Append("createtime=@createtime,");
            strSql.Append("source=@source,");
            strSql.Append("newsdetsc=@newsdetsc,");
            strSql.Append("authorid=@authorid,");
            strSql.Append("clickcount=@clickcount,");
            strSql.Append("ishead=@ishead,");
            strSql.Append("ishot=@ishot,");
            strSql.Append("lastcomid=@lastcomid,");
            strSql.Append("lastcomtime=@lastcomtime,");
            strSql.Append("comcount=@comcount");
            strSql.Append(" where newsid=@newsid ");
            SqlParameter[] parameters = {
					new SqlParameter("@newscatno", SqlDbType.VarChar,10),
					new SqlParameter("@newstitle", SqlDbType.VarChar,100),
					new SqlParameter("@createtime", SqlDbType.VarChar,50),
					new SqlParameter("@source", SqlDbType.VarChar,50),
					new SqlParameter("@newsdetsc", SqlDbType.Text),
					new SqlParameter("@authorid", SqlDbType.VarChar,20),
					new SqlParameter("@clickcount", SqlDbType.Decimal,5),
					new SqlParameter("@ishead", SqlDbType.Decimal,5),
					new SqlParameter("@ishot", SqlDbType.Decimal,5),
					new SqlParameter("@lastcomid", SqlDbType.VarChar,20),
					new SqlParameter("@lastcomtime", SqlDbType.VarChar,50),
					new SqlParameter("@comcount", SqlDbType.Decimal,5),
					new SqlParameter("@newsid", SqlDbType.Int)};
            parameters[0].Value = model.newscatno.newscatid;
            parameters[1].Value = model.newstitle;
            parameters[2].Value = model.createtime;
            parameters[3].Value = model.source;
            parameters[4].Value = model.newsdetsc;
            parameters[5].Value = model.authorid.sno;
            parameters[6].Value = model.clickcount;
            parameters[7].Value = model.ishead;
            parameters[8].Value = model.ishot;
            parameters[9].Value = model.lastcomid;
            parameters[10].Value = model.lastcomtime;
            parameters[11].Value = model.comcount;
            parameters[12].Value = model.newsid;

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
        public static bool Delete(string newsid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from news ");
            strSql.Append(" where newsid=@newsid ");
            SqlParameter[] parameters = {
					new SqlParameter("@newsid", SqlDbType.Int)			};
            parameters[0].Value = newsid;

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
        public static bool DeleteList(string newsidlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from news ");
            strSql.Append(" where newsid in (" + newsidlist + ")  ");
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
        public static News GetModel(int newsid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 newsid,newscatno,newstitle,createtime,source,newsdetsc,authorid,clickcount,ishead,ishot,lastcomid,lastcomtime,comcount from news ");
            strSql.Append(" where newsid=@newsid ");
            SqlParameter[] parameters = {
					new SqlParameter("@newsid", SqlDbType.Int)			};
            parameters[0].Value = newsid;

            News model = new News();
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
        public static News DataRowToModel(DataRow row)
        {
            News model = new News();
            NewsCat newsCat = new NewsCat();
            Student stu = new Student();
            if (row != null)
            {
                if (row["newsid"] != null)
                {
                    model.newsid = Convert.ToInt32(row["newsid"].ToString());
                }
                if (row["newscatno"] != null)
                {
                    newsCat=DAL_newscat.GetModel(Convert.ToInt32(row["newscatno"].ToString()));
                    model.newscatno = newsCat;
                }
                if (row["newstitle"] != null)
                {
                    model.newstitle = row["newstitle"].ToString();
                }
                if (row["createtime"] != null && row["createtime"].ToString() != "")
                {
                    model.createtime = row["createtime"].ToString();
                }
                if (row["source"] != null)
                {
                    model.source = row["source"].ToString();
                }
                if (row["newsdetsc"] != null)
                {
                    model.newsdetsc = row["newsdetsc"].ToString();
                }
                if (row["authorid"] != null)
                {
                    stu.sno=row["authorid"].ToString();
                    model.authorid = stu;
                }
                if (row["clickcount"] != null && row["clickcount"].ToString() != "")
                {
                    model.clickcount = decimal.Parse(row["clickcount"].ToString());
                }
                if (row["ishead"] != null && row["ishead"].ToString() != "")
                {
                    model.ishead = decimal.Parse(row["ishead"].ToString());
                }
                if (row["ishot"] != null && row["ishot"].ToString() != "")
                {
                    model.ishot = decimal.Parse(row["ishot"].ToString());
                }
                if (row["lastcomid"] != null)
                {
                    Student lastComStu = new Student();
                    lastComStu.sno=row["lastcomid"].ToString();
                    model.lastcomid = lastComStu;
                }
                if (row["lastcomtime"] != null && row["lastcomtime"].ToString() != "")
                {
                    model.lastcomtime = row["lastcomtime"].ToString();
                }
                if (row["comcount"] != null && row["comcount"].ToString() != "")
                {
                    model.comcount = decimal.Parse(row["comcount"].ToString());
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
            strSql.Append("select newsid,newscatno,newstitle,createtime,source,newsdetsc,authorid,clickcount,ishead,ishot,lastcomid,lastcomtime,comcount ");
            strSql.Append(" FROM news ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by createtime desc");
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
            strSql.Append(" newsid,newscatno,newstitle,createtime,source,newsdetsc,authorid,clickcount,ishead,ishot,lastcomid,lastcomtime,comcount ");
            strSql.Append(" FROM news ");
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
            strSql.Append("select count(1) FROM news ");
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
                strSql.Append("order by T.newsid desc");
            }
            strSql.Append(")AS Row, T.*  from news T ");
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
            parameters[0].Value = "news";
            parameters[1].Value = "newsid";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return SQLDBHelper.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

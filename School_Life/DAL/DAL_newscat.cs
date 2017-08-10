/**  版本信息模板在安装目录下，可自行修改。
* newscat.cs
*
* 功 能： N/A
* 类 名： newscat
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017-02-25 22:19:53   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Model;
using Common;
using System.Collections;
using System.Collections.Generic;

namespace DAL
{
	/// <summary>
	/// 数据访问类:newscat
	/// </summary>
	public partial class DAL_newscat
	{
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int newscatid)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from newscat");
			strSql.Append(" where newscatid=@newscatid ");
			SqlParameter[] parameters = {
					new SqlParameter("@newscatid", SqlDbType.VarChar,50)			};
			parameters[0].Value = newscatid;

			return SQLDBHelper.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 根据条件查询并进行分页
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
            paraMap.Add("startIndex", pager.getStartIndex());
            paraMap.Add("endIndex", pager.getEndIndex());
            StringBuilder sqlSb = new StringBuilder("select t2.newscatid,t2.newscatnm from(" +
                        "select ROW_NUMBER() over(order by t1.newscatid) rn,t1.* from" +
                            "(select n.newscatid,n.newscatnm from newscat n where 1=1"
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
        /// 根据条件查询总记录数
        /// </summary>
        /// <param name="paraMap"></param>
        /// <returns></returns>
        public static int queryCount(Hashtable paraMap)
        {
            StringBuilder sqlSb = new StringBuilder("select count(*) from newscat where 1=1");

            DataSet rstSet = SQLDBHelper.GetDataSetBySQL(sqlSb.ToString());
            return Convert.ToInt32(rstSet.Tables[0].Rows[0][0]);
        }


		/// <summary>
		/// 增加一条数据
		/// </summary>
        public static bool Add(NewsCat model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into newscat(");
			strSql.Append("newscatnm,cascadeid,orderidx)");
			strSql.Append(" values (");
			strSql.Append("@newscatnm,@cascadeid,@orderidx)");
			SqlParameter[] parameters = {
					new SqlParameter("@newscatnm", SqlDbType.VarChar,50),
					new SqlParameter("@cascadeid", SqlDbType.Decimal,5),
					new SqlParameter("@orderidx", SqlDbType.Decimal,5)};
			parameters[0].Value = model.newscatnm;
			parameters[1].Value = model.cascadeid;
			parameters[2].Value = model.orderidx;

			int rows=SQLDBHelper.ExecuteSql(strSql.ToString(),parameters);
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
        public static bool Update(NewsCat model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update newscat set ");
			strSql.Append("newscatnm=@newscatnm,");
			strSql.Append("cascadeid=@cascadeid,");
			strSql.Append("orderidx=@orderidx");
			strSql.Append(" where newscatid=@newscatid ");
			SqlParameter[] parameters = {
					new SqlParameter("@newscatnm", SqlDbType.VarChar,50),
					new SqlParameter("@cascadeid", SqlDbType.Decimal,5),
					new SqlParameter("@orderidx", SqlDbType.Decimal,5),
					new SqlParameter("@newscatid", SqlDbType.VarChar,50)};
			parameters[0].Value = model.newscatnm;
			parameters[1].Value = model.cascadeid;
			parameters[2].Value = model.orderidx;
			parameters[3].Value = model.newscatid;

			int rows=SQLDBHelper.ExecuteSql(strSql.ToString(),parameters);
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
		public static bool Delete(int newscatid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from newscat ");
			strSql.Append(" where newscatid=@newscatid ");
			SqlParameter[] parameters = {
					new SqlParameter("@newscatid", SqlDbType.VarChar,50)			};
			parameters[0].Value = newscatid;

			int rows=SQLDBHelper.ExecuteSql(strSql.ToString(),parameters);
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
		public static bool DeleteList(string newscatidlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from newscat ");
			strSql.Append(" where newscatid in ("+newscatidlist + ")  ");
			int rows=SQLDBHelper.ExecuteSql(strSql.ToString());
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
        public static NewsCat GetModel(int newscatid)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 newscatid,newscatnm,cascadeid,orderidx from newscat ");
			strSql.Append(" where newscatid=@newscatid ");
			SqlParameter[] parameters = {
					new SqlParameter("@newscatid", SqlDbType.VarChar,50)			};
			parameters[0].Value = newscatid;

            NewsCat model = new NewsCat();
			DataSet ds=SQLDBHelper.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
        public static NewsCat DataRowToModel(DataRow row)
		{
            NewsCat model = new NewsCat();
			if (row != null)
			{
				if(row["newscatid"]!=null)
				{
					model.newscatid=Convert.ToInt32(row["newscatid"].ToString());
				}
				if(row["newscatnm"]!=null)
				{
					model.newscatnm=row["newscatnm"].ToString();
				}
				if(row["cascadeid"]!=null && row["cascadeid"].ToString()!="")
				{
					model.cascadeid=decimal.Parse(row["cascadeid"].ToString());
				}
				if(row["orderidx"]!=null && row["orderidx"].ToString()!="")
				{
					model.orderidx=decimal.Parse(row["orderidx"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select newscatid,newscatnm,cascadeid,orderidx ");
			strSql.Append(" FROM newscat ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return SQLDBHelper.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public static DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" newscatid,newscatnm,cascadeid,orderidx ");
			strSql.Append(" FROM newscat ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SQLDBHelper.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public static int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM newscat ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.newscatid desc");
			}
			strSql.Append(")AS Row, T.*  from newscat T ");
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
			parameters[0].Value = "newscat";
			parameters[1].Value = "newscatid";
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


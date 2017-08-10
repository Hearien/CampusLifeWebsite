/**  版本信息模板在安装目录下，可自行修改。
* dept.cs
*
* 功 能： N/A
* 类 名： dept
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017-02-25 22:19:52   N/A    初版
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
	/// 数据访问类:dept
	/// </summary>
	public static class DAL_dept
	{
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public static int GetMaxId()
		{
		return SQLDBHelper.GetMaxID("id", "dept"); 
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
            StringBuilder sqlSb = new StringBuilder("select t2.id,t2.deptNo,t2.deptName,t2.deptDesc from(" +
                        "select ROW_NUMBER() over(order by t1.id) rn,t1.* from" +
                            "(select d.id,d.deptNo,d.deptName,d.deptDesc from dept d where 1=1"
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
            StringBuilder sqlSb = new StringBuilder("select count(*) from dept d where 1=1");

            DataSet rstSet = SQLDBHelper.GetDataSetBySQL(sqlSb.ToString());
            return Convert.ToInt32(rstSet.Tables[0].Rows[0][0]);
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from dept");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return SQLDBHelper.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int Add(Dept model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into dept(");
			strSql.Append("deptNo,deptName,deptDesc)");
			strSql.Append(" values (");
			strSql.Append("@deptNo,@deptName,@deptDesc)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@deptNo", SqlDbType.Char,10),
					new SqlParameter("@deptName", SqlDbType.VarChar,50),
					new SqlParameter("@deptDesc", SqlDbType.VarChar,-1)};
			parameters[0].Value = model.deptNo;
			parameters[1].Value = model.deptName;
			parameters[2].Value = model.deptDesc;

			object obj = SQLDBHelper.GetSingle(strSql.ToString(),parameters);
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
        public static bool Update(Dept model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update dept set ");
			strSql.Append("deptNo=@deptNo,");
			strSql.Append("deptName=@deptName,");
			strSql.Append("deptDesc=@deptDesc");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@deptNo", SqlDbType.Char,10),
					new SqlParameter("@deptName", SqlDbType.VarChar,50),
					new SqlParameter("@deptDesc", SqlDbType.VarChar,-1),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.deptNo;
			parameters[1].Value = model.deptName;
			parameters[2].Value = model.deptDesc;
			parameters[3].Value = model.id;

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
		public static bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from dept ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

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
		public static bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from dept ");
			strSql.Append(" where id in ("+idlist + ")  ");
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
        public static Dept GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,deptNo,deptName,deptDesc from dept ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

            Dept model = new Dept();
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
        public static Dept DataRowToModel(DataRow row)
		{
            Dept model = new Dept();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["deptNo"]!=null)
				{
					model.deptNo=row["deptNo"].ToString();
				}
				if(row["deptName"]!=null)
				{
					model.deptName=row["deptName"].ToString();
				}
				if(row["deptDesc"]!=null)
				{
					model.deptDesc=row["deptDesc"].ToString();
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
			strSql.Append("select id,deptNo,deptName,deptDesc ");
			strSql.Append(" FROM dept ");
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
			strSql.Append(" id,deptNo,deptName,deptDesc ");
			strSql.Append(" FROM dept ");
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
			strSql.Append("select count(1) FROM dept ");
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
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from dept T ");
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
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
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
			parameters[0].Value = "dept";
			parameters[1].Value = "id";
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


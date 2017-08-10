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
	/// 数据访问类:major
	/// </summary>
	public class DAL_major
	{
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public static int GetMaxId()
		{
		    return SQLDBHelper.GetMaxID("id", "major"); 
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
            StringBuilder sqlSb = new StringBuilder("select t2.id,t2.majorNo,t2.majorName,t2.deptNo,t2.deptName from(" +
                        "select ROW_NUMBER() over(order by t1.id) rn,t1.* from" +
                            "(select m.id,m.majorNo,m.majorName,m.deptNo,d.deptName from major m inner join dept d on m.deptNo=d.deptNo where 1=1"
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
            StringBuilder sqlSb = new StringBuilder("select count(*) from major m inner join dept d on m.deptNo=d.deptNo where 1=1");

            DataSet rstSet = SQLDBHelper.GetDataSetBySQL(sqlSb.ToString());
            return Convert.ToInt32(rstSet.Tables[0].Rows[0][0]);
        }

        /// <summary>
        /// 获得学生表已有专业列表
        /// </summary>
        /// <returns></returns>
        public static DataSet getMajorList()
        {
            string sql = "select distinct m.id,m.deptNo,m.majorNo,m.majorName from student s " +
                        "inner join gender g on g.genderNo = s.gender " +
                        "inner join dept d on s.dept=d.deptNo " +
                        "inner join major m on s.major = m.majorNo";
            DataSet rstSet = SQLDBHelper.GetDataSetBySQL(sql);
            return rstSet;
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from major");
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
		public static int Add(Major model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into major(");
			strSql.Append("majorNo,majorName,deptNo)");
			strSql.Append(" values (");
			strSql.Append("@majorNo,@majorName,@deptNo)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@majorNo", SqlDbType.Char,10),
					new SqlParameter("@majorName", SqlDbType.VarChar,50),
					new SqlParameter("@deptNo", SqlDbType.Char,10)};
			parameters[0].Value = model.majorNo;
			parameters[1].Value = model.majorName;
			parameters[2].Value = model.deptNo.deptNo;

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
		public static bool Update(Major model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update major set ");
			strSql.Append("majorNo=@majorNo,");
			strSql.Append("majorName=@majorName,");
			strSql.Append("deptNo=@deptNo");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@majorNo", SqlDbType.Char,10),
					new SqlParameter("@majorName", SqlDbType.VarChar,50),
					new SqlParameter("@deptNo", SqlDbType.Char,10),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.majorNo;
			parameters[1].Value = model.majorName;
			parameters[2].Value = model.deptNo;
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
			strSql.Append("delete from major ");
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
			strSql.Append("delete from major ");
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
		public static Major GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,majorNo,majorName,deptNo from major ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Major model=new Major();
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
		public static Major DataRowToModel(DataRow row)
		{
			Major model=new Major();
            Dept dpt = new Dept();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["majorNo"]!=null)
				{
					model.majorNo=row["majorNo"].ToString();
				}
				if(row["majorName"]!=null)
				{
					model.majorName=row["majorName"].ToString();
				}
				if(row["deptNo"]!=null)
				{
					dpt.deptNo=row["deptNo"].ToString();
                    model.deptNo = dpt;
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
			strSql.Append("select id,majorNo,majorName,deptNo ");
			strSql.Append(" FROM major ");
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
			strSql.Append(" id,majorNo,majorName,deptNo ");
			strSql.Append(" FROM major ");
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
			strSql.Append("select count(1) FROM major ");
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
			strSql.Append(")AS Row, T.*  from major T ");
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
			parameters[0].Value = "major";
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


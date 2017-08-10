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
	/// 数据访问类:student
	/// </summary>
	public class DAL_student
	{
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public static int GetMaxId()
		{
		    return SQLDBHelper.GetMaxID("id", "student"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(Student stu)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from student");
			strSql.Append(" where sno=@sno and pwd=@pwd");
			SqlParameter[] parameters = {
					new SqlParameter("@sno", SqlDbType.Char,20),
					new SqlParameter("@pwd", SqlDbType.VarChar,50)
			};
			parameters[0].Value = stu.sno;
            parameters[1].Value = stu.pwd;

			return SQLDBHelper.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(string sno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from student");
            strSql.Append(" where sno=@sno");
            SqlParameter[] parameters = {
					new SqlParameter("@sno", SqlDbType.Char,20),
			};
            parameters[0].Value = sno;

            return SQLDBHelper.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据条件联表获得学生信息并进行分页
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
            StringBuilder sqlSb = new StringBuilder("select t3.sno,t3.sname,t3.genderVal,t3.grade,t3.deptName,t3.majorName,t3.QQ,t3.address from(" +
                "select t2.* from(" +
                    "select ROW_NUMBER() over(order by t1.sid) rn,t1.*  from(" +
                        "select s.stuid sid,s.*,g.genderNo,g.genderVal,d.deptNo,d.deptName,m.majorNo,m.majorName " +
                        "from student s " +
                        "inner join gender g on g.genderNo = s.gender " +
                        "inner join dept d on s.dept=d.deptNo " +
                        "inner join major m on s.major = m.majorNo where 1=1 ");

            if (!Util.isNull(paraMap["sno"].ToString())) {
                sqlSb.Append(" and s.sno like '%" + paraMap["sno"].ToString() + "%'");
            }
            if (!Util.isNull(paraMap["grade"].ToString()))
            {
                sqlSb.Append(" and s.grade = " + paraMap["grade"].ToString());
            }
            if (!Util.isNull(paraMap["major"].ToString()))
            {
                sqlSb.Append(" and s.major = '" + paraMap["major"].ToString().Trim()+"'");
            }

            sqlSb.Append(")t1 " +
                            ")t2 where t2.rn>"+paraMap["startIndex"] +
                       " )t3 where t3.rn<="+paraMap["endIndex"]
                       );
            DataSet rstSet = SQLDBHelper.GetDataSetBySQL(sqlSb.ToString());
            DataTable rstTable = rstSet.Tables[0];
            List<Hashtable> list = new List<Hashtable>();
            for(int i=0;i<rstTable.Rows.Count;i++)
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
        /// 根据条件查询学生总记录数
        /// </summary>
        /// <param name="paraMap"></param>
        /// <returns></returns>
        public static int queryCount(Hashtable paraMap)
        {
            StringBuilder sqlSb = new StringBuilder( "select COUNT(*) " +
                        "from student s " +
                        "inner join gender g on g.genderNo = s.gender " +
                        "inner join dept d on s.dept=d.deptNo " +
                        "inner join major m on s.major = m.majorNo where 1=1");

            if (!Util.isNull(paraMap["sno"].ToString()))
            {
                sqlSb.Append(" and s.sno like '%" + paraMap["sno"].ToString() + "%'");
            }
            if (!Util.isNull(paraMap["grade"].ToString()))
            {
                sqlSb.Append(" and s.grade = " + paraMap["grade"].ToString());
            }
            if (!Util.isNull(paraMap["major"].ToString()))
            {
                sqlSb.Append(" and s.major = '" + paraMap["major"].ToString().Trim()+"'");
            }
            DataSet rstSet = SQLDBHelper.GetDataSetBySQL(sqlSb.ToString());
            return Convert.ToInt32(rstSet.Tables[0].Rows[0][0]);
        }

        /// <summary>
        /// 获得年级列表
        /// </summary>
        /// <returns></returns>
        public static List<String> getGradeList()
        {
            string sql = "select distinct s.grade from student s";
            DataSet rstSet = SQLDBHelper.GetDataSetBySQL(sql);
            DataTable rstTable = rstSet.Tables[0];
            List<String> list = new List<String>();
            for (int i = 0; i < rstTable.Rows.Count; i++)
            {
                list.Add(rstTable.Rows[i][0].ToString());
            }
            return list;
        }

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="sno"></param>
        /// <returns></returns>
        public static bool ResetPwd(string sno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update student set ");
            strSql.Append("pwd=@pwd");
            strSql.Append(" where sno=@sno");
            SqlParameter[] parameters = {
					new SqlParameter("@sno", SqlDbType.Char,20),
					new SqlParameter("@pwd", SqlDbType.VarChar,50)};
            parameters[0].Value = sno;
            parameters[1].Value = Util.MD5Encrypt("sno");
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
        /// 增加一条数据
        /// </summary>
        public static int Add(Model.Student model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into student(");
			strSql.Append("head,sno,sname,pwd,gender,grade,dept,major,QQ,address)");
			strSql.Append(" values (");
			strSql.Append("@head,@sno,@sname,@pwd,@gender,@grade,@dept,@major,@QQ,@address)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@head", SqlDbType.VarChar,50),
					new SqlParameter("@sno", SqlDbType.Char,20),
					new SqlParameter("@sname", SqlDbType.Char,20),
					new SqlParameter("@pwd", SqlDbType.VarChar,50),
					new SqlParameter("@gender", SqlDbType.Char,2),
					new SqlParameter("@grade", SqlDbType.Char,10),
					new SqlParameter("@dept", SqlDbType.VarChar,50),
					new SqlParameter("@major", SqlDbType.VarChar,50),
					new SqlParameter("@QQ", SqlDbType.Char,20),
					new SqlParameter("@address", SqlDbType.VarChar,50)};
			parameters[0].Value = model.head;
			parameters[1].Value = model.sno;
			parameters[2].Value = model.sname;
			parameters[3].Value = model.pwd;
			parameters[4].Value = model.gender.genderNo;
			parameters[5].Value = model.grade;
			parameters[6].Value = model.dept.deptNo;
			parameters[7].Value = model.major.majorNo;
			parameters[8].Value = model.QQ;
			parameters[9].Value = model.address;

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
		public static bool Update(Model.Student model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update student set ");
			strSql.Append("head=@head,");
			strSql.Append("sno=@sno,");
			strSql.Append("sname=@sname,");
			strSql.Append("pwd=@pwd,");
			strSql.Append("gender=@gender,");
			strSql.Append("grade=@grade,");
			strSql.Append("dept=@dept,");
			strSql.Append("major=@major,");
			strSql.Append("QQ=@QQ,");
			strSql.Append("address=@address");
            strSql.Append(" where stuid=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@head", SqlDbType.VarChar,50),
					new SqlParameter("@sno", SqlDbType.Char,20),
					new SqlParameter("@sname", SqlDbType.Char,20),
					new SqlParameter("@pwd", SqlDbType.VarChar,50),
					new SqlParameter("@gender", SqlDbType.Char,2),
					new SqlParameter("@grade", SqlDbType.Char,10),
					new SqlParameter("@dept", SqlDbType.VarChar,50),
					new SqlParameter("@major", SqlDbType.VarChar,50),
					new SqlParameter("@QQ", SqlDbType.Char,20),
					new SqlParameter("@address", SqlDbType.VarChar,50),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.head;
			parameters[1].Value = model.sno;
			parameters[2].Value = model.sname;
			parameters[3].Value = model.pwd;
			parameters[4].Value = model.gender.genderNo;
			parameters[5].Value = model.grade;
			parameters[6].Value = model.dept.deptNo;
			parameters[7].Value = model.major.majorNo;
			parameters[8].Value = model.QQ;
			parameters[9].Value = model.address;
			parameters[10].Value = model.id;

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
		public static bool Delete(string sno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from student ");
			strSql.Append(" where sno=@sno");
			SqlParameter[] parameters = {
					new SqlParameter("@sno", SqlDbType.Char,20)
			};
			parameters[0].Value = sno;

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
			strSql.Append("delete from student ");
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
		public static Student GetModel(string sno)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select * from student s inner join gender g on s.gender=g.genderNo inner join dept d on s.dept=d.deptNo inner join major m on s.major=m.majorNo");
			strSql.Append(" where s.sno=@sno");
			SqlParameter[] parameters = {
					new SqlParameter("@sno", SqlDbType.Char,20)
			};
			parameters[0].Value = sno;

			Model.Student model=new Model.Student();
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
		public static Student DataRowToModel(DataRow row)
		{
			Student model=new Model.Student();
            Gender gender = new Gender();
            Dept dept = new Dept();
            Major major = new Major();
			if (row != null)
			{
				if(row["stuid"]!=null && row["stuid"].ToString()!="")
				{
                    model.id = int.Parse(row["stuid"].ToString());
				}
				if(row["head"]!=null)
				{
					model.head=row["head"].ToString();
				}
				if(row["sno"]!=null)
				{
					model.sno=row["sno"].ToString();
				}
				if(row["sname"]!=null)
				{
					model.sname=row["sname"].ToString();
				}
				if(row["pwd"]!=null)
				{
					model.pwd=row["pwd"].ToString();
				}
                if (row["genderNo"] != null)
				{
                    gender.genderNo = row["genderNo"].ToString();
					model.gender=gender;
				}
				if(row["grade"]!=null)
				{
					model.grade=row["grade"].ToString();
				}
                if (row["deptNo"] != null)
				{
                    dept.deptNo = row["deptNo"].ToString();
					model.dept=dept;
				}
                if (row["majorNo"] != null)
				{
                    major.majorNo = row["majorNo"].ToString();
                    model.major = major;
				}
				if(row["QQ"]!=null)
				{
					model.QQ=row["QQ"].ToString();
				}
				if(row["address"]!=null)
				{
					model.address=row["address"].ToString();
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
            strSql.Append("select stuid,head,sno,sname,pwd,gender,grade,dept,major,QQ,address ");
			strSql.Append(" FROM student ");
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
            strSql.Append(" stuid,head,sno,sname,pwd,gender,grade,dept,major,QQ,address ");
			strSql.Append(" FROM student ");
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
			strSql.Append("select count(1) FROM student ");
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
			strSql.Append(")AS Row, T.*  from student T ");
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
			parameters[0].Value = "student";
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


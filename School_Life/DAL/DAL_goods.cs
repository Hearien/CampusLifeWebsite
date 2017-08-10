/**  版本信息模板在安装目录下，可自行修改。
* goods.cs
*
* 功 能： N/A
* 类 名： goods
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017-02-08 21:12:41   N/A    初版
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
	/// 数据访问类:goods
	/// </summary>
	public class DAL_goods
	{
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public static int GetMaxId()
		{
		return SQLDBHelper.GetMaxID("id", "goods"); 
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
            pager.setPageSize(Int32.Parse(paraMap["pageSize"].ToString()));
            pager.setTotalRecord(queryCount(paraMap));
            paraMap.Add("startIndex", pager.getStartIndex());
            paraMap.Add("endIndex", pager.getEndIndex());
            StringBuilder sqlSb = new StringBuilder("select t2.thumb,t2.note,t2.detail,t2.goodsid,t2.title,t2.goods_catNam,t2.brandname,t2.price,t2.upTime,t2.sname from(" +
                        "select ROW_NUMBER() over(order by t1.goodsid) rn,t1.* from" +
                            "(select g.thumb,g.note,g.detail,g.goodsid,g.title,gc.goods_catNam,b.brandname,g.price,g.upTime,s.sname from goods g " +
                            "inner join goodsCat gc on g.gooodsCat=gc.goods_catId inner join student s on g.stuId=s.sno inner join brand b on g.brandid=b.grandid where 1=1"
                                );
            if (!Util.isNull(paraMap["title"].ToString()))
            {
                sqlSb.Append(" and g.title like '%" + paraMap["title"].ToString() + "%'");
            }
            if (!Util.isNull(paraMap["fDate"].ToString()))
            {
                sqlSb.Append(" and CONVERT(datetime,g.upTime) >= CONVERT(datetime,'" + paraMap["fDate"].ToString().Trim() + "')");
            }
            if (!Util.isNull(paraMap["tDate"].ToString()))
            {
                sqlSb.Append(" and CONVERT(datetime,g.upTime) <= CONVERT(datetime,'" + paraMap["tDate"].ToString().Trim() + "')");
            }
            if (paraMap["user"]!=null&&!Util.isNull(paraMap["user"].ToString()))
            {
                sqlSb.Append(" and g.stuId=" + paraMap["user"].ToString());
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
        /// 根据条件查询商品总记录数
        /// </summary>
        /// <param name="paraMap"></param>
        /// <returns></returns>
        public static int queryCount(Hashtable paraMap)
        {
            StringBuilder sqlSb = new StringBuilder("select count(*) from goods g "+
                       " inner join goodsCat gc on g.gooodsCat=gc.goods_catId"+
                       " inner join student s on g.stuId=s.sno"+
                       " inner join brand b on g.brandid=b.grandid"+
                       " where 1=1");
            if (!Util.isNull(paraMap["title"].ToString()))
            {
                sqlSb.Append(" and g.title like '%" + paraMap["title"].ToString() + "%'");
            }
            if (!Util.isNull(paraMap["fDate"].ToString()))
            {
                sqlSb.Append(" and CONVERT(datetime,g.upTime) >= CONVERT(datetime,'" + paraMap["fDate"].ToString().Trim() + "')");
            }
            if (!Util.isNull(paraMap["tDate"].ToString()))
            {
                sqlSb.Append(" and CONVERT(datetime,g.upTime) <= CONVERT(datetime,'" + paraMap["tDate"].ToString().Trim() + "')");
            }
            DataSet rstSet = SQLDBHelper.GetDataSetBySQL(sqlSb.ToString());
            return Convert.ToInt32(rstSet.Tables[0].Rows[0][0]);
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from goods");
			strSql.Append(" where goodsid=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return SQLDBHelper.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int Add(Model.Goods model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into goods(");
			strSql.Append("gooodsCat,thumb,title,note,brandid,price,detail,upTime,stuId)");
			strSql.Append(" values (");
            strSql.Append("@gooodsCat,@thumb,@title,@note,@brandid,@price,@detail,@upTime,@userId)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@gooodsCat", SqlDbType.Int,4),
					new SqlParameter("@thumb", SqlDbType.NVarChar,100),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@note", SqlDbType.NVarChar,100),
                    new SqlParameter("@brandid", SqlDbType.Int,4),
					new SqlParameter("@price", SqlDbType.Decimal,5),
					new SqlParameter("@detail", SqlDbType.Text),
					new SqlParameter("@upTime", SqlDbType.Date,3),
					new SqlParameter("@userId", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.gooodsCat.goods_catId;
			parameters[1].Value = model.thumb;
			parameters[2].Value = model.title;
			parameters[3].Value = model.note;
            parameters[4].Value = model.brandid.brandid;
			parameters[5].Value = model.price;
			parameters[6].Value = model.detail;
			parameters[7].Value = model.upTime;
			parameters[8].Value = model.Student.sno;

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
		public static bool Update(Model.Goods model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update goods set ");
			strSql.Append("gooodsCat=@gooodsCat,");
			strSql.Append("thumb=@thumb,");
			strSql.Append("title=@title,");
			strSql.Append("note=@note,");
			strSql.Append("price=@price,");
			strSql.Append("detail=@detail,");
			strSql.Append("upTime=@upTime,");
			strSql.Append("stuId=@userId");
			strSql.Append(" where goodsid=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@gooodsCat", SqlDbType.Int,4),
					new SqlParameter("@thumb", SqlDbType.NVarChar,100),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@note", SqlDbType.NVarChar,100),
					new SqlParameter("@price", SqlDbType.Decimal,5),
					new SqlParameter("@detail", SqlDbType.Text),
					new SqlParameter("@upTime", SqlDbType.Date),
					new SqlParameter("@userId", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.gooodsCat.goods_catId;
			parameters[1].Value = model.thumb;
			parameters[2].Value = model.title;
			parameters[3].Value = model.note;
			parameters[4].Value = model.price;
			parameters[5].Value = model.detail;
			parameters[6].Value = model.upTime;
			parameters[7].Value = model.Student.sno;
			parameters[8].Value = model.id;

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
			strSql.Append("delete from goods ");
			strSql.Append(" where goodsid=@id");
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
			strSql.Append("delete from goods ");
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
		public static Model.Goods GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 goodsid,gooodsCat,brandid,thumb,title,note,price,detail,upTime,stuId from goods ");
			strSql.Append(" where goodsid=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Model.Goods model=new Model.Goods();
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
		public static Model.Goods DataRowToModel(DataRow row)
		{
			Model.Goods model=new Model.Goods();
            Model.GoodsCat goodsCat = new GoodsCat();
            Model.Student student = new Student();
            Model.Brand brand = new Brand();
			if (row != null)
			{
                if (row["goodsid"] != null && row["goodsid"].ToString() != "")
				{
                    model.id = int.Parse(row["goodsid"].ToString());
				}
                if (row["gooodsCat"] != null)
				{
                    goodsCat =DAL_goodsCat.GetModel(int.Parse(row["gooodsCat"].ToString()));
                    model.gooodsCat = goodsCat;
				}
				if(row["thumb"]!=null)
				{
                    model.thumb = row["thumb"].ToString();
				}
				if(row["title"]!=null)
				{
					model.title=row["title"].ToString();
				}
				if(row["note"]!=null)
				{
					model.note=row["note"].ToString();
				}
                if (row["brandid"] != null)
                {
                    brand = DAL_brand.GetModel(Convert.ToInt32(row["brandid"].ToString()));
                    model.brandid = brand;
                }
				if(row["price"]!=null && row["price"].ToString()!="")
				{
					model.price=decimal.Parse(row["price"].ToString());
				}
				if(row["detail"]!=null)
				{
					model.detail=row["detail"].ToString();
				}
				if(row["upTime"]!=null && row["upTime"].ToString()!="")
				{
					model.upTime=DateTime.Parse(row["upTime"].ToString());
				}
                if (row["stuId"] != null && row["stuId"].ToString() != "")
				{
					student=DAL_student.GetModel(row["stuId"].ToString());
                    model.Student = student;
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
            strSql.Append("select goodsid,gooodsCat,thumb,title,note,brandid,price,detail,upTime,stuId ");
			strSql.Append(" FROM goods ");
			if(strWhere.Trim()!="" && !strWhere.Equals(null))
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
			strSql.Append(" goodsid,gooodsCat,thumb,title,note,price,detail,upTime,stuId ");
			strSql.Append(" FROM goods ");
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
			strSql.Append("select count(1) FROM goods ");
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
			strSql.Append(")AS Row, T.*  from goods T ");
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
			parameters[0].Value = "goods";
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


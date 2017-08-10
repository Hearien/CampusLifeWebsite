/**  版本信息模板在安装目录下，可自行修改。
* brand.cs
*
* 功 能： N/A
* 类 名： brand
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
    /// 数据访问类:brand
    /// </summary>
    public static class DAL_brand
    {
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public static int GetMaxId()
        {
            return SQLDBHelper.GetMaxID("grandid", "brand");
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
            StringBuilder sqlSb = new StringBuilder("select t2.grandid,t2.brandname,t2.goods_catNam,t2.isenable from(" +
                        "select ROW_NUMBER() over(order by t1.grandid) rn,t1.* from" +
                            "(select b.grandid,b.brandname,b.isenable,gc.goods_catNam from brand b inner join goodsCat gc on b.catid=gc.goods_catId where 1=1"
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
            StringBuilder sqlSb = new StringBuilder("select count(*) from brand b inner join goodsCat gc on b.catid=gc.goods_catId where 1=1");

            DataSet rstSet = SQLDBHelper.GetDataSetBySQL(sqlSb.ToString());
            return Convert.ToInt32(rstSet.Tables[0].Rows[0][0]);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int grandid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from brand");
            strSql.Append(" where grandid=@grandid ");
            SqlParameter[] parameters = {
					new SqlParameter("@grandid", SqlDbType.Int,4)			};
            parameters[0].Value = grandid;

            return SQLDBHelper.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(Model.Brand model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into brand(");
            strSql.Append("brandname,catid,isenable)");
            strSql.Append(" values (");
            strSql.Append("@brandname,@catid,@isenable)");
            SqlParameter[] parameters = {
					new SqlParameter("@brandname", SqlDbType.VarChar,50),
					new SqlParameter("@catid", SqlDbType.Int,4),
					new SqlParameter("@isenable", SqlDbType.Int,4)};
            parameters[0].Value = model.brandname;
            parameters[1].Value = model.GoodsCat.goods_catId;
            parameters[2].Value = model.isenable;

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
        public static bool Update(Model.Brand model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update brand set ");
            strSql.Append("brandname=@brandname,");
            strSql.Append("catid=@catid,");
            strSql.Append("isenable=@isenable");
            strSql.Append(" where grandid=@grandid ");
            SqlParameter[] parameters = {
					new SqlParameter("@brandname", SqlDbType.VarChar,50),
					new SqlParameter("@catid", SqlDbType.Int,4),
					new SqlParameter("@isenable", SqlDbType.Int,4),
					new SqlParameter("@grandid", SqlDbType.Int,4)};
            parameters[0].Value = model.brandname;
            parameters[1].Value = model.GoodsCat.goods_catId;
            parameters[2].Value = model.isenable;
            parameters[3].Value = model.brandid;

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
        public static bool Delete(int grandid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from brand ");
            strSql.Append(" where grandid=@grandid ");
            SqlParameter[] parameters = {
					new SqlParameter("@grandid", SqlDbType.Int,4)			};
            parameters[0].Value = grandid;

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
        public static bool DeleteList(string grandidlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from brand ");
            strSql.Append(" where grandid in (" + grandidlist + ")  ");
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
        public static Brand GetModel(int grandid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 grandid,brandname,catid,isenable from brand ");
            strSql.Append(" where grandid=@grandid ");
            SqlParameter[] parameters = {
					new SqlParameter("@grandid", SqlDbType.Int,4)			};
            parameters[0].Value = grandid;

            Model.Brand model = new Model.Brand();
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
        public static Model.Brand DataRowToModel(DataRow row)
        {
            Model.Brand model = new Model.Brand();
            Model.GoodsCat cat = new GoodsCat();
            if (row != null)
            {
                if (row["grandid"] != null && row["grandid"].ToString() != "")
                {
                    model.brandid = int.Parse(row["grandid"].ToString());
                }
                if (row["brandname"] != null)
                {
                    model.brandname = row["brandname"].ToString();
                }
                if (row["catid"] != null && row["catid"].ToString() != "")
                {
                    cat.goods_catId = int.Parse(row["catid"].ToString());
                    model.GoodsCat = cat;
                }
                if (row["isenable"] != null && row["isenable"].ToString() != "")
                {
                    model.isenable = int.Parse(row["isenable"].ToString());
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
            strSql.Append("select grandid,brandname,catid,isenable ");
            strSql.Append(" FROM brand ");
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
            strSql.Append(" grandid,brandname,catid,isenable ");
            strSql.Append(" FROM brand ");
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
            strSql.Append("select count(1) FROM brand ");
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
                strSql.Append("order by T.grandid desc");
            }
            strSql.Append(")AS Row, T.*  from brand T ");
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
            parameters[0].Value = "brand";
            parameters[1].Value = "grandid";
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


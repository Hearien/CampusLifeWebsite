/**  版本信息模板在安装目录下，可自行修改。
* goodsCat.cs
*
* 功 能： N/A
* 类 名： goodsCat
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
using System.Collections.Generic;
using Model;
using DAL;
using Common;
using System.Collections;
namespace BLL
{
	/// <summary>
	/// goodsCat
	/// </summary>
	public class BLL_goodsCat
	{
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public static int GetMaxId()
		{
			return DAL_goodsCat.GetMaxId();
		}

        public static Pager<Hashtable> queryByCondition(Hashtable paraMap)
        {
            return DAL_goodsCat.queryByCondition(paraMap);
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int goods_catId)
		{
			return DAL_goodsCat.Exists(goods_catId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int  Add(Model.GoodsCat model)
		{
			return DAL_goodsCat.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static bool Update(Model.GoodsCat model)
		{
			return DAL_goodsCat.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool Delete(int goods_catId)
		{
			
			return DAL_goodsCat.Delete(goods_catId);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool DeleteList(string goods_catIdlist )
		{
			return DAL_goodsCat.DeleteList(goods_catIdlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static Model.GoodsCat GetModel(int goods_catId)
		{
			
			return DAL_goodsCat.GetModel(goods_catId);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
        //public static Model.goodsCat GetModelByCache(int goods_catId)
        //{
			
        //    string CacheKey = "goodsCatModel-" + goods_catId;
        //    object objModel = Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = DAL_goodsCat.GetModel(goods_catId);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch{}
        //    }
        //    return (Model.goodsCat)objModel;
        //}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static DataSet GetList(string strWhere)
		{
			return DAL_goodsCat.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public static DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return DAL_goodsCat.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Model.GoodsCat> GetModelList(string strWhere)
		{
			DataSet ds = DAL_goodsCat.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Model.GoodsCat> DataTableToList(DataTable dt)
		{
			List<Model.GoodsCat> modelList = new List<Model.GoodsCat>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.GoodsCat model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = DAL_goodsCat.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public static int GetRecordCount(string strWhere)
		{
			return DAL_goodsCat.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public static DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return DAL_goodsCat.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public static DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return DAL_goodsCat.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}


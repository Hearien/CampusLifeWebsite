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
using System.Collections.Generic;
using DAL;
using Model;
using System.Collections;
using Common;
namespace BLL
{
	/// <summary>
	/// goods
	/// </summary>
	public class BLL_goods
	{
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public static int GetMaxId()
		{
			return DAL_goods.GetMaxId();
		}

        /// <summary>
        /// 根据条件进行查询并分页
        /// </summary>
        /// <param name="paraMap"></param>
        /// <returns></returns>
        public static Pager<Hashtable> queryByCondition(Hashtable paraMap)
        {
            return DAL_goods.queryByCondition(paraMap);
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int id)
		{
			return DAL_goods.Exists(id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int  Add(Model.Goods model)
		{
			return DAL_goods.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static bool Update(Model.Goods model)
		{
			return DAL_goods.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool Delete(int id)
		{
			
			return DAL_goods.Delete(id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool DeleteList(string idlist )
		{
			return DAL_goods.DeleteList(idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static Model.Goods GetModel(int id)
		{
			
			return DAL_goods.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
        //public static Model.goods GetModelByCache(int id)
        //{
			
        //    string CacheKey = "goodsModel-" + id;
        //    object objModel = Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = DAL_goods.GetModel(id);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch{}
        //    }
        //    return (Model.goods)objModel;
        //}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static DataSet GetList(string strWhere)
		{
			return DAL_goods.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public static DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return DAL_goods.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Model.Goods> GetModelList(string strWhere)
		{
			DataSet ds = DAL_goods.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Model.Goods> DataTableToList(DataTable dt)
		{
			List<Model.Goods> modelList = new List<Model.Goods>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.Goods model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = DAL_goods.DataRowToModel(dt.Rows[n]);
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
			return DAL_goods.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public static DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return DAL_goods.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public static DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return DAL_goods.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}


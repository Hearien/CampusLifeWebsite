using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Collections;
using Common;
using Model;

namespace BLL
{
    public static class BLL_brand
    {
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public static int GetMaxId()
		{
			return DAL_brand.GetMaxId();
		}

        public static Pager<Hashtable> queryByCondition(Hashtable paraMap)
        {
            return DAL_brand.queryByCondition(paraMap);
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int grandid)
		{
			return DAL_brand.Exists(grandid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static bool Add(Brand model)
		{
			return DAL_brand.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static bool Update(Brand model)
		{
			return DAL_brand.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool Delete(int grandid)
		{
			
			return DAL_brand.Delete(grandid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool DeleteList(string grandidlist )
		{
			return DAL_brand.DeleteList(grandidlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public static Model.Brand GetModel(int grandid)
		{
			
			return DAL_brand.GetModel(grandid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
        /*public static Model.Brand GetModelByCache(int grandid)
		{
			
			string CacheKey = "brandModel-" + grandid;
			object objModel = Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = DAL_brand.GetModel(grandid);
					if (objModel != null)
					{
						int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache");
						Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.brand)objModel;
		}*/

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static DataSet GetList(string strWhere)
		{
			return DAL_brand.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public static DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return DAL_brand.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Brand> GetModelList(string strWhere)
		{
			DataSet ds = DAL_brand.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Brand> DataTableToList(DataTable dt)
		{
            List<Brand> modelList = new List<Brand>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                Brand model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = DAL_brand.DataRowToModel(dt.Rows[n]);
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
			return DAL_brand.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public static DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return DAL_brand.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public static DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return DAL_brand.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
    }
}

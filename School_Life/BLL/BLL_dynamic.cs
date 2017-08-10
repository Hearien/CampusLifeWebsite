using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using DAL;
using System.Collections;
using Common;

namespace BLL
{
    public class BLL_dynamic
    {
		/// <summary>
		/// 得到最大ID
		/// </summary>
		public static int GetMaxId()
		{
			return DAL_dynamic.GetMaxId();
		}

        public static Pager<Hashtable> queryByCondition(Hashtable paraMap)
        {
            return DAL_dynamic.queryByCondition(paraMap);
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int dynamicid)
		{
			return DAL_dynamic.Exists(dynamicid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static bool Add(Dynamic model)
		{
			return DAL_dynamic.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static bool Update(Dynamic model)
		{
			return DAL_dynamic.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool Delete(int dynamicid)
		{
			
			return DAL_dynamic.Delete(dynamicid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool DeleteList(string dynamicidlist )
		{
			return DAL_dynamic.DeleteList(dynamicidlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static Dynamic GetModel(int dynamicid)
		{
			
			return DAL_dynamic.GetModel(dynamicid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		/*public static Dynamic GetModelByCache(int dynamicid)
		{
			
			string CacheKey = "dynamicModel-" + dynamicid;
			object objModel = Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = DAL_dynamic.GetModel(dynamicid);
					if (objModel != null)
					{
						int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache");
						Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Dynamic)objModel;
		}*/

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static DataSet GetList(string strWhere)
		{
			return DAL_dynamic.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public static DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return DAL_dynamic.GetList(Top,strWhere,filedOrder);
		}

        /// <summary>
        /// 获得动态集合
        /// </summary>
        /// <returns></returns>
        public static List<Hashtable> getDynamicList()
        {
            return DAL_dynamic.getDynamicList(); 
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Dynamic> GetModelList(string strWhere)
		{
			DataSet ds = DAL_dynamic.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Dynamic> DataTableToList(DataTable dt)
		{
			List<Dynamic> modelList = new List<Dynamic>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Dynamic model = new Dynamic();;
				for (int n = 0; n < rowsCount; n++)
				{
					model = DAL_dynamic.DataRowToModel(dt.Rows[n]);
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
			return DAL_dynamic.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public static DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return DAL_dynamic.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public static DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return DAL_dynamic.GetList(PageSize,PageIndex,strWhere);
		//}
    }
}

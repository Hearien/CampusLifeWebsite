using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using DAL;
using Common;
using System.Collections;

namespace BLL
{
    public class BLL_newscat
    {
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int newscatid)
		{
			return DAL_newscat.Exists(newscatid);
		}

        public static Pager<Hashtable> queryByCondition(Hashtable paraMap)
        {
            return DAL_newscat.queryByCondition(paraMap);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static bool  Add(NewsCat model)
		{
			return DAL_newscat.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static bool Update(NewsCat model)
		{
			return DAL_newscat.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool Delete(int newscatid)
		{
			
			return DAL_newscat.Delete(newscatid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool DeleteList(string newscatidlist )
		{
			return DAL_newscat.DeleteList(newscatidlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static NewsCat GetModel(int newscatid)
		{
			
			return DAL_newscat.GetModel(newscatid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		/*public static NewsCat GetModelByCache(int newscatid)
		{
			
			string CacheKey = "newscatModel-" + newscatid;
			object objModel = Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = DAL_newscat.GetModel(newscatid);
					if (objModel != null)
					{
						int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache");
						Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.newscat)objModel;
		}*/

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static DataSet GetList(string strWhere)
		{
			return DAL_newscat.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public static DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return DAL_newscat.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<NewsCat> GetModelList(string strWhere)
		{
			DataSet ds = DAL_newscat.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public static List<NewsCat> DataTableToList(DataTable dt)
		{
            List<NewsCat> modelList = new List<NewsCat>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                NewsCat model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = DAL_newscat.DataRowToModel(dt.Rows[n]);
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
			return DAL_newscat.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public static DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return DAL_newscat.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public static DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return DAL_newscat.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
    }
}

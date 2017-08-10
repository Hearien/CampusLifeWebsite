using System;
using System.Data;
using System.Collections.Generic;
using Model;
using DAL;
using System.Collections;
using Common;
namespace BLL
{
	/// <summary>
	/// dept
	/// </summary>
	public class BLL_dept
	{
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public static int GetMaxId()
		{
			return DAL_dept.GetMaxId();
		}

        public static Pager<Hashtable> queryByCondition(Hashtable paraMap)
        {
            return DAL_dept.queryByCondition(paraMap);
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int id)
		{
			return DAL_dept.Exists(id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int  Add(Model.Dept model)
		{
			return DAL_dept.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static bool Update(Model.Dept model)
		{
			return DAL_dept.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool Delete(int id)
		{
			
			return DAL_dept.Delete(id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool DeleteList(string idlist )
		{
			return DAL_dept.DeleteList(idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static Model.Dept GetModel(int id)
		{
			
			return DAL_dept.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		/*public static Model.dept GetModelByCache(int id)
		{
			
			string CacheKey = "deptModel-" + id;
			object objModel = Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = DAL_dept.GetModel(id);
					if (objModel != null)
					{
						int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache");
						Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.dept)objModel;
		}*/

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static DataSet GetList(string strWhere)
		{
			return DAL_dept.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public static DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return DAL_dept.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Model.Dept> GetModelList(string strWhere)
		{
			DataSet ds = DAL_dept.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Model.Dept> DataTableToList(DataTable dt)
		{
			List<Model.Dept> modelList = new List<Model.Dept>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.Dept model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = DAL_dept.DataRowToModel(dt.Rows[n]);
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
			return DAL_dept.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public static DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return DAL_dept.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public static DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return DAL_dept.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}


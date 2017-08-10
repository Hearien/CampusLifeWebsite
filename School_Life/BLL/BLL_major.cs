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
	/// major
	/// </summary>
	public class BLL_major
	{
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public static int GetMaxId()
		{
			return DAL_major.GetMaxId();
		}

        public static Pager<Hashtable> queryByCondition(Hashtable paraMap)
        {
            return DAL_major.queryByCondition(paraMap);
        }

        /// <summary>
        /// 获得学生表已有专业列表
        /// </summary>
        /// <returns></returns>
        public static List<Major> getMajorList()
        {
            return DataTableToList(DAL_major.getMajorList().Tables[0]);
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(int id)
		{
			return DAL_major.Exists(id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int  Add(Model.Major model)
		{
			return DAL_major.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static bool Update(Model.Major model)
		{
			return DAL_major.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool Delete(int id)
		{
			
			return DAL_major.Delete(id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool DeleteList(string idlist )
		{
			return DAL_major.DeleteList(idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static Model.Major GetModel(int id)
		{
			
			return DAL_major.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		/*public static Model.major GetModelByCache(int id)
		{
			
			string CacheKey = "majorModel-" + id;
			object objModel = Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = DAL_major.GetModel(id);
					if (objModel != null)
					{
						int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache");
						Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.major)objModel;
		}*/

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static DataSet GetList(string strWhere)
		{
			return DAL_major.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public static DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return DAL_major.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Model.Major> GetModelList(string strWhere)
		{
			DataSet ds = DAL_major.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Model.Major> DataTableToList(DataTable dt)
		{
			List<Model.Major> modelList = new List<Model.Major>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.Major model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = DAL_major.DataRowToModel(dt.Rows[n]);
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
			return DAL_major.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public static DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return DAL_major.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public static DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return DAL_major.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}


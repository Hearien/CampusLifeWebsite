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
	/// student
	/// </summary>
	public class BLL_student
	{
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public static int GetMaxId()
		{
			return DAL_student.GetMaxId();
		}

        public static Pager<Hashtable> queryByCondition(Hashtable paraMap)
        {
            return DAL_student.queryByCondition(paraMap);
        }

        public static List<String> getGradeList()
        {
            return DAL_student.getGradeList();
        }

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="sno"></param>
        /// <returns></returns>
        public static bool ResetPwd(string sno)
        {
            return DAL_student.ResetPwd(sno);
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public static bool Exists(Student stu)
		{
			return DAL_student.Exists(stu);
		}

        public static bool Exists(string sno) {
            return DAL_student.Exists(sno);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public static int  Add(Model.Student model)
		{
			return DAL_student.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public static bool Update(Model.Student model)
		{
			return DAL_student.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool Delete(string id)
		{
			
			return DAL_student.Delete(id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool DeleteList(string idlist )
		{
			return DAL_student.DeleteList(idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static Student GetModel(string id)
		{
			
			return DAL_student.GetModel(id);
		}

		/*/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public static Model.student GetModelByCache(int id)
		{
			
			string CacheKey = "studentModel-" + id;
			object objModel = Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = DAL_student.GetModel(id);
					if (objModel != null)
					{
						int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache");
						Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.student)objModel;
		}*/

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static DataSet GetList(string strWhere)
		{
			return DAL_student.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public static DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return DAL_student.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Model.Student> GetModelList(string strWhere)
		{
			DataSet ds = DAL_student.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public static List<Model.Student> DataTableToList(DataTable dt)
		{
			List<Model.Student> modelList = new List<Model.Student>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.Student model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = DAL_student.DataRowToModel(dt.Rows[n]);
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
			return DAL_student.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public static DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return DAL_student.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public static DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return DAL_student.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}


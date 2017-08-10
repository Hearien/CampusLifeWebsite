using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data;

namespace BLL
{
    public class BLL_admin
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(Admin model)
        {
            return DAL_admin.Exists(model);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(Admin model)
        {
            return DAL_admin.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Admin model)
        {
            return DAL_admin.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(int uid)
        {

            return DAL_admin.Delete(uid);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool DeleteList(string uidlist)
        {
            return DAL_admin.DeleteList(uidlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Admin GetModel(int uid)
        {

            return DAL_admin.GetModel(uid);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        /*public static Admin GetModelByCache(int uid)
        {

            string CacheKey = "adminModel-" + uid;
            object objModel = Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = DAL_admin.GetModel(uid);
                    if (objModel != null)
                    {
                        int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache");
                        Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Admin)objModel;
        }*/

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return DAL_admin.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public static DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return DAL_admin.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<Admin> GetModelList(string strWhere)
        {
            DataSet ds = DAL_admin.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<Admin> DataTableToList(DataTable dt)
        {
            List<Admin> modelList = new List<Admin>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Admin model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DAL_admin.DataRowToModel(dt.Rows[n]);
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
            return DAL_admin.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public static DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return DAL_admin.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public static DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return DAL_admin.GetList(PageSize,PageIndex,strWhere);
        //}
    }
}

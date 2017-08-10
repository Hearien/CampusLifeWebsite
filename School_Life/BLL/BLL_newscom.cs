using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using DAL;

namespace BLL
{
    public class BLL_newscom
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int comid)
        {
            return DAL_newscom.Exists(comid);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(NewsCom model)
        {
            return DAL_newscom.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(NewsCom model)
        {
            return DAL_newscom.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(int comid)
        {

            return DAL_newscom.Delete(comid);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool DeleteList(string comidlist)
        {
            return DAL_newscom.DeleteList(comidlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static NewsCom GetModel(int comid)
        {

            return DAL_newscom.GetModel(comid);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        /*public static NewsCom GetModelByCache(int comid)
        {

            string CacheKey = "newscomModel-" + comid;
            object objModel = Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = DAL_newscom.GetModel(comid);
                    if (objModel != null)
                    {
                        int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache");
                        Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (NewsCom)objModel;
        }*/

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return DAL_newscom.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public static DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return DAL_newscom.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<NewsCom> GetModelList(string strWhere)
        {
            DataSet ds = DAL_newscom.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<NewsCom> DataTableToList(DataTable dt)
        {
            List<NewsCom> modelList = new List<NewsCom>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                NewsCom model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DAL_newscom.DataRowToModel(dt.Rows[n]);
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
            return DAL_newscom.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public static DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return DAL_newscom.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public static DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return DAL_newscom.GetList(PageSize,PageIndex,strWhere);
        //}
    }
}

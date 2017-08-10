using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data;
using System.Collections;
using Common;

namespace BLL
{
    public static class BLL_news
    {
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool Exists(int newsid)
        {
            return DAL_news.Exists(newsid);
        }

        /// <summary>
        /// 获取各分类的头条新闻
        /// </summary>
        /// <returns></returns>
        public static Hashtable getTopNews()
        {
            return DAL_news.getTopNews();
        }

        /// <summary>
        /// 根据条件进行查询并分页
        /// </summary>
        /// <param name="paraMap"></param>
        /// <returns></returns>
        public static Pager<Hashtable> queryByCondition(Hashtable paraMap)
        {
            return DAL_news.queryByCondition(paraMap);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(News model)
        {
            return DAL_news.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(News model)
        {
            return DAL_news.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Delete(string newsid)
        {

            return DAL_news.Delete(newsid);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool DeleteList(string newsidlist)
        {
            return DAL_news.DeleteList(newsidlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static News GetModel(int newsid)
        {

            return DAL_news.GetModel(newsid);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        /*public static News GetModelByCache(string newsid)
        {

            string CacheKey = "newsModel-" + newsid;
            object objModel = Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = DAL_news.GetModel(newsid);
                    if (objModel != null)
                    {
                        int ModelCache = Common.ConfigHelper.GetConfigInt("ModelCache");
                        Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.news)objModel;
        }*/

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return DAL_news.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public static DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return DAL_news.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<News> GetModelList(string strWhere)
        {
            DataSet ds = DAL_news.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static List<News> DataTableToList(DataTable dt)
        {
            List<News> modelList = new List<News>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                News model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DAL_news.DataRowToModel(dt.Rows[n]);
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
            return DAL_news.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public static DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return DAL_news.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public static DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return DAL_news.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

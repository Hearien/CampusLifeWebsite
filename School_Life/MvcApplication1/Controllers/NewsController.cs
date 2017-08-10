using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;
using Common;
using System.Collections;
using System.Web.Script.Serialization;

namespace MvcApplication1.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/

        public ActionResult News(Pager<Hashtable> pager)
        {
            Hashtable paraMap = new Hashtable();
            paraMap.Add("pageSize", "15");
            paraMap["pageCode"] = "";
            paraMap.Add("title", "");
            paraMap.Add("type", "");
            paraMap.Add("fDate", "");
            paraMap.Add("tDate", "");
            pager = BLL_news.queryByCondition(paraMap);
            return View(pager);
        }

        /// <summary>
        /// 添加新闻页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            getGoodsCat();
            return View();
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Details()
        {
            int i = Convert.ToInt32(Request.QueryString["newsId"].ToString());
            News news = BLL_news.GetModel(i);
            string str = news.newsdetsc.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;","\"");
            news.newsdetsc = str;
            return View(news);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(FormCollection frmCol)
        {
            News news = new News();
            NewsCat newsCat = new NewsCat();
            Student author = new Student();
            news.newstitle = frmCol["title"].ToString();
            newsCat.newscatid = Convert.ToInt32(frmCol["newscat"].ToString());
            news.newscatno = newsCat;
            news.source = frmCol["source"].ToString();
            string isHead = "0";
            string isHot = "0";
            if (frmCol["head"]!=null)
            {
                isHead = frmCol["head"].ToString();
            }
            if (frmCol["hot"]!=null)
            {
                isHot = frmCol["hot"].ToString();
            }
            news.ishead = Convert.ToDecimal(isHead);
            news.ishot = Convert.ToDecimal(isHot);
            String desc = frmCol["Editor1"].ToString();
            news.newsdetsc = Server.HtmlEncode(desc);
            author.sno = "1310506123";
            news.authorid = author;
            news.createtime = DateTime.Now.ToString();
            if (BLL_news.Add(news))
            {
                return Redirect("/Admin/Index");
            }
            else
            {
                return Redirect("/Admin/Index");
            }
        }


        /// <summary>
        /// 获取新闻分类
        /// </summary>
        public void getGoodsCat()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<NewsCat> newsCatList = BLL_newscat.GetModelList("");
            foreach (NewsCat newscat in newsCatList)
            {
                items.Add(new SelectListItem { Text = newscat.newscatnm, Value = newscat.newscatid.ToString() });
            }
            ViewData["newscat"] = items;
        }

        public ActionResult NewsPager(string pageCode)
        {
            Hashtable paraMap = new Hashtable();
            paraMap.Add("pageCode", pageCode);
            paraMap.Add("title", "");
            paraMap.Add("fDate", "");
            paraMap.Add("tDate", "");
            paraMap.Add("pageSize", "15");
            Pager<Hashtable> pager = BLL_news.queryByCondition(paraMap);
            paraMap.Add("news", pager.getList());
            paraMap.Add("totalPage", pager.getTotalPage());
            paraMap["pageCode"] = pager.getCurrentPage();
            return View("News",pager);
        }

        public JsonResult AddCom(string newsId,string comDesc,string sno)
        {
            News news = new News();
            Student stu = new Student();
            NewsCom newsCom = new NewsCom();
            Hashtable comTb = new Hashtable();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            newsCom.comdesc = comDesc;
            news.newsid = Int32.Parse(newsId);
            newsCom.newsid = news;
            newsCom.comtime = DateTime.Now.ToString();
            newsCom.comip = GetHostAddress();
            stu = BLL_student.GetModel(sno);
            newsCom.uerid = stu;
            if (BLL_newscom.Add(newsCom)) {
                comTb.Add("msg", "评论成功！");
                comTb.Add("com", newsCom);
            }

            return Json(jss.Serialize(comTb));
        }

        /// <summary>
        /// 获取客户端IP地址（无视代理）
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public static string GetHostAddress()
        {
            string userHostAddress = System.Web.HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }


    }
}

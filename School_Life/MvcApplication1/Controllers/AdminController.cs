using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Common;
using BLL;
using System.Collections;
using System.Web.Script.Serialization;

namespace MvcApplication1.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection frmcol)
        {
            string userName = frmcol["username"].ToString();
            string pwd = frmcol["password"].ToString();
            Admin admin = new Admin();
            admin.uname = userName;
            admin.upwd = Util.MD5Encrypt(pwd);
            admin.status = 1;
            if (BLL_admin.Exists(admin)) {
                Session["admin"] = userName;
                return Redirect("Index");
            }
            return View("Login");
        }

        /**********用户管理************/
        public ActionResult User(Pager<Hashtable> pager)
        {
            getGradeList();
            getMajorList();
            Hashtable paraMap = new Hashtable();
            //paraMap["pageCode"] = "";
            paraMap.Add("pageCode", "");
            paraMap.Add("sno", "");
            paraMap.Add("grade", "");
            paraMap.Add("major", "");
            pager= BLL_student.queryByCondition(paraMap);
            return View(pager);
        }

        public JsonResult StuPager(string pageCode,string sno,string grade,string major)
        {
            Hashtable paraMap = new Hashtable();
            paraMap.Add("pageCode", pageCode);
            paraMap.Add("sno", sno);
            paraMap.Add("grade", grade);
            if (!Util.isNull(grade)) {
                paraMap["grade"] = BLL_student.getGradeList().ElementAt(Int32.Parse(grade) - 1);
            }
            paraMap.Add("major", major);
            Pager<Hashtable> pager = BLL_student.queryByCondition(paraMap);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            paraMap.Add("students", pager.getList());
            paraMap.Add("totalPage", pager.getTotalPage());
            paraMap["pageCode"] = pager.getCurrentPage();
            return Json(jss.Serialize(paraMap));
        }

        public JsonResult DelUser(string sno)
        {
            Hashtable hashTb = new Hashtable();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (BLL_student.Delete(sno))
            {
                hashTb.Add("msg", "删除成功!");
            }
            else
            {
                hashTb.Add("msg", "删除失败!");
            }
            return Json(jss.Serialize(hashTb));
        }

        public JsonResult UptUser(string sno)
        {
            Hashtable hashTb = new Hashtable();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (BLL_student.ResetPwd(sno))
            {
                hashTb.Add("msg", "密码重置成功!");
            }
            else
            {
                hashTb.Add("msg", "密码重置失败!");
            }
            return Json(jss.Serialize(hashTb));
        }

        /****************新闻管理********************/
        public ActionResult News(Pager<Hashtable> pager)
        {
            Hashtable paraMap = new Hashtable();
            paraMap["pageCode"] = "";
            paraMap.Add("pageSize", "10");
            paraMap.Add("title", "");
            paraMap.Add("type", "");
            paraMap.Add("fDate", "");
            paraMap.Add("tDate", "");
            pager = BLL_news.queryByCondition(paraMap);
            return View(pager);
        }

        public JsonResult NewsPager(string pageCode,string title,string fDate,string tDate)
        {
            Hashtable paraMap = new Hashtable();
            paraMap.Add("pageSize", "10");
            paraMap.Add("pageCode", pageCode);
            paraMap.Add("title", title);
            paraMap.Add("fDate", fDate);
            paraMap.Add("tDate", tDate);
            Pager<Hashtable> pager = BLL_news.queryByCondition(paraMap);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            paraMap.Add("news", pager.getList());
            paraMap.Add("totalPage", pager.getTotalPage());
            paraMap["pageCode"] = pager.getCurrentPage();
            return Json(jss.Serialize(paraMap));
        }

        public JsonResult DelNews(string newsId)
        {
            Hashtable hashTb = new Hashtable();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (BLL_news.Delete(newsId))
            {
                hashTb.Add("msg", "新闻删除成功!");
            }
            else {
                hashTb.Add("msg", "新闻删除失败!");
            }
            return Json(jss.Serialize(hashTb));
        }

        /****************商品管理******************/
        public ActionResult Goods(Pager<Hashtable> pager) 
        {
            Hashtable paraMap = new Hashtable();
            paraMap["pageCode"] = "";
            paraMap.Add("pageSize", "5");
            paraMap.Add("title", "");
            paraMap.Add("fDate", "");
            paraMap.Add("tDate", "");
            pager = BLL_goods.queryByCondition(paraMap);
            return View(pager);
        }

        public JsonResult GoodsPager(string pageCode, string title, string fDate, string tDate)
        {
            Hashtable paraMap = new Hashtable();
            paraMap.Add("pageCode", pageCode);
            paraMap.Add("pageSize", "5");
            paraMap.Add("title", title);
            paraMap.Add("fDate", fDate);
            paraMap.Add("tDate", tDate);
            Pager<Hashtable> pager = BLL_goods.queryByCondition(paraMap);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            paraMap.Add("goods", pager.getList());
            paraMap.Add("totalPage", pager.getTotalPage());
            paraMap["pageCode"] = pager.getCurrentPage();
            return Json(jss.Serialize(paraMap));
        }

        public JsonResult DelGoods(string goodsId)
        {
            Hashtable hashTb = new Hashtable();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (BLL_goods.Delete(Convert.ToInt32(goodsId)))
            {
                hashTb.Add("msg", "商品删除成功!");
            }
            else
            {
                hashTb.Add("msg", "商品删除失败!");
            }
            return Json(jss.Serialize(hashTb));
        }

        /*********************动态管理*********************/
        public ActionResult Dynamic(Pager<Hashtable> pager)
        {
            Hashtable paraMap = new Hashtable();
            paraMap["pageCode"] = "";
            pager = BLL_dynamic.queryByCondition(paraMap);
            return View(pager);
        }

        public ActionResult DynamicPager(string pageCode)
        {
            Hashtable paraMap = new Hashtable();
            paraMap.Add("pageCode", pageCode);
            Pager<Hashtable> pager = BLL_dynamic.queryByCondition(paraMap);
            return View("Dynamic", pager);
        }

        public ActionResult DelDynamic(string dynamicId)
        {
            Hashtable hashTb = new Hashtable();
            BLL_dynamic.Delete(Int32.Parse(dynamicId));
            return Redirect("Dynamic");
        }

        /*********************商品分类管理*********************/
        public ActionResult GoodsCat(Pager<Hashtable> pager)
        {
            Hashtable paraMap = new Hashtable();
            paraMap["pageCode"] = "";
            pager = BLL_goodsCat.queryByCondition(paraMap);
            return View(pager);
        }
        public JsonResult GoodsCatPager(string pageCode)
        {
            Hashtable paraMap = new Hashtable();
            paraMap.Add("pageCode", pageCode);
            Pager<Hashtable> pager = BLL_goodsCat.queryByCondition(paraMap);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            paraMap.Add("goodscat", pager.getList());
            paraMap.Add("totalPage", pager.getTotalPage());
            paraMap["pageCode"] = pager.getCurrentPage();
            return Json(jss.Serialize(paraMap));
        }
        [HttpPost]
        public ActionResult InsertGoodCat(FormCollection frmcol)
        {
            GoodsCat gc = new GoodsCat();
            if (frmcol["catnm"] != null) {
                gc.goods_catNam = frmcol["catnm"].ToString();
            }
            if (frmcol["catdes"] != null) {
                gc.catdesc = frmcol["catdes"].ToString();
            }
            if (frmcol["state"] != null || frmcol["state"].ToString() != "0") {
                gc.isenable = Int32.Parse(frmcol["state"].ToString());
            }
            BLL_goodsCat.Add(gc);
            return Redirect("GoodsCat");
        }
        public JsonResult DelGoodsCat(string goodsCatId)
        {
            Hashtable hashTb = new Hashtable();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (BLL_goodsCat.Delete(Convert.ToInt32(goodsCatId)))
            {
                hashTb.Add("msg", "类别删除成功!");
            }
            else
            {
                hashTb.Add("msg", "类别删除失败!");
            }
            return Json(jss.Serialize(hashTb));
        }

        /*********************商品品牌管理*********************/
        public ActionResult Brand(Pager<Hashtable> pager)
        {
            getGoodsCat();
            Hashtable paraMap = new Hashtable();
            paraMap["pageCode"] = "";
            pager = BLL_brand.queryByCondition(paraMap);
            return View(pager);
        }
        public JsonResult BrandPager(string pageCode)
        {
            Hashtable paraMap = new Hashtable();
            paraMap.Add("pageCode", pageCode);
            Pager<Hashtable> pager = BLL_brand.queryByCondition(paraMap);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            paraMap.Add("brand", pager.getList());
            paraMap.Add("totalPage", pager.getTotalPage());
            paraMap["pageCode"] = pager.getCurrentPage();
            return Json(jss.Serialize(paraMap));
        }
        [HttpPost]
        public ActionResult InsertBrand(FormCollection frmcol)
        {
            Brand brand = new Brand();
            GoodsCat gc = new GoodsCat();
            if (frmcol["brandNm"] != null)
            {
                brand.brandname = frmcol["brandNm"].ToString();
            }
            if (frmcol["goodcat"] != null)
            {
                gc.goods_catId = Int32.Parse(frmcol["goodcat"].ToString());
                brand.GoodsCat = gc;
            }
            if (frmcol["state"] != null || frmcol["state"].ToString() != "0")
            {
                brand.isenable = Int32.Parse(frmcol["state"].ToString());
            }
            else {
                brand.isenable = 0;
            }
            BLL_brand.Add(brand);
            return Redirect("Brand");
        }
        public JsonResult DelBrand(string brandId)
        {
            Hashtable hashTb = new Hashtable();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (BLL_brand.Delete(Convert.ToInt32(brandId)))
            {
                hashTb.Add("msg", "品牌删除成功!");
            }
            else
            {
                hashTb.Add("msg", "品牌删除失败!");
            }
            return Json(jss.Serialize(hashTb));
        }

        /*********************新闻分类管理*********************/
        public ActionResult NewsCat(Pager<Hashtable> pager)
        {
            Hashtable paraMap = new Hashtable();
            paraMap["pageCode"] = "";
            pager = BLL_newscat.queryByCondition(paraMap);
            return View(pager);
        }
        public JsonResult NewsCatPager(string pageCode)
        {
            Hashtable paraMap = new Hashtable();
            paraMap.Add("pageCode", pageCode);
            Pager<Hashtable> pager = BLL_newscat.queryByCondition(paraMap);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            paraMap.Add("newscat", pager.getList());
            paraMap.Add("totalPage", pager.getTotalPage());
            paraMap["pageCode"] = pager.getCurrentPage();
            return Json(jss.Serialize(paraMap));
        }
        [HttpPost]
        public ActionResult InsertNewsCat(FormCollection frmcol)
        {
            NewsCat nc = new NewsCat();
            if (frmcol["catnm"] != null)
            {
                nc.newscatnm = frmcol["catnm"].ToString();
            }
            BLL_newscat.Add(nc);
            return Redirect("NewsCat");
        }
        public JsonResult DelNewsCat(string newsCatId)
        {
            Hashtable hashTb = new Hashtable();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (BLL_newscat.Delete(Convert.ToInt32(newsCatId)))
            {
                hashTb.Add("msg", "类别删除成功!");
            }
            else
            {
                hashTb.Add("msg", "类别删除失败!");
            }
            return Json(jss.Serialize(hashTb));
        }

        /*********************系部信息管理*********************/
        public ActionResult Dept(Pager<Hashtable> pager)
        {
            Hashtable paraMap = new Hashtable();
            paraMap["pageCode"] = "";
            pager = BLL_dept.queryByCondition(paraMap);
            return View(pager);
        }
        public JsonResult DeptPager(string pageCode)
        {
            Hashtable paraMap = new Hashtable();
            paraMap.Add("pageCode", pageCode);
            Pager<Hashtable> pager = BLL_dept.queryByCondition(paraMap);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            paraMap.Add("dept", pager.getList());
            paraMap.Add("totalPage", pager.getTotalPage());
            paraMap["pageCode"] = pager.getCurrentPage();
            return Json(jss.Serialize(paraMap));
        }
        [HttpPost]
        public ActionResult InsertDept(FormCollection frmcol)
        {
            Dept dept = new Dept();
            if (frmcol["deptNo"] != null)
            {
                dept.deptNo = frmcol["deptNo"].ToString();
            }
            if (frmcol["deptNm"] != null)
            {
                dept.deptName = frmcol["deptNm"].ToString();
            }
            if (frmcol["desc"] != null)
            {
                dept.deptDesc = frmcol["desc"].ToString();
            }
            BLL_dept.Add(dept);
            return Redirect("Dept");
        }
        public JsonResult DelDept(string deptId)
        {
            Hashtable hashTb = new Hashtable();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (BLL_dept.Delete(Convert.ToInt32(deptId)))
            {
                hashTb.Add("msg", "类别删除成功!");
            }
            else
            {
                hashTb.Add("msg", "类别删除失败!");
            }
            return Json(jss.Serialize(hashTb));
        }

        /*********************专业信息管理*********************/
        public ActionResult Major(Pager<Hashtable> pager)
        {
            getDept();
            Hashtable paraMap = new Hashtable();
            paraMap["pageCode"] = "";
            pager = BLL_major.queryByCondition(paraMap);
            return View(pager);
        }
        public JsonResult MajorPager(string pageCode)
        {
            getDept();
            Hashtable paraMap = new Hashtable();
            paraMap.Add("pageCode", pageCode);
            Pager<Hashtable> pager = BLL_major.queryByCondition(paraMap);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            paraMap.Add("major", pager.getList());
            paraMap.Add("totalPage", pager.getTotalPage());
            paraMap["pageCode"] = pager.getCurrentPage();
            return Json(jss.Serialize(paraMap));
        }
        [HttpPost]
        public ActionResult InsertMajor(FormCollection frmcol)
        {
            Major major = new Major();
            Dept dept = new Dept();
            if (frmcol["majorNo"] != null)
            {
                major.majorNo = frmcol["majorNo"].ToString();
            }
            if (frmcol["majorNm"] != null)
            {
                major.majorName = frmcol["majorNm"].ToString();
            }
            if (frmcol["dept"] != null)
            {
                dept.deptNo = frmcol["dept"].ToString();
                major.deptNo = dept;
            }
            BLL_major.Add(major);
            return Redirect("Major");
        }
        public JsonResult DelMajor(string majorId)
        {
            Hashtable hashTb = new Hashtable();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (BLL_major.Delete(Convert.ToInt32(majorId)))
            {
                hashTb.Add("msg", "专业删除成功!");
            }
            else
            {
                hashTb.Add("msg", "专业删除失败!");
            }
            return Json(jss.Serialize(hashTb));
        }

        /// <summary>
        /// 获取系别下拉列表选项
        /// </summary>
        public void getDept()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<Dept> depList = BLL_dept.GetModelList("");
            foreach (Dept dept in depList)
            {
                items.Add(new SelectListItem { Text = dept.deptName, Value = dept.deptNo });
            }
            ViewData["dept"] = items;
        }

        /// <summary>
        /// 获得学生信息搜索年级下拉列表
        /// </summary>
        public void getGradeList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<String> gradeList = BLL_student.getGradeList();
            for(int i=0;i<gradeList.Count;i++)
            {
                items.Add(new SelectListItem { Text = gradeList.ElementAt(i), Value = (i+1).ToString() });
            }
            ViewData["grade"] = items;
        }

        /// <summary>
        /// 获得学生信息搜索专业下拉列表
        /// </summary>
        public void getMajorList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<Major> majorList = BLL_major.getMajorList();
            foreach (Major major in majorList)
            {
                items.Add(new SelectListItem { Text = major.majorName, Value = major.majorNo });
            }
            ViewData["major"] = items;
        }

        /// <summary>
        /// 获取新闻类型列表
        /// </summary>
        public void getNewsTypeList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<NewsCat> typeList = BLL_newscat.GetModelList("");
            foreach (NewsCat item in typeList)
            {
                items.Add(new SelectListItem { Text = item.newscatnm, Value = item.newscatid.ToString() });
            }
            ViewData["newstype"] = items;
        }

        /// <summary>
        /// 获取商品分类下拉列表选项
        /// </summary>
        public void getGoodsCat()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<GoodsCat> goodCatList = BLL_goodsCat.GetModelList("");
            foreach (GoodsCat goodscat in goodCatList)
            {
                items.Add(new SelectListItem { Text = goodscat.goods_catNam, Value = goodscat.goods_catId.ToString() });
            }
            ViewData["goodcat"] = items;
        }

    }
}

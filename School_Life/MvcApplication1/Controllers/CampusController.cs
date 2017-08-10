using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Collections;
using BLL;
using Model;
using System.Security.Cryptography;
using System.Text;
using AsyncUploaderDemo.Models;
using Common;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace MvcApplication1.Controllers
{
    public class CampusController : Controller
    {
        //
        // GET: /Campus/
        private IFileStore _fileStore = new DiskFileStore();
        public Dictionary<String, String> dictionary = new Dictionary<String, String>();
        private Student stu = new Student();
        private Gender gender = new Gender();
        private Dept dept = new Dept();
        private Major major = new Major();
        private GoodsCat goodsCat = new GoodsCat();

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="frmCol"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(FormCollection frmCol) 
        {
            string sno = frmCol["loginNo"].Trim().ToString();
            stu.sno=sno;
            string pwdStr=frmCol["loginPwd"].Trim().ToString();
            stu.pwd = Util.MD5Encrypt(pwdStr);
            if (BLL_student.Exists(stu))
            {
                stu = BLL_student.GetModel(sno);
                Session["sno"] = stu.sno;
                Session["img"] = stu.head;
                Session["name"] = stu.sname;
                Session.Timeout = 1000;
                return Redirect("Index");
            }
            else
            {
                Response.Write("<script>alert('学号或密码错误！');</script>");
            }

            return Redirect("Index");
        }

        public ActionResult MyInfo(Student stu) {
            string sno = Request.QueryString["sno"].Trim();
            stu = BLL_student.GetModel(sno);
            return View(stu);
        }

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="myFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateInfo(string stuno,string stuname,string stuqq,string stuaddr,HttpPostedFileBase stuhead)
        {
            Student s = BLL_student.GetModel(stuno);
            s.sname = stuname;
            Session["name"] = stuname;
            s.QQ = stuqq;
            s.address = stuaddr;
            if (stuhead != null)
            {
                string fileName = stuhead.FileName;

                string preStr = "";

                preStr = System.DateTime.Now.ToString() + "_";

                preStr = preStr.Replace("-", "");

                preStr = preStr.Replace(":", "");

                preStr = preStr.Replace(" ", "");//preStr是为了改变上传的文件名称   

                string uploadPath = Server.MapPath("..\\img\\head\\") + preStr + fileName;//在这里取的是相对目录,有个temp文件夹   

                stuhead.SaveAs(uploadPath);//保存上传的文件   

                s.head = "/img/head/" + preStr + fileName;

                Session["img"] = "/img/head/" + preStr + fileName;
            }
            BLL_student.Update(s);
            return Redirect("Index");
        }

        public JsonResult UptPwd(string sno,string pwd) {
            Student s = BLL_student.GetModel(sno);
            s.pwd = Util.MD5Encrypt(pwd);
            Hashtable msg = new Hashtable();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (BLL_student.Update(s))
            {
                msg.Add("msg", true);
            }
            else {
                msg.Add("msg", false);
            }
            return Json(jss.Serialize(msg));
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Session["root"] = "/Scripts/ckfinder/userfiles/_thumbs/Images/";

            List<News> newsList = BLL_news.GetModelList("");
            ViewData["news"] = newsList;
            List<Goods> gdsList = BLL_goods.GetModelList("");
            ViewData["goods"] = gdsList;
            Hashtable newsTb = BLL_news.getTopNews();
            ViewData["topnews"] = newsTb;

            return View();
        }
        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Regist()
        {
            getGrade();
            getDept();
            return View();
        }
        /// <summary>
        /// 处理注册
        /// </summary>
        /// <param name="formCol"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Regist(FormCollection formCol)
        {
            getGrade();
            stu.sno = formCol["stuid"].Trim().ToString();
            stu.sname = formCol["stuname"].Trim().ToString();
            string pwd = formCol["stupwd"].Trim().ToString();
            stu.pwd = Util.MD5Encrypt(pwd);
            gender.genderNo = formCol["gender"].Trim().ToString();
            stu.gender = gender;
            string gradeNo = formCol["grade"].Trim().ToString();
            stu.grade = dictionary[gradeNo].ToString();
            dept.deptNo = formCol["dept"].Trim().ToString();
            stu.dept = dept;
            major.majorNo = formCol["major"].Trim().ToString();
            stu.major = major;
            stu.QQ = formCol["QQ"].Trim().ToString();
            stu.address = formCol["address"].Trim().ToString();
            if (BLL_student.Add(stu) > 0) {
                return View("Success");
            }
            return View("Regist");
        }

        public JsonResult CheckSno(string sno) {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Hashtable msg = new Hashtable();
            if (BLL_student.Exists(sno))
            {
                msg.Add("msg", true);
            }
            else
            {
                msg.Add("msg", false);
            }
            return Json(jss.Serialize(msg));
        }

        /// <summary>
        /// 用户添加商品
        /// </summary>
        /// <param name="formCol"></param>
        /// <param name="photo_guid"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Addgoods(FormCollection formCol, Guid? photo_guid)
        {
            Session["root"] = "/Scripts/ckfinder/userfiles/_thumbs/Images/";
            if (Request.Files["photo"] != null) // If uploaded synchronously
            {
                photo_guid = _fileStore.SaveUploadedFile(Request.Files["photo"]);
            }
            string imgid = photo_guid.Value.ToString();

            Goods good = new Goods();
            Brand brand = new Brand();
            good.title = formCol["title"].Trim().ToString();
            goodsCat.goods_catId = Int32.Parse(formCol["goodcat"].Trim().ToString());
            good.gooodsCat = goodsCat;
            good.note = formCol["note"].Trim().ToString();
            brand.brandid = Convert.ToInt32(formCol["brand"].ToString());
            good.brandid = brand;
            string priceStr = formCol["price"].Trim().ToString();
            good.price = decimal.Parse(priceStr);
            good.thumb = imgid;
            good.detail = formCol["Editor1"].Trim().ToString();
            good.upTime = DateTime.Now;
            stu.sno = Session["sno"].ToString();
            good.Student = stu;
            int i = BLL_goods.Add(good);
            if (i > 0) 
            {
                TempData["good"] = good;
                return RedirectToAction("Detail");
            }
            return View("Addgoods");
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout() 
        {
            Session["sno"] = null;
            Session["img"] = null;
            Session["name"] = null;
            Session.Clear();
            return Redirect("Index");
        }

        public Guid AsyncUpload()
        {
            return _fileStore.SaveUploadedFile(Request.Files[0]);
        }
        /// <summary>
        /// 添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Addgoods()
        {
            getGoodsCat();
            return View();
        }
        /// <summary>
        /// 已登录用户的商品列表
        /// </summary>
        /// <param name="goodsList"></param>
        /// <returns></returns>
        public ActionResult Mygoods(Pager<Hashtable> pager)
        {
            Hashtable paraMap = new Hashtable();
            paraMap["pageCode"] = "";
            paraMap.Add("pageSize", "8");
            paraMap.Add("title", "");
            paraMap.Add("fDate", "");
            paraMap.Add("tDate", "");
            paraMap.Add("user", Session["sno"].ToString());
            pager = BLL_goods.queryByCondition(paraMap);
            return View(pager);
        }
        /// <summary>
        /// 我的商品分页
        /// </summary>
        /// <param name="pageCode"></param>
        /// <returns></returns>
        public ActionResult MyGoodsPager(string pageCode)
        {
            Hashtable paraMap = new Hashtable();
            paraMap.Add("pageCode", pageCode);
            paraMap.Add("pageSize", "8");
            paraMap.Add("title", "");
            paraMap.Add("fDate", "");
            paraMap.Add("tDate", "");
            paraMap.Add("user", Session["sno"].ToString());
            Pager<Hashtable> pager = BLL_goods.queryByCondition(paraMap);
            paraMap.Add("goods", pager.getList());
            paraMap.Add("totalPage", pager.getTotalPage());
            paraMap["pageCode"] = pager.getCurrentPage();
            return View("Mygoods", pager);
        }

        /// <summary>
        /// 查看商品详情
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        public ActionResult Detail(Goods good)
        {
            int goodid = Convert.ToInt32(Request.QueryString["goodid"]);
            if (TempData["good"] == null)
            {
                good = BLL_goods.GetModel(goodid);
            }
            else
            {
                good = (Goods)TempData["good"];
            }
            
            return View(good);
        }
        /// <summary>
        /// 编辑修改页面
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        public ActionResult Edit(Goods good)
        {
            getGoodsCat();
            int goodid = Convert.ToInt32(Request.QueryString["goodid"]);
            good = BLL_goods.GetModel(goodid);
            return View(good);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="formCol"></param>
        /// <param name="photo_guid"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateGoods(FormCollection formCol, Guid? photo_guid)
        {
            string imgid = "";
            if (Request.Files["photo"] != null) // If uploaded synchronously
            {
                photo_guid = _fileStore.SaveUploadedFile(Request.Files["photo"]);
                imgid = photo_guid.Value.ToString();
            }
            else {
                imgid = formCol["gdthumb"].ToString();
            }

            Goods good = new Goods();
            Brand brand = new Brand();
            good.id = Int32.Parse(formCol["goodsid"].ToString());
            good.title = formCol["title"].Trim().ToString();
            goodsCat.goods_catId = Int32.Parse(formCol["goodcat"].Trim().ToString());
            good.gooodsCat = goodsCat;
            good.note = formCol["note"].Trim().ToString();
            brand.brandid = Convert.ToInt32(formCol["brand"].ToString());
            good.brandid = brand;
            string priceStr = formCol["price"].Trim().ToString();
            good.price = decimal.Parse(priceStr);
            good.thumb = imgid;
            good.detail = formCol["Editor1"].Trim().ToString();
            good.upTime = DateTime.Now;
            stu.sno = Session["sno"].ToString();
            good.Student = stu;
            if (BLL_goods.Update(good))
            {
                TempData["good"] = good;
                return RedirectToAction("Detail");
            }
            return View("Mygoods");
        }

        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="goodsList"></param>
        /// <returns></returns>
        public ActionResult Goods(Pager<Hashtable> pager)
        {
            Hashtable paraMap = new Hashtable();
            paraMap["pageCode"] = "";
            paraMap.Add("pageSize", "8");
            paraMap.Add("title", "");
            paraMap.Add("fDate", "");
            paraMap.Add("tDate", "");
            pager = BLL_goods.queryByCondition(paraMap);
            return View(pager);
        }

        /// <summary>
        /// 商品分页
        /// </summary>
        /// <param name="pageCode"></param>
        /// <param name="title"></param>
        /// <param name="fDate"></param>
        /// <param name="tDate"></param>
        /// <returns></returns>
        public ActionResult GoodsPager(string pageCode)
        {
            Hashtable paraMap = new Hashtable();
            paraMap.Add("pageCode", pageCode);
            paraMap.Add("pageSize", "8");
            paraMap.Add("title", "");
            paraMap.Add("fDate", "");
            paraMap.Add("tDate", "");
            Pager<Hashtable> pager = BLL_goods.queryByCondition(paraMap);
            paraMap.Add("goods", pager.getList());
            paraMap.Add("totalPage", pager.getTotalPage());
            paraMap["pageCode"] = pager.getCurrentPage();
            return View("Goods", pager);
        }

        public ActionResult About() {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Suggest(FormCollection frmcol) {
            Opinions opinion = new Opinions();
            String uname = null;
            if (frmcol["userName"] == null)
            {
                if (Session["sno"] == null)
                {
                    uname = "游客";
                }
                else
                {
                    uname = Session["sno"].ToString();
                }
            }
            else {
                uname = frmcol["userName"].ToString();
            }
            opinion.userid = uname;
            opinion.opntime = DateTime.Now.ToString();
            opinion.opncontent = frmcol["opinion"].ToString();
            BLL_opinion.Add(opinion);
            return Redirect("Index");
        }

        /// <summary>
        /// 获取年级下拉列表选项
        /// </summary>
        public void getGrade()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            XmlDocument doc = new XmlDocument();
            String path = Server.MapPath("/Content/year.xml");
            doc.Load(path);
            XmlNode root = doc.SelectSingleNode("year");
            XmlNodeList nodes = root.ChildNodes;
            foreach (XmlNode node in nodes)
            {
                String yearName = node.ChildNodes[0].InnerText.ToString();
                String yearValue = node.ChildNodes[1].InnerText.ToString();
                items.Add(new SelectListItem { Text = yearValue, Value = yearName });
                dictionary.Add(yearName, yearValue);
            }
            ViewData["grade"] = items;
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

        [HttpPost]
        public ActionResult GetBrandList(string cateory)
        {
            string option = "<option value='0'>--请选择--</option>";
            List<Brand> brandList = BLL_brand.GetModelList("catid = " + cateory );
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (Brand item in brandList)
            {
                items.Add(new SelectListItem { Text = item.brandname, Value = item.brandid + "" });
                option += "<option value='" + item.brandid + "'>" + item.brandname + "</option>";
            }
            return Content(option);
        }

        [HttpPost]
        public ActionResult GetMajor(string dept)
        {
            string option = "<option value='0'>--请选择--</option>";
            List<Major> subjects = BLL_major.GetModelList("deptNo = '" + dept + "'");
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (Major item in subjects)
            {
                items.Add(new SelectListItem { Text = item.majorName, Value = item.majorNo + "" });
                option += "<option value='" + item.majorNo + "'>" + item.majorName + "</option>";
            }
            return Content(option);
        }

        public ActionResult NotFound()
        {
            return View("404");
        }

        /*******************************动态发表部分*****************************/

        public ActionResult Dynamic(List<Hashtable> list)
        {
            List<Goods> gdsList = BLL_goods.GetModelList("");
            ViewData["goods"] = gdsList;
            list = BLL_dynamic.getDynamicList();
            return View(list);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Anounce(string context,HttpPostedFileBase myFile)
        {
            Dynamic dynamic = new Dynamic();
            Student s = new Student();
            //string context = sayscom;
            if (myFile != null)
            {
                string fileName = myFile.FileName;

                string preStr = "";

                preStr = System.DateTime.Now.ToString() + "_";

                preStr = preStr.Replace("-", "");

                preStr = preStr.Replace(":", "");

                preStr = preStr.Replace(" ", "");//preStr是为了改变上传的文件名称   

                string uploadPath = Server.MapPath("..\\img\\dynamic\\") + preStr + fileName;//在这里取的是相对目录,有个temp文件夹   

                myFile.SaveAs(uploadPath);//保存上传的文件   

                Session["filelist"] += uploadPath + "|";//此方法是为了获取文件的实际地址并且记录下来你可以调用此session,   

                context += "<img class='illuspic' src ='/img/dynamic/" + preStr + fileName + "'/>";
            }
            s.sno = Session["sno"].ToString();;
            dynamic.userid = s;
            dynamic.createtime = DateTime.Now.ToString();
            dynamic.context = context;

            BLL_dynamic.Add(dynamic);

            return Redirect("Dynamic");
        }

    }
}

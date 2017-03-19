using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            //將 WebFormViewEngine 從 ViewEngines.Engines 中移除
            return View("123");
        }

        public ActionResult View2()
        {
            //從Controller決定是否要用Layout => PartialView
            return PartialView("Index");
        }

        public ActionResult View3()
        {
            //從View決定是否要用Layout => this.Layout = "";
            return View();
        }

        public ActionResult Content1()
        {
            return Content("<script>alert('新增成功');location.href='/';</script>");
        }

        public ActionResult File1()
        {
            return File(Server.MapPath("~/Content/mountain.jpg"), "image/jpg");
        }

        public ActionResult File2()
        {
            return File(Server.MapPath("~/Content/mountain.jpg"), "image/jpg", "mountain.jpg");
        }

        public ActionResult Json1()
        {
            return Json(new LoginVM() { Username = "fish", Password = "112233"}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Redirect1()
        {

            return RedirectToAction("View3");
        }

        public ActionResult Redirect2()
        {
            return RedirectToActionPermanent("View3");
        }
    }
}
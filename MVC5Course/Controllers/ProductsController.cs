using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using PagedList;
using System.Data.Entity.Validation;
using System.Web.UI;

namespace MVC5Course.Controllers
{
    //[OutputCache(Duration = 60, Location = OutputCacheLocation.ServerAndClient)]
    //[Authorize]
    public class ProductsController : BaseController
    {
        private void DoSearchOnIndex(string sort, string keyword, int page)
        {
            var all = repoProduct.All().AsQueryable();
            if (!String.IsNullOrEmpty(keyword))
            {
                all = all.Where(p => p.ProductName.Contains(keyword));
            }

            if (sort == "++")
            {
                all = all.OrderBy(p => p.Price);
            }
            else
            {
                all = all.OrderByDescending(p => p.Price);
            }

            ViewBag.keyword = keyword;
            ViewData.Model = all.ToPagedList(page, 10);
        }

        public ActionResult Index(string sort, string keyword, int page = 1)
        {
            //靜態下拉選單
            ViewBag.FilterActive = new SelectList(new List<string> {"True", "False"});

            //動態下拉選單
            //var activeOptions = repoProduct.All().Select(p => p.Active.HasValue ? p.Active.Value.ToString() : "False").Distinct().ToString();
            //ViewBag.FilterActive = new SelectList(activeOptions);
            
            //DoSearch拉至外面
            DoSearchOnIndex(sort, keyword, page);
            return View();
        }

        [HttpPost]
        public ActionResult Index(Product[] data, string sort, string keyword, int page = 1)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    var prod = repoProduct.Find(item.ProductId);
                    prod.ProductName = item.ProductName;
                    prod.Price = item.Price;
                    prod.Active = item.Active;
                    prod.Stock = item.Stock;
                }
                repoProduct.UnitOfWork.Commit();
                return RedirectToAction("Index");

            }

            //靜態下拉選單
            //ViewBag.FilterActive = new SelectList(new List<string> {"True", "False"});

            //動態下拉選單
            //var activeOptions = repoProduct.All().Select(p => p.Active.HasValue ? p.Active.Value.ToString() : "False").Distinct().ToString();
            //ViewBag.FilterActive = new SelectList(activeOptions);

            DoSearchOnIndex(sort, keyword, page);
            return View();
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            /*在Edit下檢視所有項目
            作法一:用PartialView方式,直接新增一個PartialView選好範本,類別等用部分檢視且不用Layout即可完成*/
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult ProductOrderlines(int id)
        {
            /*作法二:利用Action的方式呈現資訊*/
            Product product = repoProduct.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product.OrderLine);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repoProduct.Add(product);
                repoProduct.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(View = "Error_DbEntityValidationException", ExceptionType = typeof(DbEntityValidationException))]
        public ActionResult Edit(int id, FormCollection form)
        {
            var product = repoProduct.Find(id);
            if (TryUpdateModel(product, new string[] { "ProductName", "Stock", "Active" }))
            {
                //var db = repoProduct.UnitOfWork.Context;
                //db.Entry(product).State = EntityState.Modified;
                //repoProduct.UnitOfWork.Commit();
                //return RedirectToAction("Index");
            }

            repoProduct.UnitOfWork.Commit();
            return RedirectToAction("Index");
            //return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = repoProduct.Find(id);
            repoProduct.Delete(product);
            repoProduct.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }        
    }
}

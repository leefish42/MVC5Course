﻿using System;
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
    [OutputCache(Duration = 60, Location = OutputCacheLocation.ServerAndClient)]
    //[Authorize]
    public class ProductsController : BaseController
    {
        private void DoSearchOnIndex(string sort, string keyword, int page)
        {
            var data = repoProduct.All().AsQueryable();
            if (!String.IsNullOrEmpty(keyword))
            {
                data = data.Where(p => p.ProductName.Contains(keyword));
            }

            if (sort == "++")
            {
                data = data.OrderBy(p => p.Price);
            }
            else
            {
                data = data.OrderByDescending(p => p.Price);
            }

            ViewBag.keyword = keyword;
            ViewData.Model = data.ToPagedList(page, 10);
        }

        public ActionResult Index(string sort, string keyword, int page = 1)
        {
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
            DoSearchOnIndex(sort, keyword, page);
            return View();
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
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
            if (TryUpdateModel(product, new string[] { "ProductName", "Stock" }))
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

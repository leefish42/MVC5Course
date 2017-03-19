using MVC5Course.ActionFilters;
using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    [紀錄Action執行時間]
    public abstract class BaseController : Controller
    {
        //讓所有Controller繼承BaseController,用到Repository的直接用"repoProduct."即可
        public ProductRepository repoProduct = RepositoryHelper.GetProductRepository();

        protected override void HandleUnknownAction(string actionName)
        {
            this.Redirect("/").ExecuteResult(this.ControllerContext);
        }
    }
}
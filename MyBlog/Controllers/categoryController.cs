using MyBlog.BussinessLayer.BussniessHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class categoryController : Controller
    {
        // GET: category
        public ActionResult Index(int id ,string categoryName)
        {
                var result = UtilManager.getCategoriesInArticles(id);
                ViewData["categoryName"] = result.Message;
                return View(result.Result);
        }
    }
}
using MyBlog.BussinessLayer.BussniessHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class tagController : Controller
    {
        // GET: tag
        public ActionResult Index(int id,string tagName)
        {

            var result = UtilManager.getArcivesInArticles(id);
            ViewData["tagName"] = result.Message;
            return View(result.Result);
        }
    }
}
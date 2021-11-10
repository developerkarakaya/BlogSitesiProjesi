using MyBlog.BussinessLayer.BussniessHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class arcivesController : Controller
    {
        public ActionResult Index(string yil,string ay)
        {
            ViewData["yil"] = yil;
            ViewData["ay"] = ay;
            var result = UtilManager.getArticleInArcives(yil, ay);
            return View(result.Result);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Areas.Panel.Controllers
{
    public class HomeController : Controller
    {
        // GET: Panel/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
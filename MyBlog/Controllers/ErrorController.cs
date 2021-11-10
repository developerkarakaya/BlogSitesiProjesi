 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NotFound(string aspxerrorpath)
        {

            if (!string.IsNullOrWhiteSpace(aspxerrorpath))
            {
                return RedirectToAction("NotFound");
            }
            return View();
        }
    }
}
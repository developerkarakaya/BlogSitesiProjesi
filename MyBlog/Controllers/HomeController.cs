using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.BussinessLayer;
using MyBlog.BussinessLayer.BussniessHelper;
namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            

            var result = UtilManager.GetArticle();
            return View(result.Result);
        }


        public ActionResult hakkimda()
        {
            return View();
        }

        public ActionResult iletisim()
        {
            return View();
        }
        public PartialViewResult LatersArticles()
        {
            var result = UtilManager.getLatersArticles();
            return PartialView(result.Result);
        }

        public PartialViewResult Categories()
        {
            var result = UtilManager.getCategories();
            return PartialView(result.Result);
        }

        public PartialViewResult layoutCategories()
        {
            var result = UtilManager.getCategories();
            return PartialView(result.Result);
        }

        public PartialViewResult GetArcives()
        {
            var result = UtilManager.getArsiv();
            return PartialView(result.Result);
        }

        public PartialViewResult getYoutubeVideos()
        {
            var data = UtilManager.getYoutubeVideos();
            return PartialView(data.Result);
        }

        public PartialViewResult getTags()
        {
            var data = UtilManager.getTags();
            return PartialView(data.Result);
        }

        public PartialViewResult getCommentTop()
        {
            var result = UtilManager.getLatersComments();
            return PartialView(result.Result);
        }

        public PartialViewResult SearchPartial()
        {
            return PartialView();
        }


        public ActionResult Search(string s)
        {
            var result = UtilManager.SearchContains(s);
            return View(result.Result);

        }

    }
}
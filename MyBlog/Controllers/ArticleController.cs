using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.BussinessLayer.Manager;
using MyBlog.DataEntitiess;
using MyBlog.BussinessLayer.BussniessHelper;
using MyBlog.BussinessLayer.Manager.Model;
using MyBlog.BussinessLayer.Repository;
using System.Net.Mail;
using System.Net;
namespace MyBlog.Controllers
{
    public class ArticleController : Controller
    {

        static Repository<COMMENTS> _CommentsRepository = new Repository<COMMENTS>();

        public ActionResult Detail(string Title,int? id)
        {
            var result = UtilManager.getDetail(id);
            return View(result);
        }

        [HttpPost]
        public JsonResult postComment(COMMENTS cm)
        {

            UtilManager.postComment(cm);
            return Json(cm,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult postMail(MailModel m)
        {

        var response =  UtilManager.postMail(m);
            return Json(response,JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult getSkills()
        {
           var result = UtilManager.getSkills();
            return PartialView(result.Result);
        }

        [HttpPost]
        public JsonResult getOneArticle()
        {
            var result = UtilManager.getOneArticle();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult getAllArticle()
        {
            var result = UtilManager.GetArticle();
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult getCaregoryList(int articleId)
        {
            var result = UtilManager.getCaregoryList(articleId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult getAllCategories()
        {
            var result = UtilManager.getCategories();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
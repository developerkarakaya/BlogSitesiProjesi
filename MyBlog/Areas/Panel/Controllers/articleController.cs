using MyBlog.BussinessLayer.BussniessHelper;
using MyBlog.DataEntitiess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Areas.Panel.Controllers
{
    public class articleController : Controller
    {
        public ActionResult index()
        {
            return View();
        }

        public ActionResult detail(int? id)
        {
            var result = UtilPanelManager.detail(id);
                return View(result.Result);
        }

        [HttpPost]

        public JsonResult detailJsonData(int Id)
        {
            var result = UtilPanelManager.detail(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getArticleCategories(int Id)
        {
            var result = UtilPanelManager.articleCategories(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult categoryDeleted(int id)
        {
           
            var result = UtilPanelManager.categoryDeleted(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostArticle(ARTICLE article, string[] categoryValues, string[] tagValues)
        {
            var result = UtilPanelManager.PostArticle(article,categoryValues,tagValues);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult PostArticleImage()
        {
            ARTICLE article = new ARTICLE();

            if (Request.Files.AllKeys.Any())
            {
                var file = Request.Files["photoFile"];
                var id = Request.Form[0];
                 article.Id = Convert.ToInt32(id);
                if (file != null)
                {
                    Image img = Image.FromStream(file.InputStream);
                    string yol = "/Images/ArticleImages/" + Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);
                    Bitmap bm = new Bitmap(img);
                    bm.Save(Server.MapPath(yol));
                    article.ArticleImage= yol;
                }
                
            }
            else
            {
                var id = Request.Form[1];
                article.Id = Convert.ToInt32(id);
            }
            var result = UtilPanelManager.articleDetailImagePost(article);

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getCategories()
        {
            var result = UtilManager.getCategories();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getTags()
        {
            var result = UtilPanelManager.getTags();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getArticle()
        {
            var result = UtilPanelManager.getArticle();
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult articleDelete(int articleId)
        {
            var result = UtilPanelManager.articleDelete(articleId);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult articleActiveInPassive(int articleId)
        {
            var result = UtilPanelManager.articleActiveInPassive(articleId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}












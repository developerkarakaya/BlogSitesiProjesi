using MyBlog.BussinessLayer.Manager;
using MyBlog.BussinessLayer.Repository;
using MyBlog.DataEntitiess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BussinessLayer.BussniessHelper
{
    public static class UtilPanelManager
    {
        static Repository<ARTICLE> _articleRepo = new Repository<ARTICLE>();
        static Repository<CATEGORY> _categoryRepo = new Repository<CATEGORY>();
        static Repository<COMMENTS> _commentRepo = new Repository<COMMENTS>();
        static Repository<categoryAndArticleConn> _categoryArticleConnRepo = new Repository<categoryAndArticleConn>();
        static Repository<TAG_AND_ARTICLE_CONN> _tagArticleConnRepo = new Repository<TAG_AND_ARTICLE_CONN>();
        public static ApiResponse<List<ARTICLE>> getArticle()
        {
            ApiResponse<List<ARTICLE>> response = new ApiResponse<List<ARTICLE>>();

            try
            {

                using (var db = new MYBLOGDBEntities())
                {
                    var result = db.ARTICLE.OrderByDescending(ss => ss.ArticleDate).ToList();
                    response.Message = Constant.SuccessMessage;
                    response.IsSucceed = true;
                    response.Result = result;
                    return response;
                }



            }
            catch (Exception)
            {
                response.Message = Constant.ErrorMessage;
                response.IsSucceed = false;
                response.Result = null;
                return response;
            }
        }




        public static ApiResponse<ARTICLE> PostArticle(ARTICLE article, string[] categoryValues, string[] tagValues)
        {
            ApiResponse<ARTICLE> response = new ApiResponse<ARTICLE>();
            try
            {
                if (article.Id > 0)
                {
                    using (var db = new MYBLOGDBEntities())
                    {
                        var categoryArticleConnList = _categoryArticleConnRepo.getAll().Where(ss => ss.ArticleId == article.Id).ToList();
                        var tagArticleConnList = _tagArticleConnRepo.getAll().Where(ss => ss.ArticleId == article.Id).ToList();
                        if (categoryValues != null)
                        {
                            foreach (var conn in categoryArticleConnList)
                            {
                                _categoryArticleConnRepo.Delete(conn);
                            }

                            for (int i = 0; i < categoryValues.Length; i++)
                            {
                                db.categoryAndArticleConn.Add(new categoryAndArticleConn { ArticleId = article.Id, CategoryId = Convert.ToInt32(categoryValues[i]) });
                            }

                        }
                         if (tagValues !=null)
                        {
                            foreach (var connTag in tagArticleConnList)
                            {
                                _tagArticleConnRepo.Delete(connTag);
                            }
                            for (int i = 0; i < tagValues.Length; i++)
                            {
                                db.TAG_AND_ARTICLE_CONN.Add(new TAG_AND_ARTICLE_CONN { ArticleId = article.Id, TagId = Convert.ToInt32(tagValues[i]) });
                            }
                         
                        }
                        db.SaveChanges();
                        var updateArticle = db.ARTICLE.Where(ss => ss.Id == article.Id).FirstOrDefault();
                        updateArticle.ArticleViews = 10;
                        updateArticle.Author = article.Author;
                        updateArticle.Content = article.Content;
                        updateArticle.Is_Active = article.Is_Active;
                        updateArticle.Title = article.Title;
                        db.SaveChanges();
                        response.Message = "Makale güncelleme işlemi başarılı !";
                        response.Result = updateArticle;
                    }
                }
                else
                {
                    using (var db = new MYBLOGDBEntities())
                    {
                        ARTICLE ARTICLE = new ARTICLE
                        {
                            ArticleDate = article.ArticleDate,
                            ArticleViews = 10,
                            Author = article.Author,
                            Content = article.Content,
                            Is_Active = article.Is_Active,
                            Title = article.Title,
                        };
                        db.ARTICLE.Add(ARTICLE);
                        db.SaveChanges();

                        for (int i = 0; i < categoryValues.Length; i++)
                        {
                            db.categoryAndArticleConn.Add(new categoryAndArticleConn { ArticleId = ARTICLE.Id, CategoryId = Convert.ToInt32(categoryValues[i]) });
                        }
                        for (int i = 0; i < tagValues.Length; i++)
                        {
                            db.TAG_AND_ARTICLE_CONN.Add(new TAG_AND_ARTICLE_CONN { ArticleId = ARTICLE.Id, TagId = Convert.ToInt32(tagValues[i]) });
                        }
                        db.SaveChanges();

                        response.Message = "Makale ekleme işlemi başarılı !";
                        response.IsSucceed = true;
                        response.Result = ARTICLE;
                        return response;
                    }
                }
                response.IsSucceed = true;
                return response;
            }
            catch (Exception)
            {

                response.Message = "Makale ekleme/güncelleme işlemi başarısız !";
                response.IsSucceed = false;
                response.Result = null;
                return response;
            }

        }


        

        public static ApiResponse<ARTICLE> articleDetailImagePost(ARTICLE article)
        {
            ApiResponse<ARTICLE> response = new ApiResponse<ARTICLE>();
            try
            {
                using (var db = new MYBLOGDBEntities())
                {
                    var updateDetail = db.ARTICLE.Where(ss => ss.Id == article.Id).FirstOrDefault();
                    if (!string.IsNullOrEmpty(article.ArticleImage))
                    {
                        updateDetail.ArticleImage = article.ArticleImage;
                    }
                    else
                    {
                        updateDetail.ArticleImage = db.ARTICLE.Where(ss => ss.Id == article.Id).FirstOrDefault().ArticleImage;
                    }

                    db.SaveChanges();
                    response.Message = Constant.SuccessMessage;
                    response.IsSucceed = true;
                    response.Result = updateDetail;
                    return response;
                }

            }
            catch (Exception)
            {

                response.Message = Constant.ErrorMessage;
                response.IsSucceed = false;
                response.Result = null;
                return response;
            }
        }




        public static ApiResponse<int> categoryDeleted(int categoryId)
        {
            ApiResponse<int> response = new ApiResponse<int>();


            try
            {
                using (var db = new MYBLOGDBEntities())
                {
                    var deletedCategory = db.CATEGORY.FirstOrDefault(ss => ss.Id == categoryId);
                    if(deletedCategory !=null)
                    {
                        db.CATEGORY.Remove(deletedCategory);
                        db.SaveChanges();
                        
                    }

                }
                response.IsSucceed = true;
                response.Message = "işlem başarılı";
                return response;
            }
            catch (Exception)
            {

                response.IsSucceed = false;
                response.Message = "işlem başarısız";
                return response;
            }



        }


        public static ApiResponse<int> articleDelete(int articleId)
        {
            ApiResponse<int> response = new ApiResponse<int>();
            try
            {
                _articleRepo.Delete(articleId);
                var commentList = _commentRepo.getAll().Where(ss => ss.MAKALEID == articleId).ToList();
                var categoryArticleConnList = _categoryArticleConnRepo.getAll().Where(ss => ss.ArticleId == articleId).ToList();
                var tagArticleConnList = _tagArticleConnRepo.getAll().Where(ss => ss.ArticleId == articleId).ToList();
                foreach (var connTag in tagArticleConnList)
                {
                    _tagArticleConnRepo.Delete(connTag);
                }
                foreach (var conn in categoryArticleConnList)
                {
                    _categoryArticleConnRepo.Delete(conn);
                }
                foreach (var item in commentList)
                {
                    _commentRepo.Delete(item);
                }
                response.Message = Constant.SuccessMessage;
                response.IsSucceed = true;
                return response;
            }
            catch (Exception)
            {
                response.Message = Constant.ErrorMessage;
                response.IsSucceed = false;
                return response;
            }
        }
        public static ApiResponse<int> articleActiveInPassive(int articleId)
        {
            ApiResponse<int> response = new ApiResponse<int>();
            try
            {
                using (var db = new MYBLOGDBEntities())
                {
                    var updateArticle = db.ARTICLE.FirstOrDefault(ss => ss.Id == articleId);
                    if (updateArticle.Is_Active == false)
                    {
                        updateArticle.Is_Active = true;
                    }
                    else
                    {
                        updateArticle.Is_Active = false;
                    }
                    db.SaveChanges();
                    response.Message = updateArticle.Is_Active.ToString();
                    response.IsSucceed = true;
                    return response;
                }

            }
            catch (Exception)
            {
                response.Message = Constant.ErrorMessage;
                response.IsSucceed = false;
                return response;
            }
        }



        public static ApiResponse<ARTICLE> detail(int? id)
        {
            ApiResponse<ARTICLE> response = new ApiResponse<ARTICLE>();
            try
            {
                if (id == null || id == 0)
                {
                    response.Result = new ARTICLE { Id = 0 };
                    return response;
                }

                using (var db = new MYBLOGDBEntities())
                {
                    var result = db.ARTICLE.FirstOrDefault(ss => ss.Id == id);
                    response.Message = Constant.SuccessMessage;
                    response.IsSucceed = true;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception)
            {
                response.Message = Constant.ErrorMessage;
                response.IsSucceed = false;
                response.Result = null;
                return response;
            }
        }


        public static ApiResponse<List<TAGS>> getTags()
        {
            ApiResponse<List<TAGS>> response = new ApiResponse<List<TAGS>>();
            try
            {
                using (var db = new MYBLOGDBEntities())
                {
                    var result = db.TAGS.OrderByDescending(ss => ss.Id).ToList();
                    response.Result = result;
                    response.Message = Constant.SuccessMessage;
                    response.IsSucceed = true;
                    return response;
                }
            }
            catch (Exception)
            {

                response.Result = null;
                response.Message = Constant.ErrorMessage;
                response.IsSucceed = false;
                return response;
            }
        }





        public static ApiResponse<List<CATEGORY>> articleCategories(int Id)
        {
            ApiResponse<List<CATEGORY>> response = new ApiResponse<List<CATEGORY>>();
            List<categoryAndArticleConn> liste = new List<categoryAndArticleConn>();
            List<CATEGORY> categoryList = new List<CATEGORY>();
            try
            {
                using (var db = new MYBLOGDBEntities())
                {
                    foreach (var item in db.categoryAndArticleConn.Where(ss => ss.ArticleId == Id).ToList())
                    {
                        liste.Add(item);
                    }
                    foreach (var item in liste)
                    {
                        categoryList.Add(db.CATEGORY.Where(ss => ss.Id == item.CategoryId).FirstOrDefault());
                    }
                }
                response.Result = categoryList;
                response.Message = Constant.SuccessMessage;
                response.IsSucceed = true;
                return response;

            }
            catch (Exception)
            {
                response.Result = null;
                response.Message = Constant.ErrorMessage;
                response.IsSucceed = false;
                return response;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog.DataEntitiess;
using MyBlog.BussinessLayer.Manager;
using MyBlog.BussinessLayer.Repository;
using MyBlog.BussinessLayer.Manager.Model;
using System.Net.Mail;
using System.Net;
namespace MyBlog.BussinessLayer.BussniessHelper
{
    public static class UtilManager
    {
        static Repository<ARTICLE> _ArticleRepository = new Repository<ARTICLE>();
        static Repository<CATEGORY> _CategoryRepository = new Repository<CATEGORY>();
        static Repository<COMMENTS> _CommentsRepository = new Repository<COMMENTS>();
        private static List<ARTICLE> result;
        public static ApiResponse<List<ArticleSeoModel>> GetArticle()
        {
            ApiResponse<List<ArticleSeoModel>> response = new ApiResponse<List<ArticleSeoModel>>();
            using (var db = new MYBLOGDBEntities())
            { 
                var data = db.ARTICLE.OrderByDescending(ss => ss.ArticleDate).Where(ss => ss.Is_Active == true).Skip(1).ToList();
                List<ArticleSeoModel> liste = new List<ArticleSeoModel>();
               
                foreach (var item in data)
                {
                    
                        
                    
                    liste.Add(new ArticleSeoModel
                    {
                        ArticleDate = item.ArticleDate,
                        ArticleImage = item.ArticleImage,
                        ArticleViews = item.ArticleViews,
                        Author = item.Author,
                        Content = item.Content,
                        Id = item.Id,
                        Is_Active = item.Is_Active,
                        Title = item.Title,
                        SeoLink = MyBlog.BussinessLayer.Manager.SeoUrlExtension.FriendlyUrl(item.Title),
                        categoryList= getCategoriesArticleId(item.Id),
                        commentCount = getCommentArticleCount(item.Id)
                    });
                }
                response.IsSucceed = true;
                response.Message = Constant.SuccessMessage;
                response.Result = liste;
                return response;
            }
        }

        public static string getCommentArticleCount(int articleId)
        {
            using (var db = new MYBLOGDBEntities())
            {
                var commentCount = db.COMMENTS.Where(ss => ss.MAKALEID == articleId && ss.Is_Approve==true).ToList().Count;
                return commentCount.ToString();
            }
        }




        public static List<CategorySeoModel> getCategoriesArticleId(int articleId)
        {
            List<CATEGORY> categoryListe = new List<CATEGORY>();
            List<CategorySeoModel> categoryListeSeo = new List<CategorySeoModel>();
            List<categoryAndArticleConn> categoryIdList = new List<categoryAndArticleConn>();
            using (var db = new MYBLOGDBEntities())
            {
                var result = db.categoryAndArticleConn.Where(ss => ss.ArticleId == articleId).ToList();

                foreach (var item2 in result)
                {
                    categoryIdList.Add(item2);
                }

                foreach (var item3 in categoryIdList)
                {
                    categoryListe.Add(db.CATEGORY.FirstOrDefault(ss => ss.Id == item3.CategoryId));
                }
                //foreach (var item4 in categoryListe)
                //{
                //    categoryListeSeo.Add(new CategorySeoModel
                //    {
                //        Id = item4.Id,
                //        CategoryName = item4.CategoryName,
                //        CategorySeoLink = MyBlog.BussinessLayer.Manager.SeoUrlExtension.FriendlyUrl(item4.CategoryName)
                //    });

                //}
                return categoryListeSeo;
            }
        }

        public static ArticleSeoModel getOneArticle()
        {
            ApiResponse<ArticleSeoModel> response = new ApiResponse<ArticleSeoModel>();
            using (var db = new MYBLOGDBEntities())
            {
                var data = db.ARTICLE.OrderByDescending(ss => ss.ArticleDate).Where(ss => ss.Is_Active == true).Take(1).FirstOrDefault();
                ArticleSeoModel dataSeo = new ArticleSeoModel();
                dataSeo.Id = data.Id;
                dataSeo.Is_Active = data.Is_Active;
                dataSeo.Title = data.Title;
                dataSeo.SeoLink = MyBlog.BussinessLayer.Manager.SeoUrlExtension.FriendlyUrl(data.Title);
                dataSeo.Content = data.Content;
                dataSeo.Author = data.Author;
                dataSeo.ArticleViews = data.ArticleViews;
                dataSeo.ArticleImage = data.ArticleImage;
                dataSeo.ArticleDate = data.ArticleDate;
                dataSeo.categoryList = getCategoriesArticleId(data.Id);
                dataSeo.commentCount = getCommentArticleCount(data.Id);
                response.Result = dataSeo;
                response.Message = Constant.SuccessMessage;
                response.IsSucceed = true;
                return response.Result;
            }
        }

        public static ApiResponse<List<ARTICLE>> getLatersArticles()
        {
            ApiResponse<List<ARTICLE>> response = new ApiResponse<List<ARTICLE>>();

            try
            {
                using (var db = new MYBLOGDBEntities())
                {
                    var result = db.ARTICLE.OrderByDescending(ss => ss.ArticleDate).Where(ss => ss.Is_Active == true).Take(3).ToList();
                    response.IsSucceed = true;
                    response.Message = Constant.SuccessMessage;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception)
            {
                response.IsSucceed = false;
                response.Message = Constant.ErrorMessage;
                response.Result = null;
                return response;
            }
        }
        public static ApiResponse<List<CATEGORY>> getCategories()
        {
            ApiResponse<List<CATEGORY>> response = new ApiResponse<List<CATEGORY>>();
            try
            {
                using (var db = new MYBLOGDBEntities())
                {
                    var result = db.CATEGORY.OrderBy(ss => ss.Id).ToList();
                    response.IsSucceed = true;
                    response.Message = Constant.SuccessMessage;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception)
            {
                response.IsSucceed = false;
                response.Message = Constant.ErrorMessage;
                response.Result = null;
                return response;
            }


        }






        public static ApiResponse<List<ARTICLE>> getCategoriesInArticles(int categoryId)
        {
            ApiResponse<List<ARTICLE>> response = new ApiResponse<List<ARTICLE>>();
            List<categoryAndArticleConn> articleList;
            List<ARTICLE> liste = new List<ARTICLE>();
            try
            {
                using (var db = new MYBLOGDBEntities())
                {
                    articleList = db.categoryAndArticleConn.Where(ss => ss.CategoryId == categoryId).ToList();
                    foreach (var item in articleList)
                    {
                        liste.Add(db.ARTICLE.Where(ss => ss.Id == item.ArticleId).FirstOrDefault());
                    }

                    response.Result = liste.OrderByDescending(ss => ss.ArticleDate).ToList();
                    response.Message = db.CATEGORY.FirstOrDefault(ss => ss.Id == categoryId).CategoryName;
                    response.IsSucceed = true;
                    return response;

                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static ApiResponse<List<ARTICLE>> getArcivesInArticles(int tagId)
        {
            ApiResponse<List<ARTICLE>> response = new ApiResponse<List<ARTICLE>>();
            List<TAG_AND_ARTICLE_CONN> articleList;
            List<ARTICLE> liste = new List<ARTICLE>();
            try
            {
                using (var db = new MYBLOGDBEntities())
                {
                    articleList = db.TAG_AND_ARTICLE_CONN.Where(ss => ss.TagId == tagId).ToList();
                    foreach (var item in articleList)
                    {
                        liste.Add(db.ARTICLE.Where(ss => ss.Id == item.ArticleId).FirstOrDefault());
                    }

                    response.Result = liste.OrderByDescending(ss => ss.ArticleDate).ToList();
                    response.Message = db.TAGS.FirstOrDefault(ss => ss.Id == tagId).TagName;
                    response.IsSucceed = true;
                    return response;

                }
            }
            catch (Exception)
            {
                return null;
            }
        }



        public static ApiResponse<List<arsiv1_Result>> getArsiv()
        {
            ApiResponse<List<arsiv1_Result>> response = new ApiResponse<List<arsiv1_Result>>();
            try
            {
                using (var db = new MYBLOGDBEntities())
                {

                    var result = db.Database.SqlQuery<arsiv1_Result>("EXEC arsiv").ToList();
                    response.IsSucceed = true;
                    response.Message = Constant.SuccessMessage;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception)
            {
                response.IsSucceed = true;
                response.Message = Constant.ErrorMessage;
                response.Result = null;
                return response;
            }
        }


        public static ApiResponse<List<ARTICLE>> getArticleInArcives(string yil, string ay)
        {
            ApiResponse<List<ARTICLE>> response = new ApiResponse<List<ARTICLE>>();
            try
            {
                using (var db = new MYBLOGDBEntities())
                {
                    int ayInt = MyBlog.BussinessLayer.Manager.ManagerClasses.MonthTurkceInt(ay);
                    int yilInt = Convert.ToInt32(yil);
                    var result = db.ARTICLE.Where(ss => ss.ArticleDate.Year == yilInt && ss.ArticleDate.Month == ayInt).ToList();
                    response.Message = Constant.SuccessMessage;
                    response.IsSucceed = true;
                    response.Result = result;
                    return response;
                }

            }
            catch (Exception)
            {
                response.IsSucceed = false;
                response.Message = Constant.ErrorMessage;
                response.Result = null;
                return response;

            }
        }




        public static ApiResponse<List<YOUTUBEVIDEOS>> getYoutubeVideos()
        {
            ApiResponse<List<YOUTUBEVIDEOS>> response = new ApiResponse<List<YOUTUBEVIDEOS>>();
            try
            {

                using (var db = new MYBLOGDBEntities())
                {
                    var result = db.YOUTUBEVIDEOS.ToList();
                    response.IsSucceed = true;
                    response.Message = Constant.SuccessMessage;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception)
            {
                response.Result = null;
                response.Message = Constant.ErrorMessage;
                response.IsSucceed = false;
                return response;
                throw;
            }
        }


        public static ApiResponse<List<TAGS>> getTags()
        {
            ApiResponse<List<TAGS>> response = new ApiResponse<List<TAGS>>();
            try
            {
                using (var db = new MYBLOGDBEntities())
                {
                    var data = db.TAGS.ToList();
                    response.Result = data;
                    response.Message = Constant.SuccessMessage;
                    response.IsSucceed = true;
                    return response;
                }

            }
            catch (Exception)
            {
                response.Message = Constant.ErrorMessage;
                response.IsSucceed = false;
                response.Result = null;
                return response;
                throw;
            }
        }



        public static ArticleDetailModel getDetail(int? Id)
        {
            ApiResponse<ARTICLE> response = new ApiResponse<ARTICLE>();
            try
            {
                using (var db = new MYBLOGDBEntities())
                {
                    ArticleDetailModel vm = new ArticleDetailModel();
                    vm.ArticleTags = db.TAG_AND_ARTICLE_CONN.Where(ss => ss.ArticleId == Id).ToList();
                    vm.Detail = db.ARTICLE.FirstOrDefault(ss => ss.Id == Id);
                    vm.NextArticle = db.ARTICLE.FirstOrDefault(ss => ss.Id > Id);
                    vm.PrivArticle = db.ARTICLE.OrderByDescending(ss => ss.Id).FirstOrDefault(ss => ss.Id < Id);
                    vm.RandomArticles = db.ARTICLE.ToList().Where(ss => ss.Id != vm.Detail.Id).OrderBy(ss => Guid.NewGuid()).Take(3).ToList();// aynı makale geliyor tekrar bak.
                    vm.getComments = db.COMMENTS.Where(ss => ss.Is_Approve == true && ss.MAKALEID == Id).ToList();
                    response.Message = Constant.SuccessMessage;
                    response.IsSucceed = true;
                    return vm;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static ApiResponse<MailModel> postMail(MailModel m)
        {
            ApiResponse<MailModel> response = new ApiResponse<MailModel>();
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Burası aynı kalacak
                client.Credentials = new NetworkCredential("sametkarakaya14@gmail.com", "12qwaszx12qwaszxs");
                client.EnableSsl = true;
                MailMessage msj = new MailMessage();//Yeni bir MailMesajı oluşturuyoruz
                msj.From = new MailAddress(m.email, m.isim); //iletişim kısmında girilecek mail buaraya gelecektir
                msj.To.Add("sametkarakaya14@gmail.com"); //Buraya kendi mail adresimizi yazıyoruz
                msj.Subject = m.konu; //Buraya iletişim sayfasında gelecek konu ve mail adresini mail içeriğine yazacaktır
                msj.Body = m.mesaj + "     Kimden : " + m.email; //Mail içeriği burada aktarılacakır
                client.Send(msj); //Clien sent kısmında gönderme işlemi gerçeklecektir.
                MailMessage msj1 = new MailMessage();
                msj1.From = new MailAddress("sametkarakaya14@gmail.com", "Samet Mirza Karakaya");
                msj1.To.Add(m.email); //Buraua iletişim sayfasında gelecek mail adresi gelecktir.
                msj1.Subject = "Mail'inize Cevap";  
                msj1.Body = "Size En kısa zamanda Döneceğiz. Teşekkür Ederiz Bizi tercih ettiğiniz için -Samet Mirza Karakaya";
                client.Send(msj1);
                response.IsSucceed = true;
                response.Message = "Teşekkürler Mailniz başarı bir şekilde gönderildi";
                response.Result = null;
                return response;
            }
            catch (Exception)
            {
                response.IsSucceed = false;
                response.Message = "Mesaj gönderilirken hata olıuştu";
                response.Result = null;
                return response;
            }
        }
        public static ApiResponse<List<SKILLS>> getSkills()
        {
            ApiResponse<List<SKILLS>> response = new ApiResponse<List<SKILLS>>();
            try
            {
                using (var db = new MYBLOGDBEntities())
                {
                    var result = db.SKILLS.OrderByDescending(ss => ss.SKILLYUZDE).ToList();
                    response.Result = result;
                    response.Message = Constant.SuccessMessage;
                    response.IsSucceed = true;
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



        public static void postComment(COMMENTS cm)
        {
            try
            {
                using (var db = new MYBLOGDBEntities())
                {
                    cm.CREATEDATE = DateTime.Now;
                    cm.Is_Approve = false;
                    if (cm.USTID == null)
                    {
                        cm.USTID = 0;
                    }
                    _CommentsRepository.Create(cm);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static ApiResponse<List<COMMENTS>> getLatersComments()
        {
            ApiResponse<List<COMMENTS>> respnonse = new ApiResponse<List<COMMENTS>>();
            using (var db = new MYBLOGDBEntities())
            {
                var result = db.COMMENTS.OrderByDescending(ss => ss.CREATEDATE).Where(ss => ss.Is_Approve == true).Take(5).ToList();
                respnonse.Message = Constant.SuccessMessage;
                respnonse.IsSucceed = true;
                respnonse.Result = result;
                return respnonse;
            }
        }



        public static ApiResponse<List<ARTICLE>> SearchContains(string s)
        {
            ApiResponse<List<ARTICLE>> response = new ApiResponse<List<ARTICLE>>();
            try
            {
                using (var db = new MYBLOGDBEntities())
                {
                    var result = db.ARTICLE.Where(ss => ss.Content.Contains(s) || ss.Title.Contains(s)).ToList();
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

        public static ApiResponse<List<CATEGORY>> getCaregoryList(int articleId)
        {
            ApiResponse<List<CATEGORY>> response = new ApiResponse<List<CATEGORY>>();
            List<categoryAndArticleConn> categoryIdList = new List<categoryAndArticleConn>();
            List<CATEGORY> categoryList= new List<CATEGORY>();
            try
            {
                using(var db = new MYBLOGDBEntities())
                {
                    var result = db.categoryAndArticleConn.Where(ss => ss.ArticleId == articleId).ToList();
                    
                    foreach (var item in result)
                    {
                        categoryIdList.Add(item);
                    }
                    
                    foreach (var item in categoryIdList)
                    {
                        categoryList.Add(db.CATEGORY.FirstOrDefault(ss => ss.Id == item.CategoryId));
                    }

                    response.Message = Constant.SuccessMessage;
                    response.Result = categoryList;
                    response.IsSucceed = true;
                    return response;
                }
            }
            catch (Exception)
            {
                response.Message = Constant.ErrorMessage;
                response.Result = null;
                response.IsSucceed = false;
                return response;
            }
        }
    }
}


























using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog.DataEntitiess;
namespace MyBlog.BussinessLayer.Manager
{
   public class ArticleDetailModel
    {

        public ARTICLE Detail { get; set; }
        public ARTICLE NextArticle { get; set; }
        public ARTICLE PrivArticle { get; set; }
        public List<TAG_AND_ARTICLE_CONN> ArticleTags { get; set; }

        public List<ARTICLE> RandomArticles { get; set; }

        public List<COMMENTS> getComments { get; set; }
    }
}

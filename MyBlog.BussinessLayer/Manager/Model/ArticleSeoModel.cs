using MyBlog.DataEntitiess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BussinessLayer.Manager.Model
{
    public partial class ArticleSeoModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public System.DateTime ArticleDate { get; set; }
        public string Author { get; set; }
        public string ArticleImage { get; set; }
        public Nullable<int> ArticleViews { get; set; }
        public bool Is_Active { get; set; }
        public string SeoLink { get; set; }
        public List<CategorySeoModel> categoryList { get; set; }

        public string commentCount { get; set; }
    }
}

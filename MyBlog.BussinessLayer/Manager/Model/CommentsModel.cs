using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BussinessLayer.Manager.Model
{

    [Serializable]
    public  class CommentsModel
    {
        public int ID { get; set; }
        public string Comment { get; set; }
        public string CommentByName { get; set; }
        public string EPOSTA { get; set; }
        public string WEBSITE { get; set; }
        public System.DateTime CREATEDATE { get; set; }
        public Nullable<int> USTID { get; set; }
        public Nullable<int> MAKALEID { get; set; }
    }
}

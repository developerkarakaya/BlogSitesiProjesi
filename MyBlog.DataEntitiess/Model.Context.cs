//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyBlog.DataEntitiess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MYBLOGDBEntities : DbContext
    {
        public MYBLOGDBEntities()
            : base("name=MYBLOGDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ARTICLE> ARTICLE { get; set; }
        public virtual DbSet<CATEGORY> CATEGORY { get; set; }
        public virtual DbSet<categoryAndArticleConn> categoryAndArticleConn { get; set; }
        public virtual DbSet<Months> Months { get; set; }
        public virtual DbSet<YOUTUBEVIDEOS> YOUTUBEVIDEOS { get; set; }
        public virtual DbSet<TAG_AND_ARTICLE_CONN> TAG_AND_ARTICLE_CONN { get; set; }
        public virtual DbSet<TAGS> TAGS { get; set; }
        public virtual DbSet<COMMENTS> COMMENTS { get; set; }
        public virtual DbSet<SKILLS> SKILLS { get; set; }
    
        public virtual ObjectResult<arsiv_Result> arsiv()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<arsiv_Result>("arsiv");
        }
    
        public virtual ObjectResult<arsiv1_Result> arsiv1()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<arsiv1_Result>("arsiv1");
        }
    }
}

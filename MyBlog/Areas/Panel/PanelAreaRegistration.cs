using System.Web.Mvc;

namespace MyBlog.Areas.Panel
{
    public class PanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Panel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Panel_default",
                "Panel/{controller}/{action}/{id}",
                new {controller="Home", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Panel_default_Detail",
                "Panel/{controller}/{action}/{id}",
                new { controller = "article", action = "detail", id = UrlParameter.Optional }
            );
        }
    }
}
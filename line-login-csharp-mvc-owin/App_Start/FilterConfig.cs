using System.Web;
using System.Web.Mvc;

namespace line_login_csharp_mvc_owin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

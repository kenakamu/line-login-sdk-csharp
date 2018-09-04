using Line.Login;
using Line.Login.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace line_login_csharp_web.Controllers
{
    public class HomeController : Controller
    {
        private string channelId = ConfigurationManager.AppSettings["ChannelId"];
        private string channelSecret = ConfigurationManager.AppSettings["ChannelSecret"];
        private string redirectUri = ConfigurationManager.AppSettings["RedirectUri"];
        LineLoginClient client;

        public HomeController()
        {
            client = new LineLoginClient(channelId, channelSecret, redirectUri, "test", Scope.Profile | Scope.OpenId);
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Login()
        {
            var authUri = client.GetAuthUri();
            return Redirect(authUri);
        }

        public async Task<ActionResult> Auth(object auth)
        {
            var code = Request.QueryString["code"];
            var token = await client.GetToken(code);
            return View("Index", token);
        }
    }
}
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(line_login_csharp_mvc_owin.Startup))]
namespace line_login_csharp_mvc_owin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InternetCoast.Web.Startup))]
namespace InternetCoast.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

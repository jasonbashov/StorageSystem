using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AAS.Web.Startup))]
namespace AAS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SalesWebApplication.Startup))]
namespace SalesWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

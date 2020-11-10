using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SinanDolaymanAdmin.Startup))]
namespace SinanDolaymanAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

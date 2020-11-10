using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SinanDolayman.Startup))]
namespace SinanDolayman
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

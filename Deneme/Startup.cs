using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Deneme.Startup))]
namespace Deneme
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

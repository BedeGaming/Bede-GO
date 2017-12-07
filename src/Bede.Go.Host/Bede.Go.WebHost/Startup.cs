using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bede.Go.WebHost.Startup))]
namespace Bede.Go.WebHost
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

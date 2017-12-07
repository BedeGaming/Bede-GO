using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Bede.Go.Host.Startup))]

namespace Bede.Go.Host
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

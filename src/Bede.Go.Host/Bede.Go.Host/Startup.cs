using System.Configuration;
using System.Reflection;
using DbUp;
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

            //var connectionString = ConfigurationManager.AppSettings["BedeGoConnectionString"];
            //var upgrader = DeployChanges.To
            //    .SqlDatabase(connectionString)
            //    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            //    .Build();
            //EnsureDatabase.For.SqlDatabase(connectionString);
            //upgrader.PerformUpgrade();
        }
    }
}

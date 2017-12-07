using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Bede.Go.Host;
using Bede.Go.Host.Autofac;

namespace Bede.GO.Host
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Configure);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configure(AutofacConfig.Configure);
        }
    }
}

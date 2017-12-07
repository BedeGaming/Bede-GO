using System.Web;
using System.Web.Http;

namespace Bede.GO.Host.Mac
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}

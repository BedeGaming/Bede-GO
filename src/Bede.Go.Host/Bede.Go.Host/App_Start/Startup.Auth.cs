using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Owin;
using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Cors;

namespace Bede.Go.Host
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                MetadataAddress = "https://accounts.google.com/.well-known/openid-configuration",
                ClientId = "777354584083-c48m9l02onp5ropfcuiri78j4jgrmkqo.apps.googleusercontent.com",
                ClientSecret = "rKf9eK5EzJIo6x-JbHteOi0u",
                PostLogoutRedirectUri = ConfigurationManager.AppSettings["RedirectUri"],
                RedirectUri = ConfigurationManager.AppSettings["RedirectUri"],
                Notifications = new OpenIdConnectAuthenticationNotifications
                {

                }
            });
        }
    }
}
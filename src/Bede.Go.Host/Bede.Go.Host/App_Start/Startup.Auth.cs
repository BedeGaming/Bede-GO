using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;

namespace Bede.Go.Host
{
    public partial class Startup
    {


        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            //app.UseOAuthBearerAuthentication(new OAuthAuthorizationServerOptions
            //{
            //    TokenEndpointPath = new PathString("/Token"),
            //    Provider = new ApplicationOAuthProvider(PublicClientId),
            //    AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
            //    // In production mode set AllowInsecureHttp = false
            //    AllowInsecureHttp = true
            //});

            //app.UseGoogleAuthentication(
            //    clientId: "720840102258-sie63m7pprdc680f897hsdiqf2apqmk9.apps.googleusercontent.com",
            //    clientSecret: "fAPRO7cQlkEBcewgaMZJEApl");

            //app.SetDefaultSignInAsAuthenticationType("Google");
        }
    }
}
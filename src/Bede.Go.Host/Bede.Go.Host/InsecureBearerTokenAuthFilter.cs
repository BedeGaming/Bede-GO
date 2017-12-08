using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Net.Http;
using System.Security.Claims;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace Bede.Go.Host
{
    internal class InsecureBearerTokenAuthFilter : IAuthenticationFilter
    {
        private readonly string _authenticationType;

        public InsecureBearerTokenAuthFilter(string authenticationType)
        {
            if (authenticationType == null)
            {
                throw new ArgumentNullException("authenticationType");
            }

            _authenticationType = authenticationType;
        }

        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            try
            {
                var token = context.Request.Headers.Authorization.Parameter;
                var tokenSegments = token.Split('.');
                var payload = tokenSegments[1];
                var decodedBytes = Convert.FromBase64String(payload);
                var decoded = Encoding.UTF8.GetString(decodedBytes);

                ClaimsIdentity identity = new ClaimsIdentity("Bearer");

                var tokenClaims = (dynamic) JsonConvert.DeserializeObject(decoded);

                identity.AddClaims(new List<Claim> 
                {
                    // Hack, have to cast the values as they are on a dynamic obj and confuse the runtime
                    new Claim("name", (string) tokenClaims.name),
                    new Claim("email", (string) tokenClaims.email),
                    new Claim(ClaimTypes.Name, (string) tokenClaims.name),
                    new Claim("sub", (string) tokenClaims.sub.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, (string) tokenClaims.sub.ToString())
                 });

                context.Principal = new ClaimsPrincipal(identity);
                Thread.CurrentPrincipal = context.Principal;
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = context.Principal;
                }
            }
            catch
            {
                context.Principal = null;
            }
        }

        private static IAuthenticationManager GetAuthenticationManagerOrThrow(HttpRequestMessage request)
        {
            var owinCtx = request.GetOwinContext();
            IAuthenticationManager authenticationManager = owinCtx != null ? owinCtx.Authentication : null;

            if (authenticationManager == null)
            {
                throw new InvalidOperationException("IAuthenticationManagerNotAvailable");
            }

            return authenticationManager;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            if(context.ActionContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return Task.CompletedTask;
            }

            throw new NotImplementedException("Challenge called.");
        }
    }
}
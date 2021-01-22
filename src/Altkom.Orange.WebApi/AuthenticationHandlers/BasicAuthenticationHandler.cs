using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;
using Altkom.Orange.IServices;
using Altkom.Orange.Models;
using System.Security.Claims;

namespace Altkom.Orange.WebApi.AuthenticationHandlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string authorizationKey = "Authorization";

        private readonly IAuthorizationService authorizationService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock, IAuthorizationService authorizationService) : base(options, logger, encoder, clock)
        {
            this.authorizationService = authorizationService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(authorizationKey))
            {
                return AuthenticateResult.Fail("Missing authorization header");
            }

            // using System.Net.Http.Headers
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers[authorizationKey]);
            
            if (authHeader.Scheme!="Basic")
            {
                return AuthenticateResult.Fail("Basic scheme authorization requried");
            }

            byte[] credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split(":");

            string username = credentials[0];
            string password = credentials[1];

            if (!authorizationService.TryAuthorize(username, password, out Customer customer))
            {
                return AuthenticateResult.Fail("Username or password is invalid");
            }

            ClaimsIdentity identity = new ClaimsIdentity(Scheme.Name);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            identity.AddClaim(new Claim(ClaimTypes.Name, customer.FullName));

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}

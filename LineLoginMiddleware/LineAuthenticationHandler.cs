using Line.Login;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Owin.Security.Middleware.Line
{
    /// <summary>
    /// LINE Login Authentication Handler
    /// https://msdn.microsoft.com/en-us/library/dn253815(v=vs.113).aspx
    /// </summary>
    public class LineAuthenticationHandler : AuthenticationHandler<LineAuthenticationOptions>
    {
        private const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";

        private readonly ILogger _logger;
        private LineLoginClient loginClient;

        /// <summary>
        /// Constructor
        /// </summary>
        public LineAuthenticationHandler(LineLoginClient loginClient, ILogger logger)
        {
            _logger = logger;
            this.loginClient = loginClient;
        }

        /// <summary>
        /// Get Token from LINE Login once user logged in.
        /// </summary>
        /// <returns>AuthenticationTicket</returns>
        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            AuthenticationProperties properties = null;

            try
            {
                // Get Query String
                string code = null;
                string state = null;

                var query = Request.Query;
                var values = query.GetValues("code");
                if (values != null && values.Count == 1)
                {
                    code = values[0];
                }
                values = query.GetValues("state");
                if (values != null && values.Count == 1)
                {
                    state = values[0];
                }

                // Check state to validate response
                properties = Options.StateDataFormat.Unprotect(state);
                if (properties == null)
                {
                    return null;
                }

                if (!ValidateCorrelationId(properties, _logger))
                {
                    return new AuthenticationTicket(null, properties);
                }
                
                // GetToken from LINE Login
                var token = await loginClient.GetToken(code);
                
                // Create context from obtained result.
                var context = new LineAuthenticatedContext(Context, token)
                {
                    Identity = new ClaimsIdentity(
                        Options.AuthenticationType,
                        ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType)
                };
                if (!string.IsNullOrEmpty(context.Id))
                {
                    context.Identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, context.Id, XmlSchemaString, Options.AuthenticationType));
                }
                if (!string.IsNullOrEmpty(context.Name))
                {
                    context.Identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, context.Name, XmlSchemaString, Options.AuthenticationType));
                }
                if (!string.IsNullOrEmpty(context.Email))
                {
                    context.Identity.AddClaim(new Claim(ClaimTypes.Email, context.Email, XmlSchemaString, Options.AuthenticationType));
                }
                if (!string.IsNullOrEmpty(context.Picture))
                {
                    context.Identity.AddClaim(new Claim("urn:Line:picture", context.Picture, XmlSchemaString, Options.AuthenticationType));
                }
                context.Properties = properties;
                       
                return new AuthenticationTicket(context.Identity, context.Properties);
            }
            catch (Exception ex)
            {
                _logger.WriteError(ex.Message);
            }
            return new AuthenticationTicket(null, properties);
        }

        /// <summary>
        /// User login when statescode is 401.
        /// </summary>
        /// <returns></returns>
        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode != 401)
            {
                return Task.FromResult<object>(null);
            }

            var challenge = Helper.LookupChallenge(Options.AuthenticationType, Options.AuthenticationMode);

            if (challenge == null) return Task.FromResult<object>(null);
            
            var properties = challenge.Properties;
           
            // OAuth2 10.12 CSRF
            GenerateCorrelationId(properties);
            
            // Create state for validation.
            var state = Options.StateDataFormat.Protect(properties);
            loginClient.State = state;
           
            // Redirect to login page.
            Response.Redirect(loginClient.GetAuthUri());

            return Task.FromResult<object>(null);
        }

        public override async Task<bool> InvokeAsync()
        {
            return await InvokeReplyPathAsync();
        }

        private async Task<bool> InvokeReplyPathAsync()
        {
            // Check if request path is LINE call back URL.
            if (!Options.CallbackPath.HasValue || Options.CallbackPath != Request.Path) return false;
    
            var ticket = await AuthenticateAsync();
            if (ticket == null)
            {
                _logger.WriteWarning("Invalid return state, unable to redirect.");
                Response.StatusCode = 500;
                return true;
            }

            var context = new LineReturnEndpointContext(Context, ticket)
            {
                SignInAsAuthenticationType = Options.SignInAsAuthenticationType,
                RedirectUri = ticket.Properties.RedirectUri
            };
         
            if (context.SignInAsAuthenticationType != null &&
                context.Identity != null)
            {
                var grantIdentity = context.Identity;
                if (!string.Equals(grantIdentity.AuthenticationType, context.SignInAsAuthenticationType, StringComparison.Ordinal))
                {
                    grantIdentity = new ClaimsIdentity(grantIdentity.Claims, context.SignInAsAuthenticationType, grantIdentity.NameClaimType, grantIdentity.RoleClaimType);
                }
                Context.Authentication.SignIn(context.Properties, grantIdentity);
            }

            if (context.IsRequestCompleted || context.RedirectUri == null) return context.IsRequestCompleted;
            var redirectUri = context.RedirectUri;
            if (context.Identity == null)
            {
                // add a redirect hint that sign-in failed in some way
                redirectUri = WebUtilities.AddQueryString(redirectUri, "error", "access_denied");
            }
            Response.Redirect(redirectUri);
            context.RequestCompleted();

            return context.IsRequestCompleted;
        }
    }
}
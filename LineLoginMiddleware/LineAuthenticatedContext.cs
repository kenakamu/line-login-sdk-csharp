using Line.Login.Models;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using System.Security.Claims;

namespace Owin.Security.Middleware.Line
{
    /// <summary>
    /// Line AuthenticatedContext
    /// </summary>
    public class LineAuthenticatedContext : BaseContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LineAuthenticatedContext(IOwinContext context, TokenResponse token)
            : base(context)
        {
            AccessToken = token.AccessToken;
            Id = token.JWTPayload.Sub;
            Name = token.JWTPayload.Name;
            Picture = token.JWTPayload.Picture;
            Email = token.JWTPayload.Email == null ? 
                token.JWTPayload.Sub + "@line.com" : 
                token.JWTPayload.Email;
        }

        public string AccessToken { get; private set; }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Picture { get; private set; }
        public string Email { get; private set; }
        public ClaimsIdentity Identity { get; set; }
        public AuthenticationProperties Properties { get; set; }
    }
}

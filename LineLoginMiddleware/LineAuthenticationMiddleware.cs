using Line.Login;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;
using System;

namespace Owin.Security.Middleware.Line
{
    /// <summary>
    /// LINE Login Middleware.
    /// https://msdn.microsoft.com/en-us/library/dn253811(v=vs.113).aspx
    /// </summary>
    public class LineAuthenticationMiddleware : AuthenticationMiddleware<LineAuthenticationOptions>
    {
        private readonly LineLoginClient lineLoginClient;
        private readonly ILogger _logger;

        /// <summary>
        /// constructor
        /// </summary>
        public LineAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app,
            LineAuthenticationOptions options)
            : base(next, options)
        {
            if (string.IsNullOrWhiteSpace(Options.ChannelId))
                throw new ArgumentException("ChannelId is missing.");
            if (string.IsNullOrWhiteSpace(Options.ChannelSecret))
                throw new ArgumentException("ChannelSecret is missing.");
            if (string.IsNullOrWhiteSpace(Options.RedirectUri))
                throw new ArgumentException("RedirectUri is missing.");

            lineLoginClient = new LineLoginClient(Options.ChannelId, options.ChannelSecret, options.RedirectUri, null, options.Scope);
            _logger = app.CreateLogger<LineAuthenticationMiddleware>();

            if (Options.StateDataFormat == null)
            {
                var dataProtector = app.CreateDataProtector(
                    typeof(LineAuthenticationMiddleware).FullName,
                    Options.AuthenticationType, "v1");
                Options.StateDataFormat = new PropertiesDataFormat(dataProtector);
            }

            if (string.IsNullOrEmpty(Options.SignInAsAuthenticationType))
                Options.SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType();
        }

        /// <summary>
        /// Creates an authentication handler.
        /// </summary>
        protected override AuthenticationHandler<LineAuthenticationOptions> CreateHandler()
        {
            return new LineAuthenticationHandler(lineLoginClient, _logger);
        }
    }
}
using Line.Login.Models;
using Owin;
using System;

namespace Owin.Security.Middleware.Line
{
    /// <summary>
    /// Provide UseLineAuthentication 
    /// </summary>
    public static class LineAuthenticationExtensions
    {
        public static IAppBuilder UseLineAuthentication(this IAppBuilder app,
            LineAuthenticationOptions options)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            app.Use(typeof(LineAuthenticationMiddleware), app, options);

            return app;
        }

        public static IAppBuilder UseLineAuthentication(this IAppBuilder app, string channelId, string channelSecret, string redirectUri, Scope scope)
        {
            return app.UseLineAuthentication(new LineAuthenticationOptions
            (
                channelId,
                channelSecret,
                redirectUri,
                scope
            ));
        }
    }
}
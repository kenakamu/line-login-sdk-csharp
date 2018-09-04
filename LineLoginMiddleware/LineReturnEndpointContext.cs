using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;

namespace Owin.Security.Middleware.Line
{
    /// <summary>
    /// Represents the return endpoint context.
    /// https://msdn.microsoft.com/en-us/library/microsoft.owin.security.provider.returnendpointcontext(v=vs.113).aspx
    /// </summary>
    public class LineReturnEndpointContext : ReturnEndpointContext
    {       
        public LineReturnEndpointContext(
            IOwinContext context,
            AuthenticationTicket ticket)
            : base(context, ticket)
        {
        }
    }
}

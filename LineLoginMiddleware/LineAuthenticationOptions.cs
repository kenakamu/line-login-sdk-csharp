using Line.Login.Models;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Owin.Security.Middleware.Line
{
    /// <summary>
    /// LINE AuthenticationOptions
    /// https://msdn.microsoft.com/en-us/library/microsoft.owin.security.authenticationoptions(v=vs.113).aspx
    /// </summary>
    public class LineAuthenticationOptions : AuthenticationOptions
    {      
        /// <summary>
        /// Caption for display as 3rd party login
        /// </summary>
        public string Caption
        {
            get { return Description.Caption; }
            set { Description.Caption = value; }
        }
        /// <summary>
        /// Channel ID. Unique identifier for your channel issued by LINE.
        /// </summary>
        public string ChannelId { get; set; }
        /// <summary>
        /// Channel Secret. Unique secret for your channel issued by LINE.
        /// </summary>
        public string ChannelSecret { get; set; }
        /// <summary>
        /// Redirect Uri after auth
        /// </summary>
        public string RedirectUri { get; set; }
        ///// <summary>
        ///// Authentication Provider for LINE Login
        ///// </summary>
        //public ILineAuthenticationProvider Provider { get; set; }
        /// <summary>
        /// Permissions granted by the user. You can specify multiple scopes using the URL encoded whitespace character (%20). For more information, see scopes.
        /// </summary>
        public Scope Scope { get; set; }
        /// <summary>
        /// State to verify the response.
        /// </summary>
        public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }
        /// <summary>
        /// CallbackPath for login. Default to line-signin.
        /// </summary>
        public PathString CallbackPath { get; set; }
        /// <summary>
        /// Authentication Type
        /// </summary>
        public string SignInAsAuthenticationType { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public LineAuthenticationOptions(string channelId, string channelSecret, string redirectUri, Scope scope, string callbackPath = "/line-signin")
            : base("Line")
        {
            Caption = Constants.DefaultAuthenticationType;
            AuthenticationMode = AuthenticationMode.Passive;
            ChannelId = channelId;
            ChannelSecret = channelSecret;
            RedirectUri = redirectUri;
            Scope = scope;
            CallbackPath = new PathString(callbackPath);
        }
    }
}
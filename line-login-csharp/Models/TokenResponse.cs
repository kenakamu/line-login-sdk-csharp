using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Line.Login.Models
{
    public class TokenResponse
    {
        /// <summary>
        /// Access token. Valid for 30 days.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        /// <summary>
        /// Expiration date of the access token. Expressed in the remaining number of seconds to expiry from when the API was called.
        /// </summary>
        [JsonProperty("expires_in")]
        public Int64 ExpiresIn { get; set; }
        /// <summary>
        /// JSON Web Token (JWT) that includes information about the user. This field is returned only if openid is specified in the scope. For more information, see ID tokens.
        /// </summary>
        [JsonProperty("id_token")]
        public string IdToken { get; set; }
        /// <summary>
        /// The user's information is found in the payload section.
        /// </summary>
        [JsonIgnore]
        public JWTPayload JWTPayload { get; set; }
        /// <summary>
        /// Token used to get a new access token. Valid up until 10 days after the access token expires.
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        /// <summary>
        /// Permissions obtained through the access token.
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }
        /// <summary>
        /// Bearer
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        /// <summary>
        /// Error type
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
        /// <summary>
        /// Error Description
        /// </summary>
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
    }
}

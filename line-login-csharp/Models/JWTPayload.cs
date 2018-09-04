using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Line.Login.Models
{   
    public class JWTPayload
    {
        /// <summary>
        /// https://access.line.me. URL where the ID token is generated.
        /// </summary>
        [JsonProperty("iss")]
        public string Iss { get; set; }
        /// <summary>
        /// User ID for which the ID token is generated
        /// </summary>
        [JsonProperty("sub")]
        public string Sub { get; set; }
        /// <summary>
        /// Channel ID
        /// </summary>
        [JsonProperty("aud")]
        public string Aud { get; set; }
        /// <summary>
        /// The expiry date of the token. UNIX time.
        /// </summary>
        [JsonProperty("exp")]
        public int Exp { get; set; }
        /// <summary>
        /// Time that the ID token was generated. UNIX time.
        /// </summary>
        [JsonProperty("iat")]
        public int Iat { get; set; }
        /// <summary>
        /// The nonce value specified in the authorization URL. Not included if the nonce value was not specified in the authorization request.
        /// </summary>
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        /// <summary>
        /// User's display name. Not included if the profile scope was not specified in the authorization request.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// User's profile image URL. Not included if the profile scope was not specified in the authorization request.
        /// </summary>
        [JsonProperty("picture")]
        public string Picture { get; set; }
        /// <summary>
        /// User's email address. Not included if the email scope was not specified in the authorization request.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
    }

}

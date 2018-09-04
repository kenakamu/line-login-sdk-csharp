using Newtonsoft.Json;
using System;

namespace Line.Login.Models
{
    public class VerifyResponse
    {
        /// <summary>
        /// Permissions obtained through the access token.
        /// </summary>
        [JsonProperty("scope")]
        private string Scope { get; set; }
        /// <summary>
        /// Channel ID for which the access token is issued
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        /// <summary>
        /// Expiration date of the access token. Expressed as the remaining number of seconds to expiry from when the API was called.
        /// </summary>
        [JsonProperty("expires_in")]
        public Int64 ExpiresIn { get; set; }
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

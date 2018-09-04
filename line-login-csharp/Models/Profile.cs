using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Line.Login.Models
{
    public class Profile
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
        [JsonProperty("pictureUrl")]
        public string PictureUrl { get; set; }
        [JsonProperty("statusMessage")]
        public string StatusMessage { get; set; }
    }
}

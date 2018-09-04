using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Line.Login.Models
{
    public enum Scope
    {
        [JsonProperty("profile")]
        Profile = 0x01,
        [JsonProperty("openid")]
        OpenId = 0x02,
        [JsonProperty("email")]
        Email = 0x04
    }
}

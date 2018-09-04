using Newtonsoft.Json;
using System;

namespace Line.Login.Models
{
    public class FriendshipStatusResponse
    {
        /// <summary>
        /// true if the user has added the bot as a friend and has not blocked the bot. Otherwise, false.
        /// </summary>
        [JsonProperty("friendFlag")]
        public bool FriendFlag { get; set; }        
    }
}

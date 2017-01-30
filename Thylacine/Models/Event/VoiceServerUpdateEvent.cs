using Thylacine.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Models.Event
{
    public class VoiceServerUpdateEvent
    {
        [JsonProperty("guild_id"), JsonConverter(typeof(SnowflakeConverter))]
        public ulong GuildID { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }
    }
}

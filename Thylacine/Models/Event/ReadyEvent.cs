using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Models.Event
{
    class ReadyEvent
    {

        [JsonProperty("v")]
        public int Version { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("private_channels")]
        public DMChannel[] PrivateChannels { get; set; }

        [JsonProperty("guilds")]
        public Guild[] Guilds { get; set; }

        [JsonProperty("session_id")]
        public string Session { get; set; }

        [JsonProperty("presences")]
        public PresenceUpdate[] Presences { get; set; }

        [JsonProperty("relationships")]
        public object Relationships { get; set; }

        [JsonProperty("_trace")]
        public string[] _trace { get; set; }
    }
}

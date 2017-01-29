using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordSharp.Gateway.Payload
{
    [JsonObject(MemberSerialization.OptIn)]
    class IdentifyPayload : IPayload
    {
        OpCode IPayload.OpCode => OpCode.Identify;
        string IPayload.Event => null;
        uint? IPayload.Sequence => null;
        object IPayload.Payload => this;

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("compress")]
        public bool Compress { get; set; }

        [JsonProperty("large_threshold")]
        public int LargeThreshold { get; set; }

        [JsonProperty("shard")]
        public int[] Shard { get; set; }

        [JsonProperty("properties")]
        public Dictionary<string, string> Properties { get; set; }
    }
}

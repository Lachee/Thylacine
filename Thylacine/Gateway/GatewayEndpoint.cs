using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Gateway
{
    public class GatewayEndpoint
    {
        [JsonProperty("url")]
        public string Url { get; internal set; }

        [JsonProperty("shards")]
        public int ShardCount { get; internal set; }
    }
}

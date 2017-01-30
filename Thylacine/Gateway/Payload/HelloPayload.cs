using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Gateway.Payload
{
    [JsonObject(MemberSerialization.OptIn)]
    public class HelloPayload : IPayload
    {
        OpCode IPayload.OpCode => OpCode.Hello;
        string IPayload.Event => null;
        uint? IPayload.Sequence => null;
        object IPayload.Payload => this;

        [JsonProperty("heartbeat_interval")]
        public int HeartbeatInterval { get; set; }

        [JsonProperty("_trace")]
        public string[] Trace { get; set; }
    }
}
